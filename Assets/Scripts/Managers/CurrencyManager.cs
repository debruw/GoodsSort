using System;
using Game.Managers.Currencies;
using GameTemplate.Events;
using UnityEngine;

namespace GameTemplate.Managers
{
    [CreateAssetMenu(fileName = "CurrencyManager", menuName = "Scriptable Objects/Currency Manager")]
    public class CurrencyManager : ScriptableObject
    {
        public Currency[] currencies;

        public void Initialize()
        {
            for (int i = 0; i < currencies.Length; i++)
            {
                currencies[i].Initialize(i);
            }
        }

        public void EarnCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            currencies[currencyValue.currencyId].Earn(currencyValue.changeAmount);
        }

        public void SpendCurrency(EventArgs eventArgs)
        {
            var currencyValue = eventArgs as CurrencyArgs;
            currencies[currencyValue.currencyId].Spend(currencyValue.changeAmount);
        }
    }
}