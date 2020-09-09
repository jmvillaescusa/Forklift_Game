using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftController : MonoBehaviour
{
    public Transform arms;
    public Transform armExt;
    public Transform forklift;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public Transform wheelTransformFL;
    public Transform wheelTransformFR;
    public Transform wheelTransformRL;
    public Transform wheelTransformRR;

    private float armsMinYPos;
    private float armsMaxYPos;
    private float maxBreakTorque = 100;
    private float topSpeed = 20;
    [SerializeField] private float currentSpeed;
    private float decelerationTorque = 30;
    private float spoilerRatio = 1f;
    private float maxTurnAngle = 50;
    private float maxTorque = 300;
    private Vector3 centerOfMassAdjustment = new Vector3(0f, -1f, 0f);
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass += centerOfMassAdjustment;

        armsMinYPos = arms.transform.position.y;
        armsMaxYPos = arms.transform.position.y + 3.7f;
    }

    private void Update()
    {
        UpdateWheelPositions();

        float rotationThisFrame = 360 * Time.deltaTime;
        wheelTransformFL.Rotate(0, -wheelFL.rpm / rotationThisFrame, 0);
        wheelTransformFR.Rotate(0, -wheelFR.rpm / rotationThisFrame, 0);
        wheelTransformRL.Rotate(0, -wheelRL.rpm / rotationThisFrame, 0);
        wheelTransformRR.Rotate(0, -wheelRR.rpm / rotationThisFrame, 0);

        if (Input.GetKey(KeyCode.E) && arms.transform.localPosition.y <= armsMaxYPos)
        {
            arms.Translate(Vector3.up * Time.deltaTime);
            if (arms.transform.localPosition.y > armExt.transform.localPosition.y + 0.5f)
            {
                armExt.Translate(Vector3.up * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.R) && arms.transform.localPosition.y >= armsMinYPos)
        {
            arms.Translate(-Vector3.up * Time.deltaTime);
            if (arms.transform.localPosition.y > 2.3f)
            {
                armExt.Translate(-Vector3.up * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        //currentSpeed = wheelRL.radius * wheelRL.rpm * Mathf.PI * 0.12f;
        //if (currentSpeed < topSpeed)
        //{
        //    wheelRL.motorTorque = Input.GetAxis("Vertical") * maxTorque;
        //    wheelRR.motorTorque = Input.GetAxis("Vertical") * maxTorque;
        //}
        //else
        //{
        //    wheelRL.motorTorque = 0;
        //    wheelRR.motorTorque = 0;
        //}

        Vector3 localVelocity = transform.InverseTransformDirection(body.velocity);
        body.AddForce(-transform.up * (localVelocity.z * spoilerRatio), ForceMode.Impulse);

        wheelFL.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
        wheelFR.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;

        wheelRL.motorTorque = Input.GetAxis("Vertical") * maxTorque;
        wheelRR.motorTorque = Input.GetAxis("Vertical") * maxTorque;

        if (Input.GetButton("Jump"))
        {
            wheelFL.brakeTorque = maxBreakTorque;
            wheelFR.brakeTorque = maxBreakTorque;
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
        }

        if (Input.GetAxis("Vertical") <= -0.5f && localVelocity.z > 0)
        {
            wheelRL.brakeTorque = decelerationTorque + maxTorque;
            wheelRR.brakeTorque = decelerationTorque + maxTorque;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            wheelRL.brakeTorque = decelerationTorque;
            wheelRR.brakeTorque = decelerationTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    private void UpdateWheelPositions()
    {
        WheelHit contact = new WheelHit();

        if (wheelFL.GetGroundHit(out contact))
        {
            Vector3 temp = wheelFL.transform.position;
            temp.y = (contact.point + (wheelFL.transform.up * wheelFL.radius)).y;
            wheelTransformFL.position = temp;
            Debug.Log("Test FL");
        }

        if (wheelFR.GetGroundHit(out contact))
        {
            Vector3 temp = wheelFR.transform.position;
            temp.y = (contact.point + (wheelFR.transform.up * wheelFR.radius)).y;
            wheelTransformFR.position = temp;
            Debug.Log("Test FR");
        }

        if (wheelRL.GetGroundHit(out contact))
        {
            Vector3 temp = wheelRL.transform.position;
            temp.y = (contact.point + (wheelRL.transform.up * wheelRL.radius)).y;
            wheelTransformRL.position = temp;
            Debug.Log("Test RL");
        }

        if (wheelRR.GetGroundHit(out contact))
        {
            Vector3 temp = wheelRR.transform.position;
            temp.y = (contact.point + (wheelRR.transform.up * wheelRR.radius)).y;
            wheelTransformRR.position = temp;
            Debug.Log("Test RR");
        }
    }
}