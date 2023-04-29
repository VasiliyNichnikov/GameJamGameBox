namespace Core.Inventory.Item
{
    public class KeyItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.Key;
        public override void Dispose()
        {
            // nothing
        }
    }
}