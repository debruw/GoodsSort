using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace GameTemplate.UI.Currency
{
    [CreateAssetMenu(fileName = "CurrencyManager", menuName = "Scriptable Objects/Currency Manager")]
    public class CurrencyData : ScriptableObject, IStartable
    {
        public List<Currencies.Currency> currencies = new List<Currencies.Currency>();
        public void Start()
        {
            Debug.Log("Currency Data");
        }
    }
}