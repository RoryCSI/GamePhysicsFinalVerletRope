using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCable : Particle2DLink
{
    float mMaxLength;
    float mRestitution;

    public void Initialize(GameObject object1, GameObject object2, float length, float restitution)
    {
        mObject1 = object1;
        mObject2 = object2;
        mMaxLength = length;
        mRestitution = restitution;
    }
    public override void CreateContacts(List<Particle2DContact> contacts)
    {
        float length = Vector2.Distance(mObject1.transform.position, mObject2.transform.position);
        if (length < mMaxLength)
        {
            return;
        }

        Vector2 normal = mObject2.transform.position - mObject1.transform.position;
        normal.Normalize();
        float penetration = length - mMaxLength;

        penetration *= 0.01f;

        Particle2DContact newParticleContact = gameObject.AddComponent<Particle2DContact>();
        newParticleContact.Initialize(mObject1, mObject2, 0.0f, penetration, normal, Vector2.zero, Vector2.zero);
        contacts.Add(newParticleContact);
    }
}