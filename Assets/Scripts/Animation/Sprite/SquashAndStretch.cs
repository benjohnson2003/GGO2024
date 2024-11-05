using UnityEngine;

namespace yiikes.SpriteAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SquashAndStretch : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float speed = 8;

        Vector2 targetScale;
        public Vector2 TargetScale { get { return targetScale; } set { targetScale = value; } }

        public void Squash(float x, float y)
        {
            transform.localScale = new Vector2(x, y);
        }

        void Start()
        {
            targetScale = transform.localScale;
        }

        void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale.normalized / 0.70710678f, Time.deltaTime * speed);
        }
    }
}
