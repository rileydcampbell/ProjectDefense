using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveScript : MonoBehaviour {

    Text waveText;
    private bool waveActive = false;

    void Start()
    {
        waveText = GetComponent<Text>();
    }

    void Update()
    {
        if (waveActive)
        {
            waveText.text = "Current Wave: " + WaveSpawnController.spawnController.GetCurrentWave();
        }
        else
        {
            waveText.text = "Next Wave: " + WaveSpawnController.spawnController.GetCurrentWave();
        }
        
    }

    public void UpdateWaveState(bool state)
    {
        waveActive = state;
    }
}
