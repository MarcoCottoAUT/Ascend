using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageHandler : MonoBehaviour
{
    public void ProceedStages()
    {
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (NextScene <= 6)
        {
            SceneManager.LoadScene(NextScene);
        } else
        {
            print("Invalid scene or player has beaten stage 6");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ProceedStages();
        }
    }
}
