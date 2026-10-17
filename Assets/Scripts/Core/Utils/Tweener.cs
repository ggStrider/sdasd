using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Utils
{
    public static class Tweener
    {
        #region Fire and Forget

        public static Transform LerpMove(this Transform transform, Vector3 endPos, float duration)
        {
            CoroutineRunner.Instance.StartCoroutine(transform.LerpMoveRoutine(endPos, duration));
            return transform;
        }

        public static Transform LerpRotate(this Transform transform, Quaternion endRot, float duration)
        {
            CoroutineRunner.Instance.StartCoroutine(transform.LerpRotateRoutine(endRot, duration));
            return transform;
        }
        
        public static Transform LerpRotate(this Transform transform, Vector3 endRot, float duration)
        {
            CoroutineRunner.Instance.StartCoroutine(transform.LerpRotateRoutine(Quaternion.Euler(endRot), duration));
            return transform;
        }

        public static Graphic LerpGraphicColor(this Graphic graphic, Color endColor, float duration)
        {
            CoroutineRunner.Instance.StartCoroutine(graphic.LerpGraphicColorRoutine(endColor, duration));
            return graphic;
        }

        public static void Float(float from, float to, float duration, Action<float> onChanged)
        {
            CoroutineRunner.Instance.StartCoroutine(FloatRoutine(from, to, duration, onChanged));
        }

        #endregion

        #region Awaitable

        public static IEnumerator LerpMoveRoutine(this Transform transform, Vector3 endPos, float duration)
        {
            if (transform == null)
            {
                Debug.LogError("Cannot tween with null transform!");
                yield break;
            }

            if (duration <= 0)
            {
                Debug.LogWarning($"Why you would try tween with duration: {duration}");
                duration = 0;
            }

            Vector3 startPos = transform.position;

            void onFrameEnded(float elapsedTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            }

            void onTimeElapsed()
            {
                transform.position = endPos;
            }

            yield return DoInDuration(duration, onFrameEnded, onTimeElapsed);
        }

        public static IEnumerator LerpRotateRoutine(this Transform transform, Quaternion endRot, float duration)
        {
            if (transform == null)
            {
                Debug.LogError("Cannot tween with null transform!");
                yield break;
            }

            if (duration <= 0)
            {
                Debug.LogWarning($"Why you would try tween with duration: {duration}");
                duration = 0;
            }

            Quaternion startRot = transform.rotation;

            void onFrameEnded(float elapsedTime)
            {
                transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / duration);
            }

            void onTimeElapsed()
            {
                transform.rotation = endRot;
            }

            yield return DoInDuration(duration, onFrameEnded, onTimeElapsed);
        }

        public static IEnumerator LerpGraphicColorRoutine(this Graphic graphic, Color endColor, float duration)
        {
            if (graphic == null)
            {
                Debug.LogError("Cannot tween with null graphic!");
                yield break;
            }

            if (duration <= 0)
            {
                Debug.LogWarning($"Why you would try tween with duration: {duration}");
                duration = 0f;
            }

            Color startColor = graphic.color;

            void onFrameEnded(float elapsedTime)
            {
                graphic.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            }

            void onTimeElapsed()
            {
                graphic.color = endColor;
            }

            yield return DoInDuration(duration, onFrameEnded, onTimeElapsed);
        }

        public static IEnumerator FloatRoutine(float from, float to, float duration, Action<float> onChanged)
        {
            void onFrameEnded(float elapsedTime)
            {
                onChanged?.Invoke(Mathf.Lerp(from, to, elapsedTime / duration));
            }

            void onTimeElapsed() { }

            yield return DoInDuration(duration, onFrameEnded, onTimeElapsed);
        }

        private static IEnumerator DoInDuration(float duration, Action<float> onFrameEnded, Action onTimeElapsed)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();

                elapsedTime += Time.deltaTime;
                onFrameEnded?.Invoke(elapsedTime);
            }

            onTimeElapsed?.Invoke();
        }

        #endregion
    }
}