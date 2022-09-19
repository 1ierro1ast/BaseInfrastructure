using System;
using Codebase.Infrastructure.DataStorage;

//using FunGames.Sdk.Analytics;

namespace Codebase.Core.Analytics
{
    public class AnalyticsModule : IAnalyticsModule
    {
        private readonly IGameVariables _gameVariables;
        private DateTime _startTime;
        private DateTime _finishTime;
        public AnalyticsModule(IGameVariables gameVariables)
        {
            _gameVariables = gameVariables;
        }
        
        public void LevelStart()
        {
            _startTime = DateTime.Now;
            //TinySauce.OnGameStarted("Level_" + _gameVariables.LevelNumber);
            //FunGamesAnalytics.NewProgressionEvent("Start", $"Level_" + _gameVariables.LevelNumber);
        }

        public void LevelFinish(bool isWin)
        {
            _finishTime = DateTime.Now;
            float resultTime = (float)_startTime.Subtract(_finishTime).TotalSeconds;
            string status = (isWin) ? "Complete" : "Fail";
            //TinySauce.OnGameFinished(isWin, Mathf.RoundToInt(resultTime), "Level_" + _gameVariables.LevelNumber);
            
            //FunGamesAnalytics.NewProgressionEvent(status, $"Level_" + _gameVariables.LevelNumber,
            //    $"PlayTime_" + Mathf.RoundToInt(resultTime));
        }
    }
}