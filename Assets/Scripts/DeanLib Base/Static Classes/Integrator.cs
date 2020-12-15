using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
	private static Integrator instance;

	public static Integrator Instance { get { return instance; } }

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
	}
   
    // Update is called once per frame
    void Update()
	{
		foreach (Particle2D particle in ParticleManager.Instance.mParticles)
		{
			Integrate(particle.gameObject, Time.deltaTime);
		}

	}
	void Integrate(GameObject physicsObject, float dt)
	{
		Particle2D ParticleData = physicsObject.GetComponent<Particle2D>();
		physicsObject.transform.position += new Vector3((ParticleData.Velocity.x * dt), (ParticleData.Velocity.y * dt), 0.0f);

		Vector2 resultingAcc = ParticleData.Acceleration;

		if (!ParticleData.shouldIgnoreForces)//accumulate forces here
		{
			resultingAcc += ParticleData.AccumulatedForces * (float)(ParticleData.Mass/1.0);
		}

		ParticleData.Velocity += (resultingAcc * dt);
		float damping = Mathf.Pow((float)ParticleData.DampingConstant, dt);
		ParticleData.Velocity *= damping;

		ParticleData.AccumulatedForces = Vector2.zero;
	}
}


