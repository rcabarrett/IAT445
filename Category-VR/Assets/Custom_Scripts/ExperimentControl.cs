using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentControl : MonoBehaviour
{
    public static ExperimentControl control;

    public float triallvl_rowid;
    public float explvl_rowid;
    public float SubjectID;
    public float Condition;
    public float trialID;
    public float TrialPhase;
    public float MaxNumberofTrials;


    // Start is called before the first frame update
    void Awake()
    {
        // Checks to make sure this is the only Research Assistant Present in Scene
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Subject ID " + SubjectID);
        GUI.Label(new Rect(10, 10, 100, 30), "Trial ID " + trialID);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
