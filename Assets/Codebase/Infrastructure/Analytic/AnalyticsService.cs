//using GameAnalyticsSDK;

using System;
using Codebase.Infrastructure.DataStorage;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure.Analytic
{
    public class AnalyticsService : MonoBehaviour
    {
        private static AnalyticsService _instance;
    
        private IGameVariables _gameVariables;
        private DateTime _levelStartTime;
        private int _levelNumber;

        private void Construct()
        {
            //GameAnalytics.Initialize();
            _gameVariables = AllServices.Container.Single<IGameVariables>();
            _gameVariables.ChangeLevelNumberEvent += OnLevelChanged;
        
            LevelStart();
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            
                Construct();
            }
        }

        private void OnDestroy()
        {
            _gameVariables.ChangeLevelNumberEvent -= OnLevelChanged;
        }

        private void OnLevelChanged(int level)
        {
            LevelFinish();
            _levelNumber = level;
            LevelStart();
        }

        private void LevelStart()
        {
            _levelStartTime = DateTime.Now;
            LevelStartEvent(_levelNumber);
        }

        private void LevelFinish()
        {
            var playTimeInSeconds = (_levelStartTime - DateTime.Now).TotalSeconds;
            LevelFinishEvent(_levelNumber, true, playTimeInSeconds);
        }
    
        #region Events
        private void LevelStartEvent(int level)
        {
            // GameAnalytics.NewProgressionEvent (
            //     GAProgressionStatus.Start,
            //      "Level_" + level);
        }
    
        private void LevelFinishEvent(int level, bool isWin, double playTimeInSeconds)
        {
            // var status = (isWin) ? GAProgressionStatus.Complete : GAProgressionStatus.Fail;
            // GameAnalytics.NewProgressionEvent(
            //     status,
            //     "Level_" + level,
            //     "PlayTime_" + (int) playTimeInSeconds);
        }
        
        #endregion
    }
}