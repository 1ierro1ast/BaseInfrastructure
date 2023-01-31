using Codebase.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase.Core.UI.Popups
{
    public class CanvasService : IService
    {
        private readonly Canvas _canvas;
        private readonly List<PopupBase> _popups = new();

        public CanvasService(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Add(PopupBase popup)
        {
            _popups.Add(popup);

            popup.transform.SetParent(_canvas.transform);
            popup.GetComponent<Canvas>().overrideSorting = true;
        }

        public T GetPopup<T>() where T : PopupBase
        {
            return _popups.FirstOrDefault(p => p is T) as T;
        }
    }
}