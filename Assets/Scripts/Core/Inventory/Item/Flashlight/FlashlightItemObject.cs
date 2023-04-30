namespace Core.Inventory.Item.Flashlight
{
    public class FlashlightItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.Flashlight;

        public override void Dispose()
        {
            // nothing
        }
    }
}