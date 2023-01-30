using Codebase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Settings
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "Create scene settings", order = 51)]
    public class SceneSettings : ScriptableObject, ISaveLoad
    {
        [SerializeField] private int _id;
        [SerializeField] private string _scene;
        [SerializeField] private bool _isSelected;
        [SerializeField] private bool _isSelectedDefault;
        private readonly string _selectKey = "sceneSelect";

        public string SceneName => _scene;
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }

        public void Load()
        {
            _isSelected = PlayerPrefs.GetInt(_selectKey + _id, _isSelectedDefault ? 1 : 0) > 0;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_selectKey + _id, _isSelected ? 1 : 0);
        }
    }
}