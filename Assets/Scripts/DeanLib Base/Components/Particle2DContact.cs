using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    public GameObject mObject1;
    public GameObject mObject2;
    public float mRestitution;
    public float mPenetration;
    public Vector2 mContactNormal;
    public Vector2 mMove1;
    public Vector2 mMove2;

    public void Initialize(GameObject object1, GameObject object2, float restitution, float penetration, Vector2 ContactNormal, Vector2 Move1, Vector2 Move2)
    {
        mObject1 = object1;
        mObject2 = object2;
        mRestitution = restitution;
        mPenetration = penetration;
        mContactNormal = ContactNormal;
        mMove1 = Move1;
        mMove2 = Move2;
    }    
    public void Resolve()
    {
        ResolveVelocity();
        ResolveInterpenetration();
    }    
    public float CalculateSeparatingVelocity()
    {
        Vector2 relativeVel = mObject1.GetComponent<Particle2D>().Velocity;
        if (mObject2)
        {
            relativeVel -= mObject2.GetComponent<Particle2D>().Velocity;
        }
        return Vector2.Dot(relativeVel, mContactNormal);
    }

    void ResolveVelocity()
    {
        float separatingVel = CalculateSeparatingVelocity();
        if (separatingVel > 0.0f)
        {
            return;
        }

        float newSepVel = -separatingVel * mRestitution;

        Vector2 velFromAcc = mObject1.GetComponent<Particle2D>().Acceleration;
        if (mObject2)
        {
            velFromAcc -= mObject2.GetComponent<Particle2D>().Acceleration;
        }

        float accCausedSepVelocity = Vector2.Dot(velFromAcc, mContactNormal) * Time.deltaTime;

        if (accCausedSepVelocity < 0.0f)
        {
            newSepVel += mRestitution * accCausedSepVelocity;
            if (newSepVel < 0.0f)
            {
                newSepVel = 0.0f;
            }
        }

        float deltaVel = newSepVel - separatingVel;

        float totalInverseMass = (float)(1.0 / mObject1.GetComponent<Particle2D>().Mass);
        if (mObject2)
        {
            totalInverseMass += (float)(1.0 / mObject2.GetComponent<Particle2D>().Mass);
        }
        if (totalInverseMass <= 0)
        {
            return;
        }

        float impulse = deltaVel / totalInverseMass;
        Vector2 impulsePerIMass = mContactNormal * impulse;

        Vector2 newVelocity = mObject1.GetComponent<Particle2D>().Velocity + impulsePerIMass * (float)(1.0 / mObject1.GetComponent<Particle2D>().Mass);
        mObject1.GetComponent<Particle2D>().Velocity = newVelocity;
        if (mObject2)
        {
            Vector2 newVelocity2 = mObject2.GetComponent<Particle2D>().Velocity + impulsePerIMass * -(float)(1.0 / mObject2.GetComponent<Particle2D>().Mass);
            mObject2.GetComponent<Particle2D>().Velocity = newVelocity2;
        }
    }

    void ResolveInterpenetration()
    {
        if (mPenetration <= 0.0f)
        {
            return;
        }

        float totalInverseMass = (float)(1.0 / mObject1.GetComponent<Particle2D>().Mass);
        if (mObject2)
        {
            totalInverseMass += (float)(1.0 / mObject2.GetComponent<Particle2D>().Mass);
        }
        if (totalInverseMass <= 0)
        {
            return;
        }

        Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

        mMove1 = movePerIMass * (float)(1.0 / mObject1.GetComponent<Particle2D>().Mass);
        if (mObject2)
        {
            mMove2 = movePerIMass * -(float)(1.0 / mObject2.GetComponent<Particle2D>().Mass);
        }

        Vector2 newPosition = (Vector2)mObject1.transform.position + mMove1;
        mObject1.transform.position = newPosition;
        if (mObject2)
        {
            Vector2 newPosition2 = (Vector2)mObject2.transform.position + mMove2;
            mObject2.transform.position = newPosition2;
        }
    }

}
