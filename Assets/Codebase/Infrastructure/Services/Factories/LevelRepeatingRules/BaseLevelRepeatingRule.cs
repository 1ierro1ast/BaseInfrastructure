using System.Collections.Generic;
using Codebase.Core.Level;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories.LevelRepeatingRules
{
    public abstract class BaseLevelRepeatingRule : ScriptableObject
    {

        public abstract Level GetLevel(List<Level> levels);
    }
}