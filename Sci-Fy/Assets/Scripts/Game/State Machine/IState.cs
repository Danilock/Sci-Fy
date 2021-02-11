
namespace Game.StateMachine
{
    public interface IState<T>
    {
        void EnterState(T type);
        void TickState(T type);
        void ExitState(T type);
    }
}