using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetHighScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform HighscoreText in transform)
        {
            TextMeshProUGUI Text = HighscoreText.GetComponent<TextMeshProUGUI>();

            if (PlayerPrefs.HasKey("Stage" + HighscoreText.name + "Record"))
            {
                Text.text = $"Stage {HighscoreText.name}: {PlayerPrefs.GetInt("Stage" + HighscoreText.name + "Record")}s\n(Set by {PlayerPrefs.GetString("Stage" + HighscoreText.name + "RecordHolder")})";
            } else
            {
                Text.text = $"Stage {HighscoreText.name}:\nNo record set yet!";
            }
        }
    }
}
