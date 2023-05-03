namespace Core.Inventory.Item.Knife
{
    public class KnifeItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.Knife;
        public override void Dispose()
        {
            // nothing
        }
    }
}