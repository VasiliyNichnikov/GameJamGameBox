using Core.TriggerLogic;

namespace Loaders.Data.Ready
{
    public struct TriggerData
    {
        public readonly int Id;
        public readonly TriggerType Type;

        public TriggerData(int id, TriggerType type)
        {
            Id = id;
            Type = type;
        }
    }
}