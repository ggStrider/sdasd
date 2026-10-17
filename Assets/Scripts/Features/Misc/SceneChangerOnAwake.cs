using Core.Scenes;
using UnityEngine;

namespace Features.Misc
{
    public class SceneChangerOnAwake : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;

        private void Awake()
        {
            SceneChanger.Instance.ChangeScene(_sceneToLoad);
        }
    }
}