using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class DataExtractor : MonoBehaviour
{
    public static DataExtractor UpdateData;

    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;

    // Filepaths of Output tables
    private string gazelvl_filename = "CatVR_Gazelvl.csv"; // NOTE: Eventually, the file name will based on the subject id variable
    private string triallvl_filename = "CatVR_Triallvl.csv";
    private string explvl_filename = "CatVR_Explvl.csv";
    private string projectroot_root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Category-VR_Output");

    private string gazelvl_filepath;
    private string triallvl_filepath;
    private GameObject SubjectIDStorage;
    private string explvl_filepath;

    // Logical Indices
    public float is_newtrial;
    private float TrialStartTime;

    // ExpLvl Variables
    public float Explvl_IsEndofExp = 0;
    private float EndofExpTime;
    public float TotalAccuracy = 0;


    // TrialLvl Variables: RowID, TrialID, SubjectID
    public string SubjectID;
    public int trialID = 0;
    public int TrialPhase;
    public string PlayerResponse;
    public string CorrectResponse;
    public int IsCorrect;
    public int MaxNumberofTrials;
    public float ReactionTime;
    public float TimeofChoiceMade;
    private float TimeOnFeedback;
    private float TotalExpTime;

    // GazeLvl Variables
    private string UnderReticule;
    public float IsRelevant;
    private float CurrentTrial_TimeOnRelevant = 0;
    private float CurrentTrial_TimeOnIrrelevant = 0;
    private float CurrentTrial_TimeOnButtons = 0;
    private float CurrentTrial_TimeOnOther = 0;

    // Begin the Experiment
    void Awake()
    {
        // Get SubjectID 
        SubjectID = GameObject.Find("SubjectIDStore").GetComponent<SubjectIDStore>().SubjectID;


        // Establish Directories and Outputfiles if they are not already established
        explvl_filepath = Path.Combine(projectroot_root, explvl_filename);
        triallvl_filepath = Path.Combine(projectroot_root, triallvl_filename);
        gazelvl_filepath = Path.Combine(projectroot_root, gazelvl_filename);

        if (!Directory.Exists(projectroot_root))
        {
            Directory.CreateDirectory(projectroot_root);
        }
        if (!File.Exists(gazelvl_filepath))
        {
            Initialize_GazeLvl_Table();
        }
        if (!File.Exists(triallvl_filepath))
        {
            Initialize_TrialLvl_Table(); ;
        }
        if (!File.Exists(explvl_filepath))
        {
            Initialize_ExpLvl_Table();
        }

        // Set Logical Indices
        Explvl_IsEndofExp = 0;

        Update_ExpLvl_Table();
    }

    // Update is called once per frame
    private void Update()
    {
        if (is_newtrial == 1) //Is called at the end of every trial
        {
            if (trialID > 0)
            {
                Update_TrialLvl_Table();
                if (IsCorrect == 1)
                {
                    TotalAccuracy++;
                }
            }
            trialID++;
            is_newtrial = 0;
            TrialStartTime = Time.time;

            if (trialID > MaxNumberofTrials)
            {
                Explvl_IsEndofExp = 1;
                EndofExpTime = Time.time;

                Update_ExpLvl_Table();
                // NOTE: Initiate End of Experiment Room at this point ** ** ** ** ** ** ** ** ** ** ** **
                GameObject.Find("LevelChanger").GetComponent<LevelChange>().LevelConditionsMet = 1;
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(Head.transform.position, Head.transform.forward, out hit))
        {
            UnderReticule = hit.collider.gameObject.name;

            // Check for Relevance of object being viewed
            if (hit.collider.gameObject.tag == "Relevant")
            {
                IsRelevant = 1;
                CurrentTrial_TimeOnRelevant += Time.deltaTime;
            }
            else if (hit.collider.gameObject.tag == "Irrelevant")
            {
                IsRelevant = 0;
                CurrentTrial_TimeOnIrrelevant += Time.deltaTime;
            }
            else if (hit.collider.gameObject.name == "A" || hit.collider.gameObject.name == "B" || hit.collider.gameObject.name == "C" || hit.collider.gameObject.name == "D" || hit.collider.gameObject.name == "Next")
            {
                IsRelevant = 2;
                CurrentTrial_TimeOnButtons += Time.deltaTime;
            }
            else
            {
                IsRelevant = 3;
                CurrentTrial_TimeOnOther += Time.deltaTime;
            }

        }


        TrialPhase = GameObject.Find("ResearchAssistant").GetComponent<TrialPhaseTracker>().CurrentTrialPhase;

        Update_GazeLvl_Table();
        // NOTE: To reduce RAM strain, I should set up a container that only opens the file once a buffer point has been reached

    }

    // These Functions are used to update the output tables when necessary
    public void Update_GazeLvl_Table()
    {
        StreamWriter gazelvl_writer = new StreamWriter(gazelvl_filepath, true);

        gazelvl_writer.WriteLine(SubjectID + ", " + trialID + ", " + TrialPhase + ", " + 
                            Time.time + ", " +
                            Head.transform.position.x + ", " + Head.transform.position.y + ", " + Head.transform.position.z + ", " +
                            Head.transform.rotation.x + ", " + Head.transform.rotation.y + ", " + Head.transform.rotation.z + ", " +
                            LeftHand.transform.position.x + ", " + LeftHand.transform.position.y + ", " + LeftHand.transform.position.z + ", " +
                            RightHand.transform.position.x + ", " + RightHand.transform.position.y + ", " + RightHand.transform.position.z + ", " +
                            UnderReticule + ", " + IsRelevant); // Raycast info

        // Reset file in preparation for next frame
        gazelvl_writer.Close();
    }

    public void Update_TrialLvl_Table()
    {
        // Open TrialLvl file
        StreamWriter triallvl_writer = new StreamWriter(triallvl_filepath, true);
        // Update Table with new trial info
        TotalExpTime = Time.time;
        ReactionTime = TimeofChoiceMade - TrialStartTime;
        TimeOnFeedback = TotalExpTime - TimeofChoiceMade;
        triallvl_writer.WriteLine(SubjectID + ", " + trialID + ", " +
                                  "1" + ", " + "1" + ", " + "0" + ", " +
                                  CurrentTrial_TimeOnRelevant + ", " + CurrentTrial_TimeOnIrrelevant + ", " + CurrentTrial_TimeOnButtons + ", " + CurrentTrial_TimeOnOther + ", " +
                                  PlayerResponse + ", " + CorrectResponse + ", " + IsCorrect + ", " +
                                  TrialStartTime + ", " + ReactionTime + ", " + TimeOnFeedback + ", " + TotalExpTime);
        triallvl_writer.Close();
        // Reset GazeTimers for Next Trial
        CurrentTrial_TimeOnRelevant = 0;
        CurrentTrial_TimeOnIrrelevant = 0;
        CurrentTrial_TimeOnButtons = 0;
        CurrentTrial_TimeOnOther = 0;

    }

    public void Update_ExpLvl_Table()
    {
        StreamWriter explvl_writer = new StreamWriter(explvl_filepath, true);
        if (Explvl_IsEndofExp == 0)
        {
            explvl_writer.Write(SubjectID + ", " +
                           "demo" + ", " + "1" + ", " + "1" + ", " + "0" + ", ");
            // Note: We wait until the end of the experiment to insert the CriterionPointMet, CriterionPoint, and TotalExperiment
        }

        if (Explvl_IsEndofExp == 1)
        {
            explvl_writer.WriteLine(TotalAccuracy + ", " + MaxNumberofTrials + ", " + EndofExpTime);
        }
        // Close File
        explvl_writer.Close();
    }

    // These Functions are used to initialize the output tables
    private void Initialize_GazeLvl_Table()
    {
        // Initialize Gazelvl output file
        StreamWriter gazelvl_writer = new StreamWriter(gazelvl_filepath, true);
        // Set Headers
        gazelvl_writer.WriteLine("SubjectID, TrialID, TrialPhase, " +
                                "Time, " +
                                "HMD_x, HMD_y, HMD_z, HMD_Displacement, " +
                                "HMD_Rotation_x, HMD_Rotation_y, HMD_Rotation_z," +
                                "LeftHand_x, LeftHand_y, LeftHand_z," +
                                "RightHand_x, RightHand_y, RightHand_z," +
                                "LookingAt, IsRelevant");
        // Allow other files to be opened on this path
        gazelvl_writer.Close();
    }

    private void Initialize_ExpLvl_Table()
    {
        // Initialize Expllvl output file
        StreamWriter explvl_writer = new StreamWriter(explvl_filepath, true);
        // Set Headers
        explvl_writer.WriteLine("SubjectID, " +
                                "Condition, BlueRelevance, GreenRelevance, RedRelevance, " +
                                "TotalCorrect, TrialCount, TotalExperimentTime");
        // Close File
        explvl_writer.Close();
    }

    private void Initialize_TrialLvl_Table()
    {
        // Initialize Triallvl output file
        StreamWriter triallvl_writer = new StreamWriter(triallvl_filepath, true);
        // Set Headers
        triallvl_writer.WriteLine("SubjectID, TrialID, " +
                                  "Feature1Relevance, Feature2Relevance, Feature3Relevance, " +
                                  "TimeFixatedOnRelevantFeature, TimeFixatedOnIrrelevantFeature, TimeFixatedOnButtons, TimeFixatedOnOther, "+
                                  "PlayerResponse, CorrectResponse, IsCorrect, " + 
                                  "TrialStartTime, ReactionTime, TimeSpentOnFeedback, TotalExperimentTime");
        // Close File
        triallvl_writer.Close();
    }
    
}

