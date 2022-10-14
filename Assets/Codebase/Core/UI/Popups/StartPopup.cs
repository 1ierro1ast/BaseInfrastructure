using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : Popup
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
            
            _eventBus.GamePlayStartEvent += EventBus_OnGamePlayStart;
        }

        private void OnDestroy()
        {
            _eventBus.GamePlayStartEvent -= EventBus_OnGamePlayStart;
        }

        private void EventBus_OnGamePlayStart()
        {
            Debug.Log("Game play start");
            ClosePopup();
        }

        private void OnStartButtonClick()
        {
            StartButtonClickEvent?.Invoke();
        }
    }
}
