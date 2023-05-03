using Core.Inventory.Item;
using Core.UI;
using Extensions;
using Loaders.DataRaw;
using UnityEngine;

namespace Loaders.Data.Ready
{
    public struct ItemData
    {
        public readonly int Id;
        public readonly ItemObjectType ObjectType;
        public readonly ItemViewType ViewType;
        public readonly string Title;
        public readonly string Description;
        public readonly string NameIcon;
        public readonly bool AddToHand;
        public readonly Vector3? Position;
        public readonly Quaternion? Rotation;

        public ItemData(Item rawItem)
        {
            Id = rawItem.Id;
            Title = rawItem.Title;
            Description = rawItem.Description;
            NameIcon = rawItem.NameIcon;
            AddToHand = rawItem.AddToHand;
            ObjectType = rawItem.Type.ConvertToEnum<ItemObjectType>();
            ViewType = ItemViewType.Default;
            if (rawItem.Position == null)
            {
                Position = null;
            }
            else
            {
                Position = rawItem.Position.ConvertToVector3();
            }

            if (rawItem.Rotation == null)
            {
                Rotation = null;
            }
            else
            {
                Rotation = rawItem.Rotation.ConvertToRotation();
            }
        }
    }
}