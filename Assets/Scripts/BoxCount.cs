using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCount : MonoBehaviour
{
    public BoxCollision Box;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Box.onSkid)
        {
            Debug.Log("test skid");
        }
        else
        {
            Debug.Log("test");
        }
    }
}