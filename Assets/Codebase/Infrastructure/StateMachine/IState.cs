namespace Codebase.Infrastructure.StateMachine
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IMultiPayloadedState<TPayload1, TPayload2> : IExitableState
    {
        void Enter(TPayload1 payload1, TPayload2 payload2);
    }
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}