namespace Core.Monster.StateMachine
{
    public class MonsterStateMachine : StateMachineLogic.StateMachine<MonsterStateMachine>
    {
        public MonsterRunState RunState { get; private set; }
        public MonsterWalkState WalkingState { get; private set; }

        public bool IsInitialized { get; private set; }
        
        
        public MonsterStateMachine InitWalkState(IMonsterWalking walking, IMonsterEars ears, IMonsterEyes eyes)
        {
            WalkingState = new MonsterWalkState(this, walking, ears, eyes);
            return this;
        }

        public MonsterStateMachine InitRunState(IMonsterWalking walking, IMonsterEyes eyes)
        {
            RunState = new MonsterRunState(this, walking, eyes);
            return this;
        }

        public MonsterStateMachine SetInitialized()
        {
            IsInitialized = true;
            return this;
        }
    }
}