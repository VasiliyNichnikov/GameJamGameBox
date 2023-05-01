using System;
using System.Collections;
using System.Collections.Generic;

namespace Utils.Task
{
    public class TaskManager
    {
        public ITask CurrentTask { get; private set; }

        private readonly List<ITask> _tasks = new();

        public void AddTask(
            IEnumerator taskAction,
            Action callback = null,
            TaskPriorityEnum taskPriority = TaskPriorityEnum.Default)
        {
            var task = Task.Create(taskAction, taskPriority);
            if (callback != null)
            {
                task.Subscribe(callback);
            }

            ProcessingAddedTask(task, taskPriority);
        }

        public void Break()
        {
            if (CurrentTask != null)
            {
                CurrentTask.Stop();
            }
        }

        public void Restore()
        {
            TaskQueueProcessing();
        }

        public void Clear()
        {
            Break();

            _tasks.Clear();
        }

        private void ProcessingAddedTask(ITask task, TaskPriorityEnum taskPriority)
        {
            switch (taskPriority)
            {
                case TaskPriorityEnum.Default:
                {
                    _tasks.Add(task);
                }
                    break;
                case TaskPriorityEnum.High:
                {
                    _tasks.Insert( 0, task);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(taskPriority), taskPriority, null);
            }
            
            if (CurrentTask == null)
            {
                CurrentTask = GetNextTask();

                CurrentTask?.Subscribe(TaskQueueProcessing).Start();
            }
        }

        private void TaskQueueProcessing()
        {
            CurrentTask = GetNextTask();
            if (CurrentTask != null)
            {
                CurrentTask.Subscribe(TaskQueueProcessing).Start();
            }
        }

        private ITask GetNextTask()
        {
            if (_tasks.Count > 0)
            {
                var returnValue = _tasks[0];
                _tasks.RemoveAt(0);

                return returnValue;
            }

            return null;
        }
    }
}