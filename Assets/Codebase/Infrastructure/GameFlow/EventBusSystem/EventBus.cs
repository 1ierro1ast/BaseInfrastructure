using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure.GameFlow.EventBusSystem
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();
        
        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }

            _subscribers[eventType].Add(handler);
        }

        public void Subscribe<T>(Action handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }

            _subscribers[eventType].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType)) return;

            _subscribers[eventType].Remove(handler);
        }

        public void Unsubscribe<T>(Action handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType)) return;

            _subscribers[eventType].Remove(handler);
        }

        public void Fire<T>() where T : IEvent
        {
            IEvent instance = Activator.CreateInstance<T>();
            
            var eventType = instance.GetType();
            
            if (!_subscribers.ContainsKey(eventType)) return;

            var handlers = _subscribers[eventType].ToArray();
            foreach (var handler in handlers)
            {
                handler.DynamicInvoke();
            }
        }
        
        public void Fire(IEvent eventToFire)
        {
            var eventType = eventToFire.GetType();
            
            if (!_subscribers.ContainsKey(eventType)) return;

            var handlers = _subscribers[eventType].ToArray();
            foreach (var handler in handlers)
            {
                handler.DynamicInvoke(eventToFire);
            }
        }
    }
}