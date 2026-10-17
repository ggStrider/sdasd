using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Extensions
{
    public static class SceneExtensions
    {
        public static void MoveObjectsToAnotherScene(Scene scene, params GameObject[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                SceneManager.MoveGameObjectToScene(objs[i], scene);
            }
        }
    }
}