using System.Collections.Generic;
using GameTemplate.UI.Currencies;
using GameTemplate.Audio;
using GameTemplate.Managers;
using GameTemplate.Managers.Scene;
using GameTemplate.UI;
using GameTemplate.UI.Currency;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Gameplay.UI
{
    public class UICanvas : MonoBehaviour, IStartable
    {
        #region Variables

        [SerializeField] private Transform currencyParent;
        [SerializeField] private GameObject CurrencyUIPrefab;
        public List<CurrencyUI> currencyPanels = new List<CurrencyUI>();

        #endregion

        #region Injections

        [Inject] CurrencyManager _CurrencyManager;
        [Inject] SceneLoader _SceneLoader;
        [Inject] SoundPlayer _SoundPlayer;
        [Inject] LevelManager _LevelManager;

        #endregion

        [Inject]
        public void Construct(SceneLoader sceneLoader, SoundPlayer SoundPlayer, LevelManager levelManager)
        {
            //Debug.Log("Construct UICanvas");
            _SceneLoader = sceneLoader;
            _SoundPlayer = SoundPlayer;
            _LevelManager = levelManager;
            
            GetComponentInChildren<LevelTextSetter>().SetLevelText(_LevelManager.UILevelId);
        }

        public void Start()
        {
            //Debug.Log("UI Canvas Start");
            List<Currency> currencies = _CurrencyManager._CurrencyData.currencies;

            for (int i = 0; i < currencies.Count; i++)
            {
                currencyPanels.Add(Instantiate(CurrencyUIPrefab, currencyParent).GetComponent<CurrencyUI>());
                currencyPanels[i].transform.SetParent(currencyParent);
                currencyPanels[i].Initialize(currencies[i].currencyImage, currencies[i].currencySign,
                    currencies[i].currencyAmount, currencies[i].isBuyable);
            }
        }

        public void PlayButtonClick()
        {
            _SoundPlayer.StopThemeMusic();
            _SceneLoader.LoadSceneByType(SceneType.Game);
        }
    }
}