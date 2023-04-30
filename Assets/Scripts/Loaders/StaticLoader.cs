using System;
using System.IO;
using UnityEngine;
using Utils;

namespace Loaders
{
    public static class StaticLoader
    {
        public static T LoadData<T>(string nameFile)
        {
            return LoadDataFromResource<T>(nameFile);
        }

        private static T LoadDataFromResource<T>(string nameResource)
        {
            var textAsset = (TextAsset)Resources.Load($"Json/{nameResource}", typeof(TextAsset));
            if (textAsset == null)
            {
                return default(T);
            }

            var data = JsonUtility.FromJson<T>(textAsset.text);
            return (T)Convert.ChangeType(data, typeof(T));
        }

        private static T LoadDataFromJson<T>(string nameFile)
        {
            var path = PathUtils.GetJsonRoot(nameFile);
            var dataForJson = File.ReadAllText(path);
            var resultValue = JsonUtility.FromJson<T>(dataForJson);
            return (T)Convert.ChangeType(resultValue, typeof(T));
        }
    }
}