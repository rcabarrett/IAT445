using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateTeleportation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RaiseRings()
    {
        animator.SetBool("PlayerOnPlatform", true);
    }

    public void LowerRings()
    {
        animator.SetBool("PlayerOnPlatform", false);
    }

}
