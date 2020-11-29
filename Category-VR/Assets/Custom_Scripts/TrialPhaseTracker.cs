using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script just holds the TrialPhase Variable for other scripts to reference and alter as needed throughout the experiment

public class TrialPhaseTracker : MonoBehaviour
{
    public int CurrentTrialPhase;

    public Material CorrectChoice;
    public Material IncorrectChoice;
    public Material NoChoice;
    public Material CorrectChoicePlatform;
    public Material IncorrectChoicePlatform;
    public Material NoChoicePlatform;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTrialPhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
