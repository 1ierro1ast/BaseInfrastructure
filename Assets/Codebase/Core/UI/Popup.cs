using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI
{
    public abstract class Popup : MonoBehaviour
    {
        [Header("Open/Close Settings")]
        [SerializeField]
        private GameObject _body;

        [SerializeField] private BasePopupAnimation[] _popupAnimations;
        [SerializeField] private Button _closePopupButton;
        [SerializeField] private Button _secondClosePopupButton;
        private bool _isOpen;

        public bool IsOpen => _isOpen;

        private void Awake()
        {
            _isOpen = false;
            _body.SetActive(false);

            _closePopupButton?.onClick.AddListener(OnClosePopupButtonClick);
            _secondClosePopupButton?.onClick.AddListener(OnClosePopupButtonClick);

            if (_popupAnimations.Length == 0)
                Debug.LogWarning("Animation list is empty!");

            OnInitialization();
        }

        public void OpenPopup()
        {
            if (_isOpen) return;

            _isOpen = true;
            gameObject.SetActive(true);

            if (!gameObject.activeInHierarchy)
                return;

            OnOpenPopup();
            PlayAnimation(true);
        }

        public void ClosePopup()
        {
            if (!_isOpen) return;

            _isOpen = false;

            if (!gameObject.activeInHierarchy)
                return;

            OnClosePopup();
            PlayAnimation(false);
        }

        #region Callbacks

        protected virtual void OnInitialization()
        {
        }

        protected virtual void OnOpenPopup()
        {
        }

        protected virtual void OnClosePopup()
        {
        }

        #endregion Callbacks

        private void PlayAnimation(bool isOpen)
        {
            for (int i = 0; i < _popupAnimations.Length; i++)
                _popupAnimations[i].SetOpenFlag(isOpen);
        }

        private void OnClosePopupButtonClick()
        {
            ClosePopup();
        }
    }
}