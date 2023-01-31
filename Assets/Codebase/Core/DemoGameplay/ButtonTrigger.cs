using Codebase.Infrastructure.GameFlow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.DemoGameplay
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerType _triggerType;
        private Button _button;

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
                    MessageBroker.Default
                        .Publish(new GameCompleteMessage(CompleteMessage.Win));
                    break;

                case TriggerType.Lose:
                    MessageBroker.Default
                        .Publish(new GameCompleteMessage(CompleteMessage.Lose));
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