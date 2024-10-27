using System.Collections;
using GameTemplate.Audio;
using GameTemplate.Events;
using GameTemplate.Managers;
using GameTemplate.Managers.SceneManagers;
using GameTemplate.UI;
using UnityEngine;
using VContainer;

namespace GameTemplate.Gameplay.GameState
{
    public class GameSceneState : GameStateBehaviour
    {
        public override GameState ActiveState => GameState.Game;

        [SerializeField] private Transform _levelPrefabParent;
        [SerializeField] private UIGameCanvas _uiGameCanvas;
        [SerializeField] private EarningsUI _earningsUI;

        // Wait time constants for switching to post game after the game is won or lost
        private const float k_WinDelay = 1.0f;
        private const float k_LoseDelay = 1.0f;

        [Inject] PersistentGameState m_PersistentGameState;
        [Inject] LevelManager _levelManager;
        [Inject] CurrencyManager _currencyManager;

        protected override void Start()
        {
            base.Start();

            m_PersistentGameState.Reset();
            //Do some things here
            _levelManager.Initialize(_levelPrefabParent);
            _uiGameCanvas.Initialize(_levelManager.UILevelId);

            LevelPrefab.OnGameFinished += OnGameFinished;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LevelPrefab.OnGameFinished -= OnGameFinished;
        }

        public void OnGameFinished(bool isWin)
        {
            // start the coroutine
            StartCoroutine(CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, isWin));
        }

        IEnumerator CoroGameOver(float wait, bool gameWon)
        {
            m_PersistentGameState.SetWinState(gameWon ? WinState.Win : WinState.Loss);

            //TODO change this game to game
            // wait 5 seconds for game animations to finish
            yield return new WaitForSeconds(wait);

            //win or lose canvas should open
            CurrencyArgs args =_earningsUI.SetEarnings();
            _currencyManager.EarnCurrency(args);
            _uiGameCanvas.GameFinished(m_PersistentGameState.WinState);

            if (m_PersistentGameState.WinState == WinState.Win)
            {
                SoundPlayer.Instance.PlayWinSound();
                //TODO set next level
                //_levelManager.SetNextLevel();
            }
            else
            {
                SoundPlayer.Instance.PlayLoseSound();
            }
        }

        public void NextButtonClick()
        {
            SceneLoader.Instance.LoadScene(SceneLoader.MainMenu);
        }
        
        public void RetryButtonClick()
        {
            SceneLoader.Instance.LoadScene(SceneLoader.Game);
        }
    }
}