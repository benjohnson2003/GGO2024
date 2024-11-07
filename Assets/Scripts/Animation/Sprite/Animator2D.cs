using UnityEngine;

namespace yiikes.SpriteAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Animator2D : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] Animation[] animations;

        // Current Animation
        Animation currentAnimation;
        int frames;
        int _frame;
        float fps;
        float _frameTime;

        // Components
        SpriteRenderer sr;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            Play(animations[0].name);
        }

        public void Play(string animationName)
        {
            for (int i = 0; i < animations.Length; i++)
            {
                if (animations[i].name == animationName)
                {
                    if (currentAnimation == animations[i]) return;
                    currentAnimation = animations[i];
                    frames = animations[i].frames.Length;
                    _frame = 0;
                    _frameTime = 0;
                    fps = animations[i].fps;

                    PlayFrame(currentAnimation.frames[_frame]);
                }
            }
        }

        void Update()
        {
            // Update Frame
            if (currentAnimation != null)
            {
                _frameTime += Time.deltaTime;

                if (_frameTime >= 1 / fps)
                {
                    // Frame Time
                    _frameTime = 0;
                    _frame++;
                    if (_frame >= frames)
                        _frame = 0;

                    PlayFrame(currentAnimation.frames[_frame]);
                }
            }
        }

        void PlayFrame(Frame frame)
        {
            sr.sprite = frame.sprite;
            if (frame.sfx.clips != null)
                SoundManager.instance?.PlaySound(frame.sfx);
        }
    }
}
