using System;
using System.Collections;
using UnityEngine;

namespace UnityLerpCoroutines
{
// LerpFuncInterval
// Smoothly interpolates start to end by repeatedly calling callback
    public class LerpFuncInterval : LerpInterval
    {
        private readonly float end;
        private readonly Action<float> lambdaCallback;
        private float Lerpx;
        private readonly float start;


        public LerpFuncInterval(Action<float> callback, float start, float end, float time, BlendTypes blend = BlendTypes.None)
        {
            lambdaCallback = callback;
            BlendType = blend;
            this.time = time;
            this.start = start;
            this.end = end;
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            timeElapsed = 0f;
            while (timeElapsed < time)
            {
                var step = getLerpStep();
                Lerpx = Mathf.Lerp(start, end, step);
                timeElapsed += Time.deltaTime;

                lambdaCallback(Lerpx);
                yield return null;
            }
        }
    }
}