using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercapture : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
            Destroy(gameObject);
            Debug.Log("touching");
    }    
}
