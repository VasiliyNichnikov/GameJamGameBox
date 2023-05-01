using System.Collections.Generic;
using Loaders;
using Loaders.Data.Ready;
using Loaders.DataRaw;

namespace DataHelpers
{
    public class MapDataHelper
    {
        public IReadOnlyCollection<ItemData> Items => _items.AsReadOnly();

        private const string ItemsOnMapFile = "ItemsOnMap";
        private readonly List<ItemData> _items = new List<ItemData>();

        public void Load()
        {
            var data = StaticLoader.LoadData<ItemsOnMap>(ItemsOnMapFile);
            foreach (var item in data.Items)
            {
                var loadedItem = new ItemData(item);
                _items.Add(loadedItem);
            }
        }
    }
}