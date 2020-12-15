using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{
	private static ContactResolver instance;

	public static ContactResolver Instance { get { return instance; } }

	List<Particle2DContact> mContacts = new List<Particle2DContact>();
	List<Particle2DLink> mDeadLinks = new List<Particle2DLink>();
	public List<Particle2DLink> mParticleLinks = new List<Particle2DLink>();

	int mIterations;
	int mIterationsUsed = 0;

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
		List<GameObject> rope1Segments = new List<GameObject>();
		GameObject rope1 = GameObject.Find("Rope1");
		if (rope1)
        {
			foreach (Transform child in rope1.transform)
			{
				if (child != rope1.GetComponent<Transform>())
				{
					rope1Segments.Add(child.gameObject);
				}
			}
			for (int i = 0; i < rope1Segments.Count - 1; i++)
			{
				ParticleCable newParticleRod1 = rope1.AddComponent<ParticleCable>();
				newParticleRod1.Initialize(rope1Segments[i], rope1Segments[i + 1], 2, 1f);
				mParticleLinks.Add(newParticleRod1);
			}
		}
		

		List<GameObject> rope2Segments = new List<GameObject>();
		GameObject rope2 = GameObject.Find("Rope2");
		if (rope2)
        {
			foreach (Transform child in rope2.transform)
			{
				if (child != rope2.GetComponent<Transform>())
				{
					rope2Segments.Add(child.gameObject);
				}
			}
			for (int i = 0; i < rope2Segments.Count - 1; i++)
			{
				ParticleCable newParticleRod2 = rope2.AddComponent<ParticleCable>();
				newParticleRod2.Initialize(rope2Segments[i], rope2Segments[i + 1], 2, 1f);
				mParticleLinks.Add(newParticleRod2);
			}
		}
	}
    private void Update()
    {
		foreach (Particle2DLink ParticleLink in mParticleLinks)
		{
			if (ParticleLink == null)
			{
				mDeadLinks.Add(ParticleLink);
			}
			else
            {
				ParticleLink.CreateContacts(mContacts);
			}
        }
		foreach (Particle2DLink linkToRemove in mDeadLinks)
        {
			if (linkToRemove != null)
            {
				mParticleLinks.Remove(linkToRemove);
            }
        }
		mDeadLinks.Clear();

		ResolveContacts(mContacts, 10);
		foreach (Particle2DContact contact in mContacts)
        {
			Destroy(contact);
        }
		mContacts.Clear();
    }

    public void ResolveContacts(List<Particle2DContact> contacts, int iterations)
    {
		mIterationsUsed = 0;
		while (mIterationsUsed < iterations)
        {
			float max = float.MaxValue;
			int numContacts = contacts.Count;
			int maxIndex = numContacts;
			for (int i = 0; i < numContacts; i++)
            {
				float sepVel = contacts[i].CalculateSeparatingVelocity();
				if (sepVel < max && (sepVel < 0.0f || contacts[i].mPenetration > 0.0f))
                {
					max = sepVel;
					maxIndex = i;
                }
            }
			if (maxIndex == numContacts)
            {
				break;
            }

			contacts[maxIndex].Resolve();

			for (int i = 0; i < numContacts; i++)
            {
				if (contacts[i].mObject1 == contacts[maxIndex].mObject1)
                {
					contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
				}
				else if (contacts[i].mObject1 == contacts[maxIndex].mObject2)
                {
					contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
				}

				if (contacts[i].mObject2)
                {
					if (contacts[i].mObject2 == contacts[maxIndex].mObject1)
					{
						contacts[i].mPenetration += Vector2.Dot(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
					}
					else if (contacts[i].mObject2 == contacts[maxIndex].mObject2)
					{
						contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
					}
				}

			}
			mIterationsUsed++;
        }
    }
}
