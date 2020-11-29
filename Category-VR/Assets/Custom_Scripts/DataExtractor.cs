using UnityEngine;
using System.IO;

public class DataExtractor : MonoBehaviour
{
    public static DataExtractor UpdateData;

    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;

    // Filepaths of Output tables
    private string gazelvl_filepath = "Assets/Output/Gazelvl.csv"; // NOTE: Eventually, the file name will based on the subject id variable
    private string triallvl_filepath = "Assets/Output/Triallvl.csv";
    private string explvl_filepath = "Assets/Output/Explvl.csv";

    // Logical Indices
    public float is_newtrial;
    private float TrialStartTime;

    // ExpLvl Variables
    public float Explvl_IsEndofExp = 0;
    private float EndofExpTime;
    public float explvl_CriterionPointMet;
    public float explvl_CriterionPoint;

    // TrialLvl Variables: RowID, TrialID, SubjectID
    private int triallvl_rowid = 0;
    private int explvl_rowid = 1;
    private int SubjectID = 1;
    private int trialID = 0;
    public int TrialPhase;
    public string PlayerResponse;
    public string CorrectResponse;
    public int IsCorrect;
    public int MaxNumberofTrials = 10;
    public float ReactionTime;
    public float TimeofChoiceMade;
    private float TimeOnFeedback;
    private float TotalExpTime;

    // GazeLvl Variables
    private string UnderReticule;
    public float IsRelevant;


    // Begin the Experiment
    void Awake ()
    {
        if (!File.Exists("Assets/Output/Gazelvl.csv"))
        {
            Initialize_GazeLvl_Table();
        }
        if (!File.Exists("Assets/Output/Triallvl.csv"))
        {
            Initialize_TrialLvl_Table(); ;
        }
        if (!File.Exists("Assets/Output/Explvl.csv"))
        {
            Initialize_ExpLvl_Table();
        }

        // Set Logical Indices
        Explvl_IsEndofExp = 0;

        Update_ExpLvl_Table();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (is_newtrial == 1) //Is called at the end of every trial
        {
            if (trialID > 0)
            {
                Update_TrialLvl_Table();
            }
            // NOTE: This eventually needs to reference previous rowid to allow for multiple exps to exist in one table
            triallvl_rowid++;
            trialID++;
            is_newtrial = 0;
            TrialStartTime = Time.time;

            if (trialID > MaxNumberofTrials)
            {
                Explvl_IsEndofExp = 1;
                EndofExpTime = Time.time;

                Update_ExpLvl_Table();
                // NOTE: Initiate End of Experiment Room at this point ** ** ** ** ** ** ** ** ** ** ** **
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(Head.transform.position, Head.transform.forward, out hit))
        {
            UnderReticule = hit.collider.gameObject.name;

            // Check for Relevance of object being viewed
            if (hit.collider.gameObject.tag == "Relevant" || hit.collider.gameObject.tag == "ChoiceButton")
            {
                IsRelevant = 1;
            }
            if (hit.collider.gameObject.tag == "Irrelevant")
            {
                IsRelevant = 0;
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
                            Head.transform.position.x + ", " + Head.transform.position.y + ", " + Head.transform.position.z + ", " + "" + ", " +
                            Head.transform.rotation.x + ", " + Head.transform.rotation.y + ", " + Head.transform.rotation.z + ", " + "" + ", " +
                            LeftHand.transform.position.x + ", " + LeftHand.transform.position.y + ", " + LeftHand.transform.position.z + ", " + "" + ", " +
                            RightHand.transform.position.x + ", " + RightHand.transform.position.y + ", " + RightHand.transform.position.z + ", " + "" + ", " +
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
        triallvl_writer.WriteLine(triallvl_rowid + ", " + SubjectID + ", " + trialID + ", " +
                                  "" +", " + "" + ", " + "" + ", " +
                                  PlayerResponse + ", " + CorrectResponse + ", " + IsCorrect + ", " +
                                  TrialStartTime + ", " + ReactionTime + ", " + TimeOnFeedback + ", " + TotalExpTime);
        triallvl_writer.Close();
    }
    public void Update_ExpLvl_Table()
    {
        StreamWriter explvl_writer = new StreamWriter(explvl_filepath, true);
        if (Explvl_IsEndofExp == 0)
        {
            explvl_writer.Write(explvl_rowid + ", " + SubjectID + ", " +
                           "testing" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", ");
            // Note: We wait until the end of the experiment to insert the CriterionPointMet, CriterionPoint, and TotalExperiment
        }

        if (Explvl_IsEndofExp == 1)
        {
            explvl_writer.WriteLine(explvl_CriterionPointMet + ", " + explvl_CriterionPoint + ", " + EndofExpTime);
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
                                "HMD_Rotation_x, HMD_Rotation_y, HMD_Rotation_z, HMD_RotationalDisplacement, " +
                                "LeftHand_x, LeftHand_y, LeftHand_z, LeftHand_Displacement, " +
                                "RightHand_x, RightHand_y, RightHand_z, RightHand_Displacement, " +
                                "LookingAt, IsRelevant");
        // Allow other files to be opened on this path
        gazelvl_writer.Close();
    }
    private void Initialize_ExpLvl_Table()
    {
        // Initialize Triallvl output file
        StreamWriter explvl_writer = new StreamWriter(explvl_filepath, true);
        // Set Headers
        explvl_writer.WriteLine("RowID, SubjectID, " +
                                "Condition, Location1Feature, Location2Feature, Location3Feature, Location1Relevance, Location2Relevance, Location3Relevance, " +
                                "CriterionPointMet, CriterionPoint, TotalExperimentTime");
        // Close File
        explvl_writer.Close();
    }
    private void Initialize_TrialLvl_Table()
    {
        // Initialize Triallvl output file
        StreamWriter triallvl_writer = new StreamWriter(triallvl_filepath, true);
        // Set Headers
        triallvl_writer.WriteLine("RowID, SubjectID, TrialID, " +
                                  "Feature1Value, Feature2Value, Feature3Value, " +
                                  "PlayerResponse, CorrectResponse, IsCorrect, " + 
                                  "TrialStartTime, ReactionTime, TimeSpentOnFeedback, TotalExperimentTime");
        // Close File
        triallvl_writer.Close();
    }
    
}

