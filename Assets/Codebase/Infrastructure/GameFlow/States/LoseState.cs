using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.DataStorage;
using Codebase.Infrastructure.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoseState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private LosePopup _popup;

        public LoseState(GameStateMachine gameStateMachine, IUiFactory uiFactory, IGameVariables gameVariables,
            LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
        }

        public void Exit()
        {
            _popup.RetryLevel -= PopupOnRetryLevel;
            _popup.ClosePopup();
        }

        public void Enter()
        {
            _popup = _uiFactory.CreateLoosePopup();
            _popup.RetryLevel += PopupOnRetryLevel;
            _popup.OpenPopup();
        }

        private void PopupOnRetryLevel()
        {
            _loadingCurtain.OpenPopup();
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");
        }
    }
}