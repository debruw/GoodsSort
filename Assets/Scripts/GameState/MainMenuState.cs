using GameTemplate.Audio;
using GameTemplate.Events;
using GameTemplate.Gameplay.UI;
using GameTemplate.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Gameplay.GameState
{
    /// <summary>
    /// Game Logic that runs when sitting at the MainMenu. This is likely to be "nothing", as no game has been started. But it is
    /// nonetheless important to have a game state, as the GameStateBehaviour system requires that all scenes have states.
    /// </summary>
    public class MainMenuState : GameStateBehaviour
    {
        public override GameState ActiveState => GameState.MainMenu;

        [SerializeField] private UICanvas UICanvas;

        protected override void Start()
        {
            base.Start();
            UICanvas.Initialize();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponent(UICanvas);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void OnStartClicked()
        {
            SoundPlayer.Instance.StopThemeMusic();
            SceneLoader.Instance.LoadScene(SceneLoader.Game);
        }

        [Inject] private CurrencyManager _currencyManager;
        public void AddCurrency()
        {
            _currencyManager.EarnCurrency(new CurrencyArgs(0,10,false));
        }
    }
}