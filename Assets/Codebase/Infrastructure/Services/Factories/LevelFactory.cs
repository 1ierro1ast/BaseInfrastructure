using System;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.DataStorage;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LevelsList _levelsList;

        public LevelFactory(IAssetProvider assetProvider, IGameVariables gameVariables,
            ITemporaryLevelVariables temporaryLevelVariables)
        {
            _assetProvider = assetProvider;
            _levelsList = _assetProvider.GetScriptableObject<LevelsList>(AssetPath.LevelsListsPath);
        }

        public void CreateLevel(int levelNumber, Action levelOnReady = null)
        {
            var levelPrefab = _levelsList.GetById(levelNumber);
            _assetProvider.Instantiate(levelPrefab);
            levelOnReady?.Invoke();
        }

        public void ClearLevel()
        {
            Debug.Log("Clear level");
        }
    }
}