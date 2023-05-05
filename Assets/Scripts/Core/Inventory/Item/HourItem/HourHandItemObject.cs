namespace Core.Inventory.Item.HourItem
{
    public class HourHandItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.HourHand;
        public override void Dispose()
        {
            // nothing
        }
    }
}