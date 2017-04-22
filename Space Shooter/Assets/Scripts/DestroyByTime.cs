using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    //Public vars
    public float LifeTime;

    //Private vars

    /***************Functions***************/

    /***************Private***************/
    private void Start()
    {
        Destroy(gameObject, LifeTime);
    } //End private void Start()

} //End public class NewBehaviourScript : MonoBehaviour
