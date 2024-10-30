using System;
using Audio;
using UnityEngine;
using GameTemplate.ApplicationLifecycle;
using GameTemplate.Audio;
using GameTemplate.Gameplay.GameState;
using GameTemplate.Infrastructure;
using GameTemplate.Managers;
using GameTemplate.ScriptableObjects;
using GameTemplate.UI.Currency;
using ScriptableObjects;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.ApplicationLifeCycle
{
    /// <summary>
    /// An entry point to the application, where we bind all the common dependencies to the root DI scope.
    /// </summary>
    public class ApplicationController : LifetimeScope
    {
        IDisposable m_Subscriptions;

        public AudioData audioData;
        public CurrencyData currencyData;
        public SceneData sceneData;
        public LevelData levelData;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(audioData);
            builder.RegisterInstance(currencyData);
            builder.RegisterInstance(sceneData);
            builder.RegisterInstance(levelData);
            
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<CurrencyManager>(Lifetime.Singleton);
            builder.Register<SoundPlayer>(Lifetime.Singleton);

            builder.Register<PersistentGameState>(Lifetime.Singleton);

            builder.RegisterInstance(new MessageChannel<QuitApplicationMessage>()).AsImplementedInterfaces();
        }

        public void Start()
        {
            var quitApplicationSub = Container.Resolve<ISubscriber<QuitApplicationMessage>>();

            var subHandles = new DisposableGroup();
            subHandles.Add(quitApplicationSub.Subscribe(QuitGame));
            m_Subscriptions = subHandles;

            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
            SceneManager.LoadScene(sceneData.scenes[SceneType.MainMenu]);
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