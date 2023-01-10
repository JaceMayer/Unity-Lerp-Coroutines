using System.Collections;
using UnityEngine;

namespace UnityLerpCoroutines
{
    // The base Interval class
    public enum IntervalPlayType
    {
        Once,
        Loop,
        Stop,
        Finished
    }

    public class IntervalBase
    {
        public IntervalPlayType PlayType = IntervalPlayType.Once;

        public virtual float getLength()
        {
            return 0.0f;
        }

        public virtual void Start()
        {
            Debug.Log("BaseIval:: start");
        }

        public virtual string Print()
        {
            return "BaseIval:: Print";
        }

        public virtual void Start(float T)
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual IEnumerator RunInterval()
        {
            yield return null;
        }

        public virtual IEnumerator RunInterval(float T)
        {
            yield return null;
        }
    }
}