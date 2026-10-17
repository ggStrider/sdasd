using UnityEngine;
using UnityEngine.UI;

namespace Features.Tasks.Inheritors
{
    public class ToggleTaskAction : BaseTaskAction
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private bool _to;
        
        public override void DoAction(GameTask task)
        {
            _toggle.isOn = _to;
        }
    }
}