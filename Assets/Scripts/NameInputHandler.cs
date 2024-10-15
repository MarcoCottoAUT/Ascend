using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameInputHandler : MonoBehaviour
{
    // Update is called once per frame
    bool ButtonPressed = false;
    public TextMeshProUGUI Username;

    public Animator InputAnimator;
    public GameObject StartScreen;

    public TextMeshProUGUI WelcomeUserText;
    public GameObject RealStartScreen;
    public TextMeshProUGUI LoggedInUserText;

    public void StartButtonPress()
    {
        if (!ButtonPressed)
        {
            StartCoroutine(ConfirmUsername());
        }
    }
    public IEnumerator ConfirmUsername()
    {
        print("Called");

        if (!ButtonPressed)
        {
            ButtonPressed = true;
        }

        print(Username.text.Length);

        if (Username.text.Length <= 1)
        {
            InputAnimator.Play("InputError", -1, 0);
            yield return new WaitForSeconds(0.5f);
            ButtonPressed = false;
        } else
        {
            PlayerPrefs.SetString("PlayerLoggedIn", Username.text.ToLower());
            WelcomeUserText.text = $"welcome, {PlayerPrefs.GetString("PlayerLoggedIn")}";
            LoggedInUserText.text = "logged in as: " + PlayerPrefs.GetString("PlayerLoggedIn");

            foreach (Transform ScreenPart in RealStartScreen.transform)
            {
                ScreenPart.gameObject.SetActive(false);
            }

            StartScreen.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!ButtonPressed)
            {
                StartButtonPress();
            }
        }
    }
}
