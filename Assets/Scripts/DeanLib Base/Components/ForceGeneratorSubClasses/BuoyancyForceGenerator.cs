using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BuoyancyForceGenerator : ForceGenerator2D
{
    public void Initialize(float liquidDensity, float surfaceLevel, float maxDepth)
    {
        mLiquidDensity = liquidDensity;
        mSurfaceLevel = surfaceLevel;
        mMaxDepth = maxDepth;
    }
    public override void UpdateForce(GameObject theObject)
    {
        Vector2 diff = Vector2.zero;
        if (theObject.transform.position.y > mSurfaceLevel + mMaxDepth)
        {
            return;
        }
        if(theObject.transform.position.y <= mSurfaceLevel - mMaxDepth)
        {
            diff.y = mLiquidDensity * 1;
        }
        else if(theObject.transform.position.y <= mSurfaceLevel)
        {
            diff.y = mLiquidDensity * 1 * ((theObject.transform.position.y - mMaxDepth - mSurfaceLevel) / (2 * mMaxDepth));
            
        }
        theObject.GetComponent<Particle2D>().AccumulatedForces += diff;
    }

    private float mLiquidDensity;
    private float mSurfaceLevel;
    private float mMaxDepth;
}