using Core.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Menus
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        [Space]
        [SerializeField] private DropdownLevelSelector _lvlSelector;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => SceneChanger.Instance.ChangeScene(_lvlSelector.CurrentLevel));
            _quitButton.onClick.AddListener(Application.Quit);
        }
    }
}