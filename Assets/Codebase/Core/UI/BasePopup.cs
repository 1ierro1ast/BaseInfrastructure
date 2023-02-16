using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI
{
    public abstract class BasePopup : MonoBehaviour
    {

        [Header("Open/Close Settings")] [SerializeField]
        private GameObject _body;

        [SerializeField] private PopupAnimator[] _popupAnimators;
        [SerializeField] private Button _closePopupButton;
        [SerializeField] private Button _secondClosePopupButton;
        [SerializeField] private bool _isOpen;

        public bool IsOpen => _isOpen;

        private void Awake()
        {
            _body.SetActive(_isOpen);

            _closePopupButton?.onClick.AddListener(OnClosePopupButtonClick);
            _secondClosePopupButton?.onClick.AddListener(OnClosePopupButtonClick);

            if (_popupAnimators.Length == 0)
                Debug.LogWarning("Animation list is empty!");

            OnInitialization();
        }

        public void OpenPopup()
        {
            if (_isOpen) return;

            if (!gameObject.activeInHierarchy)
                return;

            _isOpen = true;
            gameObject.SetActive(true);

            OnOpenPopup();
            PlayAnimation(_isOpen);
        }

        public void ClosePopup()
        {
            if (!_isOpen) return;

            if (!gameObject.activeInHierarchy)
                return;

            _isOpen = false;

            OnClosePopup();
            PlayAnimation(_isOpen);
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
            for (int i = 0; i < _popupAnimators.Length; i++)
                _popupAnimators[i].SetOpenFlag(isOpen);
        }

        private void OnClosePopupButtonClick()
        {
            ClosePopup();
        }
    }
}