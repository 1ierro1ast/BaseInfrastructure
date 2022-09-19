using Codebase.Infrastructure.Services;

namespace Codebase.Core.Analytics
{
    public interface IAnalyticsModule : IService
    {
        public void LevelStart();
        public void LevelFinish(bool isWin);
        
    }
}