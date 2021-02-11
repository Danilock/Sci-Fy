namespace Game.StateMachine
{
    public interface ISetState<T>
    {
        void SetState(IState<T> newState);
    }
}
