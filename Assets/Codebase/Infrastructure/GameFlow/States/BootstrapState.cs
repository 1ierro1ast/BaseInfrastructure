using Codebase.Core.Ads;
using Codebase.Core.Analytics;
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
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _services = allServices;

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
            var sceneService = _services.Single<ISceneService>();
            var settings = sceneService.GetCurrentSceneSettings();

            _stateMachine.Enter<LoadLevelState, string>(settings.SceneName);
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterGetSetService();
            RegisterSaveLoadService();
            RegisterGameSettings();
            RegisterSceneService();

            RegisterGameVariables();
            RegisterAdsModule();
            RegisterAnalyticsModule();
            RegisterTemporaryLevelVariables();
            RegisterCanvasService();

            RegisterLevelFactory();
        }

        private void RegisterGameSettings()
        {
            GameSettings gameSettings = _services.Single<IAssetProvider>()
                .GetScriptableObject<GameSettings>(AssetPath.GameSettings);

            _services.RegisterSingle(gameSettings);
        }

        private void RegisterSceneService()
        {
            _services.RegisterSingle<ISceneService>(new SceneService(_services.Single<IAssetProvider>(),
                _services.Single<ISaveLoadService>(), AssetPath.SceneSettings));
        }

        private void RegisterAdsModule()
        {
            _services.RegisterSingle<IAdsModule>(
                new AdsModule());
        }

        private void RegisterAnalyticsModule()
        {
            _services.RegisterSingle<IAnalyticsModule>(
                new AnalyticsModule(_services.Single<IGameVariables>()));
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

        private void RegisterGetSetService()
        {
            _services.RegisterSingle<IGetSetPrefsService>(new GetSetPrefsService());
        }

        private void RegisterSaveLoadService()
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
        }

        private void RegisterGameVariables()
        {
            _services.RegisterSingle<IGameVariables>(
                new GameVariables(_services.Single<IGetSetPrefsService>()));
        }

        private void RegisterLevelFactory()
        {
            _services.RegisterSingle<ILevelFactory>(new LevelFactory(_services.Single<IAssetProvider>()));
        }

        private void RegisterCanvasService()
        {
            var assetProvider = _services.Single<IAssetProvider>();
            var builder = new CanvasBuilder(assetProvider);

            builder
                .BuildStartPopup()
                .BuildOverlayPopup()
                .BuildWinPopup()
                .BuildLosePopup();

            var canvas = builder.MainCanvas;

            _services.RegisterSingle(canvas);
        }
    }
}