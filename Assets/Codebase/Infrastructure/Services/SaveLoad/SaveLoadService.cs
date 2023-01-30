using System.Collections.Generic;

namespace Codebase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly HashSet<ISaveLoad> _variables = new();

        public void Register(ISaveLoad variable)
        {
            _variables.Add(variable);
        }

        public void Load()
        {
            foreach (var variable in _variables)
                variable.Load();
        }

        public void Save()
        {
            foreach (var variable in _variables)
                variable.Save();
        }
    }
}