using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    private Ray ray;
    private RaycastHit rayHit;
    private float distance;
    private Vector3 direction;

    public bool onSkid;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0.1f;
        direction = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(new Vector3(transform.position.x, transform.position.y - 0.45f, transform.position.z), direction * distance);

        if (Physics.Raycast(ray, out rayHit)) {
            if (rayHit.transform.tag == "Skid" && rayHit.distance <= distance)
            {
                onSkid = true;
            }
            else if (rayHit.transform.tag == "Box")
            {

            }
            else
            {
                onSkid = false;
            }
        }
    }
}