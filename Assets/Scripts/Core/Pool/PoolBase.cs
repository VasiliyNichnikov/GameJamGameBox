using System;
using System.Collections.Generic;

namespace Core.Pool
{
    public abstract class PoolBase<TType, TPoolObject> where TType : Enum where TPoolObject : IPoolObject
    {
        protected readonly List<TPoolObject> UsedObjects = new List<TPoolObject>();
        protected readonly List<TPoolObject> UnusedObjects = new List<TPoolObject>();

        public int ObjectsInUseCount => UsedObjects.Count;
        public int UnusedObjectsCount => UnusedObjects.Count;
        public int AllCount => ObjectsInUseCount + UnusedObjectsCount;


        public abstract TPoolObject GetOrCreateObject(TType typeObject);

        public void HideObject(TPoolObject obj)
        {
            if (UsedObjects.Contains(obj))
            {
                UsedObjects.Remove(obj);
                obj.Hide();
                UnusedObjects.Add(obj);
            }
        }

        public void HideAll()
        {
            foreach (var obj in UsedObjects)
            {
                obj.Hide();
                UnusedObjects.Add(obj);
            }

            UsedObjects.Clear();
        }

        public void ClearAll()
        {
            foreach (var obj in UnusedObjects)
            {
                obj.Die();
            }

            UnusedObjects.Clear();

            foreach (var obj in UsedObjects)
            {
                obj.Die();
            }

            UsedObjects.Clear();
        }
    }
}