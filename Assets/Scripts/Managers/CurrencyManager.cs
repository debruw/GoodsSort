using System;
using GameTemplate.Events;
using GameTemplate.UI.Currency;
using GameTemplate.UI.Currency;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Managers
{
    public class CurrencyManager
    {
        public CurrencyData _CurrencyData;

        [Inject]
        public void Construct(CurrencyData CurrencyData)
        { 
            Debug.Log("Constructing currency manager");
            _CurrencyData = CurrencyData;
            
            for (int i = 0; i < _CurrencyData.currencies.Count; i++)
            {
                _CurrencyData.currencies[i].Initialize(i);
            }
        }

        public void EarnCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            _CurrencyData.currencies[currencyValue.currencyId].Earn(currencyValue.changeAmount);
        }

        public void SpendCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            _CurrencyData.currencies[currencyValue.currencyId].Spend(currencyValue.changeAmount);
        }
    }
}