using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagesHandler : MonoBehaviour
{
    public MenuHandler MenuHandler;
    public Animator MenuButtonsAnimator;

    private void Start()
    {
        if (gameObject.name == "Stages")
        {
            foreach (Transform Button in transform.Find("StageButtons"))
            {
                if (PlayerPrefs.GetInt("Stage" + Button.name + "Unlocked") == 1)
                {
                    Button.GetComponent<Button>().interactable = true;
                }
            }
        }
    }
    public void DisableDebounce()
    {
        print("Disabled");
        MenuHandler.Debounce = false;
    }

    public void ShowMenuButtons()
    {
        MenuButtonsAnimator.SetBool("PlayHide", false);
    }

    public void ShowMenuButtonFromSettings()
    {
        MenuButtonsAnimator.SetBool("SettingsHide", false);
    }
}
