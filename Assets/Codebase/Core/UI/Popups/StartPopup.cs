using Codebase.Infrastructure.GameFlow;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class StartPopup : Popup
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
                .Receive<GameLevelMessage>()
                .Where(msg => msg.Message == LevelMessage.Started)
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