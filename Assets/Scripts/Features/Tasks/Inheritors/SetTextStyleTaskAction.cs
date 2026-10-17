using TMPro;
using UnityEngine;

namespace Features.Tasks.Inheritors
{
    public class SetTextStyleTaskAction : BaseTaskAction
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private FontStyles _style;
        
        public override void DoAction(GameTask task)
        {
            _label.fontStyle = _style;
        }
    }
}