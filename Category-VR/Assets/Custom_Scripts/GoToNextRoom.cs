using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GoToNextRoom : MonoBehaviour
{
    // Used Locally for verifying interaction
    public SteamVR_Action_Boolean TriggerClick;
    private SteamVR_Input_Sources inputSource;
    public AudioClip WelcomeMessage;

    public int SubjectID_IsSet;

    private void Start()
    {

        Invoke("Welcome", 5f);

    }

    void Welcome()
    {
        AudioSource.PlayClipAtPoint(WelcomeMessage, transform.position);
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (startingGrabType != GrabTypes.None)
        {
            if (SubjectID_IsSet == 1)
            {
                GameObject.Find("LevelChanger").GetComponent<LevelChange>().LevelConditionsMet = 1;
            }

        }
    }
}
