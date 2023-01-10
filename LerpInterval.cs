using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLerpCoroutines
{
    // The base Lerp Class
    public static class SmoothingMethods
    {
        public static float None(float Expression)
        {
            return Expression;
        }

        public static float EaseIn(float Expression)
        {
            return Expression * Expression;
        }

        private static float Square(float Expression)
        {
            return Expression * Expression;
        }

        private static float Flip(float Expression)
        {
            return 1 - Expression;
        }

        public static float EaseOut(float Expression)
        {
            return Flip(Square(Flip(Expression)));
        }

        public static float EaseInOut(float Expression)
        {
            return Mathf.Lerp(EaseIn(Expression), EaseOut(Expression), Expression);
        }
    }

    public enum TransformType
    {
        local,
        global,
        RigidBody2D
    }

    public enum BlendTypes
    {
        None,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public class LerpInterval : IntervalBase
    {
        public BlendTypes BlendType;
        public GameObject gameObject;

        public Dictionary<BlendTypes, Func<float, float>> LerpSmoothing = new()
        {
            { BlendTypes.None, SmoothingMethods.None },
            { BlendTypes.EaseIn, SmoothingMethods.EaseIn },
            { BlendTypes.EaseOut, SmoothingMethods.EaseOut },
            { BlendTypes.EaseInOut, SmoothingMethods.EaseInOut }
        };

        public float time;
        public float timeElapsed = 0;
        public TransformType transformType = TransformType.local;

        public float getLerpStep()
        {
            return LerpSmoothing[BlendType](timeElapsed / time);
        }

        public override float getLength()
        {
            return time;
        }
    }
}