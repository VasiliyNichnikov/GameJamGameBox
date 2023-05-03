using UnityEngine;

namespace Core.Inventory.Item.Flashlight
{
    public class FlashlightItemObject : PlotItemObject
    {
        [SerializeField, Header("Плохая реализация. Но Jam). Только для объекта в руке")] private Light _light;
        
        public override ItemObjectType ObjectType => ItemObjectType.Flashlight;

        private void Start()
        {
            if (_light != null)
            {
                _light.enabled = false;
            }
        }

        public override void AddToHand()
        {
            _light.enabled = true;
        }

        public override void Dispose()
        {
            // nothing
        }
    }
}