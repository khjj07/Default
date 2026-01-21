using UnityEngine;

namespace Default
{
    public class FollowTransformWidget : WidgetBase
    {
        public Vector3 offset;

        public void Follow(Camera camera, Transform targetTransform)
        {
            SetPosition(camera, targetTransform.position,offset);
        }
    }
}