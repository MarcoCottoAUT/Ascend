using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    bool CoinCollected = false;
    Animator CoinAnimator;
    AudioSource CoinSound;

    private void Start()
    {
        CoinAnimator = GetComponent<Animator>();
        CoinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!CoinCollected)
            {
                CoinCollected = true;
                CoinAnimator.SetBool("CoinCollected", true);

                CoinSound.Play();
            }
        }
    }
}
