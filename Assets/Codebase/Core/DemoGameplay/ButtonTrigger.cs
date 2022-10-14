using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.DemoGameplay
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerType _triggerType;
        private Button _button;
        private IEventBus _eventBus;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _eventBus = AllServices.Container.Single<IEventBus>();
            _button?.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            switch (_triggerType)
            {
                case TriggerType.Win:
                    _eventBus.BroadcastPlayerWin();
                    break;
                case TriggerType.Lose:
                    _eventBus.BroadcastPlayerLose();
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