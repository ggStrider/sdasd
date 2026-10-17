using System;
using Core.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Inputs
{
    public class InputReader : Singleton<InputReader>
    {
        private PlayerActionsMap _map;

        public event Action<Vector2> OnMoveInput;
        public event Action OnInteractInput;
        
        public InputReader() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _map = new PlayerActionsMap();
            
            _map.Player.Move.performed += NotifyMove;
            _map.Player.Move.canceled += NotifyMove;

            _map.Player.Interact.performed += NotifyInteract;
            
            _map.Player.Enable();
        }

        private void NotifyMove(InputAction.CallbackContext context)
        {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>());
        }
        
        private void NotifyInteract(InputAction.CallbackContext context)
        {
            OnInteractInput?.Invoke();
        }
    }
}