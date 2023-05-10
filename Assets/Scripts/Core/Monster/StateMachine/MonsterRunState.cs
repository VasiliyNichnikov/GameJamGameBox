using Core.StateMachineLogic;

namespace Core.Monster.StateMachine
{
    public class MonsterRunState : State<MonsterStateMachine>
    {
        private readonly IMonsterWalking _walking;
        private readonly IMonsterEyes _eyes;
        
        public MonsterRunState(MonsterStateMachine machine, IMonsterWalking walking,  IMonsterEyes eyes) : base(machine)
        {
            _walking = walking;
            _eyes = eyes;
        }

        public override void Enter()
        {
            // Запускаем звук погони за игроком
            Main.Instance.SoundManager.MeetingWithEnemy();
            _walking.SetSpeedMovement(SpeedType.Run);
        }

        public override void LogicUpdate()
        {
            if (_eyes.IsPlayerVisible || _walking.IsPlayerNearby)
            {
                _walking.RunAfterPlayer();
                return;
            }

            Machine.ChangeState(Machine.WalkingState);
        }

        public override void Exit()
        {
            // Отключаем звук погони за игроком
            Main.Instance.SoundManager.EscapeFromEnemy();
        }
    }
}