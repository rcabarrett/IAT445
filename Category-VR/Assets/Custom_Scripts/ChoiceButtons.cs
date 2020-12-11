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
        private int TutorialTrial;

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
                    
                    if (CurrentStimulusCategory == "TutorialCube")
                    {
                        if (GameObject.Find("ResearchAssistant").GetComponent<Tutorial>().trialID == 2)
                        {
                            // Turn Red
                            GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoice;
                            GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoicePlatform;
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectSound, transform.position);
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<Tutorial>().Tutorial5, transform.position);

                            foreach (GameObject otherbutton in OtherChoiceButtons)
                            {
                                otherbutton.GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoice;
                            }
                        }
                        else
                        {
                            // Turn Green
                            GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoice;
                            GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectChoicePlatform;
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectSound, transform.position);
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<Tutorial>().Tutorial3, transform.position);

                        }

                        // Update Output Tables
                        GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase = 4;
                    }
                    else
                    {
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
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CorrectSound, transform.position);
                        }
                        else // Incorrect Choice - Proceed to Feedback
                        {
                            // Update Output Tables
                            GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase = 4;
                            GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().IsCorrect = 0;

                            // Turn Red
                            GetComponentInChildren<Renderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoice;
                            GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectChoicePlatform;
                            AudioSource.PlayClipAtPoint(GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().IncorrectSound, transform.position);
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
}