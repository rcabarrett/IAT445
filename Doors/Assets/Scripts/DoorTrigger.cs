using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorAnimator door;

    void OnTriggerEnter(Collider other)
    {
        door.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        door.CloseDoor();
    }
}
