using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : Popup
    {
        private IEventBus _eventBus;
        protected override void OnInitialization()
        {
            base.OnInitialization();

            _eventBus = AllServices.Container.Single<IEventBus>();
            
            _eventBus.LevelFinishedEvent += EventBusOnLevelFinishedEvent;
        }

        private void EventBusOnLevelFinishedEvent()
        {
            _eventBus.LevelFinishedEvent -= EventBusOnLevelFinishedEvent;
            ClosePopup();
        }

        private void OnDestroy()
        {
            _eventBus.LevelFinishedEvent -= EventBusOnLevelFinishedEvent;
        }
    }
}
