using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenHandler : MonoBehaviour
{
    public Animator LevelFailedAnimator;
    public TextMeshProUGUI RandomMessage;

    public Animator RandomMessageAnimator;
    public GameObject Player;

    public Animator TryAgainAnimator;
    public Animator QuitAnimator;

    public AudioSource LevelMusic;
    public AudioSource LoseMusic;

    public void HandleLoseScreen()
    {
        LevelMusic.Stop();
        LoseMusic.Play();

        Player.transform.position = new Vector3(0, -100, 0);
        Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        Player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Player.GetComponent<PlayerMovement>().enabled = false;

        gameObject.SetActive(true);
        LevelFailedAnimator.SetBool("ShowText", true);

        RandomMessageAnimator.SetBool("ShowLabel", true);
        TryAgainAnimator.SetBool("ShowButton", true);
        QuitAnimator.SetBool("ShowButton", true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
