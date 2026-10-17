using UnityEngine;

namespace Core.Misc
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError($"[{typeof(T)}] Instance is not initialized! Returning null...");
                }
                
                return _instance;
            }
        }

        /// if your object meant to be child -> use false
        protected virtual bool UseDontDestroyOnLoad => false;

        private static T _instance;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                // keyword 'this' doesnt work here so we just getting this component, lol
                _instance = GetComponent<T>();
                
                if(UseDontDestroyOnLoad)
                   DontDestroyOnLoad(gameObject);
            }
        }

        public static GameObject TrySpawnSingletonInstance()
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("[" + typeof(T).Name.ToUpper() + "]");
                _instance = go.AddComponent<T>();
            }

            return _instance.gameObject;
        }
    }
}