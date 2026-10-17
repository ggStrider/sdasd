using System.Collections;
using Core.Misc;
using Core.Sounds;
using Features.Player;
using Features.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Interactions.Inheritors
{
    public class InteractTeleportWithTransition : BaseInteractionObject
    {
        [SerializeField] private Transform _teleportPoint;

        [Space]
        [SerializeField] private AudioClip _clipToStartPlaying;
        [SerializeField] private AudioClip _sfxSound;
        [SerializeField] private float _clipTransitionDuration = 2f;

        [SerializeField] private bool _awaitForClip = false;

        [Space] [SerializeField] private UnityEvent _onFaded;
        
        private bool _canInteract = true;
        
        public override void Interact()
        {
            if (!_canInteract)
                return;

            StartCoroutine(Teleport());
        }

        private IEnumerator Teleport()
        {
            _canInteract = false;
            
            EventBus.OnStartedTeleporting?.Invoke();
            
            Fader fader = FindAnyObjectByType<Fader>();
            yield return fader.Fade();
            
            FindAnyObjectByType<PlayerMovement>().SetPosition(_teleportPoint.position);

            if (_awaitForClip)
            {
                yield return SfxPlayer.Instance.PlayShotRoutine(_sfxSound, .5f);
            }
            else
            {
                SfxPlayer.Instance.PlayShot(_sfxSound, .5f);
            }

            if (_clipToStartPlaying)
            {
                yield return MusicPlayer.Instance.CrossfadeToRoutine(_clipToStartPlaying, _clipTransitionDuration);
                MusicPlayer.Instance.SetLoop(true);
            }

            var tps = GetComponents<InteractTeleportWithTransition>();
            if (tps.Length == 2)
            {
                this.enabled = false;
                if (tps[0] == this)
                {
                    tps[1].enabled = true;
                }
                else
                {
                    tps[0].enabled = true;
                }
            }
           
            _onFaded?.Invoke();
            EventBus.OnEndedTeleporting?.Invoke();
            yield return fader.UnFade(8f);
            
            // maybe?
            _canInteract = true;
        }
    }
}