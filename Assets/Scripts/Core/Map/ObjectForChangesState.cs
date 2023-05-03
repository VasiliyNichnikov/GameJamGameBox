using UnityEngine;

namespace Core.Map
{
    public class ObjectForChangesState : MonoBehaviour
    {
        public void ChangeState(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}