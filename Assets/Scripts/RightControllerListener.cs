using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RightControllerListener : MonoBehaviour {

    public VRTK_ControllerEvents controllerEvents;
    public GameObject menu;
    public GameObject waveState;

    private void OnEnable()
    {
        controllerEvents.ButtonOnePressed += ControllerEvents_ButtonOnePressed;
        controllerEvents.ButtonOneReleased += ControllerEvents_ButtonOneReleased;
    }

    private void OnDisable()
    {
        controllerEvents.ButtonOnePressed -= ControllerEvents_ButtonOnePressed;
        controllerEvents.ButtonOneReleased -= ControllerEvents_ButtonOneReleased;
    }

    private void ControllerEvents_ButtonOneReleased(object sender, ControllerInteractionEventArgs e)
    {

    }

    private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        menu.SetActive(true);
        WaveSpawnController.spawnController.StartWave();
    }
}
