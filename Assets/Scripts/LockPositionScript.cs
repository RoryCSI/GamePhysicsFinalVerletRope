using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPositionScript : MonoBehaviour
{
    float x;
    float y;
    float z;

    public bool lockToMouse = false;

    private void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        Particle2D particle = gameObject.GetComponent<Particle2D>();
        if (particle)
        {
            particle.Velocity = new Vector2(0, 0);
        }
        if (lockToMouse && Input.GetMouseButton(0))
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) < 2f)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, z);
                x = transform.position.x;
                y = transform.position.y;
                z = transform.position.z;
            }
        }
        else
        {
            transform.position = new Vector3(x, y, z);
        }
    }


}