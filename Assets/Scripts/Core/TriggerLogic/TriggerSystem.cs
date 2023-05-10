using System;
using System.Collections.Generic;
using Core.TriggerLogic.Triggers;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.TriggerLogic
{
    public class TriggerSystem : IDisposable
    {
        private readonly List<ITriggerListener> _triggers = new List<ITriggerListener>();

        public static ITriggerListener GetAndAddTrigger(TriggerSystem system, JsonMessage<TriggerData> message)
        {
            var type = message.Data.Type;

            ITriggerListener trigger = null;
            switch (type)
            {
                case TriggerType.OffLightManyTimes:
                    trigger = new LightChangeTrigger(system, message);
                    break;
                case TriggerType.DestroyBlock:
                    trigger = new DestroyBlockTrigger();
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
                    if (!trigger.ManyTimer)
                    {
                        completedTriggers.Enqueue(trigger);
                    }
                }
            }

            if (completedTriggers.Count == 0)
            {
                return;
            }
            
            while (completedTriggers.Count > 0)
            {
                var trigger = completedTriggers.Dequeue();
                trigger.TriggerDestroy();
                trigger.Dispose();
                _triggers.Remove(trigger);
            }
        }

        public void Dispose()
        {
            if (_triggers == null || _triggers.Count > 0)
            {
                return;
            }

            foreach (var trigger in _triggers)
            {
                
                trigger.Dispose();
            }
        }
    }
}