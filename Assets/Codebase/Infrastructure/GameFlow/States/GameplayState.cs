﻿using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly ITemporaryLevelVariables _temporaryLevelVariables;

        private OverlayPopup _overlayPopup;
        private readonly CompositeDisposable _disposables = new();

        public GameplayState(GameStateMachine gameStateMachine,
            IUiFactory uiFactory, ITemporaryLevelVariables temporaryLevelVariables)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _temporaryLevelVariables = temporaryLevelVariables;
        }

        public void Exit()
        {
            _disposables.Clear();
        }

        public void Enter()
        {
            _temporaryLevelVariables.IsWin = false;
            _overlayPopup = _uiFactory.GetOverlayPopup();
            _overlayPopup.OpenPopup();

            MessageBroker.Default
                .Publish(new GameStatusMessage(LevelStatusMessage.Started));

            MessageBroker.Default
                .Receive<GameCompleteMessage>()
                .Subscribe(msg => CompliteLevelStatus(msg.Message))
                .AddTo(_disposables);
        }

        private void CompliteLevelStatus(CompleteMessage message)
        {
            var isWin = message == CompleteMessage.Win;

            MessageBroker.Default
                .Publish(new GameStatusMessage(LevelStatusMessage.Finished));

            Observable
                .FromMicroCoroutine(WaitCoroutine)
                .Subscribe(_ => SetState(isWin));
        }

        private void SetState(bool isWin)
        {
            _temporaryLevelVariables.IsWin = isWin;

            if (isWin)
                _gameStateMachine.Enter<WinState>();
            else
                _gameStateMachine.Enter<LoseState>();
        }

        private IEnumerator WaitCoroutine()
        {
            var duration = .7f;
            for (float t = 0f; t < duration; t += Time.deltaTime)
                yield return null;
        }
    }
}