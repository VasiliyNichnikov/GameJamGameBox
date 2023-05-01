using System;
using Core.UI;
using Newtonsoft.Json;
using UnityEngine;

namespace Loaders
{
    public static class StaticLoader
    {
        public static T LoadData<T>(string nameFile)
        {
            return LoadDataFromResource<T>(nameFile);
        }

        public static AudioClip LoadData(string nameResource)
        {
            return LoadAudioClipFromResource(nameResource);
        }

        public static T LoadDialog<T>(string nameDialog) where T: DialogBase
        {
            return LoadDialogFromResource<T>(nameDialog);
        }

        private static T LoadDataFromResource<T>(string nameResource)
        {
            var textAsset = (TextAsset)Resources.Load($"Json/{nameResource}", typeof(TextAsset));
            if (textAsset == null)
            {
                return default(T);
            }
            
            var data = JsonConvert.DeserializeObject<T>(textAsset.text);
            return (T)Convert.ChangeType(data, typeof(T));
        }

        private static AudioClip LoadAudioClipFromResource(string nameResource)
        {
            var data = (AudioClip)Resources.Load($"Sound/{nameResource}", typeof(AudioClip));
            if (data == null)
            {
                return default(AudioClip);
            }

            return data;
        }

        private static T LoadDialogFromResource<T>(string nameDialog) where T: DialogBase
        {
            var data = (T)Resources.Load($"Prefabs/UI/{nameDialog}", typeof(T));
            if (data == null)
            {
                return default(T);
            }

            return data;
        }
    }
}