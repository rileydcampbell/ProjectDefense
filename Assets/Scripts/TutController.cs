using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

public class TutController : MonoBehaviour {

    public GameObject message1;
    public GameObject message2;
    public GameObject message3;
    public GameObject message4;
    public GameObject message5;
    public GameObject message6;
    public GameObject message7;

    public int currentMenu = 1;
    public bool wavePressed = false;

    public VRTK_ControllerEvents controllerEvents;

    void Start () {
        message1.SetActive(true);
	}

    private void OnEnable()
    {
        controllerEvents.TriggerPressed += ControllerEvents_TriggerPressed;
        controllerEvents.TriggerReleased += ControllerEvents_TriggerReleased;
        controllerEvents.ButtonOnePressed += ControllerEvents_ButtonOnePressed;
        controllerEvents.ButtonOneReleased += ControllerEvents_ButtonOneReleased;
    }


    private void OnDisable()
    {
        controllerEvents.TriggerPressed -= ControllerEvents_TriggerPressed;
        controllerEvents.TriggerReleased -= ControllerEvents_TriggerReleased;
        controllerEvents.ButtonOnePressed -= ControllerEvents_ButtonOnePressed;
        controllerEvents.ButtonOneReleased -= ControllerEvents_ButtonOneReleased;
    }

    private void ControllerEvents_ButtonOneReleased(object sender, ControllerInteractionEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ControllerEvents_ButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        wavePressed = true;
        message5.SetActive(false);
        currentMenu += 1;
    }

    private void ControllerEvents_TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {

    }

    private void ControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if(currentMenu == 1)
        {
            message1.SetActive(false);
            message2.SetActive(true);
            currentMenu += 1;
            return;
        }
        if (currentMenu == 2)
        {
            message2.SetActive(false);
            message3.SetActive(true);
            currentMenu += 1;
            return;
        }
        if (currentMenu == 3)
        {
            bool hasBuiltTurret = false;
            GameObject[] searchArray = GameObject.FindGameObjectsWithTag("Turret");
            foreach(GameObject turret in searchArray)
            {
                if(turret.GetComponent<Turret>().GetTurretType() == "Mortar")
                {
                    hasBuiltTurret = true;
                }
            }
            if (hasBuiltTurret)
            {
                message3.SetActive(false);
                message4.SetActive(true);
                currentMenu += 1;
                GoldManager.goldManager.ModifyGold(475);
                return;
            }
        }
        if(currentMenu == 4)
        {
            bool hasUpgradedTurret = false;
            GameObject[] searchArray = GameObject.FindGameObjectsWithTag("Turret");
            foreach (GameObject turret in searchArray)
            {
                if (turret.GetComponent<Turret>().GetLevel() >= 2)
                {
                    hasUpgradedTurret = true;
                }
            }
            if (hasUpgradedTurret)
            {
                message4.SetActive(false);
                message5.SetActive(true);
                currentMenu += 1;
                return;
            }
        }
        if (currentMenu == 6)
        {
            message6.SetActive(false);
            message7.SetActive(true);
            currentMenu += 1;
            print("Next Menu");
            return;
        }
        if (currentMenu == 7)
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    private void Update()
    {
        if(wavePressed && !WaveSpawnController.spawnController.IsWaveActive())
        {
            message6.SetActive(true);
            currentMenu += 1;
            wavePressed = false;
            print(currentMenu);
            return;
        }
    }
}
