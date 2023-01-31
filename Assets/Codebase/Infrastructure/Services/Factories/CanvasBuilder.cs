using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.Services.Factories
{
    public class CanvasBuilder : CanvasBuilderBase
    {
        public CanvasBuilder(IAssetProvider assetProvider) : base(assetProvider)
        {
            CreateCanvas(UIPath.MainCanvas);
        }

        public override CanvasBuilderBase BuildStartPopup()
        {
            CreatePopup<StartPopup>(UIPath.StartPopup);
            return this;
        }

        public override CanvasBuilderBase BuildOverlayPopup()
        {
            CreatePopup<OverlayPopup>(UIPath.OverlayPopup);
            return this;
        }

        public override CanvasBuilderBase BuildLosePopup()
        {
            CreatePopup<LosePopup>(UIPath.LosePopup);
            return this;
        }

        public override CanvasBuilderBase BuildWinPopup()
        {
            CreatePopup<WinPopup>(UIPath.WinPopup);
            return this;
        }

        public override CanvasBuilderBase BuildShopPopup()
        {
            CreatePopup<ShopPopup>(UIPath.ShopPopup);
            return this;
        }

        private void CreatePopup<T>(string path) where T : PopupBase
        {
            var popup = assetProvider.Instantiate<T>(path);
            MainCanvas.Add(popup);
        }
    }
}