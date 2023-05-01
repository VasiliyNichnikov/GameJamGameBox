using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Quests
{
    /// <summary>
    /// Храним все квесты на карте
    /// </summary>
    public class QuestsStorage : MonoBehaviour
    {
        public IReadOnlyCollection<QuestBase> Quests => _quests;

        [SerializeField] private List<QuestBase> _quests = new List<QuestBase>();

        [CanBeNull]
        public IQuest GetQuestByType(QuestType type)
        {
            return _quests.FirstOrDefault(quest => quest.Type == type);
        }
    }
}