using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    public float rotateSpeed = 1.0f;

    [Header("Rod Projectile")]
    public Sprite rodEndSprite1;
    public Sprite rodEndSprite2;
    public Color rodEndColor1;
    public Color rodEndColor2;
    public float rodEndMass1;
    public float rodEndMass2;
    public float rodEndVelocity1;
    public float rodEndVelocity2;
    public Vector2 rodEndAcceleration1;
    public Vector2 rodEndAcceleration2;
    public float rodEndDampingConstant1;
    public float rodEndDampingConstant2;
    public bool rodEndshouldIgnoreForces1;
    public bool rodEndshouldIgnoreForces2;

    [Header("Spring Projectile")]
    public Sprite springEndSprite1;
    public Sprite springEndSprite2;
    public Color springEndColor1;
    public Color springEndColor2;
    public float springEndMass1;
    public float springEndMass2;
    public float springEndVelocity1;
    public float springEndVelocity2;
    public Vector2 springEndAcceleration1;
    public Vector2 springEndAcceleration2;
    public float springEndDampingConstant1;
    public float springEndDampingConstant2;
    public bool springEndshouldIgnoreForces1;
    public bool springEndshouldIgnoreForces2;
    public float springConstant;
    public float restLength;
    private enum ProjectileType
    {
        SPRING,
        ROD,
        NumberOfTypes
    }

    private ProjectileType mCurrentProjectile = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(Vector3.forward * 1.0f);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(Vector3.back * 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mCurrentProjectile += 1;
            if (mCurrentProjectile == ProjectileType.NumberOfTypes)
            {
                mCurrentProjectile = 0;
            }
            Debug.Log(mCurrentProjectile.ToString());
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (mCurrentProjectile)
            {
                case ProjectileType.ROD:
                    {
                        GameObject Projectile1 = new GameObject("RodProjectile");
                        Projectile1.transform.position = transform.position;
                        Projectile1.transform.rotation = transform.rotation;
                        Projectile1.transform.localScale = new Vector3(2, 2, 1);
                        Projectile1.tag = "Projectile";
                        SpriteRenderer Projectile1Renderer = Projectile1.AddComponent<SpriteRenderer>();
                        Projectile1Renderer.sprite = rodEndSprite1;
                        Projectile1Renderer.color = rodEndColor1;
                        Particle2D Projectile1Data = Projectile1.AddComponent<Particle2D>();
                        Projectile1Data.Mass = rodEndMass1;
                        Projectile1Data.Velocity = transform.up * rodEndVelocity1;
                        Projectile1Data.Acceleration = rodEndAcceleration1;
                        Projectile1Data.DampingConstant = rodEndDampingConstant1;
                        Projectile1Data.shouldIgnoreForces = rodEndshouldIgnoreForces1;
                        ParticleManager.Instance.mParticles.Add(Projectile1Data);

                        GameObject Projectile2 = new GameObject("RodProjectile");
                        Projectile2.transform.position = transform.position + new Vector3(0,1,0);
                        Projectile2.transform.rotation = transform.rotation;
                        Projectile2.transform.localScale = new Vector3(2, 2, 1);
                        Projectile2.tag = "Projectile";
                        SpriteRenderer Projectile2Renderer = Projectile2.AddComponent<SpriteRenderer>();
                        Projectile2Renderer.sprite = rodEndSprite2;
                        Projectile2Renderer.color = rodEndColor2;
                        Particle2D Projectile2Data = Projectile2.AddComponent<Particle2D>();
                        Projectile2Data.Mass = rodEndMass2;
                        Projectile2Data.Velocity = transform.up * rodEndVelocity2;
                        Projectile2Data.Acceleration = rodEndAcceleration2;
                        Projectile2Data.DampingConstant = rodEndDampingConstant2;
                        Projectile2Data.shouldIgnoreForces = rodEndshouldIgnoreForces2;
                        ParticleManager.Instance.mParticles.Add(Projectile2Data);

                        GameObject particleLinkObject = new GameObject("LINK " + Projectile1.name + " " + Projectile2.name);
                        ParticleRod newParticleRod = particleLinkObject.AddComponent<ParticleRod>();
                        newParticleRod.Initialize(Projectile1, Projectile2, 2);
                        ContactResolver.Instance.mParticleLinks.Add(newParticleRod);
                        break;
                    }
                case ProjectileType.SPRING:
                    {
                        GameObject Projectile1 = new GameObject("SpringProjectile");
                        Projectile1.transform.position = transform.position;
                        Projectile1.transform.rotation = transform.rotation;
                        Projectile1.transform.localScale = new Vector3(2, 2, 1);
                        Projectile1.tag = "Projectile";
                        SpriteRenderer Projectile1Renderer = Projectile1.AddComponent<SpriteRenderer>();
                        Projectile1Renderer.sprite = springEndSprite1;
                        Projectile1Renderer.color = springEndColor1;
                        Particle2D Projectile1Data = Projectile1.AddComponent<Particle2D>();
                        Projectile1Data.Mass = springEndMass1;
                        Projectile1Data.Velocity = transform.up * springEndVelocity1;
                        Projectile1Data.Acceleration = springEndAcceleration1;
                        Projectile1Data.DampingConstant = springEndDampingConstant1;
                        Projectile1Data.shouldIgnoreForces = springEndshouldIgnoreForces1;
                        ParticleManager.Instance.mParticles.Add(Projectile1Data);

                        GameObject Projectile2 = new GameObject("SpringProjectile");
                        Projectile2.transform.position = transform.position + new Vector3(0, 1, 0);
                        Projectile2.transform.rotation = transform.rotation;
                        Projectile2.transform.localScale = new Vector3(2, 2, 1);
                        Projectile2.tag = "Projectile";
                        SpriteRenderer Projectile2Renderer = Projectile2.AddComponent<SpriteRenderer>();
                        Projectile2Renderer.sprite = springEndSprite2;
                        Projectile2Renderer.color = springEndColor2;
                        Particle2D Projectile2Data = Projectile2.AddComponent<Particle2D>();
                        Projectile2Data.Mass = springEndMass2;
                        Projectile2Data.Velocity = transform.up * springEndVelocity2;
                        Projectile2Data.Acceleration = springEndAcceleration2;
                        Projectile2Data.DampingConstant = springEndDampingConstant2;
                        Projectile2Data.shouldIgnoreForces = springEndshouldIgnoreForces2;
                        ParticleManager.Instance.mParticles.Add(Projectile2Data);

                        ForceManager.Instance.CreateSpringForceGenerator(Projectile1, Projectile2, springConstant, restLength);
                        break;
                    }
            }
        }
    }
}
