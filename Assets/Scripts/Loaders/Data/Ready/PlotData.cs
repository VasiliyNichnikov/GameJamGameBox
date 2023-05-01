using System.Collections.Generic;
using Utils;

namespace Loaders.Data.Ready
{
    public struct PlotData
    {
        public readonly int Id;
        public readonly List<JsonMessage<StepData>> Steps;

        public PlotData(int id, List<JsonMessage<StepData>> steps)
        {
            Id = id;
            Steps = steps;
        }
    }
}