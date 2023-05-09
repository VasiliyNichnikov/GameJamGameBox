using System;
using Core.Doors;
using UnityEngine;

namespace Core.Map
{
    public class ObjectForChangesState : MonoBehaviour
    {
        [SerializeField] private DoorBase _door;

        [SerializeField] private string _nameObject;

        private void Start()
        {
            name = _nameObject;
        }


        public void ChangeState(bool state)
        {
            gameObject.SetActive(state);
        }

        public void OpenDoor()
        {
            if (_door != null)
            {
                _door.Open(false);
            }
        }
    }
}