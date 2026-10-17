using TMPro;
using UnityEngine;

namespace Features.Tasks.Inheritors
{
    public class SetDescriptionTextTaskAction : BaseTaskAction
    {
        [SerializeField] private TMP_Text _label;
        
        public override void DoAction(GameTask task)
        {
            _label.text = task.Description;
        }
    }
}