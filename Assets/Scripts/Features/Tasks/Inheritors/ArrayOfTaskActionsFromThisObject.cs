using System;
using System.Collections.Generic;
using System.Linq;

namespace Features.Tasks.Inheritors
{
    public class ArrayOfTaskActionsFromThisObject : BaseTaskAction
    {
        private BaseTaskAction[] _actions;

        private void Awake()
        {
            List<BaseTaskAction> childActions = GetComponentsInChildren<BaseTaskAction>().ToList();
            childActions.Remove(this);

            _actions = childActions.ToArray();
        }

        public override void DoAction(GameTask task)
        {
            for (int i = 0; i < _actions.Length; i++)
            {
                _actions[i].DoAction(task);
            }
        }
    }
}