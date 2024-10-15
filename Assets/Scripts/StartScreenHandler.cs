using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartScreenHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI StartText;
    public float WaitPerCharacter;

    public Animator InputAnimator;
    public GameObject UsernameInput;

    public AudioSource TypeSound;
    public IEnumerator AnimateStartScreen()
    {
        print("Called");

        for (int i = 0; i < 3; i++)
        {
            StartText.text = "|";
            yield return new WaitForSeconds(0.5f);

            StartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

        string FirstHalf = "welcome to ascend.";

        for (int i = 0; i < FirstHalf.Length; i++)
        {
            StartText.text += FirstHalf[i] + "|";
            TypeSound.Play();

            yield return new WaitForSeconds(WaitPerCharacter);
            StartText.text = StartText.text.Substring(0, StartText.text.Length - 1);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 2; i++)
        {
            StartText.text += "|";
            yield return new WaitForSeconds(0.5f);

            StartText.text = FirstHalf;
            yield return new WaitForSeconds(0.5f);
        }

        StartText.text += "\n\n";
        FirstHalf += "\n\n";

        for (int i = 0; i < 2; i++)
        {
            StartText.text += "|";
            yield return new WaitForSeconds(0.5f);

            StartText.text = FirstHalf;
            yield return new WaitForSeconds(0.5f);
        }

        string SecondHalf = "input a username to play:";

        for (int i = 0; i < SecondHalf.Length; i++)
        {
            StartText.text += SecondHalf[i] + "|";
            TypeSound.Play();

            yield return new WaitForSeconds(WaitPerCharacter);
            StartText.text = StartText.text.Substring(0, StartText.text.Length - 1);
        }

        yield return new WaitForSeconds(0.5f);
        UsernameInput.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        EventSystem.current.SetSelectedGameObject(UsernameInput, null);
    }
}
