using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public abstract class CanvasBuilderBase
    {
        protected readonly IAssetProvider assetProvider;

        public CanvasService MainCanvas { get; private set; }

        public CanvasBuilderBase(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        protected void CreateCanvas(string path)
        {
            var canvas = assetProvider.Instantiate<Canvas>(path);
            MainCanvas = new(canvas);

            Object.DontDestroyOnLoad(canvas);
        }

        public abstract CanvasBuilderBase BuildStartPopup();

        public abstract CanvasBuilderBase BuildOverlayPopup();

        public abstract CanvasBuilderBase BuildWinPopup();

        public abstract CanvasBuilderBase BuildLosePopup();

        public abstract CanvasBuilderBase BuildShopPopup();
    }
}