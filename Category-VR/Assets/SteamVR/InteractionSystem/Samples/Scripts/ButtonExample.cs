using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonExample : MonoBehaviour
    {
        // public InteractableExample hoverButton;
        public HoverButton hoverButton;

        public GameObject stimulus;
        public GameObject[] randomObjects;
        public GameObject SpawnPoint;
        List<GameObject> pool;
        public int poolAmount = 4;
        int currentIndex = 0;

        private GameObject previousStimulus = null;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);

            pool = new List<GameObject>();
            for (int i = 0; i < poolAmount; i++)
            {
                int randomIndex = Random.Range(0, randomObjects.Length);
                GameObject obj = (GameObject)Instantiate(randomObjects[randomIndex]);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        private void OnButtonDown(Hand hand)
        {
            StartCoroutine(StartNextTrial());
        }

        private IEnumerator StartNextTrial()
        {
            if (previousStimulus != null)
            {
                previousStimulus.gameObject.SetActive(false);
                Destroy(previousStimulus);
            }

            // GameObject SetStimulus = GameObject.Instantiate<GameObject>(stimulus);
            // previousStimulus = SetStimulus;
            previousStimulus = pool[currentIndex];
            // SetStimulus.transform.position = this.transform.position;
            // SetStimulus.transform.rotation = Quaternion.Euler(0, 0, 0);

            pool[currentIndex].transform.position = this.transform.position;
            pool[currentIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
            pool[currentIndex].transform.localScale = new Vector3(10,10,10);
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
                
                // pool[].gameObject.SetActive(false);
            }

            


            // Set 'object' color
            // SetStimulus.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

            yield return null;

            //Rigidbody rigidbody = planting.GetComponent<Rigidbody>();
            //if (rigidbody != null)
            //    rigidbody.isKinematic = true;

            //this is where you set the growth parameters
            // Vector3 initialScale = Vector3.one * 0.01f;
            //Vector3 targetScale = Vector3.one * (1 + (Random.value * 0.25f));

            //how long does it take for the "plant" to grow
            //float startTime = Time.time;
            //float overTime = 0.5f;
            //float endTime = startTime + overTime;

            // Here the code actually 'grows' the object to the desired size over the desired time scale
            //while (Time.time < endTime)
            //{
            //    planting.transform.localScale = Vector3.Slerp(initialScale, targetScale, (Time.time - startTime) / overTime);
            //    yield return null;
            //}


            //if (rigidbody != null)
            //    rigidbody.isKinematic = false;
        }
    }
}