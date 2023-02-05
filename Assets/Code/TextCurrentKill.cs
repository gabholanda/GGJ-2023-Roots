using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCurrentKill : MonoBehaviour
{
    public WaveManager waveManager;
    public TextMeshProUGUI currentKill;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        currentKill = GetComponentInChildren<TextMeshProUGUI>();
        currentKill.fontSize = 20;
        currentKill.text = "Enemy killed: " + waveManager.currentKilled.ToString();
    }

    void Update()
    {
        currentKill.text = "Enemy killed: " + waveManager.currentKilled.ToString();
    }
}
