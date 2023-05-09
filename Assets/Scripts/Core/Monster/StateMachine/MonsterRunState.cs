using Core.StateMachineLogic;

namespace Core.Monster.StateMachine
{
    public class MonsterRunState : State<MonsterStateMachine>
    {
        private readonly IMonsterWalking _walking;
        private readonly IMonsterEars _ears;
        
        public MonsterRunState(MonsterStateMachine machine, IMonsterWalking walking, IMonsterEars ears) : base(machine)
        {
            _walking = walking;
            _ears = ears;
        }

        public override void Enter()
        {
            // Запускаем звук погони за игроком
            Main.Instance.SoundManager.MeetingWithEnemy();
        }

        public override void LogicUpdate()
        {
            if (_walking.IsPlayerVisible)
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