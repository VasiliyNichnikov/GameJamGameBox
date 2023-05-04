using System;
using Loaders;
using Loaders.Data.Ready;
using UnityEngine;

namespace Core.Quests
{
    public class QuestManager : IQuestManager, ILoader
    {
        public event Action<int> OnStartPlotAfterCompletedQuest;


        public void QuestCompleted(QuestData data)
        {
            OnStartPlotAfterCompletedQuest?.Invoke(data.PlotIdAfterComplete);
        }

        public void LoadAwake()
        {
            InitQuestsOnScene();
        }

        public void LoadStart()
        {
            // nothing
        }

        private void InitQuestsOnScene()
        {
            var quests = Main.Instance.Data.QuestHelper.Quests;
            foreach (var quest in quests)
            {
                var questOnScene = Main.Instance.QuestsStorage.GetQuestByType(quest.Data.QuestType);
                if (questOnScene == null)
                {
                    Debug.LogWarning($"Quest on scene not found. Type: {quest.Data.QuestType}");
                    continue;
                }
                questOnScene.Init(this, quest);
            }
        }
    }
}