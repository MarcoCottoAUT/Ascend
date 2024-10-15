using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStartSequence : MonoBehaviour
{
    public Animator StartScreenAnimator;
    public AudioSource MenuMusic;

    public void ShowMenu()
    {
        StartScreenAnimator.SetBool("Hide", true);
        MenuMusic.Play();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
