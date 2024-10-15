using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuQuoteRandomizer : MonoBehaviour
{
    public List<String> Quotes = new List<String>();
    TextMeshProUGUI QuoteText;
    // Start is called before the first frame update
    void Start()
    {
        QuoteText = GetComponent<TextMeshProUGUI>();
        QuoteText.text = Quotes[UnityEngine.Random.Range(1, Quotes.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
