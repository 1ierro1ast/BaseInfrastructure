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
        private IGameFlowBroadcaster _gameFlowBroadcaster;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _gameFlowBroadcaster = AllServices.Container.Single<IGameFlowBroadcaster>();
            _button?.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            switch (_triggerType)
            {
                case TriggerType.Win:
                    _gameFlowBroadcaster.BroadcastPlayerWin();
                    break;
                case TriggerType.Lose:
                    _gameFlowBroadcaster.BroadcastPlayerLose();
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