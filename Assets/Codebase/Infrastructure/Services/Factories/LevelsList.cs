using System.Collections.Generic;
using Codebase.Core.Level;
using Codebase.Infrastructure.Services.Factories.LevelRepeatingRules;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "LevelsData", order = 51)]
    public class LevelsList : ScriptableObject
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private BaseLevelRepeatingRule _levelRepeatingRule;

        public Level GetById(int id)
        {
            return id < _levels.Count ? _levels[id] : _levelRepeatingRule.GetLevel(_levels);
        }
    }
}