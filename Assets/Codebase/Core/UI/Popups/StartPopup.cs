using System;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : BasePopup
    {
        public event Action StartButtonClickEvent;
        [SerializeField] private Button _startButton;
        private IEventBus _eventBus;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            _startButton.onClick.AddListener(OnStartButtonClick);
            _eventBus = AllServices.Container.Single<IEventBus>();
            
            _eventBus.Subscribe<GameplayStarted>(OnGameplayStarted);
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
