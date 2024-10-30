using DG.Tweening;
using GameTemplate.Gameplay.GameState;
using UnityEngine;

namespace GameTemplate.UI
{
    public class UIGameCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject TopPanel, WinPanel, LosePanel;

        public void Initialize(int UIlevelID)
        {
            LevelTextSetter[] levelTextSetters = GetComponentsInChildren<LevelTextSetter>();
            foreach (var levelTextSetter in levelTextSetters)
            {
                levelTextSetter.SetLevelText(UIlevelID);
            }

            if (UIlevelID == 1)
            {
                TopPanel.SetActive(false);
            }
        }
        
        public void GameFinished(WinState gameWon)
        {
            if (gameWon == WinState.Win)
            {
                OpenPanel(WinPanel.GetComponent<CanvasGroup>());
            }
            else
            {
                OpenPanel(LosePanel.GetComponent<CanvasGroup>());
            }
        }

        void OpenPanel(CanvasGroup group)
        {
            group.DOFade(1, 1);
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }
}