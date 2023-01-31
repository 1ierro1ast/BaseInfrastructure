using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.Services.Factories
{
    public class CanvasBuilder : CanvasBuilderBase
    {
        public CanvasBuilder(IAssetProvider assetProvider) : base(assetProvider)
        {
            CreateCanvas(AssetPath.MainCanvas);
        }

        public override CanvasBuilderBase BuildStartPopup()
        {
            CreatePopup<StartPopup>(AssetPath.StartPopup);
            return this;
        }

        public override CanvasBuilderBase BuildOverlayPopup()
        {
            CreatePopup<OverlayPopup>(AssetPath.OverlayPopup);
            return this;
        }

        public override CanvasBuilderBase BuildLosePopup()
        {
            CreatePopup<LosePopup>(AssetPath.LosePopup);
            return this;
        }

        public override CanvasBuilderBase BuildWinPopup()
        {
            CreatePopup<WinPopup>(AssetPath.WinPopup);
            return this;
        }

        private void CreatePopup<T>(string path) where T : PopupBase
        {
            var popup = assetProvider.Instantiate<T>(path);
            MainCanvas.Add(popup);
        }
    }
}