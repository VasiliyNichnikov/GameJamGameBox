using System.Collections.Generic;

namespace Loaders.DataRaw
{
    public struct Quest
    {
        public int Id { get; set; }
        public string QuestType { get; set; }
        public List<int> RequiredItems { get; set; }
        public int PlotIdAfterComplete { get; set; }
    }
    
    public struct QuestsInGame
    {
        public List<Quest> Quests { get; set; }
    }
}