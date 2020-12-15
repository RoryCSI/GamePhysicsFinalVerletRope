using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletRopeAttachedScript : MonoBehaviour
{
    public GameObject attachment1;
    public GameObject attachment2;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    private float segmentLength = 0.5f;
    private int totalSegments = 45;
    private float lineWidth = 0.2f;

    public int stiffness = 50;

    public struct RopeSegment
    {
        public Vector2 currentPos;
        public Vector2 oldPos;

        public RopeSegment(Vector2 pos)
        {
            currentPos = pos;
            oldPos = pos;
        }
    }

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < totalSegments; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= segmentLength;
        }
    }

    void Update()
    {
        DrawRope();
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        Vector2 gravity = new Vector2(0f, -1.0f);

        for (int i = 1; i < totalSegments; i++)
        {
            RopeSegment segment1 = ropeSegments[i];

            Vector3 newPos = segment1.currentPos + (segment1.currentPos - segment1.oldPos) + (gravity * Time.fixedDeltaTime);
            segment1.oldPos = segment1.currentPos;
            segment1.currentPos = newPos;

            ropeSegments[i] = segment1;
        }

        for (int i = 0; i < stiffness; i++)
        {
            ApplyConstraints();
        }
    }

    private void ApplyConstraints()
    {
        //First Rope segment follows mouse
        RopeSegment startSegment = ropeSegments[0];
        startSegment.currentPos = attachment1.transform.position;
        ropeSegments[0] = startSegment;

        RopeSegment endSegment = ropeSegments[totalSegments-1];
        endSegment.currentPos = attachment2.transform.position;
        ropeSegments[totalSegments-1] = endSegment;


        //for loop to apply to all segments
        for (int i = 0; i < totalSegments - 1; i++)
        {
            RopeSegment segment1 = ropeSegments[i];
            RopeSegment segment2 = ropeSegments[i + 1];

            //Check Collisions with particle2Ds.
            foreach (Particle2D particle in ParticleManager.Instance.mParticles)
            {
                segment1.currentPos = CircleCollisionConstraint(segment1, particle.gameObject);
            }


            //Actual rope behavior constraints
            ////////////////////////////////////////////////////////////////////////////////
            //Directly from Jasoni Tutorial: https://www.youtube.com/watch?v=FcnvwtyxLds
            float dist = (segment1.currentPos - segment2.currentPos).magnitude;
            float error = Mathf.Abs(dist - segmentLength);
            Vector2 changeDir = Vector2.zero;

            if (dist > segmentLength)
            {
                changeDir = (segment1.currentPos - segment2.currentPos).normalized;
            }
            else if (dist < segmentLength)
            {
                changeDir = (segment2.currentPos - segment1.currentPos).normalized;
            }
            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                segment1.currentPos -= changeAmount * 0.5f;
                ropeSegments[i] = segment1;
                segment2.currentPos += changeAmount * 0.5f;
                ropeSegments[i + 1] = segment2;
            }
            else
            {
                segment2.currentPos += changeAmount;
                ropeSegments[i + 1] = segment2;
            }
            ////////////////////////////////////////////////////////////////////////////////
        }
    }

    private Vector3 CircleCollisionConstraint(RopeSegment segment, GameObject collidedCircle)
    {
        Particle2D particle = collidedCircle.GetComponent<Particle2D>();
        SpriteRenderer spriteRenderer = collidedCircle.GetComponent<SpriteRenderer>();
        float distance = Vector2.Distance((Vector2)collidedCircle.transform.position, segment.currentPos);

        if (distance - (spriteRenderer.bounds.size.x / 2) < 0)
        {
            Vector2 dir = (segment.currentPos - (Vector2)collidedCircle.transform.position).normalized;
            Vector2 hitPos = (Vector2)collidedCircle.transform.position + dir * (spriteRenderer.bounds.size.x / 2);

            if (!particle.shouldIgnoreForces)
            {
                particle.Velocity -= (hitPos - segment.currentPos) * 0.1f;
            }
            return hitPos;
        }
        else
        {
            return segment.currentPos;
        }
    }

    //////////////////////////////////////////////////////////////////////////////
    //Directly from Jasoni Tutorial: https://www.youtube.com/watch?v=FcnvwtyxLds
    private void DrawRope()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[totalSegments];
        for (int i = 0; i < totalSegments; i++)
        {
            ropePositions[i] = ropeSegments[i].currentPos;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
    //////////////////////////////////////////////////////////////////////////////
}