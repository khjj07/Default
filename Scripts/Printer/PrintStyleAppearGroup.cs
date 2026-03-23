using System;
using DG.Tweening;
using UnityEngine;

namespace KCoreKit
{
    [Serializable]
    public class PrintStyleScaleModifier
    {
        public float scaleSpeed = 1;
        public Vector3 beginScale = Vector3.one;
        public Vector3 endScale = Vector3.one;
        public Ease scaleEase;
    }
    
    [Serializable]
    public class PrintStylePositionModifier
    {
        public float positionSpeed = 1;
        public Vector3 beginPosition;
        public Vector3 endPosition;
        public Ease positionEase;
    }
    
    [Serializable]
    public class PrintStyleRotationModifier
    {
        public float rotationSpeed = 1;
        public Vector3 beginRotation;
        public Vector3 endRotation;
        public Ease rotationEase;
    }

    [Serializable]
    public class PrintStyleColorModifier
    {
        public float colorSpeed = 1;
        public Color beginColor = Color.black;
        public Color endColor = Color.black;
        public Ease colorEase;
    }
    
    [Serializable]
    public class PrintStyleDetailGroup
    {
        public float interval;

    }
}