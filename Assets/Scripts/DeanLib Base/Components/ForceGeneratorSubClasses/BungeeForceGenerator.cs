using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeForceGenerator : ForceGenerator2D
{
    public void Initialize(GameObject object1, Vector2 anchorPoint, float springConstant, float restLength)
    {
        mObject1 = object1;
        mAnchorPoint = anchorPoint;
        mSpringConstant = springConstant;
        mRestLength = restLength;
    }
    public override void UpdateForce(GameObject theObject)
    {
        Vector2 diff = (Vector2)theObject.transform.position - mAnchorPoint;
        float dist = Vector2.Distance(theObject.transform.position, mAnchorPoint);

        if (dist <= mRestLength)
        {
            return;
        }
        float magnitude = dist - mRestLength;
        magnitude *= mSpringConstant;

        diff.Normalize();
        diff *= -magnitude;

        theObject.GetComponent<Particle2D>().AccumulatedForces += diff;
    }

    private GameObject mObject1;
    private Vector2 mAnchorPoint;
    private float mSpringConstant;
    private float mRestLength;
}
