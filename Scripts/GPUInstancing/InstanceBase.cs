using UnityEngine;

namespace Default
{
    public abstract class InstanceBase : MonoBehaviour
    {
        [ReadOnly] 
        public string guid;
    }
}