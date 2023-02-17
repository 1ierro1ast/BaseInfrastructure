using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure.GameFlow.EventBusSystem
{
    public class EventBus : IEventBus
    {
        private Dictionary<Type, List<Action>> _subscribers;
        private bool _firing;
        private List<int> _idsForRemove;

        public EventBus()
        {
            _subscribers = new Dictionary<Type, List<Action>>();
            _idsForRemove = new List<int>(100);
        }

        public void Subscribe<T>(Action callback) where T : IEvent
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                _subscribers[typeof(T)].Add(callback);
            }
            else
            {
                _subscribers.Add(typeof(T), new List<Action>{callback});
            }
        }

        public void Unsubscribe<T>(Action callback) where T : IEvent
        {
            if (!_subscribers.ContainsKey(typeof(T))) return;

            var subscribedMethods = _subscribers[typeof(T)];

            if (!subscribedMethods.Contains(callback)) return;

            if (!_firing) subscribedMethods.Remove(callback);
            else
            {
                subscribedMethods[subscribedMethods.IndexOf(callback)] = null;
            }
        }

        public void Fire<T>() where T : IEvent
        {
            if (!_subscribers.ContainsKey(typeof(T))) return;
            _firing = true;
            var actionsCache = new List<Action>();

            actionsCache.AddRange(_subscribers[typeof(T)]);

            foreach (var item in actionsCache)
            {
                if (item == null)
                {
                    _idsForRemove.Add(actionsCache.IndexOf(item));
                    continue;
                }

                item.Invoke();
            }

            _firing = false;
            
            foreach (var id in _idsForRemove)
            {
                _subscribers[typeof(T)].RemoveAt(id);
            }

            _idsForRemove.Clear();
        }
    }
}