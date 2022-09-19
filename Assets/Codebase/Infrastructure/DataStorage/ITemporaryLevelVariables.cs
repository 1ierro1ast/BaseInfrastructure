using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.DataStorage
{
    public interface ITemporaryLevelVariables : IService
    {
        bool IsWin { get; set; }

        void ClearData();
    }
}
