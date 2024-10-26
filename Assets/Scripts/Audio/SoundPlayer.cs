using UnityEngine;

namespace GameTemplate.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip m_ThemeMusic;

        [SerializeField]
        private AudioClip m_WinSound;
        
        [SerializeField]
        private AudioClip m_LoseSound;

        [SerializeField]
        private AudioSource m_source;

        /// <summary>
        /// static accessor for ClientMusicPlayer
        /// </summary>
        public static SoundPlayer Instance { get; private set; }
        
        private void Awake()
        {
            m_source = GetComponent<AudioSource>();

            if (Instance != null)
            {
                throw new System.Exception("Multiple Sound Players!");
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void PlayThemeMusic(bool restart)
        {
            PlayTrack(m_ThemeMusic, true, restart);
        }

        public void PlayWinSound()
        {
            PlayTrack(m_WinSound, false, false);
        }
        
        public void PlayLoseSound()
        {
            PlayTrack(m_LoseSound, false, false);
        }

        private void PlayTrack(AudioClip clip, bool looping, bool restart)
        {
            if (m_source.isPlaying)
            {
                // if we dont want to restart the clip, do nothing if it is playing
                if (!restart && m_source.clip == clip) { return; }
                m_source.Stop();
            }
            m_source.clip = clip;
            m_source.loop = looping;
            m_source.time = 0;
            m_source.Play();
        }
    }
}