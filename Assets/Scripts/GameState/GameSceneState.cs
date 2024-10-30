using System;
using _Game.Scripts.Timer;
using AssetKits.ParticleImage;
using Cysharp.Threading.Tasks;
using GameTemplate.Audio;
using GameTemplate.Events;
using GameTemplate.Managers;
using GameTemplate.Managers.Scene;
using GameTemplate.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Gameplay.GameState
{
    public class GameSceneState : GameStateBehaviour
    {
        #region Variables

        public override GameState ActiveState => GameState.Game;
        public static Action OnFirstTouch;

        [SerializeField] private Transform _levelPrefabParent;
        [SerializeField] private UIGameCanvas _uiGameCanvas;
        [SerializeField] private EarningsUI _earningsUI;
        [SerializeField] private TimerController _timerController;
        [SerializeField] private ParticleImage _winParticleImage;
        [SerializeField] private GameObject _allLinesFilledText;

        // Wait time constants for switching to post game after the game is won or lost
        private const float k_WinDelay = 2.0f;
        private const float k_LoseDelay = 2.0f;

        #endregion

        #region Injections

        [Inject] PersistentGameState m_PersistentGameState;
        [Inject] LevelManager _levelManager;
        [Inject] CurrencyManager _currencyManager;
        [Inject] SceneLoader m_SceneLoader;
        [Inject] SoundPlayer m_SoundPlayer;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            OnFirstTouch += StartTimer;
        }

        protected override void Start()
        {
            base.Start();

            m_PersistentGameState.Reset();
            //Do some things here
            _levelManager.SpawnLevel(_levelPrefabParent);

            _uiGameCanvas.Initialize(_levelManager.UILevelId);

            LevelPrefab.OnGameFinished += OnGameFinished;
            TimerController.OnTimesUp += OnGameFinished;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInHierarchy<ComboController>();
            builder.RegisterComponentInHierarchy<StarController>();
            builder.RegisterComponentInHierarchy<TimerController>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LevelPrefab.OnGameFinished -= OnGameFinished;
            TimerController.OnTimesUp -= OnGameFinished;
            OnFirstTouch = null;
        }

        public void StartTimer()
        {
            OnFirstTouch -= StartTimer;

            //We dont use timer at first level
            if (_levelManager.LevelId == 0)
                return;

            _timerController.StartTimer();
        }

        public void OnGameFinished(bool isWin, bool isAllLinesFilled)
        {
            if (isAllLinesFilled)
            {
                _allLinesFilledText.SetActive(true);
            }

            // start the coroutine
            _ = CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, isWin);
        }

        public void OnGameFinished(bool isWin)
        {
            // start the coroutine
            _ = CoroGameOver(isWin ? k_WinDelay : k_LoseDelay, isWin);
        }

        async UniTaskVoid CoroGameOver(float wait, bool gameWon)
        {
            m_PersistentGameState.SetWinState(gameWon ? WinState.Win : WinState.Loss);
            if (gameWon) _winParticleImage.Play();

            //TODO change this game to game
            // wait for game animations to finish
            await UniTask.Delay((int)(wait * 1000)); // waits for wait*1 second

            //win or lose canvas should open
            CurrencyArgs args = _earningsUI.SetEarnings();
            _currencyManager.EarnCurrency(args);
            _uiGameCanvas.GameFinished(m_PersistentGameState.WinState);

            if (m_PersistentGameState.WinState == WinState.Win)
            {
                m_SoundPlayer.PlayWinSound();
                //TODO set next level
                _levelManager.SetNextLevel();
            }
            else
            {
                m_SoundPlayer.PlayLoseSound();
            }
        }

        public void NextButtonClick()
        {
            if (_levelManager.LevelId < 2)
            {
                m_SceneLoader.LoadSceneByType(SceneType.Game);
            }
            else
            {
                m_SceneLoader.LoadSceneByType(SceneType.MainMenu);
            }
        }

        public void RetryButtonClick()
        {
            m_SceneLoader.LoadSceneByType(SceneType.Game);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                _levelManager.SetNextLevel();
                RetryButtonClick();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _levelManager.SetPreviousLevel();
                RetryButtonClick();
            }
        }
#endif
    }
}