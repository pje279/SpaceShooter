using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    //Public vars

    //Private vars

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    } //End private void OnTriggerExit(Collider other)
} //End public class DestroyByBoundary : MonoBehaviour
