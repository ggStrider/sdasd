using System;
using UnityEngine;

namespace Core.Misc
{
    public class Singleton<T> where T : Singleton<T>
    {
        public Singleton()
        {
            if (_instance != null)
            {
                throw new Exception($"Singleton instance of {typeof(T).Name} already exist! " +
                                    "You cannot create one more!");
            }

            _instance = (T)this;
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError($"Singleton Instance of {typeof(T).Name} is null! Returning null...");
                }

                return _instance;
            }
        }

        private static T _instance;
    }
}