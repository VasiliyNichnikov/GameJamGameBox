using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Тут мы храним структуру и ее расширение
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class JsonMessage<TData> where TData: struct
    {
        public TData Data { get; }
        private readonly object _extension;
        
        /// <summary>
        /// С object плохое решение, но иначе никак
        /// </summary>
        public JsonMessage(TData data, object extension)
        {
            Data = data;
            _extension = extension;
        }

        public T? GetExt<T>() where T: struct
        {
            if (_extension == null)
            {
                Debug.LogWarning("Extension is null");
                return null;
            }
            return (T)_extension;
        }
    }
}