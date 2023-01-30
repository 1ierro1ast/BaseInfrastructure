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
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEventBus _eventBus;
        private readonly IUiFactory _uiFactory;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;

        private OverlayPopup _overlayPopup;

        public GameplayState(GameStateMachine gameStateMachine, IEventBus eventBus,
            IUiFactory uiFactory, ITemporaryLevelVariables temporaryLevelVariables)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _temporaryLevelVariables = temporaryLevelVariables;
        }

        public void Exit()
        {
            _eventBus.PlayerWinEvent -= EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent -= EventBus_OnPlayerLoseEvent;
        }

        public void Enter()
        {
            _temporaryLevelVariables.IsWin = false;
            _overlayPopup = _uiFactory.GetOverlayPopup();
            _overlayPopup.OpenPopup();

            _eventBus.BroadcastGamePlayStart();

            _eventBus.PlayerWinEvent += EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent += EventBus_OnPlayerLoseEvent;
        }

        private void EventBus_OnPlayerLoseEvent()
        {
            _eventBus.BroadcastLevelFinished();
            MainThreadDispatcher.StartUpdateMicroCoroutine(LoseCoroutine());
        }

        private void EventBus_OnPlayerWinEvent()
        {
            _eventBus.BroadcastLevelFinished();
            MainThreadDispatcher.StartUpdateMicroCoroutine(WinCoroutine());
        }

        private void ToWinState()
        {
            _temporaryLevelVariables.IsWin = true;
            _gameStateMachine.Enter<WinState>();
        }

        private void ToLoseState()
        {
            _temporaryLevelVariables.IsWin = false;
            _gameStateMachine.Enter<LoseState>();
        }

        private IEnumerator WinCoroutine()
        {
            var duration = .7f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            ToWinState();
        }

        private IEnumerator LoseCoroutine()
        {
            var duration = .7f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            ToLoseState();
        }
    }
}