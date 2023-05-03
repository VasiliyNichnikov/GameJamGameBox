using Core.Map;
using UnityEngine;

namespace Loaders.Data.Ready
{
    public struct SoundExtensionData
    {
        public readonly AudioClip Clip;

        public SoundExtensionData(AudioClip clip)
        {
            Clip = clip;
        }
    }

    public struct TextDialogExtensionData
    {
        public readonly string NamePerson;
        public readonly string MessagePerson;

        public TextDialogExtensionData(string namePerson, string messagePerson)
        {
            NamePerson = namePerson;
            MessagePerson = messagePerson;
        }
    }

    public struct TimerExtensionData
    {
        public readonly float Time;

        public TimerExtensionData(float time)
        {
            Time = time;
        }
    }

    public struct ChangeStateObjectExtensionData
    {
        public readonly string ObjectForChanges;
        public readonly bool State;

        public ChangeStateObjectExtensionData(string objectForChanges, bool state)
        {
            ObjectForChanges = objectForChanges;
            State = state;
        }
    }
}