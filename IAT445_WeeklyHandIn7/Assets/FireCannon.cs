using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour
{

    public Transform spawnPoint;
    public Rigidbody Munition;
    public float velocity = 3000;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody CannonBase;
            // Spawns Cannon Ball
            CannonBase = Instantiate(Munition, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
            CannonBase.AddForce(spawnPoint.forward * velocity);
        }
    }
}