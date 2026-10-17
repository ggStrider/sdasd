using System.Collections;
using Core.Misc;
using UnityEngine;

namespace Core.Sounds
{
    public abstract class BaseSfxPlayer<T> : MonoSingleton<T> where T : BaseSfxPlayer<T>
    {
        [SerializeField, Range(0f, 1f)] protected float StartVolume = 1f;
        protected AudioSource AudioSource;

        protected override void Awake()
        {
            base.Awake();
            AudioSource = new GameObject("SFX").AddComponent<AudioSource>();
            AudioSource.volume = StartVolume;
            AudioSource.transform.SetParent(transform);
        }

        public void SetLoop(bool to)
        {
            AudioSource.loop = to;
        }

        public void Play(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        public void PlayShot(AudioClip clip, float vol = -1f)
        {
            if (Mathf.Approximately(vol, -1f))
            {
                vol = AudioSource.volume;
            }
            AudioSource.PlayOneShot(clip, vol);
        }

        public IEnumerator PlayShotRoutine(AudioClip clip, float vol = -1f)
        {
            PlayShot(clip, vol);
            yield return new WaitForSeconds(clip.length);
        }
    }
}