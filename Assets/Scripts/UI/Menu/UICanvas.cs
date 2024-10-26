using System.Collections.Generic;
using Game.Managers.Currencies;
using GameTemplate.Events;
using GameTemplate.Managers;
using GameTemplate.UI.Currency;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace GameTemplate.Gameplay.UI
{
    public class UICanvas : MonoBehaviour
    {
        [SerializeField] private Transform currencyParent;
        [SerializeField] private GameObject CurrencyUIPrefab;

        [Inject] private CurrencyManager m_CurrencyManager;

        public List<CurrencyUI> currencyPanels = new List<CurrencyUI>();

        public void Initialize()
        {
            Currency[] currencies = m_CurrencyManager.currencies;

            for (int i = 0; i < currencies.Length; i++)
            {
                currencyPanels.Add(Instantiate(CurrencyUIPrefab, currencyParent).GetComponent<CurrencyUI>());
                currencyPanels[i].transform.SetParent(currencyParent);
                currencyPanels[i].Initialize(currencies[i].currencyImage, currencies[i].currencySign,
                    currencies[i].currencyAmount, currencies[i].isBuyable);
            }
        }
    }
}