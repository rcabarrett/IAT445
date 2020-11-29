using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem.Sample
{
    [RequireComponent(typeof(Interactable))]
    public class ChoiceButtons : MonoBehaviour
    {
        public SteamVR_Action_Boolean TriggerClick;
        private SteamVR_Input_Sources inputSource;

        private string CurrentStimulusCategory;
        public GameObject[] OtherChoiceButtons;
        public GameObject ThisGameObject;
        private string ThisChoiceButton;

        private void Start()
        {
            ThisChoiceButton = ThisGameObject.tag;
        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

            if (startingGrabType != GrabTypes.None)
            {
                if (GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase == 2)
                {
                    // Check Current Category
                    CurrentStimulusCategory = GameObject.Find("Next").GetComponent<InputTester>().CurrentStimulus.tag;
                    // Update Output Table
                    GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().CorrectResponse = CurrentStimulusCategory;
                    GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().PlayerResponse = ThisGameObject.tag;
                    GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().TimeofChoiceMade = Time.time;


                    if (ThisChoiceButton == CurrentStimulusCategory) // Correct Choice - Proceed to Feedback
                    {
                        // Update Output Tables
                        GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase = 4;
                        GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().IsCorrect = 1;

                        // Turn Green
                        GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoice;
                        GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoicePlatform;
                    }
                    else // Incorrect Choice - Proceed to Feedback
                    {
                        // Update Output Tables
                        GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase = 4;
                        GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().IsCorrect = 0;

                        // Turn Red
                        GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoice;
                        GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoicePlatform;
                        // Turn Correct Button Green
                        foreach (GameObject otherbutton in OtherChoiceButtons)
                        {
                            if (otherbutton.tag == CurrentStimulusCategory)
                            {
                                otherbutton.GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoice;
                            }
                        }
                    }

                }
            }
        }
    }
}