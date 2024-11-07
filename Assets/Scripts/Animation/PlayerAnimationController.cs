using System.Collections.Generic;
using UnityEngine;
using yiikes.SpriteAnimation;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Components")]
    public List<Animator2D> treads = new List<Animator2D>();
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    public void TreadsMoving(bool moving)
    {
        foreach (Animator2D tread in treads)
            tread.Play(moving ? "Tread_Move" : "Tread_Idle");
    }

    public void Particles(int id)
    {
        particleSystems[id].Play();
    }
}
