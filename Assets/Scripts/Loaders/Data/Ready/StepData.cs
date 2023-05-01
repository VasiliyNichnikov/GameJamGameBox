using Core.PlotLogic;

namespace Loaders.Data.Ready
{
    public struct StepData
    {
        public readonly StepType Type;

        public StepData(StepType type)
        {
            Type = type;
        }
    }
}