using UnityEngine;

namespace Default
{
    public abstract class GameStateBase : MonoBehaviour
    {
        public virtual void OnEnter()
        {
            gameObject.SetActive(true);
        }
        public virtual void OnExit()
        {
            gameObject.SetActive(false);
        }
    }
}