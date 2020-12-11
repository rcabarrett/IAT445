using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectIDStore : MonoBehaviour
{
    public string SubjectID;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
