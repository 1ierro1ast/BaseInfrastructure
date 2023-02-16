using Codebase.Core.Ads;
using Codebase.Core.Analytics;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.Settings;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class BootstrapState : IState
    {
        private const string NextSceneName = "MainScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            EnterLoadLevel();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(NextSceneName);
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterSaveLoadService();
            RegisterGameSettings();

            RegisterGameVariables();
            RegisterEventBus();
            RegisterAdsModule();
            RegisterAnalyticsModule();
            RegisterTemporaryLevelVariables();
            RegisterUiFactory();

            RegisterLevelFactory();
        }

        private void RegisterGameSettings()
        {
            GameSettings gameSettings = _services.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath);
            _services.RegisterSingle(gameSettings);
        }

        private void RegisterAdsModule()
        {
            _services.RegisterSingle<IAdsModule>(
                new AdsModule());
        }

        private void RegisterAnalyticsModule()
        {
            _services.RegisterSingle<IAnalyticsModule>(
                new AnalyticsModule(_services.Single<IGameVariables>(), _services.Single<IEventBus>()));
        }

        private void RegisterTemporaryLevelVariables()
        {
            _services.RegisterSingle<ITemporaryLevelVariables>(
                new TemporaryLevelVariables());
        }

        private void RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        }

        private void RegisterSaveLoadService()
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
        }

        private void RegisterGameVariables()
        {
            _services.RegisterSingle<IGameVariables>(
                new GameVariables(_services.Single<ISaveLoadService>()));
        }

        private void RegisterLevelFactory()
        {
            _services.RegisterSingle<ILevelFactory>(
                new LevelFactory(_services.Single<IAssetProvider>(), 
                    _services.Single<IGameVariables>(),
                    _services.Single<ITemporaryLevelVariables>()));
        }

        private void RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(
                new EventBus());
        }

        private void RegisterUiFactory()
        {
            _services.RegisterSingle<IUiFactory>(
                new UiFactory(_services.Single<IAssetProvider>()));
        }
    }
}