using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Services.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssetProvider _assetProvider;

        private StartPopup _startPopup;
        private OverlayPopup _overlayPopup;
        private WinPopup _winPopup;
        private LosePopup _losePopup;
        private Canvas _mainCanvas;

        [Inject]
        public UiFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            InitializePopups();
        }

        private void InitializePopups()
        {
            CreateMainCanvas();
            GetStartPopup();
            GetOverlayPopup();
            GetWinPopup();
            GetLosePopup();
        }

        private void CreateMainCanvas()
        {
            if (_mainCanvas == null)
                _mainCanvas = _assetProvider.Instantiate<Canvas>(AssetPath.MainCanvasPath);
            Object.DontDestroyOnLoad(_mainCanvas);
        }

        public StartPopup GetStartPopup()
        {
            if (_startPopup == null)
            {
                _startPopup = InitializePopup<StartPopup>(AssetPath.StartPopupPath);
            }
            return _startPopup;
        }

        public OverlayPopup GetOverlayPopup()
        {
            if (_overlayPopup == null)
            {
                _overlayPopup = InitializePopup<OverlayPopup>(AssetPath.OverlayPopupPath);
            }
            return _overlayPopup;
        }

        public WinPopup GetWinPopup()
        {
            if (_winPopup == null)
            {
                _winPopup = InitializePopup<WinPopup>(AssetPath.WinPopupPath);
            }
            return _winPopup;
        }

        public LosePopup GetLosePopup()
        {
            if (_losePopup == null)
            {
                _losePopup = InitializePopup<LosePopup>(AssetPath.LosePopupPath);
            }
            return _losePopup;
        }

        public TPopup InitializePopup<TPopup>(string path) where TPopup : BasePopup
        {
            var popup = _assetProvider.Instantiate<TPopup>(path);
            
            AddToMainCanvas(popup);

            return popup;
        }

        private void AddToMainCanvas(BasePopup basePopup)
        {
            basePopup.transform.SetParent(_mainCanvas.transform);
            basePopup.GetComponent<Canvas>().overrideSorting = true;
        }
    }
}