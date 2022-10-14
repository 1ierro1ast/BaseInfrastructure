using System;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface ILevelFactory : IService
    {
        void ClearLevel();
        void CreateLevel(int gameVariablesLevelNumber, Action levelOnReady = null);
    }
}