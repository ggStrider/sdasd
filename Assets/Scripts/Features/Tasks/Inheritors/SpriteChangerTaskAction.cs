using UnityEngine;

namespace Features.Tasks.Inheritors
{
    public class SpriteChangerTaskAction : BaseTaskAction
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _sprite;
        
        public override void DoAction(GameTask task)
        {
            _renderer.sprite = _sprite;
        }
    }
}