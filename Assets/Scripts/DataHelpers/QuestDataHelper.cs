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
                var selectedItems = GetItems(item.RequiredItems);
                var quest = new QuestData(item.Id, item.QuestType.ConvertToEnum<QuestType>(), item.PlotIdAfterComplete, selectedItems);
                
                if (item.QuestSafeExtension != null)
                {
                    var startValue = item.QuestSafeExtension.Value.StartValue;
                    var rightAnswer = item.QuestSafeExtension.Value.RightAnswer;
                    var extension = ExtensionHelper.GetMessageForQuestSafe(quest, startValue, rightAnswer);
                    _quests.Add(extension);
                    continue;
                }
                _quests.Add(new JsonMessage<QuestData>(quest, null));
            }
        }

        private List<ItemData> GetItems(IReadOnlyCollection<int> itemIds)
        {
            var selectedItems = new List<ItemData>();
            var items = Main.Instance.Data.MapHelper.Items;
            foreach (var item in items)
            {
                selectedItems.AddRange(from id in itemIds where item.Id == id select item);
            }

            return selectedItems;
        }
    }
}