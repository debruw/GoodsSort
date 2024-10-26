using System;
using GameTemplate.Audio;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameTemplate.Gameplay.UI
{

    public class UISettingsPanel : MonoBehaviour
    {
        [SerializeField]
        private UISwitcher.UISwitcher m_SoundToggle;

        [SerializeField]
        private UISwitcher.UISwitcher m_MusicToggle;

        private void OnEnable()
        {
            // Note that we initialize the slider BEFORE we listen for changes (so we don't get notified of our own change!)
            m_SoundToggle.isOn = UserPrefs.GetSoundState();
            m_SoundToggle.onValueChanged.AddListener(OnSoundToggleChanged);

            // initialize music slider similarly.
            m_MusicToggle.isOn = UserPrefs.GetMusicState();
            m_MusicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }

        private void OnDisable()
        {
            m_SoundToggle.onValueChanged.RemoveListener(OnSoundToggleChanged);
            m_MusicToggle.onValueChanged.RemoveListener(OnMusicToggleChanged);
        }

        private void OnSoundToggleChanged(bool state)
        {
            Debug.Log(state);
            UserPrefs.SetSoundState(state);
        }

        private void OnMusicToggleChanged(bool state)
        {
            UserPrefs.SetMusicState(state);
        }
    }

}
