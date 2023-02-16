using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.GameFlow.EventBusSystem;
using Codebase.Infrastructure.GameFlow.Events;
using Codebase.Infrastructure.Services.DataStorage;
using UnityEngine;

namespace Codebase.Core.Analytics
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly IGameVariables _gameVariables;
        private readonly IEventBus _eventBus;
        private DateTime _startTime;
        private DateTime _finishTime;
        private const string ModuleTag = "[AnalyticsModule]: ";

        public AnalyticsModule(IGameVariables gameVariables, IEventBus eventBus)
        {
            _gameVariables = gameVariables;
            _eventBus = eventBus;

            _eventBus.Subscribe<GameplayStarted>(OnGameplayStarted);
            _eventBus.Subscribe<PlayerWin>(OnPlayerWin);
            _eventBus.Subscribe<PlayerLose>(OnPlayerLose);
        }

        private void OnGameplayStarted()
        {
            LevelStart();
        }

        private void OnPlayerWin()
        {
            LevelFinish(true);
        }

        private void OnPlayerLose()
        {
            LevelFinish(false);
        }

        private void LevelStart()
        {
            _startTime = DateTime.Now;
            Debug.Log($"{ModuleTag}Level_{_gameVariables.LevelNumber.ToString()} started");
        }

        private void LevelFinish(bool isWin)
        {
            _finishTime = DateTime.Now;
            float resultTime = Mathf.Abs((float)_startTime.Subtract(_finishTime).TotalSeconds);
            string status = (isWin) ? "Complete" : "Fail";
            Debug.Log(
                $"{ModuleTag}Level_{_gameVariables.LevelNumber.ToString()} finished with status: {status}, time: {Mathf.RoundToInt(resultTime).ToString()}");
        }
    }
}