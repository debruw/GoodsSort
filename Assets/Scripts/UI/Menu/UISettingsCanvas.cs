using System;
using UnityEngine;

namespace GameTemplate.Gameplay.UI
{
    /// <summary>
    /// Controls the special Canvas that has the settings icon and the settings window.
    /// The window itself is controlled by UISettingsPanel; the button is controlled here.
    /// </summary>
    public class UISettingsCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_SettingsPanelRoot;

        void Awake()
        {
            // hide the settings window at startup (this is just to handle the common case where an artist forgets to disable the window in the prefab)
            DisablePanels();
        }

        void DisablePanels()
        {
            m_SettingsPanelRoot.SetActive(false);
        }

        /// <summary>
        /// Called directly by the settings button in the UI prefab
        /// </summary>
        public void OnClickSettingsButton()
        {
            m_SettingsPanelRoot.SetActive(!m_SettingsPanelRoot.activeSelf);
        }

        /// <summary>
        /// Called directly by the quit button in the UI prefab
        /// </summary>
        public void OnClickQuitButton()
        {
            m_SettingsPanelRoot.SetActive(false);
        }

    }
}
