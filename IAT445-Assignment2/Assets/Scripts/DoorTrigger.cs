using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorAnimator door;

    void OnTriggerEnter(Collider other)
    {
        door.OpenDoor();
        Debug.Log("EnterCollider");
    }

    private void OnTriggerExit(Collider other)
    {
        door.CloseDoor();
        Debug.Log("ExitCollider");
    }
}
