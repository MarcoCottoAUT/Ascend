using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    private int TimerStart = 60;

    public int Timer;
    public LoseScreenHandler LoseScreen;
    public bool CountingDown = false;

    IEnumerator Countdown()
    {
        print(TimerStart);
        CountingDown = true;

        for (int i = TimerStart - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            if (!CountingDown)
            {
                break;
            }

            if (TimerText)
            {
                Timer = i;
                TimerText.text = $"{i}s";

                if (i <= 40 && i >= 20)
                {
                    TimerText.color = new Color32(212, 245, 66, 255);
                }
                else if (i <= 20 && i >= 10)
                {
                    TimerText.color = new Color32(255, 126, 28, 255);
                }
                else if (i <= 10)
                {
                    TimerText.color = new Color32(217, 33, 0, 255);
                }
            }

            if (i == 0)
            {
                LoseScreen.HandleLoseScreen();
            }
        }
    }

    private void Start()
    {
        //PlayerPrefs.DeleteKey("Stage" + SceneManager.GetActiveScene().buildIndex + "RecordHolder");
        //PlayerPrefs.DeleteKey("Stage" + SceneManager.GetActiveScene().buildIndex + "Record");

        if (!PlayerPrefs.HasKey("PlayerLoggedIn"))
        {
            PlayerPrefs.SetString("PlayerLoggedIn", "admin");
        }

        //print(PlayerPrefs.GetString("PlayerLoggedIn"));

        StartCoroutine(Countdown());
    }
}
