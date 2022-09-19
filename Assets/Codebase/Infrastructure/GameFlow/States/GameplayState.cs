using System.Collections;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.DataStorage;
using Codebase.Infrastructure.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFlowBroadcaster _gameFlowBroadcaster;
        private readonly IUiFactory _uiFactory;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _loadingCurtain;

        private OverlayPopup _overlayPopup;


        public GameplayState(GameStateMachine gameStateMachine, IGameFlowBroadcaster gameFlowBroadcaster,
            IUiFactory uiFactory, ITemporaryLevelVariables temporaryLevelVariables,
            ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _gameFlowBroadcaster = gameFlowBroadcaster;
            _uiFactory = uiFactory;
            _temporaryLevelVariables = temporaryLevelVariables;
            _coroutineRunner = coroutineRunner;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _gameFlowBroadcaster.PlayerWinEvent -= GameFlowBroadcaster_OnPlayerWinEvent;
            _gameFlowBroadcaster.PlayerLoseEvent -= GameFlowBroadcaster_OnPlayerLoseEvent;
        }

        public void Enter()
        {
            _temporaryLevelVariables.IsWin = false;
            _loadingCurtain.ClosePopup();
            _overlayPopup = _uiFactory.CreateOverlayPopup();
            _overlayPopup.OpenPopup();

            _gameFlowBroadcaster.BroadcastGamePlayStart();

            _gameFlowBroadcaster.PlayerWinEvent += GameFlowBroadcaster_OnPlayerWinEvent;
            _gameFlowBroadcaster.PlayerLoseEvent += GameFlowBroadcaster_OnPlayerLoseEvent;
        }

        private void GameFlowBroadcaster_OnPlayerLoseEvent()
        {
            _gameFlowBroadcaster.BroadcastLevelFinished();
            _coroutineRunner.StartCoroutine(LoseCoroutine());
        }

        private void GameFlowBroadcaster_OnPlayerWinEvent()
        {
            _gameFlowBroadcaster.BroadcastLevelFinished();
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