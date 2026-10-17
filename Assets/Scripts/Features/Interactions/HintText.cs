using Core.Utils;
using TMPro;
using UnityEngine;

namespace Features.Interactions
{
    public class HintText : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _hint;

        [SerializeField] private float _animDuration = 0.5f;

        private void Awake()
        {
            _parent.localScale = Vector3.zero;
            _label.text = _hint;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Tweener.Float(_parent.localScale.x, 1f, _animDuration,
                x =>
                {
                    _parent.localScale = new Vector3(x, x, x);
                });
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Tweener.Float(_parent.localScale.x, 0f, _animDuration,
                x =>
                {
                    _parent.localScale = new Vector3(x, x, x);
                });
        }
    }
}