using System;
using GameTemplate.Events;
using GameTemplate.Managers;
using TMPro;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace GameTemplate.UI
{
    public class EarningsUI : MonoBehaviour
    {
        public TextMeshProUGUI EarnedCoinText;

        public CurrencyArgs SetEarnings()
        {
            int randomEarning = Random.Range(10, 20);
            EarnedCoinText.text = "+" + randomEarning;
            return new CurrencyArgs(0, randomEarning, false);
        }
    }
}