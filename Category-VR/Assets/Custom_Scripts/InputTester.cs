using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;
using System.Linq;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    [RequireComponent(typeof(Interactable))]
    public class InputTester : MonoBehaviour
    {
        // Used Locally for verifying interaction
        public SteamVR_Action_Boolean TriggerClick;
        private SteamVR_Input_Sources inputSource;

        // Used for setting up Stimulus Presentation Order
        public GameObject[] randomObjects;
        public GameObject SpawnPointMarker;
        private Vector3 SpawnPointCoordinates;
        List<GameObject> pool;
        public int poolAmount;
        int currentIndex = 0;
        private GameObject previousStimulus = null;

        // Used for Global Tracking of Experimental Status
        public GameObject CurrentStimulus = null;
        private MeshRenderer[] ButtonMeshes;
        private object OrderofStimuli;

        private void Start()
        {
            pool = new List<GameObject>();
            for (int i = 0; i < poolAmount; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, randomObjects.Length);
                GameObject obj = (GameObject)Instantiate(randomObjects[randomIndexi]);
                obj.SetActive(false);
                pool.Add(obj);
            }

            
            ////var rnd = new System.Random();
            ////var result = pool.OrderBy(item => rnd.Next());

            ////foreach (var item in result)
            ////{
            ////    Console.WriteLine(item.name);
            ////}

            Vector3 SpawnPointCoordinates = SpawnPointMarker.transform.position;
        }


        private IEnumerator StartNextTrial()
        {
            GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase = 2;
            GameObject.Find("ResearchAssistant").GetComponent<DataExtractor>().is_newtrial = 1;

            // reset colour of buttons and platform
            ButtonMeshes = GameObject.Find("Choices").GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer material in ButtonMeshes)
                material.material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().NoChoice;
            
            GameObject.Find("Glow").GetComponent<MeshRenderer>().material = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().NoChoicePlatform;


            // get rid of previous stimulus
            if (previousStimulus != null)
            {
                previousStimulus.gameObject.SetActive(false);
                Destroy(previousStimulus);
            }

            // Used locally for removing from play at next trial
            previousStimulus = pool[currentIndex];

            // Used Globally to track current stimulus object
            CurrentStimulus = pool[currentIndex];

            pool[currentIndex].transform.position = SpawnPointMarker.transform.position;
            pool[currentIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
            pool[currentIndex].transform.localScale = new Vector3(30, 30, 30);
            pool[currentIndex].SetActive(true);

            if (++currentIndex >= pool.Count)// last issue
            {
                currentIndex = 0;
                pool.Clear();
                for (int a = 0; a < poolAmount; a++)
                {
                    int randomIndex = Random.Range(0, randomObjects.Length);
                    GameObject obj = (GameObject)Instantiate(randomObjects[randomIndex]);
                    obj.SetActive(false);
                    pool.Add(obj);
                }

            }

            yield return null;

        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

            if (startingGrabType != GrabTypes.None)
            {
                // 4 = feedback phase, 0 = first trial of experiment
                if (GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase == 4 || GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase == 0) 
                {
                    StartCoroutine(StartNextTrial());
                    
                }
                
            }
        }
    }
}