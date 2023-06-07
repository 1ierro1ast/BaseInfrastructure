using System.Collections.Generic;
using Codebase.Core.Level;
using Codebase.Utils.StoredData;

namespace Codebase.Infrastructure.Services.Factories.LevelRepeatingRules
{
    //[CreateAssetMenu(fileName = "LoopLevelRepeatingRule", menuName = "LevelRepeatingRules/Loop", order = 0)]
    public class LoopLevelRepeatingRule : BaseLevelRepeatingRule
    {
        public override Level GetLevel(List<Level> levels)
        {
            StoredInt id = new StoredInt("NextLevelForLoading", 0);
            if (id.Value >= levels.Count) id.Value = 0;
            var level = levels[id.Value];
            id.Value++;
            return level;
        }
    }
}