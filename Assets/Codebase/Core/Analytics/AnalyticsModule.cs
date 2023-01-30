using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using System;
using UnityEngine;

namespace Codebase.Core.Analytics
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly IGameVariables _gameVariables;
        private readonly IEventBus _eventBus;
        private readonly ISceneService _sceneService;
        private DateTime _startTime;
        private DateTime _finishTime;
        private const string ModuleTag = "[AnalyticsModule]: ";

        public AnalyticsModule(IGameVariables gameVariables, IEventBus eventBus, ISceneService sceneService)
        {
            _gameVariables = gameVariables;
            _eventBus = eventBus;
            _sceneService = sceneService;

            _eventBus.GamePlayStartEvent += EventBus_OnGamePlayStartEvent;
            _eventBus.OnPlayerWinEvent += EventBus_OnPlayerWinEvent;
            _eventBus.OnPlayerLoseEvent += EventBus_OnPlayerLoseEvent;
        }

        private void EventBus_OnGamePlayStartEvent()
        {
            LevelStart();
        }

        private void EventBus_OnPlayerWinEvent()
        {
            LevelFinish(true);
        }

        private void EventBus_OnPlayerLoseEvent()
        {
            LevelFinish(false);
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