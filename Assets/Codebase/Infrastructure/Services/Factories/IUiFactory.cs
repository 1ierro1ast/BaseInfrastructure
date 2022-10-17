using Codebase.Core.UI.Popups;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface IUiFactory : IService
    {
        StartPopup GetStartPopup();
        OverlayPopup GetOverlayPopup();
        WinPopup GetWinPopup();
        LosePopup GetLosePopup();
    }
}