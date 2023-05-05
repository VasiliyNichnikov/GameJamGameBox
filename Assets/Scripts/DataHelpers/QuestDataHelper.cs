using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Quests;
using Extensions;
using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;
using Loaders.DataRaw;
using Utils;

namespace DataHelpers
{
    public class QuestDataHelper
    {
        public IReadOnlyCollection<JsonMessage<QuestData>> Quests => _quests;

        private const string QuestsFile = "Quests";

        private readonly List<JsonMessage<QuestData>> _quests = new List<JsonMessage<QuestData>>();

        public void Load()
        {
            var data = StaticLoader.LoadData<QuestsInGame>(QuestsFile);
            foreach (var item in data.Quests)
            {
                var quest = new QuestData(item.Id, item.QuestType.ConvertToEnum<QuestType>(), item.PlotIdAfterComplete);

                if (item.QuestCollectingItemsException != null)
                {
                    var extension = ExtensionHelper.GetMessageForCollectingItems(quest, item.QuestCollectingItemsException.Value);
                    _quests.Add(extension);
                    continue;
                }
                if (item.QuestSafeExtension != null)
                {
                    var extension = ExtensionHelper.GetMessageForQuestSafe(quest, item.QuestSafeExtension.Value);
                    _quests.Add(extension);
                    continue;
                }
                _quests.Add(new JsonMessage<QuestData>(quest, null));
            }
        }
    }
}