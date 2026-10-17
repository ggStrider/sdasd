using System.Collections.Generic;
using Core.Inputs;
using Core.Misc;
using UnityEngine;

namespace Features.Interactions
{
    public class InteractManager : MonoBehaviour
    {
        [SerializeField] private float _interactionRadius = 0.75f;
        private readonly List<Collider2D> _interactResults = new List<Collider2D>(16);

        private bool _canInteract = true;

        private void Awake()
        {
            InputReader.Instance.OnInteractInput += TryInteract;
            EventBus.OnLoseLevel += ForbidInteract;
        }

        private void OnDestroy()
        {
            if (InputReader.Instance != null)
            {
                InputReader.Instance.OnInteractInput -= TryInteract;
                EventBus.OnLoseLevel -= ForbidInteract;
            }
        }
        
        private void ForbidInteract()
        {
            _canInteract = false;
        }

        private void TryInteract()
        {
            if (!_canInteract)
                return;
            
            int count = Physics2D.OverlapCircle(
                transform.position, _interactionRadius, ContactFilter2D.noFilter, _interactResults);

            for (int i = 0; i < count; ++i)
            {
                var res = _interactResults[i].gameObject.GetComponentsInChildren<BaseInteractionObject>(includeInactive: true);
                
                if (res.Length > 0)
                {
                    for (int j = 0; j < res.Length; ++j)
                    {
                        var interactionObject = res[j];
                        if(interactionObject.enabled)
                            interactionObject.Interact();
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color32(255, 0, 0, 50);
            Gizmos.DrawSphere(transform.position, _interactionRadius);
        }
#endif
    }
}