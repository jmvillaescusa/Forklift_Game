using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform forklift;
    private float speed = 70;

    private int zoom = 20;
    private int normal = 60;
    private float smooth = 2f;

    private bool isZoomed = false;
    public new Camera camera;

    private void Update()
    {
        transform.position = new Vector3(forklift.position.x, forklift.position.y + 6.5f, forklift.position.z); 

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
        }

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth); 
        }
    }
}
