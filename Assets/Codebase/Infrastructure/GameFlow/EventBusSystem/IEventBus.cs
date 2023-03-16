using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow.EventBusSystem
{
    public interface IEventBus : IService
    {
        public void Subscribe<T>(Action<T> handler) where T : IEvent;
        public void Subscribe<T>(Action handler) where T : IEvent;
        public void Unsubscribe<T>(Action<T> handler) where T : IEvent;
        public void Unsubscribe<T>(Action handler) where T : IEvent;
        public void Fire<T>() where T : IEvent;
        public void Fire(IEvent eventToFire);
    }
}