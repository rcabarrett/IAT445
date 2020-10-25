using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;

    [SerializeField] private DoorAnimator door;


    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        door.OpenDoor();
    }

    public void CloseDoor()
    {
        door.CloseDoor();
    }
}
