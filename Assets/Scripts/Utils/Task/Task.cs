using System;
using System.Collections;
using UnityEngine;

namespace Utils.Task
{
    public class Task: ITask
    {
        public TaskPriorityEnum Priority { get; }

        private readonly MonoBehaviour _coroutineHost;
        private readonly IEnumerator _taskAction;
        
        private Action _feedback;
        private Coroutine _coroutine;

        public static ITask Create(IEnumerator taskAction, TaskPriorityEnum priority = TaskPriorityEnum.Default)
        {
            return new Task(taskAction, priority);
        }
        
        private Task(IEnumerator taskAction, TaskPriorityEnum priority = TaskPriorityEnum.Default)
        {
            _coroutineHost = LauncherCoroutine.CoroutineHost;
            Priority = priority;
            _taskAction = taskAction;
        }
        
        public void Start()
        {
            if (_coroutine == null)
            {
                _coroutine = _coroutineHost.StartCoroutine(RunTask());
            }
        }
        
        public void Stop()
        {
            if (_coroutine != null)
            {
                _coroutineHost.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        public ITask Subscribe(Action feedback)
        {
            // todo разобраться с утечками памяти
            _feedback += feedback;

            return this;
        }

        private IEnumerator RunTask()
        {
            yield return _taskAction;

            CallSubscribe();
        }

        private void CallSubscribe()
        {
            if (_feedback != null)
            {
                _feedback();

            }
        }
    }
}