using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	// Start is called before the first frame update
	private static ParticleManager instance;

	public static ParticleManager Instance { get { return instance; } }

	public List<Particle2D> mParticles = new List<Particle2D>();
	List<Particle2D> mParticlesToDelete = new List<Particle2D>();

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

	private void Start()
	{
		Particle2D[] particles = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D));
		foreach (Particle2D particle in particles)
		{
			mParticles.Add(particle);
		}
	}

	// Update is called once per frame
	//void Update()
	//{
		
	//	foreach (Particle2D particle in mParticles)
	//	{
	//		foreach (Particle2D otherParticle in mParticles)
	//		{
	//			if (CollisionDetector.DetectCollision(particle, otherParticle))
	//			{
	//				if(particle != otherParticle)
 //                   {
	//					mParticlesToDelete.Add(particle);
	//					mParticlesToDelete.Add(otherParticle);
	//				}
	//			}
	//		}
	//	}
	//	foreach (Particle2D particle in mParticlesToDelete)
	//	{
	//		DeleteParticle(particle);
	//	}
	//	mParticlesToDelete.Clear();
	//}
	public void DeleteParticle(Particle2D particle)
    {
		mParticles.Remove(particle);
		Destroy(particle.gameObject);
    }
}
