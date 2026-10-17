using UnityEngine;

namespace Features.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;

        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

        private static readonly int WalkingKey = Animator.StringToHash("is-walking");

        private void FixedUpdate()
        {
            _animator.SetBool(WalkingKey, Mathf.Abs(_movement.HorizontalVelocity) > 0.05f);

            if (Mathf.Abs(_movement.HorizontalVelocity) > 0f)
            {
                if (_movement.HorizontalInput > 0f)
                {
                    _renderer.flipX = false;
                }
                else if (_movement.HorizontalInput < 0f)
                {
                    _renderer.flipX = true;
                }
            }
        }
    }
}