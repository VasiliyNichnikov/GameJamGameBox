﻿using System.Collections.Generic;
using Loaders;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Прогружаем всю игру и UI
    /// </summary>
    public class LoaderGameSceneAndUI
    {
        private bool _isLoaded;
        private readonly List<ILoader> _loaders = new List<ILoader>();
        
        public LoaderGameSceneAndUI AddLoader(ILoader loader)
        {
            if (_loaders.Contains(loader))
            {
                return this;
            }

            _loaders.Add(loader);
            return this;
        }

        public void StartLoadingOnAwake()
        {
            if (_isLoaded)
            {
                Debug.LogWarning("LoaderGameScene. Loaders is completed");
                return;
            }
            
            foreach (var loader in _loaders)
            {
                loader.LoadAwake();
            }
        }

        public void StartLoadingOnStart()
        {
            if (_isLoaded)
            {
                Debug.LogWarning("LoaderGameScene. Loaders is completed");
                return;
            }
            
            foreach (var loader in _loaders)
            {
                loader.LoadStart();
            }
            
            _isLoaded = true;
            _loaders.Clear();
        }
    }
}