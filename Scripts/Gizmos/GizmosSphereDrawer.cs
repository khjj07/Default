using UnityEngine;

namespace Default
{
    public class GizmosSphereDrawer : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField] public float size = 1.0f;

        [SerializeField] private bool _wireFrame;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            if (_wireFrame)
                Gizmos.DrawWireSphere(transform.position, size);
            else
                Gizmos.DrawSphere(transform.position, size);
        }
#endif
    }
}