using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongScript : MonoBehaviour
{ 
    public float speed = 1f;
    public float dist = 10;
    private Vector3 initialPosition;

    float y;
    float z;

    private void Start()
    {
        initialPosition = transform.position;
        speed *= 0.01f;

        y = Mathf.Clamp(transform.position.y, -10, 10);
        z = Mathf.Clamp(transform.position.z, -10, 10);
    }
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position)>= dist)
        {
            speed *= -1;
        }

        Particle2D particle = gameObject.GetComponent<Particle2D>();
        if (particle)
        {
            particle.Velocity = new Vector2(0, 0);
        }

        transform.position = new Vector3(transform.position.x + speed, y, z);

        y = Mathf.Clamp(transform.position.y, -10, 10);
        z = Mathf.Clamp(transform.position.z, -10, 10);
    }
}
