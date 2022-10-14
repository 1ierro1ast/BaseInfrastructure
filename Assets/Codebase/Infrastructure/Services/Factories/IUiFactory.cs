using Codebase.Core.UI.Popups;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface IUiFactory : IService
    {
        StartPopup CreateStartPopup();
        OverlayPopup CreateOverlayPopup();
        WinPopup CreateWinPopup();
        LosePopup CreateLoosePopup();
    }
}