using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.Factories
{
    public interface IUiFactory : IService
    {
        StartPopup CreateStartPopup();
        OverlayPopup CreateOverlayPopup();
        WinPopup CreateWinPopup();
        LosePopup CreateLoosePopup();
    }
}