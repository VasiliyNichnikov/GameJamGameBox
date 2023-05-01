using Loaders.Data.Ready;
using Utils;

namespace Core.PlotLogic.PlotAction
{
    public abstract class PlotActionBase
    {
        public abstract void RunAction(JsonMessage<StepData> message);
        public abstract bool IsEventCompleted();
    }
}