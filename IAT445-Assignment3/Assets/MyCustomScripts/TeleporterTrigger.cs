using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterTrigger : MonoBehaviour
{
    float CountDown = 0;
    float Timer = 5;

    // Where is this teleporter taking us?
    [SerializeField] private string sceneName;


    private void Update()
    {
        if (CountDown == 1)
        {
            // Start Count Down to teleportation
            Timer -= Time.deltaTime * 1f;
            Debug.Log(Timer);
        }

        if (Timer < 0)
        {
            Debug.Log("Loading Scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

    [SerializeField] private InitiateTeleportation Teleport;

    void OnTriggerEnter(Collider other)
    {
        // Activate Teleporter Animation
        Teleport.RaiseRings();

        // Initiate CountDown
        CountDown = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        Teleport.LowerRings();
        // If the player steps off the teleporter, reset the count down
        CountDown = 0;
        Timer = 5;
    }
}
