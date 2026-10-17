using Core.Extensions;
using Core.Inputs;
using Core.Scenes;
using Core.Sounds;
using Core.Utils;
using Features.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    [DefaultExecutionOrder(-9999)]
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField] private Canvas _fullHdCanvasPrefab;
        [SerializeField] private SceneTransitorUI _transitorPrefab;
        [SerializeField] private EventSystem _eventSystemPrefab;

        [Space]
        [SerializeField] private SfxPlayer _sfxPlayerPrefab;
        [SerializeField] private MusicPlayer _musicPlayerPrefab;
        
        // persisting scene even when you loading another scene
        // core is always with us, carrying important components
        private static Scene _core;
        
        private void Awake()
        {
            bool hasMoreThanOneBootstrap = FindObjectsByType<ProjectBootstrap>(FindObjectsSortMode.None).Length > 1;
            if (hasMoreThanOneBootstrap)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            // Mono
            GameObject coroutineRunner = CoroutineRunner.TrySpawnSingletonInstance();

            Canvas coreCanvas = Instantiate(_fullHdCanvasPrefab);
            coreCanvas.name = "Core Canvas full HD";
            coreCanvas.sortingOrder = 100;
            
            SceneTransitorUI transitor = Instantiate(_transitorPrefab, coreCanvas.transform);
            transitor.SetOutTransitEnded();

            // its creating singleton in constructor
            _ = new SceneChanger(transitor);
            _ = new InputReader();

            EventSystem eventSystem = Instantiate(_eventSystemPrefab);

            MusicPlayer musicInstance = Instantiate(_musicPlayerPrefab);
            SfxPlayer sfxInstance = Instantiate(_sfxPlayerPrefab);
            
            SceneExtensions.MoveObjectsToAnotherScene(_core, 
                gameObject,
                coroutineRunner,
                coreCanvas.gameObject,
                eventSystem.gameObject,
                musicInstance.gameObject,
                sfxInstance.gameObject
                );
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SpawnCoreScene()
        {
            const string coreSceneName = "Core";
            Scene coreScene = SceneManager.GetSceneByName(coreSceneName);

            if (coreScene.IsValid() && coreScene.isLoaded)
            {
                _core = coreScene;
            }
            else
            {
                _core = SceneManager.CreateScene(coreSceneName);
            }
        }
    }
}