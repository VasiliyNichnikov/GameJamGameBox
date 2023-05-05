using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct Quest
    {
        public int Id { get; set; }
        public string QuestType { get; set; }
        public List<int> RequiredItems { get; set; }
        public int PlotIdAfterComplete { get; set; }
        public QuestSafeExtension? QuestSafeExtension { get; set; }
        public QuestCollectingItemsException? QuestCollectingItemsException { get; set; }
    }
    
    public struct QuestsInGame
    {
        public List<Quest> Quests { get; set; }
    }
}