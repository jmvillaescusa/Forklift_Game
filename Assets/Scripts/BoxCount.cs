using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            //Debug.Log("Test");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            //Debug.Log("Test 2");
        }
    }
}