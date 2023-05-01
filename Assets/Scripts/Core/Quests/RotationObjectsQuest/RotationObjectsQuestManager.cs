using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Quests.RotationObjectsQuest
{
    public class RotationObjectsQuestManager : MonoBehaviour, IQuest
    {
        [SerializeField] private List<RotationObject> _rotationsObject;

        private void Start()
        {
            Init();
        }
        
        private void Init()
        {
            foreach (var rObject in _rotationsObject)
            {
                rObject.Init(PrintState);
            }
        }

        /// <summary>
        /// написано для теста
        /// </summary>
        private void PrintState()
        {
            Debug.LogWarning($"IsCompleted: {IsCompletedQuest()}");
        }
        
        public bool IsCompletedQuest()
        {
            return _rotationsObject.All(obj => obj.IsRightTurn);
        }
    }
}