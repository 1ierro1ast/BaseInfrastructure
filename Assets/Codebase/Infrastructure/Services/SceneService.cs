using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.SaveLoad;
using Codebase.Infrastructure.Services.Settings;

namespace Codebase.Infrastructure.Services
{
    public class SceneService : ISceneService, ISaveLoad
    {
        private readonly SceneSettings[] _sceneSettings;
        private int _sceneIndex;

        public SceneService(IAssetProvider assetProvider, ISaveLoadService saveLoadService, string path)
        {
            _sceneSettings = assetProvider.GetAllScriptableObjects<SceneSettings>(path);
            saveLoadService.Register(this);
            Load();
        }

        public SceneSettings GetCurrentSceneSettings()
        {
            var count = _sceneSettings.Length;
            for (int i = 0; i < count; i++)
            {
                var settings = _sceneSettings[i];
                if (!settings.IsSelected)
                    continue;

                _sceneIndex = i;
                return settings;
            }

            _sceneIndex = 0;
            return _sceneSettings[_sceneIndex];
        }

        public void SetNextScene()
        {
            _sceneSettings[_sceneIndex].IsSelected = false;

            _sceneIndex++;
            _sceneIndex = _sceneIndex >= _sceneSettings.Length ? 0 : _sceneIndex;

            _sceneSettings[_sceneIndex].IsSelected = true;
        }

        public void Load()
        {
            foreach (var item in _sceneSettings)
                item.Load();
        }

        public void Save()
        {
            foreach (var item in _sceneSettings)
                item.Save();
        }
    }
}