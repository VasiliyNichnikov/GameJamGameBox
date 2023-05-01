using System.Collections.Generic;
using Core.TriggerLogic;
using Extensions;
using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;
using Utils;

namespace DataHelpers
{
    public class TriggerDataHelper
    {
        private const string TriggersFile = "Triggers";

        public IReadOnlyCollection<JsonMessage<TriggerData>> Triggers => _triggers;

        private readonly List<JsonMessage<TriggerData>> _triggers = new List<JsonMessage<TriggerData>>();

        public void Load()
        {
            var data = StaticLoader.LoadData<TriggersData>(TriggersFile);
            
            foreach (var trigger in data.Triggers)
            {
                var triggerData = new TriggerData(trigger.Id, trigger.Type.ConvertToEnum<TriggerType>());
                if (trigger.SoundExtension != null)
                {
                    var jsonMessage = ExtensionHelper.GetMessageForSoundExt(triggerData, trigger.SoundExtension.Value.Sound);
                    _triggers.Add(jsonMessage);
                    continue;
                }
            }
        }
    }
}