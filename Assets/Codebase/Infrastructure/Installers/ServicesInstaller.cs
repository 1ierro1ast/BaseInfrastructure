using Codebase.Core.Ads;
using Codebase.Core.Analytics;
using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        private CoroutineRunnner _coroutineRunnerInstance;

        public override void InstallBindings()
        {
            RegisterAssetProvider();
            RegisterSaveLoadService();

            RegisterGameVariables();
            RegisterEventBus();
            RegisterAdsModule();
            RegisterAnalyticsModule();
            RegisterTemporaryLevelVariables();
            RegisterUiFactory();

            RegisterLevelFactory();
            RegisterLoadingCurtain();
            RegisterCoroutineRunner();
            RegisterSceneLoader();
            RegisterGameStateMachine();
        }

        private void RegisterCoroutineRunner()
        {
            _coroutineRunnerInstance = new GameObject("CoroutineRunner", new[] { typeof(CoroutineRunnner) })
                .GetComponent<CoroutineRunnner>();
            
            DontDestroyOnLoad(_coroutineRunnerInstance);
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(_coroutineRunnerInstance)
                .AsSingle()
                .NonLazy();
        }

        private void RegisterSceneLoader()
        {
            SceneLoader sceneLoader = new SceneLoader(_coroutineRunnerInstance);
            Container
                .Bind<SceneLoader>()
                .FromInstance(sceneLoader)
                .AsSingle()
                .NonLazy();
        }

        private void RegisterLoadingCurtain()
        {
            LoadingCurtain loadingCurtainInstance = Instantiate(_loadingCurtainPrefab);
            DontDestroyOnLoad(loadingCurtainInstance);
            
            Container
                .Bind<LoadingCurtain>()
                .FromInstance(loadingCurtainInstance)
                .AsSingle()
                .NonLazy();
        }

        private void RegisterGameStateMachine()
        {
            
            Container
                .Bind<GameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();
            
        }

        private void RegisterLevelFactory()
        {
            Container
                .Bind<ILevelFactory>()
                .To<LevelFactory>()
                .AsSingle();
        }

        private void RegisterUiFactory()
        {
            Container
                .Bind<IUiFactory>()
                .To<UiFactory>()
                .AsSingle();
        }

        private void RegisterTemporaryLevelVariables()
        {
            Container
                .Bind<ITemporaryLevelVariables>()
                .To<TemporaryLevelVariables>()
                .AsSingle();
        }

        private void RegisterAnalyticsModule()
        {
            Container
                .Bind<IAnalyticsModule>()
                .To<AnalyticsModule>()
                .AsSingle();
        }

        private void RegisterAdsModule()
        {
            Container
                .Bind<IAdsModule>()
                .To<AdsModule>()
                .AsSingle();
        }

        private void RegisterEventBus()
        {
            Container
                .Bind<IEventBus>()
                .To<EventBus>()
                .AsSingle();
        }

        private void RegisterGameVariables()
        {
            Container
                .Bind<IGameVariables>()
                .To<GameVariables>()
                .AsSingle();
        }

        private void RegisterSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }

        private void RegisterAssetProvider()
        {
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
    }
}