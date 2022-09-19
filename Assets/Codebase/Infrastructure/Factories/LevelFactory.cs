using System;
using Codebase.Core.Level;
using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.DataStorage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Infrastructure.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly int _levelsAmount;

        public LevelFactory(IAssetProvider assetProvider, IGameVariables gameVariables,
            ITemporaryLevelVariables temporaryLevelVariables)
        {
            _assetProvider = assetProvider;
            _levelsAmount = _assetProvider.GetAssetAmount(AssetPath.LevelsPath);
        }

        public void CreateLevel(int levelNumber, Action levelOnReady = null)
        {
            var levels = _assetProvider.GetAllObjects<Level>(AssetPath.LevelsPath);
            string levelPath = AssetPath.LevelsPath + "/";
            if (levelNumber > levels.Length - 1)
            {
                levelPath = levelPath + "Level" + Random.Range(1, _levelsAmount+1);
            }
            else
            {
                levelPath = levelPath + "Level" + levelNumber;
            }
            Debug.Log(levelPath);
            _assetProvider.Instantiate<Level>(levelPath);
            levelOnReady?.Invoke();
        }

        public void ClearLevel()
        {
            Debug.Log("Clear level");
        }
    }
}