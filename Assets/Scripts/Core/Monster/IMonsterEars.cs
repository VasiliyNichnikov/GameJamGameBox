using UnityEngine;

namespace Core.Monster
{
    public interface IMonsterEars
    {
        /// <summary>
        /// Самая громкая найденная точка 
        /// </summary>
        Vector3 LoudestPoint { get; }
        
        /// <summary>
        /// Расстояние на котором может слушать враг
        /// </summary>
        float HearingDistance { get; }
        
        bool SoundDetected();
    }
}