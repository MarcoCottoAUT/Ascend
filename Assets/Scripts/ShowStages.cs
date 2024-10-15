using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStages : MonoBehaviour
{
    public Animator StagesAnimator;
    public Animator SettingsAnimator;
    public MenuHandler MenuHandler;
    public void ShowStagesFrame()
    {
        StagesAnimator.SetBool("Show", true);
    }

    public void ShowSettingsFrame()
    {
        SettingsAnimator.SetBool("Show", true);
    }

    public void DisableDebounce()
    {
        MenuHandler.Debounce = false;
    }
}
