using UnityEngine;

namespace Default
{
    public class FollowTransformWidget : WidgetBase
    {
        public Vector3 offset;
        [SerializeField] private float size;

        public void Follow(Camera camera, Transform targetTransform)
        {
            SetPosition(camera, targetTransform.position,offset);
            SetSize(camera,size);
        }
    }
}