using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    //Public vars
    public float tumble;

    //Private vars
    private Rigidbody rb;

    /***************Functions***************/

    /***************Private***************/
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
	} //End void Start()
} //End public class RandomRotator : MonoBehaviour
