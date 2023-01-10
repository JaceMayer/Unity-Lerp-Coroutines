using System.Collections;

namespace UnityLerpCoroutines
{
    // The Sequence Class
    public class Sequence : IntervalBase
    {
        private FixedCoroutine _fixedCoroutine;
        private IEnumerator currentCoroutine;
        private IntervalBase[] Ivals;

        public Sequence(params IntervalBase[] Ivals)
        {
            this.Ivals = Ivals;
        }

        public override void Start()
        {
            _fixedCoroutine = CoroutineMgr._CoroutineMgr.StartCoroutine(RunInterval(0));
        }

        public void Loop()
        {
            if (PlayType == IntervalPlayType.Loop) return;
            PlayType = IntervalPlayType.Loop;
            _fixedCoroutine = CoroutineMgr._CoroutineMgr.StartCoroutine(RunInterval(0));
        }

        // Returns true/false depending on whether the Sequence is playing
        public bool IsPlaying()
        {
            return PlayType != IntervalPlayType.Stop && PlayType != IntervalPlayType.Finished;
        }

        // Sets the Sequence to stop
        public override void Stop()
        {
            PlayType = IntervalPlayType.Stop;
            CoroutineMgr._CoroutineMgr.StopCoroutine(_fixedCoroutine);
        }

        public override string Print()
        {
            var Outp = "Sequence: \n";
            foreach (var ival in Ivals) Outp += ival.Print() + "\n";

            return Outp;
        }

        public void append(params IntervalBase[] IvalL)
        {
            foreach (var ival in IvalL) Ivals = Ivals.Append(ival);
        }

        // Gets the length of the Sequence in seconds
        public override float getLength()
        {
            var s = 0f;
            foreach (var Ival in Ivals) s += Ival.getLength();

            return s;
        }

        // Cycles through each IntervalBase object
        // A sequence iterates through its Intervals one-by-one and runs it until it's ended
        public override IEnumerator RunInterval(float t = 0)
        {
            foreach (var Ival in Ivals)
            {
                currentCoroutine = Ival.RunInterval(0);
                while (currentCoroutine != null && currentCoroutine.MoveNext())
                    if (currentCoroutine.Current != null &&
                        currentCoroutine.Current.GetType() == typeof(FixedWaitForSeconds))
                        yield return (FixedWaitForSeconds)currentCoroutine.Current;
                    else
                        yield return null;
            }

            currentCoroutine = null;
            if (PlayType == IntervalPlayType.Loop)
                _fixedCoroutine = CoroutineMgr._CoroutineMgr.StartCoroutine(RunInterval(0));
            else
                PlayType = IntervalPlayType.Finished;
        }
    }
}