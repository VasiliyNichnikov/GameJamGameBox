using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct Trigger
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public LightTriggerExtension? LightTriggerExtension { get; set; }
    }

    public struct TriggersRaw
    {
        public List<Trigger> Triggers { get; set; }
    }
}