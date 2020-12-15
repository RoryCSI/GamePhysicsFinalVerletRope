using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellGenerator : ForceGenerator2D
{
    public void Initialize(Vector2 point, float magnitude, bool isAttractive)
    {
        mPoint = point;
        mMagnitude = magnitude;
        mIsAttractiveForce = isAttractive;
    }
    public override void UpdateForce(GameObject theObject)
    {
        Vector2 diff;
        if (mIsAttractiveForce)
        {
           diff = mPoint - (Vector2)theObject.transform.position;
        }
        else 
        {
           diff = (Vector2)theObject.transform.position- mPoint;
        }
        float range = 10;
        float dist = Vector2.Distance(mPoint, theObject.transform.position);
        if (dist < range)
        {
            float proportionAway = dist / range;
            proportionAway = 1 - proportionAway;
            diff.Normalize();

            theObject.GetComponent<Particle2D>().AccumulatedForces += (diff * (mMagnitude * proportionAway));
        }
    }

    private bool mIsAttractiveForce;
    private Vector2 mPoint;
    private float mMagnitude;
}