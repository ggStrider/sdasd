using UnityEngine;

namespace Core.Sounds
{
    public class PlayMusicAtStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _crossfadeDuration = 2f;
        
        private void Start()
        {
            MusicPlayer.Instance.CrossfadeTo(_clip, _crossfadeDuration);
        }
    }
}