using Audio;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer
    {
        public AudioSource m_source;
        
        
        AudioData _audioData;
        private Transform _Holder;

        [Inject]
        public void Construct(AudioData audioData)
        {
            _audioData = audioData;
            
            _Holder = new GameObject("SoundPlayer").transform;
        }

        public void PlayThemeMusic(bool restart)
        {
            PlayTrack(_audioData.GetAudio(AudioID.Music), true, restart);
        }
        
        public void StopThemeMusic()
        {
            StopTrack();
        }

        public void PlayTimerMusic(bool restart)
        {
            PlayTrack(_audioData.GetAudio(AudioID.Ticking), true, restart);
        }

        public void PlayWinSound()
        {
            PlayTrack(_audioData.GetAudio(AudioID.Win), false, false);
        }

        public void PlayLoseSound()
        {
            PlayTrack(_audioData.GetAudio(AudioID.Lose), false, false);
        }

        public void PlayTimesUpSound()
        {
            PlayTrack(_audioData.GetAudio(AudioID.TimesUp), false, false);
        }

        private void PlayTrack(AudioClip clip, bool looping, bool restart)
        {
            if (m_source == null)
            {
                var clone = Object.Instantiate(_audioData.audioObject);
                m_source = clone.GetComponent<AudioSource>();
            }
            
            if (m_source.isPlaying)
            {
                // if we dont want to restart the clip, do nothing if it is playing
                if (!restart && m_source.clip == clip)
                {
                    return;
                }

                m_source.Stop();
            }

            m_source.clip = clip;
            m_source.loop = looping;
            m_source.time = 0;
            m_source.Play();
        }

        private void StopTrack()
        {
            m_source.DOFade(0, 1).OnComplete(() =>
            {
                m_source.Stop();
                m_source.volume = 1;
            });
        }
    }
}