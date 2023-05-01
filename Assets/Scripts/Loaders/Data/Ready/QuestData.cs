using System.Collections.Generic;
using Core.Quests;

namespace Loaders.Data.Ready
{
    public struct QuestData
    {
        public readonly int Id;
        public readonly QuestType QuestType;
        public readonly List<ItemData> RequiredItems;
        public readonly int PlotIdAfterComplete;

        public QuestData(int id, QuestType questType, int plotIdAfterComplete, List<ItemData> requiredItems)
        {
            Id = id;
            QuestType = questType;
            RequiredItems = requiredItems;
            PlotIdAfterComplete = plotIdAfterComplete;
        }
    }
}