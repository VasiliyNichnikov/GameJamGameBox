using Loaders.Data.Ready;
using Utils;

namespace Core.Quests
{
    public interface IQuest
    {
        QuestType Type { get; }
        void Init(QuestManager manager, JsonMessage<QuestData> message);
        bool IsCompletedQuest();
    }
}