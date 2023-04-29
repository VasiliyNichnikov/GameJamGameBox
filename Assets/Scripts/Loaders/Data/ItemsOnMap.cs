using System;
using System.Collections.Generic;

namespace Loaders.Data
{
    [Serializable]
    public class Item
    {
        public string Type;
        public string Title;
        public string Description;
        public string NameIcon;
        public List<float> Position;
        public List<float> Rotation;
    }
    
    [Serializable]
    public class ItemsOnMap
    {
        public List<Item> Items;
    }
}