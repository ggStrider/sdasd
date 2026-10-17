using UnityEngine;
using UnityEngine.UI;

namespace Core.Extensions
{
    public static class UIExtensions
    {
        public static void ToggleRaycastTargetTo(this RectTransform transform, bool active)
        {
            ToggleRaycastTargetTo((Transform)transform, active);
        }

        public static void ToggleRaycastTargetTo(this Transform transform, bool active)
        {
            Graphic[] targets = transform.GetComponentsInChildren<Graphic>(includeInactive: true);
            for (int i = 0; i < targets.Length; ++i)
            {
                targets[i].raycastTarget = active;
            }
        }
    }
}