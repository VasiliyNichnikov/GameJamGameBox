using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.UI
{
    public class SpriteStorage : MonoBehaviour
    {
        [Serializable]
        private struct Data
        {
            public Sprite Sprite => _sprite;
            public string Name => _name;

            [SerializeField] private Sprite _sprite;
            [SerializeField] private string _name;
        }

        [SerializeField] private List<Data> _sprites;


        [CanBeNull]
        public Sprite GetSpriteByName(string name)
        {
            return _sprites.FirstOrDefault(sprite => sprite.Name == name).Sprite;
        }
    }
}