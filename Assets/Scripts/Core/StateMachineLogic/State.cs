namespace Core.StateMachineLogic
{
    public abstract class State<T> where T: StateMachine<T>
    {
        protected T Machine;

        protected State(T machine)
        {
            Machine = machine;
        }
        
        public abstract void Enter();
        public abstract void LogicUpdate();
        public abstract void Exit();
    }
}