using UnityEngine;
using System.IO;

public class DataExtractor : MonoBehaviour
{
    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;

    // Filepaths of Output tables
    private string gazelvl_filepath = "Assets/Output/Gazelvl.csv"; // NOTE: Eventually, the file name will based on the subject id variable
    private string triallvl_filepath = "Assets/Output/Triallvl.csv";
    private string explvl_filepath = "Assets/Output/Explvl.csv";

    // Logical Indices
    private int is_newtrial = 1;
    private int Explvl_IsEndofExp = 0;
    private int triallvl_ChoiceMade = 0;
    public int explvl_CriterionPointMet = 0;
    public int explvl_CriterionPoint;

    // Tracking Variables: RowID, TrialID, SubjectID
    private int triallvl_rowid = 1;
    private int explvl_rowid = 1;
    private int SubjectID = 1;
    private int trialID = 1;
    private int TrialPhase = 2;
    public int MaxNumberofTrials = 10;
    private RaycastHit UnderReticule;

    // Begin the Experiment
    void Start ()
    {
        Initialize_GazeLvl_Table();
        Initialize_TrialLvl_Table();
        Initialize_ExpLvl_Table();

        // Still need to create a check for each of these that will allow us to know if headers need to be entered at all. 
        // They won't be necessary after running this exp once, except maybe in the case of gaze lvl if I choose to make a new file for every subject.

        // Set Logical Indices
        trialID = 1;
        Explvl_IsEndofExp = 0;
        triallvl_ChoiceMade = 0;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (is_newtrial == 1) //Is called at the end of every trial
        {
            Update_TrialLvl_Table();
            // NOTE: This eventually needs to reference previous rowid to allow for multiple exps to exist in one table
            triallvl_rowid++;
            trialID++;
            is_newtrial = 0;
        }

        Update_GazeLvl_Table();

        // NOTE: To reduce RAM strain, I should set up a container that only opens the file once a buffer point has been reached

    }

    // These Functions are used to update the output tables when necessary
    private void Update_GazeLvl_Table()
    {
        StreamWriter gazelvl_writer = new StreamWriter(gazelvl_filepath, true);

        gazelvl_writer.WriteLine(SubjectID + ", " + trialID + ", " + TrialPhase + ", " + 
                            Time.time + ", " +
                            Head.transform.position.x + ", " + Head.transform.position.y + ", " + Head.transform.position.z + ", " + "" + ", " +
                            Head.transform.rotation.x + ", " + Head.transform.rotation.y + ", " + Head.transform.rotation.z + ", " + "" + ", " +
                            LeftHand.transform.position.x + ", " + LeftHand.transform.position.y + ", " + LeftHand.transform.position.z + ", " + "" + ", " +
                            RightHand.transform.position.x + ", " + RightHand.transform.position.y + ", " + RightHand.transform.position.z + ", " + "" + ", " +
                            "" + ", " + ""); // Raycast info

        // Reset file in preparation for next frame
        gazelvl_writer.Close();
    }
    private void Update_TrialLvl_Table()
    {
        // Open TrialLvl file
        StreamWriter triallvl_writer = new StreamWriter(triallvl_filepath, true);
        // Update Table with new trial info
        triallvl_writer.WriteLine(triallvl_rowid + ", " + SubjectID + ", " + trialID + ", " +
                                  "" +", " + "" + ", " + "" + ", " + 
                                  "" + ", " + "" + ", " + 
                                  "" + ", " + "" + ", " + "" + ", " + Time.time);
        triallvl_writer.Close();
    }
    private void Update_ExpLvl_Table(StreamWriter explvl_writer)
    {
        if (Explvl_IsEndofExp == 0)
        {
            explvl_writer.Write(explvl_rowid + ", " + SubjectID + ", " +
                           "testing" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", " + "" + ", ");
            // Note: We wait until the end of the experiment to insert the CriterionPointMet, CriterionPoint, and TotalExperiment
        }

        if (Explvl_IsEndofExp == 1)
        {
            explvl_writer.WriteLine(explvl_CriterionPointMet + ", " + explvl_CriterionPoint + ", " + Time.time);
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
        // Establish Explvl data since most of this already known from start of experiment, or will be determined from researcher input in previous scene
        // Note CriterionPointMet, CriterionPoint, and TotalExpTime are not set here as they are not known until the end of the experiment.
        Update_ExpLvl_Table(explvl_writer);
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
                                  "PlayerResponse, CorrectResponse, " +
                                  "TrialStartTime, ReactionTime, TimeSpentOnFeedback, TotalExperimentTime");
        // Close File
        triallvl_writer.Close();
    }
    
}

