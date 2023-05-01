using System;

namespace Utils.Task
{
    public interface ITask
    {
        public TaskPriorityEnum Priority { get; }

        public void Start();
        public ITask Subscribe(Action feedback);
        void Stop();
    }
}