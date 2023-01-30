using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly IGameVariables _gameVariables;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;
        private WinPopup _popup;

        public WinState(GameStateMachine gameStateMachine, IUiFactory uiFactory, IGameVariables gameVariables,
            LoadingCurtain loadingCurtain, ISceneService sceneService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _gameVariables = gameVariables;
            _loadingCurtain = loadingCurtain;
            _sceneService = sceneService;
        }

        public void Exit()
        {
            _popup.ClosePopup();
            _popup.OnNextLevel -= NextLevel;
        }

        public void Enter()
        {
            _popup = _uiFactory.GetWinPopup();
            _popup.OpenPopup();
            _popup.OnNextLevel += NextLevel;
        }

        private void NextLevel()
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(NextLevelCoroutine());
        }

        private IEnumerator NextLevelCoroutine()
        {
            _loadingCurtain.OpenPopup();

            var duration = .6f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            _gameVariables.IterateLevelNumber();

            _sceneService.SetNextScene();
            var settings = _sceneService.GetCurrentSceneSettings();

            _gameStateMachine.Enter<LoadLevelState, string>(settings.SceneName);
        }
    }
}