using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProximityPrompt : MonoBehaviour
{
    public float ActivationDistance = 1f;
    public KeyCode ActivationKey = KeyCode.E;

    public bool InRange;
    public bool Triggered;

    private Transform PlayerTransform;
    public Animator LeverInfoAnimator;
    public TextMeshProUGUI LeverInfoText;

    private void Start()
    {
        PlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        float Distance = (gameObject.transform.position - PlayerTransform.position).magnitude;

        if (Distance <= ActivationDistance)
        {
            if (!InRange)
            {
                InRange = true;

                if (!Triggered)
                {
                    LeverInfoText.text = "[E]";
                    LeverInfoAnimator.SetTrigger("Appear");
                }
            }

            if (!Triggered)
            {
                if (Input.GetKeyDown(ActivationKey))
                {
                    Triggered = true;
                }
            }
        }
        else
        {
            if (InRange && !Triggered)
            {
                InRange = false;
                LeverInfoAnimator.SetTrigger("Disappear");
            }
        }
    }
}
