using System;
using UnityEngine;

namespace Core.Monster
{
    [Serializable]
    public struct SettingsWalkingLogic
    {
        public float MinBeta => _minBeta;
        public float MaxBeta => _maxBeta;

        [SerializeField, Header("Минимальное значений случайной погрешности Beta"), Range(0.1f, 3f)]
        private float _minBeta;

        [SerializeField, Header("Максимальное значени случайной погрешности Beta"), Range(0.1f, 3)]
        private float _maxBeta;
    }
}