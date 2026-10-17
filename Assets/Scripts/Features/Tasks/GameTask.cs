using System;
using UnityEngine;

namespace Features.Tasks
{
    [Serializable]
    public class GameTask
    {
        public string Id;
        
        [Space]
        public BaseTaskAction Init;
        public BaseTaskAction OnCompleted;

        [Space]
        public string Description;
        [HideInInspector] public bool Completed;
    }
}