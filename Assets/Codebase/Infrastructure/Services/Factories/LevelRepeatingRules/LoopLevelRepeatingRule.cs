using System.Collections.Generic;
using Codebase.Core.Level;
using Codebase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories.LevelRepeatingRules
{
    //[CreateAssetMenu(fileName = "LoopLevelRepeatingRule", menuName = "LevelRepeatingRules/Loop", order = 0)]
    public class LoopLevelRepeatingRule : BaseLevelRepeatingRule
    {
        public override Level GetLevel(List<Level> levels)
        {
            var saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            var id = saveLoadService.LoadInt("NextLevelForLoading", 0);
            if (id >= levels.Count) id = 0;
            var level = levels[id];
            id++;
            saveLoadService.SaveInt("NextLevelForLoading", id);
            return level;
        }
    }
}