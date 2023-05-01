using Core.UI.Inventory;
using Loaders;

namespace Core.UI
{
    public class MainDialogsLoader : ILoader
    {
        public InventoryView InventoryView => _inventoryView;

        private InventoryView _inventoryView;

        private readonly DialogManager _manager;

        public MainDialogsLoader(DialogManager manager)
        {
            _manager = manager;
        }
        
        public void Load()
        {
            
            
        }
    }
}