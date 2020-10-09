using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potisfull : MonoBehaviour
{
    public GameObject beanspawn;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(beanspawn.gameObject);
    }
}
