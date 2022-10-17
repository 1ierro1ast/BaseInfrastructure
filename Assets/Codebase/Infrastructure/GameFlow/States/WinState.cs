using System.Collections;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly IGameVariables _gameVariables;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private WinPopup _popup;

        public WinState(GameStateMachine gameStateMachine, IUiFactory uiFactory, IGameVariables gameVariables,
            LoadingCurtain loadingCurtain, ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _gameVariables = gameVariables;
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
            _popup.ClosePopup();
            _popup.NextLevelEvent -= Popup_OnNextLevelEvent;
        }

        public void Enter()
        {
            _popup = _uiFactory.GetWinPopup();
            _popup.OpenPopup();
            _popup.NextLevelEvent += Popup_OnNextLevelEvent;
        }

        private void Popup_OnNextLevelEvent()
        {
            _coroutineRunner.StartCoroutine(NextLevelCoroutine());
        }
        
        private IEnumerator NextLevelCoroutine()
        {
            _loadingCurtain.OpenPopup();
            yield return new WaitForSeconds(0.6f);
            _gameVariables.IterateLevelNumber();
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");
        }
    }
}