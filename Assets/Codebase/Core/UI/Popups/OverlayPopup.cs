using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : Popup
    {
        private IGameFlowBroadcaster _gameFlowBroadcaster;
        protected override void OnInitialization()
        {
            base.OnInitialization();

            _gameFlowBroadcaster = AllServices.Container.Single<IGameFlowBroadcaster>();
            
            _gameFlowBroadcaster.LevelFinishedEvent += GameFlowBroadcaster_OnLevelFinishedEvent;
        }

        private void GameFlowBroadcaster_OnLevelFinishedEvent()
        {
            _gameFlowBroadcaster.LevelFinishedEvent -= GameFlowBroadcaster_OnLevelFinishedEvent;
            ClosePopup();
        }

        private void OnDestroy()
        {
            _gameFlowBroadcaster.LevelFinishedEvent -= GameFlowBroadcaster_OnLevelFinishedEvent;
        }
    }
}
