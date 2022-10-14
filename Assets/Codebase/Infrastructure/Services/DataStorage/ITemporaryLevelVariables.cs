namespace Codebase.Infrastructure.Services.DataStorage
{
    public interface ITemporaryLevelVariables : IService
    {
        bool IsWin { get; set; }

        void ClearData();
    }
}
