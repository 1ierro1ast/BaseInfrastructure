using System;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class LosePopup : PopupBase
    {
        [SerializeField] private Button _retryButton;

        public event Action OnRetryLevel;

        protected override void OnInitialization()
        {
            base.OnInitialization();
            _retryButton.onClick.AddListener(OnRetryButton);
        }

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _retryButton.interactable = true;
        }

        private void OnRetryButton()
        {
            _retryButton.interactable = false;
            BroadcastRetryLevel();
        }

        public void BroadcastRetryLevel()
        {
            OnRetryLevel?.Invoke();
        }
    }
}