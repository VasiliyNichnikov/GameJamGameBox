namespace Core.Inventory.Item.Bird
{
    public class BirdItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.Bird;
        public override void Dispose()
        {
            // nothing
        }
    }
}