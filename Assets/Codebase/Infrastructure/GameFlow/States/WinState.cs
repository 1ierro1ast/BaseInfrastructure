using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CanvasService _canvasService;
        private readonly IGameVariables _gameVariables;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;
        private WinPopup _winPopup;

        public WinState(GameStateMachine gameStateMachine, CanvasService canvasService, IGameVariables gameVariables,
            LoadingCurtain loadingCurtain, ISceneService sceneService)
        {
            _gameStateMachine = gameStateMachine;
            _canvasService = canvasService;
            _gameVariables = gameVariables;
            _loadingCurtain = loadingCurtain;
            _sceneService = sceneService;
        }

        public void Exit()
        {
            _winPopup.ClosePopup();
            _winPopup.OnNextLevel -= NextLevel;
        }

        public void Enter()
        {
            _winPopup = _winPopup != null ?
                _winPopup : _canvasService.GetPopup<WinPopup>();

            _winPopup.OpenPopup();
            _winPopup.OnNextLevel += NextLevel;
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