using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupCapsules : MonoBehaviour
{
    private int count;
    public TextMeshProUGUI counter;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        
    }

    void SetCountText()
    {
        counter.text = "You have " + count.ToString() + " power cores.";
    }
}
