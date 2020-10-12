using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPressurePlate : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pressure Plate Activated!");
        door.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        door.CloseDoor();
    }
}
