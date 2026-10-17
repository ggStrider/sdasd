using System.Collections;
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeDuration = 1f;

        private int _fadeId;

        public IEnumerator Fade()
        {
            return FadeTo(1f, _fadeDuration);
        }

        public IEnumerator UnFade(float customDuration = -1f)
        {
            if (customDuration < 0f)
                customDuration = _fadeDuration;

            return FadeTo(0f, customDuration);
        }

        private IEnumerator FadeTo(float targetAlpha, float duration)
        {
            int id = ++_fadeId;

            transform.ToggleRaycastTargetTo(false);

            float startAlpha = _image.color.a;

            float time = duration * Mathf.Abs(targetAlpha - startAlpha);
            float elapsed = 0f;

            while (elapsed < time)
            {
                yield return null;

                if (id != _fadeId)
                    yield break;

                elapsed += Time.deltaTime;
                SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, elapsed / time));
            }

            SetAlpha(targetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _image.color = _image.color.WithAlpha01(alpha);
        }
    }
}