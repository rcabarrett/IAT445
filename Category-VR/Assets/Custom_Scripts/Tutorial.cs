using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem.Sample;

public class Tutorial : MonoBehaviour
{
    public static DataExtractor UpdateData;

    // Logical Indices
    public float is_newtrial;
    private float TrialStartTime;

    // ExpLvl Variables
    public float Explvl_IsEndofExp = 0;

    // TrialLvl Variables: RowID, TrialID, SubjectID
    public int trialID = 0;
    public int TrialPhase;
    public string PlayerResponse;
    public string CorrectResponse;
    public int IsCorrect;
    public int MaxNumberofTrials = 10;

    [SerializeField] private string sceneName;
    public AudioClip Tutorial1;
    public AudioClip Tutorial2;
    public AudioClip Tutorial3;
    public AudioClip Tutorial4;
    public AudioClip Tutorial5;

    private void Start()
    {
        GameObject.Find("Next").GetComponent<InputTester>().IsTutorial = 1;

        Invoke("StartTutorial", 2f);
    }

    void StartTutorial()
    {
        AudioSource.PlayClipAtPoint(Tutorial1, transform.position);
    }

    // Update is called once per frame
    private void Update ()
    {
        if (is_newtrial == 1) //Is called at the end of every trial
        {
            // NOTE: This eventually needs to reference previous rowid to allow for multiple exps to exist in one table
            trialID++;
            is_newtrial = 0;


            if (trialID > MaxNumberofTrials)
            {
                GameObject.Find("LevelChanger").GetComponent<LevelChange>().LevelConditionsMet = 1;
            }
            if (trialID == 1)
            {
                AudioSource.PlayClipAtPoint(Tutorial2, transform.position);
            }
            if (trialID == 2)
            {
                AudioSource.PlayClipAtPoint(Tutorial4, transform.position);
            }
        }

        TrialPhase = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase;

    }

}

