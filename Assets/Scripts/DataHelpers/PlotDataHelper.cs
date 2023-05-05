using System.Collections.Generic;
using Core.PlotLogic;
using Extensions;
using Loaders;
using Loaders.Data.Raw;
using Loaders.Data.Ready;
using Utils;

namespace DataHelpers
{
    public class PlotDataHelper
    {
        private const string PlotFile = "Plot";

        public IReadOnlyCollection<PlotData> Plots => _plots.AsReadOnly();

        private readonly List<PlotData> _plots = new List<PlotData>();

        public void Load()
        {
            var data = StaticLoader.LoadData<PlotRaw>(PlotFile);

            foreach (var plot in data.Plot)
            {
                var allStepsData = new List<JsonMessage<StepData>>();
                foreach (var step in plot.Steps)
                {
                    var stepData = new StepData(step.Type.ConvertToEnum<StepType>());
                    if (step.SoundExtension != null)
                    {
                        var jsonMessage =
                            ExtensionHelper.GetMessageForSoundExt(stepData, step.SoundExtension.Value.Sound);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }

                    if (step.TextDialogExtension != null)
                    {
                        var textDialog = step.TextDialogExtension.Value;
                        var jsonMessage =
                            ExtensionHelper.GetMessageForTextDialogExt(stepData, textDialog.NamePerson,
                                textDialog.MessagePerson);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }

                    if (step.TimerExtension != null)
                    {
                        var timer = step.TimerExtension.Value.Timer;
                        var jsonMessage = ExtensionHelper.GetMessageForTimer(stepData, timer);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }

                    if (step.ChangeStateObjectExtension != null)
                    {
                        var nameObject = step.ChangeStateObjectExtension.Value.NameObject;
                        var state = step.ChangeStateObjectExtension.Value.State;
                        var jsonMessage = ExtensionHelper.GetMessageForChangeStateObject(stepData, nameObject, state);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }

                    if (step.OpenDoorExtension != null)
                    {
                        var nameDoor = step.OpenDoorExtension.Value.NameDoor;
                        var nameAnimation = step.OpenDoorExtension.Value.NameAnimation;
                        var jsonMessage = ExtensionHelper.GetMessageForOpenDoor(stepData, nameDoor, nameAnimation);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }

                    if (step.CreateItemExtension != null)
                    {
                        var extensionRaw = step.CreateItemExtension.Value;
                        var jsonMessage = ExtensionHelper.GetMessageForCreateItem(stepData, extensionRaw);
                        allStepsData.Add(jsonMessage);
                        continue;
                    }
                }

                var plotData = new PlotData(plot.Id, allStepsData);
                _plots.Add(plotData);
            }
        }
    }
}