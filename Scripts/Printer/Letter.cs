using System;
using DG.Tweening;
using UnityEngine;

namespace KCoreKit
{
    [Serializable]
    public class Letter
    {
        public PrintStyle style;
        public char value;

        public Vector3 position;
        public Vector3 scale = Vector3.one;
        public Vector3 rotation;
        public Color color = Color.black;

        public Tween repeatPositionTween;
        public Tween repeatScaleTween;
        public Tween repeatRotationTween;
        public Tween repeatColorTween;

        public Letter(char value, PrintStyle style,Color color)
        {
            this.color = color;
            this.value = value;
            this.style = style;
        }

        public Sequence AppearSequence()
        {
            var sequence = DOTween.Sequence();
            if (style.appearOption.usePosition)
            {
                position = style.appearPosition.beginPosition;
                sequence.Join(DOTween.To(() => position, x => position = x, style.appearPosition.endPosition,
                        style.appearOption.interval / style.appearPosition.positionSpeed)
                    .SetEase(style.appearPosition.positionEase));
            }

            if (style.appearOption.useScale)
            {
                scale = style.appearScale.beginScale;
                sequence.Join(DOTween.To(() => scale, x => scale = x, style.appearScale.endScale,
                        style.appearOption.interval / style.appearScale.scaleSpeed)
                    .SetEase(style.appearScale.scaleEase));
            }

            if (style.appearOption.useRotation)
            {
                rotation = style.appearRotation.beginRotation;
                sequence.Join(DOTween
                    .To(() => rotation, x => rotation = x, style.appearRotation.endRotation,
                        style.appearOption.interval / style.appearRotation.rotationSpeed)
                    .SetEase(style.appearRotation.rotationEase));
            }

            if (style.appearOption.useColor)
            {
                color = style.appearColor.beginColor;
                sequence.Join(DOTween.To(() => color, x => color = x, style.appearColor.endColor,
                        style.appearOption.interval / style.appearColor.colorSpeed)
                    .SetEase(style.appearColor.colorEase));
            }

            return sequence;
        }

        public Sequence RepeatSequence()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(style.repeatOffset);
            sequence.AppendCallback(() =>
            {
                if (style.repeatOption.usePosition)
                {
                    position = style.repeatPosition.beginPosition;
                    repeatPositionTween = DOTween
                        .To(() => position, x => position = x, style.repeatPosition.endPosition,
                            style.repeatOption.interval / style.repeatPosition.positionSpeed)
                        .SetEase(style.repeatPosition.positionEase)
                        .SetLoops(-1, LoopType.Yoyo);
                    repeatPositionTween.Play();
                }

                if (style.repeatOption.useScale)
                {
                    scale = style.repeatScale.beginScale;
                    repeatScaleTween = DOTween
                        .To(() => scale, x => scale = x, style.repeatScale.endScale,
                            style.repeatOption.interval / style.repeatScale.scaleSpeed)
                        .SetEase(style.repeatScale.scaleEase)
                        .SetLoops(-1, LoopType.Yoyo);
                    repeatScaleTween.Play();
                }

                if (style.repeatOption.useRotation)
                {
                    rotation = style.repeatRotation.beginRotation;
                    repeatRotationTween = DOTween
                        .To(() => rotation, x => rotation = x, style.repeatRotation.endRotation,
                            style.repeatOption.interval / style.repeatRotation.rotationSpeed)
                        .SetEase(style.repeatRotation.rotationEase)
                        .SetLoops(-1, LoopType.Yoyo);
                    repeatRotationTween.Play();
                }

                if (style.repeatOption.useColor)
                {
                    color = style.repeatColor.beginColor;
                    repeatColorTween = DOTween
                        .To(() => color, x => color = x, style.repeatColor.endColor,
                            style.repeatOption.interval / style.repeatColor.colorSpeed)
                        .SetEase(style.repeatColor.colorEase)
                        .SetLoops(-1, LoopType.Yoyo);
                    repeatColorTween.Play();
                }
            });

            return sequence;
        }

        public Sequence DisappearSequence()
        {
            var sequence = DOTween.Sequence();

            if (style.disappearOption.usePosition)
            {
                sequence.Join(DOTween.To(() => position, x => position = x, style.disappearPosition.endPosition,
                        style.disappearOption.interval / style.disappearPosition.positionSpeed)
                    .SetEase(style.disappearPosition.positionEase));
            }

            if (style.disappearOption.useScale)
            {
                sequence.Join(DOTween.To(() => scale, x => scale = x, style.disappearScale.endScale,
                        style.disappearOption.interval / style.disappearScale.scaleSpeed)
                    .SetEase(style.disappearScale.scaleEase));
            }

            if (style.disappearOption.useRotation)
            {
                sequence.Join(DOTween.To(() => rotation, x => rotation = x, style.disappearRotation.endRotation,
                        style.disappearOption.interval / style.disappearRotation.rotationSpeed)
                    .SetEase(style.disappearRotation.rotationEase));
            }

            if (style.disappearOption.useColor)
            {
                sequence.Join(DOTween.To(() => color, x => color = x, style.disappearColor.endColor,
                        style.disappearOption.interval / style.disappearColor.colorSpeed)
                    .SetEase(style.disappearColor.colorEase));
            }

            return sequence;
        }

        public void KillRepeatTween()
        {
            repeatPositionTween?.Kill();
            repeatScaleTween?.Kill();
            repeatRotationTween?.Kill();
            repeatColorTween?.Kill();

            repeatPositionTween = null;
            repeatScaleTween = null;
            repeatRotationTween = null;
            repeatColorTween = null;
        }
    }
}