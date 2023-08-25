namespace Infrastructure
{
    public interface IExitableState
    {
        public void Exit();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }

    public interface IState : IExitableState
    {
        public void Enter();
    }
}
