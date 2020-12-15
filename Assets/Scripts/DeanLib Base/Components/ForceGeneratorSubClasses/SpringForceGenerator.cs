using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpringForceGenerator : ForceGenerator2D
{
    public void Initialize(GameObject object1, GameObject object2, float springConstant, float restLength)
    {
        mObject1 = object1;
        mObject2 = object2;
        mSpringConstant = springConstant;
        mRestLength = restLength;
    }
    public override void UpdateForce(GameObject theObject)
    {
        if (mObject1 == null || mObject2 == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 diff = mObject1.transform.position - mObject2.transform.position;
        float dist = Vector2.Distance(mObject1.transform.position, mObject2.transform.position);

        float magnitude = dist - mRestLength;
        magnitude *= mSpringConstant;

        diff.Normalize();
        diff *= -magnitude;

        mObject1.GetComponent<Particle2D>().AccumulatedForces += diff;
        mObject2.GetComponent<Particle2D>().AccumulatedForces -= diff;
    }

    private GameObject mObject1;
    private GameObject mObject2;
    private float mSpringConstant;
    private float mRestLength;
}