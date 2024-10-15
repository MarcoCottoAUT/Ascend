using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenHandler : MonoBehaviour
{
    public Animator LevelClearedAnimator;
    public TextMeshProUGUI ClearTime;
    
    public TextMeshProUGUI RecordTime;
    public Animator ClearLabelAnimator;

    public Animator RecordLabelAnimator;
    public UIHandler StageFinishHandler;

    public GameObject Player;
    public Animator NextStageAnimator;

    public AudioSource VictorySound;
    public AudioSource LevelMusic;

    public Animator QuitAnimator;
    public UIHandler UIHandler;

    public void HandleWinScreen(int StageNumber)
    {
        UIHandler.CountingDown = false;
        UIHandler.enabled = false;
        VictorySound.Play();

        LevelMusic.Stop();
        string RecordHolder;

        Player.transform.position = new Vector3(0, -100, 0);
        Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        Player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        Player.GetComponent<PlayerMovement>().enabled = false;

        gameObject.SetActive(true);
        LevelClearedAnimator.SetBool("ShowText", true);

        ClearLabelAnimator.SetBool("ShowLabel", true);
        RecordLabelAnimator.SetBool("ShowLabel", true);

        if (NextStageAnimator)
        {
            NextStageAnimator.SetBool("ShowButton", true);
        }

        QuitAnimator.SetBool("ShowButton", true);

        ClearTime.text = $"TIME TAKEN: {60 - StageFinishHandler.Timer}s";

        if (PlayerPrefs.HasKey("Stage" + SceneManager.GetActiveScene().buildIndex + "Record"))
        {
            if (PlayerPrefs.GetInt("Stage" + SceneManager.GetActiveScene().buildIndex + "Record") > (60 - StageFinishHandler.Timer)) {
                PlayerPrefs.SetInt("Stage" + SceneManager.GetActiveScene().buildIndex + "Record", (60 - StageFinishHandler.Timer));

                PlayerPrefs.SetString("Stage" + SceneManager.GetActiveScene().buildIndex + "RecordHolder", PlayerPrefs.GetString("PlayerLoggedIn"));
            }
        } else
        {
            PlayerPrefs.SetInt("Stage" + SceneManager.GetActiveScene().buildIndex + "Record", (60 - StageFinishHandler.Timer));
            PlayerPrefs.SetString("Stage" + SceneManager.GetActiveScene().buildIndex + "RecordHolder", PlayerPrefs.GetString("PlayerLoggedIn"));
        }

        RecordHolder = PlayerPrefs.GetString("Stage" + SceneManager.GetActiveScene().buildIndex + "RecordHolder");
        RecordTime.text = $"RECORD TIME: {PlayerPrefs.GetInt("Stage" + SceneManager.GetActiveScene().buildIndex + "Record")}s\n(Set by: {RecordHolder})";
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
