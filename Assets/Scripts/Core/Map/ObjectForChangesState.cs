using UnityEngine;

namespace Core.Map
{
    public class ObjectForChangesState : MonoBehaviour
    {
        [SerializeField] private Animation _animation;

        public void ChangeState(bool state)
        {
            gameObject.SetActive(state);
        }

        public void PlayAnimation(string nameAnimation)
        {
            _animation.Play(nameAnimation);
        }
    }
}