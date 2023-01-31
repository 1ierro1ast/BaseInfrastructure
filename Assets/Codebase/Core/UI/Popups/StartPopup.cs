using Codebase.Infrastructure.GameFlow;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : PopupBase
    {
        public event Action OnStartButtonClick;

        [SerializeField] private Button _startButton;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            OpenPopup();

            _startButton
                .OnClickAsObservable()
                .Subscribe(_ => StartButtonClick())
                .AddTo(this);

            MessageBroker.Default
                .Receive<GameStatusMessage>()
                .Where(msg => msg.Message == LevelStatusMessage.Started)
                .Subscribe(_ => GamePlayStart())
                .AddTo(this);
        }

        private void GamePlayStart()
        {
            Debug.Log("Game play start");
        }

        private void StartButtonClick()
        {
            OnStartButtonClick?.Invoke();
        }
    }
}