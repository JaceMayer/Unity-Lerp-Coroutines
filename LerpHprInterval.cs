using System.Collections;
using UnityEngine;

namespace UnityLerpCoroutines
{
    // The LerpHprInterval class
    // Smoothly moves the objects eulerAngles from what they currently are to hpr
    public class LerpHprInterval : LerpInterval
    {
        public Vector3 lerpTo;
        private float Lerpx, Lerpy, Lerpz;

        public LerpHprInterval(GameObject gameObject, Vector3 hpr, float time, BlendTypes blend = BlendTypes.None)
        {
            BlendType = blend;
            this.gameObject = gameObject;
            lerpTo = hpr;
            this.time = time;
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            var startPos = gameObject.transform.localEulerAngles;
            timeElapsed = 0f;
            while (timeElapsed < time)
            {
                var step = getLerpStep();
                Lerpx = Mathf.LerpAngle(startPos.x, lerpTo.x, step);
                Lerpy = Mathf.LerpAngle(startPos.y, lerpTo.y, step);
                Lerpz = Mathf.LerpAngle(startPos.z, lerpTo.z, step);
                timeElapsed += Time.deltaTime;

                gameObject.transform.localEulerAngles = new Vector3(Lerpx, Lerpy, Lerpz);
                yield return null;
            }

            gameObject.transform.localEulerAngles = lerpTo;
        }
    }
}