using System.Collections.Generic;
using Codebase.Core.Level;
using Codebase.Extensions;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories.LevelRepeatingRules
{
    //[CreateAssetMenu(fileName = "RandomLevelsRule", menuName = "LevelRepeatingRules/Random", order = 51)]
    public class RandomLevelsRule : BaseLevelRepeatingRule
    {
        public override Level GetLevel(List<Level> levels)
        {
            return levels.GetRandom();
        }
    }
}