using Core.Inventory;
using Core.Pool;
using Loaders;

namespace Core.Map
{
    public class MapManager : IMapManager
    {
        private readonly MapLoader _loader;
        private readonly ItemObjectPool _pool;

        public MapManager(LoaderManager loader)
        {
            _pool = new ItemObjectPool();
            _loader = new MapLoader(_pool);
            
            // Добавляем в загрузчики
            loader.AddLoader(_loader);
        }
    }
}