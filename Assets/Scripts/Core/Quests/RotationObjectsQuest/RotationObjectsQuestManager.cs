using System.Collections.Generic;
using System.Linq;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.Quests.RotationObjectsQuest
{
    public class RotationObjectsQuestManager : QuestBase
    {
        [SerializeField] private List<RotationObject> _rotationsObject;

        public override QuestType Type => QuestType.RotationObjects;

        public override void Init(QuestManager manager, JsonMessage<QuestData> message)
        {
            base.Init(manager, message);

            foreach (var rotationObject in _rotationsObject)
            {
                rotationObject.Init(CheckAfterTurn);
            }
        }

        private void CheckAfterTurn()
        {
            if (IsCompletedQuest())
            {
                Manager.QuestCompleted(Message.Data);
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            foreach (var rotationObject in _rotationsObject)
            {
                rotationObject.CompleteQuest();
            }
        }

        public override bool IsCompletedQuest()
        {
            return _rotationsObject.All(obj => obj.IsRightTurn);
        }
    }
}