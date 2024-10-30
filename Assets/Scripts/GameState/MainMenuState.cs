using GameTemplate.Gameplay.GameplayObjects.Audio;
using GameTemplate.Gameplay.UI;
using GameTemplate.Managers.Scene;
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

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterComponentInHierarchy<UICanvas>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}