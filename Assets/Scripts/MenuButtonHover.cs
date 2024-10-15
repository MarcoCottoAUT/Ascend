using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator ButtonAnimator;
    Button Button;
    private void Start()
    {
        ButtonAnimator = GetComponent<Animator>();
        Button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Button.interactable) { 
            return; 
        }

        ButtonAnimator.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Button.interactable)
        {
            return;
        }

        ButtonAnimator.SetBool("Hover", false);
    }
}
