using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using UnityEngine;
using Zenject;

namespace Codebase.Core.UI.Popups
{
    public class OverlayPopup : BasePopup
    {
        private IEventBus _eventBus;
        [Inject]
        private void Construct(IEventBus eventBus)
        {
            Debug.Log($"ctor {nameof(OverlayPopup)}.{nameof(Construct)}");

            _eventBus = eventBus;
            
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
