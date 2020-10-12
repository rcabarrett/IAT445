using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
        Debug.Log("OpenDoor");
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
        Debug.Log("CloseDoor");
    }

}
