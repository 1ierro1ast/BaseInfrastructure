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
    public class GameReadyState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly CanvasService _canvasService;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;
        private readonly LoadingCurtain _loadingCurtain;
        private StartPopup _startPopup;

        public GameReadyState(GameStateMachine gameStateMachine, CanvasService canvasService,
            ITemporaryLevelVariables temporaryLevelVariables, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _canvasService = canvasService;
            _temporaryLevelVariables = temporaryLevelVariables;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _startPopup.OnStartButtonClick -= StartButtonClick;
        }

        public void Enter()
        {
            _loadingCurtain.OpenPopup();
            _temporaryLevelVariables.ClearData();

            _startPopup = _startPopup != null ?
                _startPopup : _canvasService.GetPopup<StartPopup>();

            _startPopup.OpenPopup();
            _startPopup.OnStartButtonClick += StartButtonClick;

            MessageBroker.Default
                .Publish(new GameStatusMessage(LevelStatusMessage.Loaded));

            Observable
                .FromMicroCoroutine(Waiting)
                .Subscribe(_ => _loadingCurtain.ClosePopup());
        }

        private void StartButtonClick()
        {
            _startPopup.ClosePopup();

            Observable
                .FromMicroCoroutine(Waiting)
                .Subscribe(_ => _gameStateMachine.Enter<GameplayState>());
        }

        private IEnumerator Waiting()
        {
            var duration = .5f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;
        }
    }
}