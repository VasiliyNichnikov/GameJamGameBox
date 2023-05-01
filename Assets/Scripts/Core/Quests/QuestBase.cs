using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.Quests
{
    public abstract class QuestBase : MonoBehaviour, IQuest
    {
        public abstract QuestType Type { get; }

        protected QuestManager Manager;
        protected JsonMessage<QuestData> Message;


        public virtual void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            Manager = manager;
            Message = message;
        }

        public abstract bool IsCompletedQuest();
    }
}