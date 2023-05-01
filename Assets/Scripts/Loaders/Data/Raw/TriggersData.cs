using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct Trigger
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public SoundExtension? SoundExtension { get; set; }
    }

    public struct TriggersData
    {
        public List<Trigger> Triggers { get; set; }
    }
}