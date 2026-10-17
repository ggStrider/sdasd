using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseClickBindableButton : MonoBehaviour
    {
        [SerializeField] protected Button ButtonToBind;

        protected virtual void Awake()
        {
            ButtonToBind.onClick.AddListener(Bind);
        }

        protected abstract void Bind();

#if UNITY_EDITOR
        private void Reset()
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (ButtonToBind == null)
                {
                    ButtonToBind = GetComponent<Button>();
                }
            };
        }
#endif
    }
}