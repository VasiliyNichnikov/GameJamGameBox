using Core.SoundLogic.AtmosphericSounds;
using JetBrains.Annotations;

namespace Core.SoundLogic
{
    public interface IBackgroundSoundManager
    {
        /// <summary>
        /// Встреча с врагом
        /// </summary>
        void MeetingWithEnemy();

        /// <summary>
        /// Отдаление от врага
        /// </summary>
        void EscapeFromEnemy();

        /// <summary>
        /// Вход в комнату
        /// </summary>
        void EntranceToRoom([CanBeNull] ISoundRoom roomSound);

        /// <summary>
        /// Выход из комнаты в коридор
        /// </summary>
        void ExitFromRoomToHall([CanBeNull] ISoundRoom roomSound);
    }
}