using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayGoodbye : MonoBehaviour
{
    public AudioClip Goodbye;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoodbyeSound", 1f);
    }

    void GoodbyeSound()
    {
        AudioSource.PlayClipAtPoint(Goodbye, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
