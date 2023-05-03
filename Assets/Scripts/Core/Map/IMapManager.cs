using Core.Inventory.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Map
{
    public interface IMapManager
    {
        void AddItemOnScene(ItemObjectType type, Vector3 position, Quaternion rotation);

        [CanBeNull]
        ObjectForChangesState GetObjectForChanges(string nameObject);
    }
}