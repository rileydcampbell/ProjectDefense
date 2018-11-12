using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNotification : MonoBehaviour {

    Text waveText;
    private bool waveActive = false;
    private float timeToDisplay = 2.5f;
    private CanvasRenderer canvasRender;
    public GameObject waveMessage;
    private float alphaFade = 1;

    void Awake()
    {
        waveText = GetComponent<Text>();
        canvasRender = GetComponent<CanvasRenderer>();
    }

    void OnEnable()
    {
        timeToDisplay = 2.5f;
        canvasRender.SetAlpha(1f);
        alphaFade = 1f;
        if (GetWaveState())
        {
            waveText.text = "Wave Already Spawning";
        }
        else
        {
            waveText.text = "Starting Next Wave";
        }
    }

    void Update()
    {
        timeToDisplay -= Time.deltaTime;
        if(canvasRender.GetAlpha() > 0 && timeToDisplay < 1.5f)
        {
            alphaFade -= 0.67f * Time.deltaTime;
            canvasRender.SetAlpha(alphaFade);
        }

        if(timeToDisplay <= 0)
        {
            waveMessage.SetActive(false);
        }

    }

    bool GetWaveState()
    {
        return WaveSpawnController.spawnController.IsWaveActive();
    }
}
