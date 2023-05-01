using System;
using System.Collections.Generic;
using Core.UI.Inventory;
using Core.UI.Quests;
using JetBrains.Annotations;
using Loaders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.UI
{
    public static class DialogFactoryUI
    {
        private static readonly Dictionary<Type, string> _dialogs = new Dictionary<Type, string>
        {
            { typeof(InventoryView), "Inventory/InventoryView" },
            { typeof(HintCollectorView), "HintCollector" },
            { typeof(MessagePersonView), "Quest/MessagePersonView" }
        };


        [CanBeNull]
        public static T GetDialog<T>() where T : DialogBase
        {
            if (!_dialogs.ContainsKey(typeof(T)))
            {
                Debug.LogError($"GetDialog. Not found dialog: {typeof(T)}");
                return null;
            }

            var path = _dialogs[typeof(T)];
            var prefab = StaticLoader.LoadDialog<T>(path);
            var dialog = Object.Instantiate(prefab);
            return dialog;
        }
    }
}