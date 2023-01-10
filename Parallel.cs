using System.Collections;
using System.Collections.Generic;

namespace UnityLerpCoroutines
{
    // The Parallel Class
    public class Parallel : IntervalBase
    {
        private List<FixedCoroutine> coros = new();
        private IntervalBase[] Ivals;

        public Parallel(params IntervalBase[] Ivals)
        {
            this.Ivals = Ivals;
        }

        public override string Print()
        {
            var Outp = "Parallel: \n";
            foreach (var ival in Ivals) Outp += ival.Print() + "\n";

            return Outp;
        }

        public override void Start()
        {
            CoroutineMgr._CoroutineMgr.StartCoroutine(RunInterval(0));
        }

        public override void Stop()
        {
            foreach (var coro in coros) CoroutineMgr._CoroutineMgr.StopCoroutine(coro);
        }

        public void append(IntervalBase ival)
        {
            Ivals = Ivals.Append(ival);
        }

        // Gets the length of the Sequence in seconds
        public override float getLength()
        {
            var s = 0f;
            foreach (var Ival in Ivals) s += Ival.getLength();

            return s;
        }

        // Cycles through each IntervalBase object and plays them all
        // Unlike Sequence, Parallel's don't wait for the IntervalBase to end
        public override IEnumerator RunInterval(float t = 0)
        {
            foreach (var Ival in Ivals)
            {
                var currentCoroutine = Ival.RunInterval(0);
                coros.Add(CoroutineMgr._CoroutineMgr.StartCoroutine(currentCoroutine));
            }

            // Pause until all items have ended using getLength
            yield return new FixedWaitForSeconds(getLength());
            coros = new List<FixedCoroutine>();
        }
    }
}