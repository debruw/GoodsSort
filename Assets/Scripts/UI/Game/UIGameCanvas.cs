using DG.Tweening;
using GameTemplate.Gameplay.GameState;
using UnityEngine;

namespace GameTemplate.UI
{
    public class UIGameCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject WinPanel, LosePanel;
        
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
        }
    }
}