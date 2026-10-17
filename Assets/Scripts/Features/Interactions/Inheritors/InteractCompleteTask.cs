using System;
using Features.Tasks;
using UnityEngine;

namespace Features.Interactions.Inheritors
{
    public class InteractCompleteTask : BaseInteractionObject
    {
        [SerializeField] private string _taskId;

        private void Start()
        {
            HasSomeErrors();
        }

        private bool HasSomeErrors()
        {
            bool has = false;
            if (string.IsNullOrEmpty(_taskId))
            {
                Debug.LogError("You didn't put task id to complete!");
                has = true;
            }

            if (!SceneTaskManager.Instance)
            {
                Debug.LogError($"No instance of {nameof(SceneTaskManager)}!");
                return true;
            }

            if (!SceneTaskManager.Instance.IsTaskExists(_taskId))
            {
                Debug.LogError($"Task with id {_taskId} doesn't exist in manager!");
                has = true;
            }

            return has;
        }

        public override void Interact()
        {
            if (HasSomeErrors())
                return;
            
            SceneTaskManager.Instance.TryComplete(_taskId);
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
                return;
            
            SceneTaskManager taskManager = FindAnyObjectByType<SceneTaskManager>();
            if (taskManager != null)
            {
                if (!taskManager.IsTaskExists(_taskId))
                {
                    string existingTasks = taskManager.GetTasksId();
                    Debug.LogWarning($"Task with id '{_taskId}' doesn't exist in scene task manager! " +
                                     $"\n\n<color=green>{existingTasks}</color>");
                }
                else
                {
                    Debug.Log($"<color=green>Task with id '{_taskId}' exists!</color>");
                }
            }
        }
#endif
    }
}