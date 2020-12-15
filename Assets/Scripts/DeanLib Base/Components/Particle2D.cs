using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public double Mass;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    public Vector2 AccumulatedForces;
    public double DampingConstant;
    public bool shouldIgnoreForces;

    public float mRadius;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            mRadius = spriteRenderer.bounds.extents.x;
        }
        else mRadius = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBecameInvisible()
    {
        //ParticleManager.Instance.DeleteParticle(this);
    }
}
