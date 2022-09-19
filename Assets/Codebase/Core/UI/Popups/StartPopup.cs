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
        private IGameFlowBroadcaster _gameFlowBroadcaster;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();
            _startButton.onClick.AddListener(OnStartButtonClick);
            _gameFlowBroadcaster = AllServices.Container.Single<IGameFlowBroadcaster>();
            
            _gameFlowBroadcaster.GamePlayStartEvent += GameFlow_BroadcasterOnGamePlayStart;
        }

        private void OnDestroy()
        {
            _gameFlowBroadcaster.GamePlayStartEvent -= GameFlow_BroadcasterOnGamePlayStart;
        }

        private void GameFlow_BroadcasterOnGamePlayStart()
        {
            Debug.Log("Game play start");
            ClosePopup();
        }

        private void OnStartButtonClick()
        {
            StartButtonClickEvent?.Invoke();
        }

        public void BroadcastStartLevel()
        {
            StartButtonClickEvent?.Invoke();
        }
    }
}
