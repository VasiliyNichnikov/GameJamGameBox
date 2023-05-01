using System;

namespace Core.TriggerLogic
{
    public interface ITriggerListener
    {
        bool IsTriggered { get; }
        /// <summary>
        /// Проверяем может ли срабатывать триггер
        /// </summary>
        void CheckTrigger();
        /// <summary>
        /// Выполнить работу триггеры
        /// </summary>
        void ExecuteTrigger();
        /// <summary>
        /// Срабатывает после выполнения триггера
        /// </summary>
        void TriggerCompleted();
    }
}