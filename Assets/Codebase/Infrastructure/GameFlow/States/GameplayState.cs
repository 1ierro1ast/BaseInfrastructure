using System.Collections;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEventBus _eventBus;
        private readonly IUiFactory _uiFactory;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _loadingCurtain;

        private OverlayPopup _overlayPopup;


        public GameplayState(GameStateMachine gameStateMachine, IEventBus eventBus,
            IUiFactory uiFactory, ITemporaryLevelVariables temporaryLevelVariables,
            ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _temporaryLevelVariables = temporaryLevelVariables;
            _coroutineRunner = coroutineRunner;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _eventBus.PlayerWinEvent -= EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent -= EventBus_OnPlayerLoseEvent;
        }

        public void Enter()
        {
            _temporaryLevelVariables.IsWin = false;
            _loadingCurtain.ClosePopup();
            _overlayPopup = _uiFactory.GetOverlayPopup();
            _overlayPopup.OpenPopup();

            _eventBus.BroadcastGamePlayStart();

            _eventBus.PlayerWinEvent += EventBus_OnPlayerWinEvent;
            _eventBus.PlayerLoseEvent += EventBus_OnPlayerLoseEvent;
        }

        private void EventBus_OnPlayerLoseEvent()
        {
            _eventBus.BroadcastLevelFinished();
            _coroutineRunner.StartCoroutine(LoseCoroutine());
        }

        private void EventBus_OnPlayerWinEvent()
        {
            _eventBus.BroadcastLevelFinished();
            _coroutineRunner.StartCoroutine(WinCoroutine());
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
            yield return new WaitForSeconds(0.7f);
            ToWinState();
        }

        private IEnumerator LoseCoroutine()
        {
            yield return new WaitForSeconds(0.7f);
            ToLoseState();
        }
    }
}