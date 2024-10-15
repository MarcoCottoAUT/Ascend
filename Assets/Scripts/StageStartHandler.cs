using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageStartHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Animator StartAnimator;
    public GameObject Player;
    public UIHandler UIHandler;

    private void Start()
    {
        StartAnimator = GetComponent<Animator>();
        Player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void HideScreen()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        Player.GetComponent<PlayerMovement>().enabled = true;
        UIHandler.enabled = true;
    }
}
