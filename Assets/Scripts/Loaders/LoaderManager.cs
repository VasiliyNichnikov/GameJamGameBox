using System.Collections.Generic;
using UnityEngine;

namespace Loaders
{
    public class LoaderManager
    {
        private readonly List<ILoader> _loaders = new List<ILoader>();
        private bool _isLoaded;
        
        public void AddLoader(ILoader loader)
        {
            if (_loaders.Contains(loader))
            {
                return;
            }

            _loaders.Add(loader);
        }

        public void StartLoading()
        {
            if (_isLoaded)
            {
                Debug.LogError("LoaderManager. Data is already loaded");
                return;
            }
            
            foreach (var loader in _loaders)
            {
                loader.Load();
            }

            _isLoaded = true;
        }
    }
}