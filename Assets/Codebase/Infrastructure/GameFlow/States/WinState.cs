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
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly IGameVariables _gameVariables;
        private readonly LoadingCurtain _loadingCurtain;

        private WinPopup _popup;

        public WinState(GameStateMachine gameStateMachine, IUiFactory uiFactory, IGameVariables gameVariables,
            LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _gameVariables = gameVariables;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _popup.ClosePopup();
            _popup.OnNextLevel -= NextLevel;
        }

        public void Enter()
        {
            _popup = _uiFactory.GetWinPopup();
            _popup.OpenPopup();
            _popup.OnNextLevel += NextLevel;
        }

        private void NextLevel()
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(NextLevelCoroutine());
        }

        private IEnumerator NextLevelCoroutine()
        {
            _loadingCurtain.OpenPopup();

            var duration = .6f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;

            _gameVariables.IterateLevelNumber();
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");
        }
    }
}