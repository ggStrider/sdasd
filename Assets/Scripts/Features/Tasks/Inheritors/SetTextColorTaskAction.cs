using TMPro;
using UnityEngine;

namespace Features.Tasks.Inheritors
{
    public class SetTextColorTaskAction : BaseTaskAction
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Color32 _color;
        
        public override void DoAction(GameTask task)
        {
            _label.color = _color;
        }
    }
}