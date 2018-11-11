using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveScript : MonoBehaviour {

    Text waveText;
    void Start()
    {
        waveText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Current Wave: " + WaveSpawnController.spawnController.GetCurrentWave();
    }
}
