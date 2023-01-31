using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services.DataStorage;
using System;
using UniRx;
using UnityEngine;

namespace Codebase.Core.Analytics
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly IGameVariables _gameVariables;
        private DateTime _startTime;
        private DateTime _finishTime;
        private const string ModuleTag = "[AnalyticsModule]: ";

        public AnalyticsModule(IGameVariables gameVariables)
        {
            _gameVariables = gameVariables;

            MessageBroker.Default
                .Receive<GameStatusMessage>()
                .Where(msg => msg.Message == LevelStatusMessage.Started)
                .Subscribe(_ => LevelStart());

            MessageBroker.Default
                .Receive<GameCompleteMessage>()
                .Subscribe(msg => CompliteStatus(msg.Message));
        }

        private void CompliteStatus(CompleteMessage message)
        {
            var isWin = message == CompleteMessage.Win;
            LevelFinish(isWin);
        }

        private void LevelStart()
        {
            _startTime = DateTime.Now;
            Debug.Log($"{ModuleTag}Level_{_gameVariables.LevelNumber} started");
        }

        private void LevelFinish(bool isWin)
        {
            _finishTime = DateTime.Now;
            float resultTime = (float)_startTime.Subtract(_finishTime).TotalSeconds;
            string status = (isWin) ? "Complete" : "Fail";
            Debug.Log($"{ModuleTag}Level_{_gameVariables.LevelNumber} finished with status: {status}, time: {Mathf.RoundToInt(resultTime)}");
        }
    }
}