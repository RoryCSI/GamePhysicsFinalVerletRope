using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionDetector
{
    public static bool DetectCollision(Particle2D particle, Particle2D otherParticle)
    {
        if (Vector2.Distance(particle.transform.position, otherParticle.transform.position) < particle.mRadius + otherParticle.mRadius)
        {
            return true;
        }
        else
        {
            return false;
        }  
    }    
}
