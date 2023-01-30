using Codebase.Infrastructure.Services.Settings;

namespace Codebase.Infrastructure.Services
{
    public interface ISceneService : IService
    {
        SceneSettings GetCurrentSceneSettings();

        void SetNextScene();
    }
}