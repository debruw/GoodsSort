using DG.Tweening;
using GameTemplate.Gameplay.GameState;
using UnityEngine;

namespace GameTemplate.UI
{
    public class UIGameCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject WinPanel, LosePanel;

        public void Initialize(int levelID)
        {
            LevelTextSetter[] levelTextSetters = GetComponentsInChildren<LevelTextSetter>();
            foreach (var levelTextSetter in levelTextSetters)
            {
                levelTextSetter.SetLevelText(levelID);
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