using UnityEngine;

namespace Kusume
{
    public class LedgerBase<T> : ScriptableObject
    {
        [SerializeField]
        private T[] values;
        public T[] Values { get => values; }
        public T this[int i] { get => values[i]; }
        public int Count { get => values.Length; }
    }
}

