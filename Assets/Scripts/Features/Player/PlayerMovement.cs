using System;
using Core.Inputs;
using Core.Misc;
using UnityEngine;

namespace Features.Player
{
    public enum PlayerStates
    {
        OnGround,
        InWater,
    };

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementOnGroundSpeed = 3f;
        
        [Space]
        [SerializeField] private float _movementSpeedUnderWater = 3f;

        [Space] [SerializeField] private float _fallInWaterSpeed = 0.5f;

        private Rigidbody2D _rigidbody;

        private PlayerStates _playerState = PlayerStates.OnGround;
        public PlayerStates PlayerState => _playerState;
        
        private Vector2 _inputDir;
        private bool _canMove = true;

        public float HorizontalVelocity => _rigidbody.linearVelocityX;
        public float HorizontalInput => _inputDir.x;

        private const string WATER_TAG = "Water";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            InputReader.Instance.OnMoveInput += SetInputMovementDirection;

            EventBus.OnStartedTeleporting += DisableMovingAbility;
            EventBus.OnEndedTeleporting += EnableMovingAbility;

            EventBus.OnCompletedLevel += DisableMovingAbility;
        }

        private void OnDestroy()
        {
            if (InputReader.Instance != null)
            {
                InputReader.Instance.OnMoveInput -= SetInputMovementDirection;
            }

            EventBus.OnStartedTeleporting -= DisableMovingAbility;
            EventBus.OnEndedTeleporting -= EnableMovingAbility;

            EventBus.OnCompletedLevel -= DisableMovingAbility;
        }

        private void SetInputMovementDirection(Vector2 newInputDir)
        {
            _inputDir = newInputDir;
        }

        public void SetPosition(Vector3 pos)
        {
            _rigidbody.position = pos;
        }

        private void DisableMovingAbility()
        {
            _canMove = false;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void EnableMovingAbility()
        {
            _canMove = true;
        }
        
        private void FixedUpdate()
        {
            switch (_playerState)
            {
                case PlayerStates.OnGround:
                    MoveOnGround();
                    break;
                
                case PlayerStates.InWater:
                    MoveInWater();
                    break;
                
                default:
                    throw new Exception("state doesn't exist!");
            }
        }

        private void MoveInWater()
        {
            if (!_canMove)
                return;
            
            bool hasInput = !Mathf.Approximately(_inputDir.x, 0) || !Mathf.Approximately(_inputDir.y, 0);
       
            if (hasInput)
            {
                _rigidbody.linearVelocity = _inputDir.normalized * _movementSpeedUnderWater;
            }
            else
            {
                const float maxDelta = 0.3f;
                
                _rigidbody.linearVelocityX = Mathf.MoveTowards(_rigidbody.linearVelocityX,
                    0, maxDelta * Time.deltaTime);
                _rigidbody.linearVelocityY = -_fallInWaterSpeed;
            }
        }

        private void MoveOnGround()
        {
            if (!_canMove)
                return;

            _rigidbody.linearVelocityX = _inputDir.x * _movementOnGroundSpeed;
        }

        private int _waterDepth;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(WATER_TAG))
            {
                ++_waterDepth;
                _playerState = PlayerStates.InWater;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(WATER_TAG))
            {
                --_waterDepth;
                if(_waterDepth <= 0)
                    _playerState = PlayerStates.OnGround;
            }
        }
    }
}