using UnityEngine;

namespace Core.Monster
{
    public interface IMonsterWalking
    {
        bool IsMovement { get; }
        
        bool IsPlayerVisible { get; }

        /// <summary>
        /// Ищем игрока
        /// </summary>
        void SearchPlayer();
        
        /// <summary>
        /// Идем на услышанный звук
        /// </summary>
        void GoForSound();

        /// <summary>
        /// Бежать за игроком
        /// </summary>
        void RunAfterPlayer();
    }
}