using GameTemplate.Audio;
using UnityEngine;

namespace GameTemplate.Gameplay.GameplayObjects.Audio
{
    /// <summary>
    /// Simple class to play game theme on scene load
    /// </summary>
    public class MainMenuMusicStarter : MonoBehaviour
    {
        // set whether theme should restart if already playing
        [SerializeField]
        bool m_Restart;

        void Start()
        {
            SoundPlayer.Instance.PlayThemeMusic(m_Restart);
        }
    }
}
