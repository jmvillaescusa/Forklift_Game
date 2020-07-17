using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform forklift;
    public float distance;
    public float height;
    public float rotationDamping = 3.0f;
    public float heightDamping = 2.0f;
    private float desiredAngle = 0;

    private void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        desiredAngle = forklift.eulerAngles.y;
        float desiredHeight = forklift.position.y + height;

        currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);

        Vector3 finalPosition = forklift.position - (currentRotation * Vector3.forward * distance);
        finalPosition.y = currentHeight;
        transform.LookAt(forklift);
        transform.position = finalPosition;
    }

    private void FixedUpdate()
    {
        desiredAngle = forklift.eulerAngles.y;

        Vector3 localVelocity = forklift.InverseTransformDirection(forklift.GetComponent<Rigidbody>().velocity);
        if (localVelocity.z < -0.5f)
        {
            desiredAngle += 180;
        }
    }
}
