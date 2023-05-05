using Core.Quests;

namespace Loaders.Data.Ready
{
    public struct QuestData
    {
        public readonly int Id;
        public readonly QuestType QuestType;
        public readonly int PlotIdAfterComplete;

        public QuestData(int id, QuestType questType, int plotIdAfterComplete)
        {
            Id = id;
            QuestType = questType;
            PlotIdAfterComplete = plotIdAfterComplete;
        }
    }
}