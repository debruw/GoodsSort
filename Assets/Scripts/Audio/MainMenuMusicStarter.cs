using GameTemplate.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Gameplay.GameplayObjects.Audio
{
    /// <summary>
    /// Simple class to play game theme on scene load
    /// </summary>
    public class MainMenuMusicStarter : MonoBehaviour,IStartable
    {
        // set whether theme should restart if already playing
        [SerializeField]
        bool m_Restart;
        
        [Inject] SoundPlayer m_SoundPlayer;

        public void Start()
        {
            m_SoundPlayer.PlayThemeMusic(m_Restart);
        }
    }
}
