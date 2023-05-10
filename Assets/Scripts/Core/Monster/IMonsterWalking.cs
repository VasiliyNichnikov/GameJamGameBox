using UnityEngine;

namespace Core.Monster
{
    public enum SpeedType
    {
        Walk,
        Run,
        Rotation
    }
    
    public interface IMonsterWalking
    {
        bool IsPlayerNearby { get; }
        bool IsMovement { get; }
        
        /// <summary>
        /// Выставляем скорость перемещения
        /// </summary>
        void SetSpeedMovement(SpeedType speedType);
        
        /// <summary>
        /// Ищем игрока
        /// </summary>
        void SearchPlayer();

        /// <summary>
        /// Идем на услышанный звук
        /// </summary>
        void GoForSound(Vector3 loudestPoint);

        /// <summary>
        /// Бежать за игроком
        /// </summary>
        void RunAfterPlayer();
    }
}