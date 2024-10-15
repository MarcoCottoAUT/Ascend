using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public bool Debounce = false;
    public Animator MenuButtonsAnimator;

    public Animator SettingsAnimator;
    public Animator StagesAnimator;

    public AudioSource MenuMusic;
    public GameObject StartScreen;

    public StartScreenHandler StartScreenHandler;
    public TextMeshProUGUI LoggedInUserText;

    private void Start()
    {
        //PlayerPrefs.SetString("PlayerLoggedIn", "admin");

        if (!PlayerPrefs.HasKey("PlayerLoggedIn"))
        {
            StartScreen.SetActive(true);
            StartCoroutine(StartScreenHandler.AnimateStartScreen());
        } else
        {
            LoggedInUserText.text = "logged in as: " + PlayerPrefs.GetString("PlayerLoggedIn");
            MenuMusic.Play();
        }
    }

    private void OnApplicationQuit()
    {
        if (PlayerPrefs.HasKey("PlayerLoggedIn"))
        {
            PlayerPrefs.DeleteKey("PlayerLoggedIn");
        }
    }

    public void ButtonPressed(string ButtonName)
    {
        if (!Debounce)
        {
            Debounce = true;

            if (ButtonName == "PlayButton")
            {
                MenuButtonsAnimator.SetBool("PlayHide", true);
            }
            else if (ButtonName == "SettingsButton")
            {
                MenuButtonsAnimator.SetBool("SettingsHide", true);
            }
            else if (ButtonName == "PlayBackButton")
            {
                StagesAnimator.SetBool("Show", false);
            }
            else if (ButtonName == "SettingsBackButton")
            {
                SettingsAnimator.SetBool("Show", false);
            } else if (ButtonName == "CreditsButton")
            {
                SceneManager.LoadScene("Credits");
            }
            else if (ButtonName == "QuitButton")
            {
                Application.Quit();
            }
        }
    }
}
