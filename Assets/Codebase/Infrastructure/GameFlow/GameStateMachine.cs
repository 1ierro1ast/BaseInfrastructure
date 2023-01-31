using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services),

                [typeof(GameReadyState)] = new GameReadyState(this, services.Single<CanvasService>(),
                    services.Single<ITemporaryLevelVariables>(), loadingCurtain),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
                    services.Single<ILevelFactory>(), services.Single<IGameVariables>()),

                [typeof(GameplayState)] = new GameplayState(this, services.Single<CanvasService>(), 
                    services.Single<ITemporaryLevelVariables>()),

                [typeof(WinState)] = new WinState(this, services.Single<CanvasService>(),
                    services.Single<IGameVariables>(), loadingCurtain, services.Single<ISceneService>()),

                [typeof(LoseState)] = new LoseState(this, services.Single<CanvasService>(), loadingCurtain,
                services.Single<ISceneService>())
            };
        }
    }
}