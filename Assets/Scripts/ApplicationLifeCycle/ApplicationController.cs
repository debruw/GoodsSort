using System;
using UnityEngine;
using GameTemplate.ApplicationLifecycle;
using GameTemplate.Gameplay.GameState;
using GameTemplate.Infrastructure;
using GameTemplate.Managers;
using GameTemplate.Managers.SceneManagers;
using VContainer;
using VContainer.Unity;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace GameTemplate.ApplicationLifeCycle
{
    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationController : LifetimeScope
    {
        IDisposable m_Subscriptions;
        [SerializeField] private CurrencyManager _currencyManager;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<LevelManager>(Lifetime.Singleton);

            builder.Register<PersistentGameState>(Lifetime.Singleton);

            builder.RegisterInstance(_currencyManager);

            builder.RegisterInstance(new MessageChannel<QuitApplicationMessage>()).AsImplementedInterfaces();
        }

        private void Start()
        {
            var quitApplicationSub = Container.Resolve<ISubscriber<QuitApplicationMessage>>();

            var subHandles = new DisposableGroup();
            subHandles.Add(quitApplicationSub.Subscribe(QuitGame));
            m_Subscriptions = subHandles;

            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;

            SceneManager.LoadScene("MainMenu");
        }

        protected override void OnDestroy()
        {
            if (m_Subscriptions != null)
            {
                m_Subscriptions.Dispose();
            }

            base.OnDestroy();
        }

        private void QuitGame(QuitApplicationMessage msg)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}