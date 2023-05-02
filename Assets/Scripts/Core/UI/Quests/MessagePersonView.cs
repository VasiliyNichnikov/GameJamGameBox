using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Quests
{
    public class MessagePersonView : DialogBase
    {
        [SerializeField] private Text _namePerson;
        [SerializeField] private Text _messagePerson;

        public void Init(string namePerson, string messagePerson)
        {
            base.Init();
            
            _namePerson.text = namePerson;
            _messagePerson.text = messagePerson;
        }

        public override void Dispose()
        {
            // nothing
        }
    }
}