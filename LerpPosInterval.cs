using System.Collections;
using UnityEngine;

namespace UnityLerpCoroutines
{
    // The LerpPosInterval class
    // Smoothly interpolates the items positon from what it currently is to pos
    public class LerpPosInterval : LerpInterval
    {
        public Vector3 lerpTo;
        private float Lerpx, Lerpy, Lerpz;

        public LerpPosInterval(GameObject gameObject, Vector3 pos, float time, BlendTypes blend = BlendTypes.None,
            TransformType transformType = TransformType.local)
        {
            BlendType = blend;
            this.gameObject = gameObject;
            lerpTo = pos;
            this.time = time;
            this.transformType = transformType;
        }

        public override string Print()
        {
            return "LerpPosInterval(" + gameObject.name + ", " + lerpTo + ", " + time;
        }

        public override IEnumerator RunInterval(float t = 0)
        {
            Vector3 startPos;
            timeElapsed = 0f;
            if (transformType == TransformType.local)
                startPos = gameObject.transform.localPosition;
            else
                startPos = gameObject.transform.position;

            while (timeElapsed < time)
            {
                var step = getLerpStep();
                Lerpx = Mathf.Lerp(startPos.x, lerpTo.x, step);
                Lerpy = Mathf.Lerp(startPos.y, lerpTo.y, step);
                Lerpz = Mathf.Lerp(startPos.z, lerpTo.z, step);
                timeElapsed += Time.deltaTime;

                if (transformType == TransformType.local)
                    gameObject.transform.localPosition = new Vector3(Lerpx, Lerpy, Lerpz);
                else if (transformType == TransformType.RigidBody2D)
                    gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector3(Lerpx, Lerpy, Lerpz));
                else
                    gameObject.transform.position = new Vector3(Lerpx, Lerpy, Lerpz);

                yield return timeElapsed;
            }

            if (transformType == TransformType.local)
                gameObject.transform.localPosition = lerpTo;
            else
                gameObject.transform.position = lerpTo;
        }
    }
}