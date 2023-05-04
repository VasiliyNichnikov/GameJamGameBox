using UnityEngine;

namespace Core.Quests
{
    public abstract class InteractionObjectBase : MonoBehaviour
    {
        public abstract bool IsDisplayedHintAfterInput { get; }
        public abstract bool IsQuestCompleted { get; protected set; }
        public virtual bool HasHint => false;
        public virtual string Hint => string.Empty;
        public abstract void Input();
    }
}