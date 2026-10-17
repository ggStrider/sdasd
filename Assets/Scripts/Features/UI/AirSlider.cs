using Core.Misc;
using Features.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI
{
    public class AirSlider : MonoBehaviour
    {
        [SerializeField] private Slider _airSlider;
        [SerializeField] private float _secondsUntilDieInWater = 45f;
        
        private PlayerMovement _playerMovement;
        private bool _canConsumeAir = true;

        private void Awake()
        {
            _playerMovement = FindFirstObjectByType<PlayerMovement>();
            EventBus.OnCompletedLevel += ForbidAirConsuming;
        }

        private void OnDestroy()
        {
            EventBus.OnCompletedLevel -= ForbidAirConsuming;
        }

        private void ForbidAirConsuming()
        {
            _canConsumeAir = false;
        }

        private void FixedUpdate()
        {
            if (!_canConsumeAir)
                return;
            
            PlayerStates state = _playerMovement.PlayerState;
            if (state == PlayerStates.InWater)
            {
                float range = _airSlider.maxValue - _airSlider.minValue;
                float step = range / _secondsUntilDieInWater * Time.deltaTime;
                _airSlider.value = Mathf.MoveTowards(_airSlider.value, 0, step);
                if (Mathf.Approximately(_airSlider.value, 0f))
                {
                    ForbidAirConsuming();
                    EventBus.OnLoseLevel?.Invoke();
                }
            }
        }
    }
}