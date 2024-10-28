using GameTemplate.Events;
using TMPro;
using UnityEngine;
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