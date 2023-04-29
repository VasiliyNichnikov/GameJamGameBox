using Core.Pool;
using UnityEngine;

namespace Core.Inventory.Item
{
    public interface IItemObject : IPoolObject
    {
        ItemObjectType ObjectType { get; }
        void ToTake();
    }
}