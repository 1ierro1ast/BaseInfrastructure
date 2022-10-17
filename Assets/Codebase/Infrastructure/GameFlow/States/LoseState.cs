using System.Collections;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutineRunner _coroutineRunner;
        private LosePopup _popup;

        public LoseState(GameStateMachine gameStateMachine, IUiFactory uiFactory, LoadingCurtain loadingCurtain, 
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
            _popup.RetryLevelEvent -= Popup_OnRetryLevelEvent;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _uiFactory.GetLosePopup();
            _popup.RetryLevelEvent += Popup_OnRetryLevelEvent;
            _popup.OpenPopup();
        }

        private void Popup_OnRetryLevelEvent()
        {
            _coroutineRunner.StartCoroutine(RetryLevelCoroutine());
        }

        private IEnumerator RetryLevelCoroutine()
        {
            _loadingCurtain.OpenPopup();
            yield return new WaitForSeconds(0.6f);
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");
        }
    }
}