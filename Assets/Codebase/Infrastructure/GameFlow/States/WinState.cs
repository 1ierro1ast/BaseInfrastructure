using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

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
            _popup.NextLevelEvent -= PopupOnNextLevelEvent;
            _popup.ButtonClickedEvent -= PopupOnButtonClickedEvent;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _uiFactory.CreateWinPopup();
            _popup.OpenPopup();
            _popup.NextLevelEvent += PopupOnNextLevelEvent;
            _popup.ButtonClickedEvent += PopupOnButtonClickedEvent;
        }

        private void PopupOnButtonClickedEvent()
        {
            _loadingCurtain.OpenPopup();
        }

        private void PopupOnNextLevelEvent()
        {
            _gameVariables.IterateLevelNumber();
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");
        }
    }
}