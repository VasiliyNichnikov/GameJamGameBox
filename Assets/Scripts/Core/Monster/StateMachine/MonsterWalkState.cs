using Core.StateMachineLogic;

namespace Core.Monster.StateMachine
{
    public class MonsterWalkState : State<MonsterStateMachine>
    {
        private readonly IMonsterWalking _walking;
        private readonly IMonsterEars _ears;
        
        public MonsterWalkState(MonsterStateMachine machine, IMonsterWalking walking, IMonsterEars ears) : base(machine)
        {
            _walking = walking;
            _ears = ears;
        }

        public override void Enter()
        {

        }

        public override void LogicUpdate()
        {
            if (_walking.IsPlayerVisible)
            {
                Machine.ChangeState(Machine.RunState);
                return;
            }
            
            if (_walking.IsMovement)
            {
                return;
            }
            
            if (_ears.SoundDetected())
            {
                _walking.GoForSound();
            }
            else
            {
                _walking.SearchPlayer();
            }
        }

        public override void Exit()
        {
            
        }
    }
}