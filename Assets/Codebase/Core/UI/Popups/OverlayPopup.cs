using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using Codebase.Infrastructure.Services;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : BasePopup
    {
        private IEventBus _eventBus;
        protected override void OnInitialization()
        {
            base.OnInitialization();

            _eventBus = AllServices.Container.Single<IEventBus>();
            
            _eventBus.Subscribe<LevelFinished>(OnLevelFinished);
        }

        private void OnLevelFinished()
        {
            _eventBus.Unsubscribe<LevelFinished>(OnLevelFinished);
            ClosePopup();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<LevelFinished>(OnLevelFinished);
        }
    }
}
