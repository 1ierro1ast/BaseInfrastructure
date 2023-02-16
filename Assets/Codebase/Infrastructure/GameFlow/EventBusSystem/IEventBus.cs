using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow.EventBusSystem
{
    public interface IEventBus : IService
    {
        void Subscribe<T>(Action callback) where T : IEvent;
        void Unsubscribe<T>(Action callback) where T : IEvent;
        void Fire<T>() where T : IEvent;
    }
}