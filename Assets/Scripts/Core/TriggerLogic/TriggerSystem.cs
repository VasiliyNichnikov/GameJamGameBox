using System.Collections.Generic;
using Core.TriggerLogic.Triggers;
using Loaders;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.TriggerLogic
{
    public class TriggerSystem
    {
        private readonly List<ITriggerListener> _triggers = new List<ITriggerListener>();

        public static ITriggerListener GetAndAddTrigger(TriggerSystem system, JsonMessage<TriggerData> message)
        {
            var type = message.Data.Type;

            ITriggerListener trigger = null;
            switch (type)
            {
                case TriggerType.SoundAfterObjectRotationQuest:
                    trigger = new SoundAfterObjectRotationQuestTrigger(system, message);
                    break;

                case TriggerType.MonsterAction:
                    trigger = new MonsterActionTrigger(system, message);
                    break;
                case TriggerType.QuestCompleted:
                    trigger = new QuestCompletedTrigger(system, message);
                    break;
            }

            if (trigger == null)
            {
                Debug.LogError($"Not found type trigger: {type}");
                return null;
            }

            system.AddTrigger(trigger);
            return trigger;
        }
        

        private void AddTrigger(ITriggerListener trigger)
        {
            if (_triggers.Contains(trigger))
            {
                Debug.LogError("TriggerSystem. Trigger contains in list triggers");
                return;
            }

            _triggers.Add(trigger);
        }

        public void RemoveTrigger(ITriggerListener trigger)
        {
            if (_triggers.Contains(trigger))
            {
                _triggers.Remove(trigger);
            }
        }

        public void CheckTriggers()
        {
            var completedTriggers = new Queue<ITriggerListener>();
            foreach (var trigger in _triggers)
            {
                trigger.CheckTrigger();
                if (trigger.IsTriggered)
                {
                    trigger.ExecuteTrigger();
                    completedTriggers.Enqueue(trigger);
                }
            }

            if (completedTriggers.Count == 0)
            {
                return;
            }
            
            while (completedTriggers.Count > 0)
            {
                var trigger = completedTriggers.Dequeue();
                trigger.TriggerCompleted();
            }
        }
    }
}