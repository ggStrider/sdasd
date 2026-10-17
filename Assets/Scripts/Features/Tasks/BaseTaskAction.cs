using UnityEngine;

namespace Features.Tasks
{
    public abstract class BaseTaskAction : MonoBehaviour
    {
        public abstract void DoAction(GameTask task);
    }
}