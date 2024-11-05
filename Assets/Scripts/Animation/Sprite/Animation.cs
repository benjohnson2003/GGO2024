using UnityEngine;

namespace yiikes.SpriteAnimation
{
    [CreateAssetMenu(fileName = "New Animation", menuName = "Animator2D/Animation", order = 1)]
    public class Animation : ScriptableObject
    {
        public Frame[] frames;
        public float fps = 12;
    }

    [System.Serializable]
    public struct Frame
    {
        public Sprite sprite;
        public SoundProfile sfx;
    }
}
