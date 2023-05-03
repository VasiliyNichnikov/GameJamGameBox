using System;
using System.Collections.Generic;

namespace Loaders.DataRaw
{
    [Serializable]
    public class Item
    {
        public int Id;
        public string Type;
        public string Title;
        public string Description;
        public string NameIcon;
        public bool AddToHand;
        public List<float> Position;
        public List<float> Rotation;
    }
    
    [Serializable]
    public class ItemsOnMap
    {
        public List<Item> Items;
    }
}