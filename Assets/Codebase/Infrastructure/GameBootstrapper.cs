using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtain;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(LoadingCurtain);
            _game = new Game(this, LoadingCurtain, AllServices.Container);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}