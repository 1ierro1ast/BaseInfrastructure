using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.Factories
{
    public interface ILevelFactory : IService
    {
        void ClearLevel();
        void CreateLevel(int gameVariablesLevelNumber, Action levelOnReady = null);
    }
}