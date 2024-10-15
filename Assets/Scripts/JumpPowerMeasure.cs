using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpPowerMeasure : MonoBehaviour
{
    public Slider Jumpbar;
    TextMeshProUGUI Text;

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = $"{Mathf.Round(Jumpbar.value * 10)}";
    }
}
