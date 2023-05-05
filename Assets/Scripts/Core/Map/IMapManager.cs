using Core.Inventory.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Map
{
    public interface IMapManager
    {
        void AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation);
        ItemObjectBase AddItemOnScene(int itemId, Vector3 position, Quaternion rotation);
        ItemObjectBase AddItemOnScene(int itemId, Vector3 position, Vector3 scale, Quaternion rotation, bool ignoreForRaise = false);

        [CanBeNull]
        ObjectForChangesState GetObjectForChanges(string nameObject);
    }
}