using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    public ProximityPrompt ProximityPrompt;
    public float AutoDeactivateTime = 5f;
    
    public Animator PlatformAnimator;
    public GameObject LeverInfo;

    public Animator LeverInfoAnimator;
    public TextMeshProUGUI Timer;
    private Animator Animator;

    public float DebounceTime = 2f;
    bool Activated = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Vector2 viewportPosition = Camera.main.WorldToScreenPoint(LeverInfo.transform.position);
        Timer.transform.position = viewportPosition;
    }

    void ActivateLever(bool Activate)
    {
        if (Activate)
        {
            PlatformAnimator.SetBool("MoveUp", true);
            PlatformAnimator.SetBool("MoveDown", false);

            Animator.SetBool("Deactivate", false);
            Animator.SetBool("Activate", true);
        } else
        {
            PlatformAnimator.SetBool("MoveUp", false);
            PlatformAnimator.SetBool("MoveDown", true);

            Animator.SetBool("Deactivate", true);
            Animator.SetBool("Activate", false);
        }
    }

    IEnumerator DisplayLeverCountdown()
    {
        for (float  i = AutoDeactivateTime; i > 0; i--)
        {
            Timer.text = $"{i}s";
            yield return new WaitForSeconds(1f);
        }

        if (!ProximityPrompt.InRange)
        {
            LeverInfoAnimator.SetTrigger("Disappear");
        } else
        {
            Timer.text = "[E]";
        }
        StopCoroutine(DisplayLeverCountdown());
    }

    IEnumerator Debounce()
    {
        yield return new WaitForSeconds(DebounceTime);
        ProximityPrompt.Triggered = false;

        Activated = false;
        StopCoroutine(Debounce());
    }

    IEnumerator AutomaticActivation()
    {
        ActivateLever(true);
        StartCoroutine(DisplayLeverCountdown());
        yield return new WaitForSeconds(AutoDeactivateTime);
        ActivateLever(false);

        StartCoroutine(Debounce());
        StopCoroutine(AutomaticActivation());
    }

    // Update is called once per frame
    void Update()
    {
        if (ProximityPrompt.Triggered && !Activated)
        {
            Activated = true;
            StartCoroutine(AutomaticActivation());
        }
    }
}
