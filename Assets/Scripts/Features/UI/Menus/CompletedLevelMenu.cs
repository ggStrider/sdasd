using Core.Misc;
using Core.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Menus
{
    public class CompletedLevelMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _wonMenuParent;
        
        [Space]
        [SerializeField] private Button _exitButton;
        
        private void Awake()
        {
            _wonMenuParent.SetActive(false);
            EventBus.OnCompletedLevel += OnCompletedLevel;
        }

        private void OnDestroy()
        {
            EventBus.OnCompletedLevel -= OnCompletedLevel;
        }
        
        private void OnCompletedLevel()
        {
            _wonMenuParent.SetActive(true);
            _exitButton.onClick.AddListener(() => SceneChanger.Instance.ChangeScene(Constants.MENU_SCENE_NAME));
        }
    }
}