using System;
using System.Collections.Generic;
using Core.Inventory.Builders;
using Core.Inventory.Data;
using Core.Inventory.View;
using UnityEngine;

namespace Core.Inventory
{
    public class TestInitInventory : MonoBehaviour
    {
        [SerializeField] private InventoryView _view;

        private void Start()
        {
            var data = new List<IItemData>()
            {
                new ItemKeyData(),
                new ItemFlashlightData()
            };
            _view.Refresh(data.AsReadOnly());
        }
    }
}