using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animator;

    public int levelToLoad;
    public int LevelConditionsMet;

    private void Update()
    {
        if (LevelConditionsMet == 1)
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel (int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
