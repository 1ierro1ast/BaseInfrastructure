using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CanvasService _canvasService;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;
        private LosePopup _losePopup;

        public LoseState(GameStateMachine gameStateMachine, CanvasService canvasService,
            LoadingCurtain loadingCurtain, ISceneService sceneService)
        {
            _gameStateMachine = gameStateMachine;
            _canvasService = canvasService;
            _loadingCurtain = loadingCurtain;
            _sceneService = sceneService;
        }

        public void Exit()
        {
            _losePopup.OnRetryLevel -= RetryLevel;
            _losePopup.ClosePopup();
        }

        public void Enter()
        {
            _losePopup = _losePopup != null ?
                _losePopup : _canvasService.GetPopup<LosePopup>();

            _losePopup.OnRetryLevel += RetryLevel;
            _losePopup.OpenPopup();
        }

        private void RetryLevel()
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(RetryLevelCoroutine());
        }

        private IEnumerator RetryLevelCoroutine()
        {
            _loadingCurtain.OpenPopup();

            var duration = .6f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            var settings = _sceneService.GetCurrentSceneSettings();
            _gameStateMachine.Enter<LoadLevelState, string>(settings.SceneName);
        }
    }
}