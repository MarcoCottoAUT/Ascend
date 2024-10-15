using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFinishHandler : MonoBehaviour
{
    BoxCollider2D Collider;
    public int FlagStage;

    bool Touched = false;
    public WinScreenHandler winScreenHandler;
    public GameObject WinScreen;

    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        //print("Scene" + (SceneManager.GetActiveScene().buildIndex + 1) + "Unlocked");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!Touched)
            {
                Touched = true;

                if (winScreenHandler)
                {
                    //WinScreen.SetActive(true);
                    winScreenHandler.HandleWinScreen(FlagStage);

                    if (SceneManager.GetActiveScene().name == "Stage6")
                    {
                        return;
                    }

                    if (PlayerPrefs.GetInt("Stage" + (SceneManager.GetActiveScene().buildIndex + 1) + "Unlocked") == 0)
                    {
                        PlayerPrefs.SetInt("Stage" + (SceneManager.GetActiveScene().buildIndex + 1) + "Unlocked", 1);
                    }
                }
            }
        }
    }
}
