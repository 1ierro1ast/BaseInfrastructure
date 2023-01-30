using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;
        private LosePopup _popup;

        public LoseState(GameStateMachine gameStateMachine, IUiFactory uiFactory,
            LoadingCurtain loadingCurtain, ISceneService sceneService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _sceneService = sceneService;
        }

        public void Exit()
        {
            _popup.OnRetryLevel -= RetryLevel;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _popup != null ? _popup : _uiFactory.GetLosePopup();
            _popup.OnRetryLevel += RetryLevel;
            _popup.OpenPopup();
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