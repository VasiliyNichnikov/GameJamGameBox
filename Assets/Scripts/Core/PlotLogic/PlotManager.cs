using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.PlotLogic.PlotAction;
using Core.Quests;
using Loaders.Data.Ready;
using UnityEngine;
using Utils;

namespace Core.PlotLogic
{
    public class PlotManager : IDisposable
    {
        private struct ActionData
        {
            public readonly JsonMessage<StepData> Data;
            public readonly PlotActionBase PlotAction;

            public ActionData(JsonMessage<StepData> data, PlotActionBase plotAction)
            {
                Data = data;
                PlotAction = plotAction;
            }
        }
        private readonly Queue<ActionData> _steps = new Queue<ActionData>();

        private IEnumerator _checkerExecutionOfPlot;

        private readonly QuestManager _questManager;
        
        public PlotManager(QuestManager questManager)
        {
            _questManager = questManager;
            
            _questManager.OnStartPlotAfterCompletedQuest += StartPlot;
        }
        
        private void StartPlot(int plotId)
        {
            // Если сюжет и так идет, то мы должны добавить в очередь
            var plots = Main.Instance.Data.PlotHelper.Plots;
            var selectedPlot = plots.FirstOrDefault(plot => plot.Id == plotId);
            Debug.Log($"StartPlot: {plotId}. Number steps: {selectedPlot.Steps.Count}");
            foreach (var step in selectedPlot.Steps)
            {
                PlotActionBase action = null;
                switch (step.Data.Type)
                {
                    case StepType.Sound:
                        action = new PlotSoundPlayer();
                        break;
                    case StepType.MonsterAction:
                        break;
                    case StepType.TextDialog:
                        action = new PlotTextDialogDisplay();
                        break;
                    case StepType.Timer:
                        action = new PlotTimer();
                        break;
                    case StepType.ChangeStateObject:
                        action = new PlotChangeStateObject();
                        break;
                }

                if (action == null)
                {
                    Debug.LogWarning($"Not found action for step: {step.Data.Type}");
                    continue;
                }

                _steps.Enqueue(new ActionData(step, action));
            }

            if (_checkerExecutionOfPlot == null)
            {
                _checkerExecutionOfPlot = CheckSteps();
                Main.Instance.StartCoroutine(_checkerExecutionOfPlot);
            }
            else
            {
                Debug.LogError("CheckerExecutionOfPlot is not null");
            }
        }

        private IEnumerator CheckSteps()
        {
            while (_steps.Count > 0)
            {
                var currentData = _steps.Dequeue();
                var stepData = currentData.Data;
                var action = currentData.PlotAction;
                action.RunAction(stepData);
                while (!action.IsEventCompleted())
                {
                    // Ожидаем пока не закончится шаг
                    yield return null;
                }
            }

            _checkerExecutionOfPlot = null;
        }

        public void Dispose()
        {
            _questManager.OnStartPlotAfterCompletedQuest -= StartPlot;
        }
    }
}