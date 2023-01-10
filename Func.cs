using System;
using System.Collections;

namespace UnityLerpCoroutines
{
    public class Func : IntervalBase
    {
        private readonly Action callback;

        public Func(Action callback)
        {
            this.callback = callback;
        }

        public override string Print()
        {
            return "Func()";
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            callback();
            yield return null;
        }
    }
}