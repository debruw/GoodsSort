using System;
using GameTemplate.Utils;
using UnityEngine;

namespace Game.Managers.Currencies
{
    [System.Serializable]
    public class Currency
    {
        public Sprite currencyImage;
        public string currencySign;
        public int currencyAmount;
        public bool isBuyable;

        private int currencyId;

        public int CurrencyId
        {
            get => currencyId;
        }
        
        public void Initialize(int cId)
        {
            this.currencyId = cId;
            currencyAmount = UserPrefs.GetCurrency(currencyId, currencyAmount);
        }

        public void Reset(EventArgs args)
        {
            currencyAmount = 0;
            SetPlayerPref();
        }

        public void Spend(int spentAmount)
        {
            currencyAmount -= spentAmount;
            SetPlayerPref();
        }

        public void Earn(int earningsAmount)
        {
            currencyAmount += earningsAmount;
            SetPlayerPref();
        }

        public void SetPlayerPref()
        {
            UserPrefs.SetCurrency(currencyId, currencyAmount);
        }
    }
}