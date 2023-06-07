using System;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : BasePopup
    {
        public event Action StartButtonClickEvent;
        [SerializeField] private Button _startButton;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            Debug.Log($"ctor {nameof(StartPopup)}.{nameof(Construct)}");
            _eventBus = eventBus;
            _eventBus.Subscribe<GameplayStarted>(OnGameplayStarted);
        }

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GameplayStarted>(OnGameplayStarted);
        }

        private void OnGameplayStarted()
        {
            Debug.Log("Game play start");
        }

        private void OnStartButtonClick()
        {
            StartButtonClickEvent?.Invoke();
        }
    }
}
