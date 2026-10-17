using UnityEngine;

namespace Features.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Fader _fader;

        private void Start()
        {
            _fader.transform.SetAsLastSibling();
        }
    }
}