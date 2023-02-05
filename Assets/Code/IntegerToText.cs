using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntegerToText : MonoBehaviour
{
    public WaveManager waveManager;
    public TextMeshProUGUI waveText;
    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        waveText = GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = "Wave: " + waveManager.wave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave: " + waveManager.wave.ToString();
    }
}
