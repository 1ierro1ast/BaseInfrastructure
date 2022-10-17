using System.Collections;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
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
        private readonly ICoroutineRunner _coroutineRunner;
        private StartPopup _startPopup;

        public GameReadyState(GameStateMachine gameStateMachine, IUiFactory uiFactory,
            ITemporaryLevelVariables temporaryLevelVariables, IEventBus eventBus,
            LoadingCurtain loadingCurtain, ICoroutineRunner coroutineRunner)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
            _temporaryLevelVariables = temporaryLevelVariables;
            _eventBus = eventBus;
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
            _startPopup.StartButtonClickEvent -= StartPopup_OnStartButtonClickEvent;
        }

        public void Enter()
        {
            _temporaryLevelVariables.ClearData();
            
            _startPopup = _uiFactory.GetStartPopup();
            _startPopup.OpenPopup();
            _startPopup.StartButtonClickEvent += StartPopup_OnStartButtonClickEvent;
            
            _eventBus.BroadcastLevelLoaded();
            _coroutineRunner.StartCoroutine(CloseCurtainCoroutine());
        }

        private IEnumerator CloseCurtainCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            _loadingCurtain.ClosePopup();
        }

        private void StartPopup_OnStartButtonClickEvent()
        {
            _loadingCurtain.OpenPopup();
            _coroutineRunner.StartCoroutine(GoToGameCoroutine());
        }

        private IEnumerator GoToGameCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            _startPopup.ClosePopup();
            yield return new WaitForSeconds(0.5f);
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}