using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.UI.Currency
{
    public class CurrencyUI : MonoBehaviour
    {
        #region Variables

        public TextMeshProUGUI currencyAmountText;
        public Image currencyImage;
        public GameObject AddButton;
        public Transform flyCurrencyTargetTransform;

        [SerializeField] private float punch = 0.2f;
        [SerializeField] private float punchDuration = 0.5f;
        [SerializeField] private int vibrato = 6;
        [SerializeField] private Transform punchAnimationParent;

        private string currencySign;
        private int currencyAmount;
        private Coroutine incrementRoutine;
        private Tween punchTween;

        #endregion

        public void Initialize(Sprite currencyIcon, string currencySign, int currencyAmount, bool isBuyable)
        {
            this.currencyImage.sprite = currencyIcon;
            this.currencySign = currencySign;
            AddButton.SetActive(isBuyable);
            //TODO Plus button click
            SetCurrency(currencyAmount);
        }

        public void SetCurrency(int nextAmount)
        {
            if (currencyAmount < nextAmount)
            {
                PunchAnimation();
            }
            else if (currencyAmount > nextAmount)
            {
                PunchAnimation();
            }

            currencyAmount = nextAmount;

            currencyAmountText.text = NumberHelper.ToStringScientific(nextAmount) + "" + currencySign;
        }

        public void SetCurrencyIncremental(int nextAmount)
        {
            if (incrementRoutine != null)
            {
                StopCoroutine(incrementRoutine);
            }

            PunchAnimation();
            incrementRoutine = StartCoroutine(MoneyChangeRoutine(nextAmount));
        }

        public IEnumerator MoneyChangeRoutine(int nextAmount)
        {
            float lerpValue = 0;
            while (lerpValue < 1)
            {
                currencyAmount = (int)Mathf.Lerp(currencyAmount, nextAmount, lerpValue);
                currencyAmountText.text = NumberHelper.ToStringScientific(currencyAmount) + currencySign;
                lerpValue += Time.deltaTime * 2f;
                yield return null;
            }

            SetCurrency(nextAmount);
        }

        private void PunchAnimation()
        {
            punchTween?.Kill(true);
            punchTween = punchAnimationParent.DOPunchScale(Vector3.one * punch, punchDuration, vibrato);
        }
    }
}