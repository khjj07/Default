using UnityEngine;

namespace Default
{
    public abstract class DependentComponent<T> : MonoBehaviour
    {
        public abstract void Initialize(T dependedObject);
    }
}