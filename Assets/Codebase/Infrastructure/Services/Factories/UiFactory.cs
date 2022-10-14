using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public class UiFactory : IUiFactory
    {
        private IAssetProvider _assetProvider;

        private StartPopup _startPopup;
        private OverlayPopup _overlayPopup;
        private WinPopup _winPopup;
        private LosePopup _losePopup;

        public UiFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            InitializePopups();
        }

        private void InitializePopups()
        {
            CreateStartPopup();
            CreateOverlayPopup();
            CreateWinPopup();
            CreateLoosePopup();
        }
        public StartPopup CreateStartPopup()
        {
            if (_startPopup == null)
            {
                _startPopup = _assetProvider.Instantiate<StartPopup>(AssetPath.StartPopupPath);
                Object.DontDestroyOnLoad(_startPopup);
            }
            return _startPopup;
        }

        public OverlayPopup CreateOverlayPopup()
        {
            if (_overlayPopup == null)
            {
                _overlayPopup = _assetProvider.Instantiate<OverlayPopup>(AssetPath.OverlayPopupPath);
                Object.DontDestroyOnLoad(_overlayPopup);
            }
            return _overlayPopup;
        }

        public WinPopup CreateWinPopup()
        {
            if (_winPopup == null)
            {
                _winPopup = _assetProvider.Instantiate<WinPopup>(AssetPath.WinPopupPath);
                Object.DontDestroyOnLoad(_winPopup);
            }
            return _winPopup;
        }

        public LosePopup CreateLoosePopup()
        {
            if (_losePopup == null)
            {
                _losePopup = _assetProvider.Instantiate<LosePopup>(AssetPath.LosePopupPath);
                Object.DontDestroyOnLoad(_losePopup);
            }
            return _losePopup;
        }
    }
}