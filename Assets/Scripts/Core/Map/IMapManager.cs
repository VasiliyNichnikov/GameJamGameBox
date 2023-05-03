using Core.Inventory.Item;
using UnityEngine;

namespace Core.Map
{
    public interface IMapManager
    {
        void AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation);
    }
}