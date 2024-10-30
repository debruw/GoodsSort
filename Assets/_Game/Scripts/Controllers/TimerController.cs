using System;
using System.Collections;
using GameTemplate.Audio;
using GameTemplate.Managers.Scene;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Timer
{
    public class TimerController : MonoBehaviour
    {
        #region Variables

        // Public Variables
        public static Action<bool> OnTimesUp;
        public static Action<float> OnSetTimer;

        // Private Variables
        [SerializeField] private TextMeshProUGUI txtTimer;
        [SerializeField] private Color timerTextColorForFast;
        [SerializeField] private float pumpEffectSineSizeMultiplier = 1.1f;
        [SerializeField] private float pumpEffectSineSpeed = 4f;
        [SerializeField] private float pumpEffectSineSpeedFast = 6f;

        private float _totalDurationInSeconds = 30;
        private float _timer;

        private float _sineTimer;
        private float _sineValue;

        private bool timerPaused;
        private bool firstTick = true;

        #endregion Variables

        #region Injections

        [Inject] LevelManager _levelManager;
        [Inject] private SoundPlayer m_SoundPlayer;

        #endregion

        private void Awake()
        {
            OnSetTimer += SetTimer;
            LevelPrefab.OnGameFinished += StopTimer;
        }

        private void OnDestroy()
        {
            OnSetTimer -= SetTimer;
            LevelPrefab.OnGameFinished -= StopTimer;
        }

        public void SetTimer(float durationInSeconds)
        {
            _totalDurationInSeconds = durationInSeconds;
            _timer = _totalDurationInSeconds;
            UpdateTimerText();
        }

        public void StopTimer(bool isWin, bool isAllLinesFilled)
        {
            timerPaused = true;
            txtTimer.color = Color.white;
            txtTimer.transform.localScale = Vector3.one;
        }

        public void StartTimer()
        {
            previousSecond = MathF.Floor(Math.Clamp(_timer % 60f, 0f, 59f));

            UpdateTimerText();
            
            StartCoroutine(StartTimerCor());
        }
        
        private IEnumerator StartTimerCor()
        {
            m_SoundPlayer.PlayTimerMusic(true);
            while (_timer > 0)
            {
                if (!timerPaused)
                {
                    _timer -= Time.deltaTime;

                    _sineTimer += Time.deltaTime * (_timer <= 5f ? pumpEffectSineSpeedFast : pumpEffectSineSpeed);

                    UpdateTimerText();

                    TimerSinePumpAnimation(_timer <= 5f);
                }

                yield return null;
            }

            //Game Finished LOSE
            txtTimer.text = "00:00";
            m_SoundPlayer.PlayTimesUpSound();
            OnTimesUp?.Invoke(false);
            txtTimer.color = Color.white;
            txtTimer.transform.localScale = Vector3.one;
        }

        private float previousSecond;
        private bool coin0Lose, coin1Lose, coin2Lose;

        private void UpdateTimerText()
        {
            var minutes = Mathf.Clamp(Mathf.Floor(_timer / 60f), 0, 10);
            var seconds = _timer % 60f;
            txtTimer.text = $"{minutes:00}:{MathF.Floor(Math.Clamp(seconds, 0f, 59f)):00}";
        }

        private void TimerSinePumpAnimation(bool fastVersion)
        {
            _sineValue = Mathf.Cos(_sineTimer) * -1 / 2f + .5f;

            txtTimer.transform.localScale =
                Vector3.Lerp(Vector3.one, Vector3.one * pumpEffectSineSizeMultiplier, _sineValue);

            if (fastVersion)
            {
                txtTimer.color = Color.Lerp(Color.white, timerTextColorForFast, _sineValue);
            }
        }
    }
}