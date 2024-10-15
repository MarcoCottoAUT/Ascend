using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHandler : MonoBehaviour
{
    public Transform SpawnPosition;
    Animator PlayerAnimator;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }

    public void ResetHurtBool()
    {
        PlayerAnimator.SetBool("Hurt", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            gameObject.transform.position = SpawnPosition.position;

            if (!PlayerAnimator.GetBool("Hurt"))
            {
                PlayerAnimator.SetBool("Hurt", true);
            }
        }
    }
}
