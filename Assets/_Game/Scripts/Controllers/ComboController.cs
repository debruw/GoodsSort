using System.Collections;
using GameTemplate._Game.Scripts.Match;
using GameTemplate.Managers.Scene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace _Game.Scripts.Timer
{
    public class ComboController : MonoBehaviour, IStartable
    {
        #region Variables

        // Public Variables
        public TextMeshProUGUI ComboCountText;
        public Slider ComboSlider;

        public int ComboCount
        {
            get { return _comboCount; }
        }

        // Private Variables
        private int _comboCount = 0;
        [SerializeField] float ComboTime = 25f;
        private float _timer = 0f;

        #endregion

        private void Awake()
        {
            ComboCountText.text = string.Empty;
            ComboSlider.value = 0;

            MatchGroup.OnMatched += Combo;
            LevelPrefab.OnGameFinished += StopCombo;
        }

        public void Start()
        {
        }

        private void OnDestroy()
        {
            MatchGroup.OnMatched -= Combo;
            LevelPrefab.OnGameFinished -= StopCombo;
        }

        Coroutine ComboCoroutine;

        public void Combo(Vector3 position)
        {
            if (!gameObject.activeInHierarchy)
                return;
            _comboCount++;
            ComboCountText.text = "x" + _comboCount.ToString();
            _timer = ComboTime - _comboCount;

            if (ComboCoroutine != null) StopCoroutine(ComboCoroutine);
            ComboCoroutine = StartCoroutine(StartComboCor());
        }

        public void StopCombo(bool isWin, bool isAllLinesFilled)
        {
            if (ComboCoroutine != null) StopCoroutine(ComboCoroutine);
        }

        private IEnumerator StartComboCor()
        {
            ComboSlider.value = _timer / (ComboTime - _comboCount);

            while (_timer > 0)
            {
                _timer -= Time.deltaTime;
                ComboSlider.value = _timer / (ComboTime - _comboCount);
                yield return null;
            }

            _comboCount = 0;
            ComboCountText.text = string.Empty;
        }
    }
}