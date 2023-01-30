using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        public LoadingCurtain LoadingCurtain;
        public GameStateMachine StateMachine;

        private void Awake()
        {
            StateMachine = new GameStateMachine(new SceneLoader(), LoadingCurtain, AllServices.Container);
            
            StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(LoadingCurtain);
            DontDestroyOnLoad(this);
        }
    }
}