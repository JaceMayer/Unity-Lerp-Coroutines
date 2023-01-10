using System;
using System.Collections;

namespace UnityLerpCoroutines
{
    public class Wait : IntervalBase
    {
        private DateTime dt;

        private readonly float waitDuration;

        public Wait(float duration)
        {
            waitDuration = duration;
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            dt = DateTime.Now.AddSeconds(waitDuration);
            // Pauses the current sequence/parallel for t seconds
            while (DateTime.Now < dt) yield return null;
        }
    }
}