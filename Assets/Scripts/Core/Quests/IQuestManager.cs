using System;

namespace Core.Quests
{
    public interface IQuestManager
    {
        /// <summary>
        /// Запускаем сюжет после завершение сюжета
        /// В int передаем id сюжета для запуска
        /// </summary>
        event Action<int> OnStartPlotAfterCompletedQuest;
    }
}