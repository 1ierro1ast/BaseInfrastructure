using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Core.DemoGameplay
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerType _triggerType;
        private Button _button;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button?.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            switch (_triggerType)
            {
                case TriggerType.Win:
                    _eventBus.Fire<PlayerWin>();
                    break;
                case TriggerType.Lose:
                    _eventBus.Fire<PlayerLose>();
                    break;
            }

            _button.interactable = false;
        }
    }

    public enum TriggerType
    {
        Win,
        Lose
    }
}