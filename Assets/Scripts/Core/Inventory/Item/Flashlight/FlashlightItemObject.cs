using UnityEngine;

namespace Core.Inventory.Item.Flashlight
{
    public class FlashlightItemObject : PlotItemObject
    {
        [SerializeField, Header("Плохая реализация. Но Jam). Только для объекта в руке")] private Light _light;
        
        public override ItemObjectType ObjectType => ItemObjectType.Flashlight;

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