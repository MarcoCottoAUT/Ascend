using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageButtonHandler : MonoBehaviour
{
    bool Debounce = false;
    public void LoadStage(string StageName)
    {
        if (!Debounce)
        {
            Debounce = true;
            SceneManager.LoadScene($"Stage{StageName}");
        }
    }
}
