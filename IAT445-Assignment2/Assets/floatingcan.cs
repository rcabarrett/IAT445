using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingcan : MonoBehaviour
{
    public float hoverforce;
    public GameObject can;


    void OnTriggerStay(Collider can)
    {
        can.attachedRigidbody.AddForce(Vector3.up * hoverforce, ForceMode.Acceleration);
    }
}