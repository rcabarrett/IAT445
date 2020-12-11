using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetParticipantNumber : MonoBehaviour
{
    public string SubjectID;
    public GameObject InputField;

    public void StoreSubjectID()
    {
        SubjectID = InputField.GetComponent<Text>().text;
        GameObject.Find("SubjectIDStore").GetComponent<SubjectIDStore>().SubjectID = SubjectID;
        GameObject.Find("Next").GetComponent<GoToNextRoom>().SubjectID_IsSet = 1;
    }

}
