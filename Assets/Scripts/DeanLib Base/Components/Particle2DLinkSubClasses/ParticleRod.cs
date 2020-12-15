using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ParticleRod : Particle2DLink
{
    float mLength;

    public void Initialize(GameObject object1, GameObject object2, float length)
    {
        mObject1 = object1;
        mObject2 = object2;
        mLength = length;
    }
    public override void CreateContacts(List<Particle2DContact> contacts)
    {
        if (mObject1 == null || mObject2 == null)
        {
            Destroy(gameObject);
            return;
        }
        float penetration = 0.0f;
        float length = Vector2.Distance(mObject1.transform.position, mObject2.transform.position);
        if (length == mLength)
        {
            return;
        }

        Vector2 normal = mObject2.transform.position - mObject1.transform.position;
        normal.Normalize();

        if (length > mLength)
        {
            penetration = length - mLength;
        }
        else
        {
            normal *= -1.0f;
            penetration = mLength - length;
        }
        penetration *= 0.01f;


        Particle2DContact newParticleContact = gameObject.AddComponent<Particle2DContact>();
        newParticleContact.Initialize(mObject1, mObject2, 0.0f, penetration, normal, Vector2.zero, Vector2.zero);
        contacts.Add(newParticleContact);
    }
}
