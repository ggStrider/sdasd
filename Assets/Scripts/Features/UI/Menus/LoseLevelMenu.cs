using System;
using Core.Misc;
using Core.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Menus
{
    public class LoseLevelMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;

        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _quitButton;


        private void Awake()
        {
            _parent.SetActive(false);
            EventBus.OnLoseLevel += ShowLose;
        }

        private void OnDestroy()
        {
            EventBus.OnLoseLevel -= ShowLose;
        }

        private void ShowLose()
        {
            _parent.SetActive(true);
            _retryButton.onClick.AddListener(SceneChanger.Instance.ReloadScene);
            _quitButton.onClick.AddListener(() => SceneChanger.Instance.ChangeScene(Constants.MENU_SCENE_NAME));
        }
    }
}