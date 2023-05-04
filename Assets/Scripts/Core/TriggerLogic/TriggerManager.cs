using Loaders;

namespace Core.TriggerLogic
{
    public class TriggerManager : ILoader
    {
        private readonly TriggerSystem _system;

        public TriggerManager()
        {
            _system = new TriggerSystem();
        }

        public void Update()
        {
            _system.CheckTriggers();
        }
        
        public void LoadAwake()
        {
            var data = Main.Instance.Data.TriggerHelper;
            foreach (var trigger in data.Triggers)
            {
                TriggerSystem.GetAndAddTrigger(_system, trigger);
            }
        }

        public void LoadStart()
        {
            // nothing
        }
    }
}