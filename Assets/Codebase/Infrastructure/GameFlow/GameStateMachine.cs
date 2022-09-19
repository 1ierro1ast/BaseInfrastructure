using System;
using System.Collections.Generic;
using Codebase.Core.Ads;
using Codebase.Core.UI;
using Codebase.Infrastructure.DataStorage;
using Codebase.Infrastructure.Factories;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner),

                [typeof(GameReadyState)] = new GameReadyState(this, services.Single<IUiFactory>(),
                    services.Single<ITemporaryLevelVariables>(), services.Single<IGameFlowBroadcaster>(),
                    loadingCurtain, coroutineRunner),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
                    services.Single<ILevelFactory>(), services.Single<IGameVariables>(), services.Single<IAdsModule>()),

                [typeof(GameplayState)] = new GameplayState(this, services.Single<IGameFlowBroadcaster>(),
                    services.Single<IUiFactory>(), services.Single<ITemporaryLevelVariables>(), coroutineRunner,
                    loadingCurtain),

                [typeof(WinState)] = new WinState(this, services.Single<IUiFactory>(),
                    services.Single<IGameVariables>(), loadingCurtain),

                [typeof(LoseState)] = new LoseState(this, services.Single<IUiFactory>(),
                    services.Single<IGameVariables>(), loadingCurtain)
            };
        }
    }
}