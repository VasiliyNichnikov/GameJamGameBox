using UnityEngine;

namespace Core.Quests
{
    public abstract class InteractionObjectBase : MonoBehaviour
    {
        public abstract bool IsDisplayedHintAfterInput { get; }
        public abstract bool IsQuestCompleted { get; protected set; }
        public abstract void Input();
    }
}