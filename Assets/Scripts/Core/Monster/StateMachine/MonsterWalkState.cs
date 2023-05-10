using Core.StateMachineLogic;
using UnityEngine;

namespace Core.Monster.StateMachine
{
    public class MonsterWalkState : State<MonsterStateMachine>
    {
        private readonly IMonsterWalking _walking;
        private readonly IMonsterEyes _eyes;
        private readonly IMonsterEars _ears;

        public MonsterWalkState(MonsterStateMachine machine, IMonsterWalking walking, IMonsterEars ears, IMonsterEyes eyes) : base(machine)
        {
            _walking = walking;
            _ears = ears;
            _eyes = eyes;
        }

        public override void Enter()
        {
            _walking.SetSpeedMovement(SpeedType.Walk);
        }

        public override void LogicUpdate()
        {
            if (_eyes.IsPlayerVisible)
            {
                Machine.ChangeState(Machine.RunState);
                return;
            }
            
            // Упрощение игры
            // if (_walking.IsMovement)
            // {
            //     return;
            // }
            
            if (_ears.SoundDetected())
            {
                _walking.GoForSound(_ears.LoudestPoint);
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