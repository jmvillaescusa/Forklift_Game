using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCount : MonoBehaviour
{
    public bool test;
    public List<GameObject> boxes;
    private GameObject tempBox;

    private int boxCount;

    // Start is called before the first frame update
    void Start()
    {
        boxCount = 0;
        if (boxes == null)
        {
            boxes = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(boxCount);
        foreach (GameObject box in boxes) {

        }
    }
}