﻿using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Quests
{
    public class MessagePersonView : DialogBase
    {
        [SerializeField] private Text _namePerson;
        [SerializeField] private Text _messagePerson;
        [SerializeField] private Button _exitButton;
        
        public void Init(string namePerson, string messagePerson, bool showExitButton)
        {
            base.Init();
            
            _namePerson.text = namePerson;
            _messagePerson.text = messagePerson;

            _exitButton.gameObject.SetActive(showExitButton);
        }

        public override void Dispose()
        {
            // nothing
        }

        public void OnMenu()
        {
            Debug.LogWarning("Go to menu");
        }
    }
}