using Codebase.Core.Level;
using Codebase.Infrastructure.Services.AssetManagement;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Infrastructure.Services.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly int _levelsAmount;

        public LevelFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _levelsAmount = _assetProvider.GetAssetAmount(AssetPath.Levels);
        }

        public void CreateLevel(int levelNumber, Action levelOnReady = null)
        {
            var levels = _assetProvider.GetAllObjects<Level>(AssetPath.Levels);
            string levelPath = AssetPath.Levels + "/";

            if (levelNumber > levels.Length - 1)
                levelPath = levelPath + "Level" + Random.Range(1, _levelsAmount + 1);
            else
                levelPath = levelPath + "Level" + levelNumber;

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