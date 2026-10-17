using System.Collections;
using Core.Extensions;
using Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI
{
    public class SceneTransitorUI : MonoBehaviour
    {
        [Header("First step")]
        [SerializeField] private Image _blackBg;
        [SerializeField] private float _toBlackDuration = 1f;

        [Header("Second step")]
        [SerializeField] private CanvasGroup _allItems;
        [SerializeField] private float _toAllItemsDuration = 1.5f;

        public IEnumerator TransitIn()
        {
            ToggleRaycastTargetTo(true);
            _blackBg.color = Color.black.WithAlpha01(0f);

            yield return Tweener.FloatRoutine(0f, 1f, _toBlackDuration,
                val => _blackBg.color = _blackBg.color.WithAlpha01(val));

            yield return Tweener.FloatRoutine(0f, 1f, _toAllItemsDuration,
                val => _allItems.alpha = val);
        }

        private void ToggleRaycastTargetTo(bool active)
        {
            Graphic[] targets = GetComponentsInChildren<Graphic>(includeInactive: true);
            for (int i = 0; i < targets.Length; ++i)
            {
                targets[i].raycastTarget = active;
            }
        }

        public void SetInTransitEnded()
        {
            ToggleRaycastTargetTo(true);
            _blackBg.color = Color.black;
            _allItems.alpha = 1f;
        }

        public IEnumerator TransitOut()
        {
            ToggleRaycastTargetTo(false);
            _blackBg.color = Color.black.WithAlpha01(1f);

            yield return Tweener.FloatRoutine(1f, 0f, _toAllItemsDuration,
                val => _allItems.alpha = val);

            yield return Tweener.FloatRoutine(1f, 0f, _toBlackDuration,
                val => _blackBg.color = _blackBg.color.WithAlpha01(val));
        }
        
        public void SetOutTransitEnded()
        {
            ToggleRaycastTargetTo(false);
            _blackBg.color = Color.black.WithAlpha01(0f);
            _allItems.alpha = 0f;
        }

#if UNITY_EDITOR
        [ContextMenu("Test_InTransition")]
        private void Test_InTransition()
        {
            StartCoroutine(TransitIn());
        }

        [ContextMenu("Test_OutTransition")]
        private void Test_OutTransition()
        {
            StartCoroutine(TransitOut());
        }
#endif
    }
}