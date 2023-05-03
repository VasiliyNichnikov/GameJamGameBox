namespace Core.Inventory.Item.ToiletPaper
{
    public class ToiletPaperItemObject : PlotItemObject
    {
        public override ItemObjectType ObjectType => ItemObjectType.ToiletPaper;
        public override void Dispose()
        {
            // nothing
        }
    }
}