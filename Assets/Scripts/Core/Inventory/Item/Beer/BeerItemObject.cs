namespace Core.Inventory.Item.Beer
{
    public class BeerItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.Beer;
        public override void Dispose()
        {
            // nothing
        }
    }
}