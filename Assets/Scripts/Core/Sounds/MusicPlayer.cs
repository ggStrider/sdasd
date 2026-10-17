using System.Collections;
using UnityEngine;

namespace Core.Sounds
{
    public class MusicPlayer : BaseSfxPlayer<MusicPlayer>
    {
        protected override bool UseDontDestroyOnLoad => true;

        private AudioSource _fadeOutSource;
        private float _baseVolume;
        private int _crossfadeId;
        private bool _isFading;

        public void CrossfadeTo(AudioClip clip, float duration)
        {
            StartCoroutine(CrossfadeToRoutine(clip, duration));
        }

        public IEnumerator CrossfadeToRoutine(AudioClip clip, float duration)
        {
            if (clip == null)
                yield break;

            if (AudioSource.clip == clip && AudioSource.isPlaying)
                yield break;

            int id = ++_crossfadeId;

            if (!_isFading)
                _baseVolume = AudioSource.volume;

            if (!AudioSource.clip || !AudioSource.isPlaying || duration <= 0f)
            {
                FinishFade();
                Play(clip);
                yield break;
            }

            _isFading = true;
            EnsureFadeSource();

            _fadeOutSource.clip = AudioSource.clip;
            _fadeOutSource.time = AudioSource.time;
            _fadeOutSource.volume = AudioSource.volume;
            _fadeOutSource.Play();

            AudioSource.clip = clip;
            AudioSource.volume = 0f;
            AudioSource.Play();

            float startOutVolume = _fadeOutSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                yield return null;

                if (id != _crossfadeId)
                    yield break;

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                _fadeOutSource.volume = Mathf.Lerp(startOutVolume, 0f, t);
                AudioSource.volume = Mathf.Lerp(0f, _baseVolume, t);
            }

            FinishFade();
        }

        private void FinishFade()
        {
            _isFading = false;

            if (_fadeOutSource != null)
            {
                _fadeOutSource.Stop();
                _fadeOutSource.clip = null;
            }

            AudioSource.volume = _baseVolume;
        }

        private void EnsureFadeSource()
        {
            if (_fadeOutSource == null)
            {
                _fadeOutSource = gameObject.AddComponent<AudioSource>();
                _fadeOutSource.playOnAwake = false;
            }

            _fadeOutSource.outputAudioMixerGroup = AudioSource.outputAudioMixerGroup;
            _fadeOutSource.loop = AudioSource.loop;
            _fadeOutSource.pitch = AudioSource.pitch;
            _fadeOutSource.spatialBlend = AudioSource.spatialBlend;
            _fadeOutSource.priority = AudioSource.priority;
        }
    }
}