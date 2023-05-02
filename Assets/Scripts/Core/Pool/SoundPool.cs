using System.Linq;
using Core.SoundLogic;
using UnityEngine;

namespace Core.Pool
{
    public class SoundPool : PoolBase<SoundType, SoundItem>
    {
        private readonly Transform _parent;
        
        public SoundPool(Transform parent)
        {
            _parent = parent;
        }
        
        public override SoundItem GetOrCreateObject(SoundType soundType)
        {
            var selectedSound = UnusedObjects.FirstOrDefault(item => item.SoundType == soundType);
            if (selectedSound != null)
            {
                UnusedObjects.Remove(selectedSound);
                UsedObjects.Add(selectedSound);
                return selectedSound;
            }


            var prefab = Main.Instance.SoundStorage.GetSoundItemByType(soundType);
            if (prefab == null)
            {
                Debug.LogError($"SoundPool. Not created sound: {soundType}");
                return null;
            }

            var createdSound = Object.Instantiate(prefab, _parent, false);
            UsedObjects.Add(createdSound);
            return createdSound;
        }
    }
}