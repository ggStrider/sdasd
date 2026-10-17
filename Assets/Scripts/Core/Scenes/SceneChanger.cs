using System.Collections;
using Core.Utils;
using Features.UI;
using UnityEngine.SceneManagement;

namespace Core.Scenes
{
    public class SceneChanger : Misc.Singleton<SceneChanger>
    {
        private readonly SceneTransitorUI _transitor;
        private Scene _current;

        public SceneChanger(SceneTransitorUI transitor) : base()
        {
            _transitor = transitor;
            _current = SceneManager.GetActiveScene();
        }
        
        private IEnumerator ChangeSceneInternal(string sceneName)
        {
            yield return _transitor.StartCoroutine(_transitor.TransitIn());
            yield return SceneManager.UnloadSceneAsync(_current);
            
            yield return SceneManager.LoadSceneAsync(sceneName, mode: LoadSceneMode.Additive);
            _current = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(_current);

            _transitor.StartCoroutine(_transitor.TransitOut());
        }
        
        public void ChangeScene(string sceneName)
        {
            CoroutineRunner.Instance.StartCoroutine(ChangeSceneInternal(sceneName));
        }

        public void ReloadScene()
        {
            ChangeScene(_current.name);
        }
    }
}
