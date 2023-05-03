using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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
        [CanBeNull] public List<float> Position;
        [CanBeNull] public List<float> Rotation;
    }
    
    [Serializable]
    public class ItemsOnMap
    {
        public List<Item> Items;
    }
}