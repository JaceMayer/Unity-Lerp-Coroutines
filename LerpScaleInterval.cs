using System.Collections;
using UnityEngine;

namespace UnityLerpCoroutines
{
    // The LerpScaleInterval class
    // Smoothly interpolates the items scale from what it currently is to scale
    public class LerpScaleInterval : LerpInterval
    {
        public Vector3 lerpTo;
        private float Lerpx, Lerpy, Lerpz;

        public LerpScaleInterval(GameObject gameObject, Vector3 scale, float time, BlendTypes blend = BlendTypes.None,
            TransformType transformType = TransformType.local)
        {
            BlendType = blend;
            this.gameObject = gameObject;
            lerpTo = scale;
            this.time = time;
            this.transformType = transformType;
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            var startPos = gameObject.transform.localScale;
            timeElapsed = 0f;
            while (timeElapsed < time)
            {
                var step = getLerpStep();
                Lerpx = Mathf.Lerp(startPos.x, lerpTo.x, step);
                Lerpy = Mathf.Lerp(startPos.y, lerpTo.y, step);
                Lerpz = Mathf.Lerp(startPos.z, lerpTo.z, step);
                timeElapsed += Time.deltaTime;


                gameObject.transform.localScale = new Vector3(Lerpx, Lerpy, Lerpz);
                yield return null;
            }

            gameObject.transform.localScale = lerpTo;
        }
    }
}