using System.Collections;
using UnityEngine;

namespace yiikes.SpriteAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFlash : MonoBehaviour
    {
        // Components
        SpriteRenderer sr;
        Material flashMaterial;
        Material defaultMaterial;

        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            defaultMaterial = sr.material;
        }

        public void Flash(float time = 0)
        {
            if (time == 0)
                StartCoroutine(FrameFlash());
            else
                TimedFlash(time);
        }

        IEnumerator FrameFlash()
        {
            sr.material = flashMaterial;
            yield return null;
            sr.material = defaultMaterial;
        }

        IEnumerator TimedFlash(float time)
        {
            sr.material = flashMaterial;
            yield return new WaitForSeconds(time);
            sr.material = defaultMaterial;
        }
    }
}
