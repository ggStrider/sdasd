using System;
using System.Collections.Generic;
using System.Text;
using Core.Misc;
using UnityEngine;

namespace Features.Tasks
{
    public class SceneTaskManager : MonoSingleton<SceneTaskManager>
    {
        [SerializeField] private List<GameTask> _sceneTasks = new List<GameTask>();

        private void Start()
        {
            for (int i = 0; i < _sceneTasks.Count; ++i)
            {
                _sceneTasks[i].Init?.DoAction(_sceneTasks[i]);
            }
        }

        public void TryComplete(string taskId)
        {
            int index = GetTaskIndexById(taskId);
            if (index == -1)
            {
                Debug.LogError("Task id == -1 means task you trying to complete == null! Id: " + taskId);
                return;
            }

            GameTask task = _sceneTasks[index];
            
            if (task.Completed)
                return;
            
            task.Completed = true;
            task.OnCompleted?.DoAction(task);
            
            if(AreAllTasksCompleted())
                EventBus.OnCompletedLevel?.Invoke();
        }

        public bool IsTaskExists(string id)
        {
            return GetTaskIndexById(id) != -1;
        }

        private bool AreAllTasksCompleted()
        {
            for (int i = 0; i < _sceneTasks.Count; i++)
            {
                if (!_sceneTasks[i].Completed)
                    return false;
            }

            return true;
        }

        public string GetTasksId()
        {
            string formatted = string.Empty;
            for (int i = 0; i < _sceneTasks.Count; i++)
            {
                formatted += _sceneTasks[i].Id + "\n";
            }

            return formatted;
        }

        private int GetTaskIndexById(string id)
        {
            for (int i = 0; i < _sceneTasks.Count; i++)
            {
                if (_sceneTasks[i].Id == id)
                    return i;
            }

            return -1;
        }
    }
}