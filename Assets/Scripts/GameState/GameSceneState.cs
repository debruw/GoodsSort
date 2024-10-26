using System.Collections;
using GameTemplate.Audio;
using GameTemplate.Managers.Pool;
using GameTemplate.Managers.SceneManagers;
using GameTemplate.UI;
using UnityEngine;
using VContainer;

namespace GameTemplate.Gameplay.GameState
{
    public class GameSceneState : GameStateBehaviour
    {
        public override GameState ActiveState => GameState.Game;
        
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private PoolingManager _poolingManager;
        [SerializeField] private Transform _levelPrefabParent;
        [SerializeField] private UIGameCanvas _uiGameCanvas;

        // Wait time constants for switching to post game after the game is won or lost
        private const float k_WinDelay = 2.0f;
        private const float k_LoseDelay = 2.0f;

        [Inject] PersistentGameState m_PersistentGameState;

        protected override void Awake()
        {
            base.Awake();

            m_PersistentGameState.Reset();
            //Do some things here
            _levelManager.Initialize(_levelPrefabParent);
            _poolingManager.Initialize();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterInstance(_levelManager);
            builder.RegisterInstance(_poolingManager);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _poolingManager.ResetPool();
        }

        public void GameFinished(bool isWin)
        {
            // start the coroutine
            StartCoroutine(CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, false));
        }

        IEnumerator CoroGameOver(float wait, bool gameWon)
        {
            m_PersistentGameState.SetWinState(gameWon ? WinState.Win : WinState.Loss);

            //TODO change this game to game
            // wait 5 seconds for game animations to finish
            yield return new WaitForSeconds(wait);

            //SceneLoader.Instance.LoadScene("PostGame");
            //TODO win or lose canvas should open
            _uiGameCanvas.GameFinished(m_PersistentGameState.WinState);
            
            if (m_PersistentGameState.WinState == WinState.Win)
            {
                SoundPlayer.Instance.PlayWinSound();
            }
            else
            {
                SoundPlayer.Instance.PlayLoseSound();
            }
            
        }
    }
}