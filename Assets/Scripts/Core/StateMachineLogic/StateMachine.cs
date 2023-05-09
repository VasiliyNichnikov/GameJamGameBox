namespace Core.StateMachineLogic
{
    public abstract class StateMachine<T> where T : StateMachine<T>
    {
        public State<T> CurrentState { get; private set; }

        
        public void InitStartingState(State<T> startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(State<T> newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}