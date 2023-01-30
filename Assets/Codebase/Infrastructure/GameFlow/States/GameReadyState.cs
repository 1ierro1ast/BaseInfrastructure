using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameReadyState : IState
    {
        private readonly IUiFactory _uiFactory;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;
        private readonly IEventBus _eventBus;
        private readonly LoadingCurtain _loadingCurtain;
        private StartPopup _startPopup;

        public GameReadyState(GameStateMachine gameStateMachine, IUiFactory uiFactory,
            ITemporaryLevelVariables temporaryLevelVariables, IEventBus eventBus,
            LoadingCurtain loadingCurtain)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _temporaryLevelVariables = temporaryLevelVariables;
            _eventBus = eventBus;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _startPopup.StartButtonClickEvent -= StartButtonClick;
        }

        public void Enter()
        {
            _loadingCurtain.OpenPopup();
            _temporaryLevelVariables.ClearData();

            _startPopup = _uiFactory.GetStartPopup();
            _startPopup.OpenPopup();
            _startPopup.StartButtonClickEvent += StartButtonClick;

            _eventBus.BroadcastLevelLoaded();
            MainThreadDispatcher.StartUpdateMicroCoroutine(CloseCurtainCoroutine());
        }

        private IEnumerator CloseCurtainCoroutine()
        {
            var duration = .5f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            _loadingCurtain.ClosePopup();
        }

        private void StartButtonClick()
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(GoToGameCoroutine());
        }

        private IEnumerator GoToGameCoroutine()
        {
            var duration = .5f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            _startPopup.ClosePopup();

            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            _gameStateMachine.Enter<GameplayState>();
        }
    }
}