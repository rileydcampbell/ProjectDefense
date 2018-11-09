using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SellTurret : MonoBehaviour {

    public VRTK_ControllerEvents controllerEvents;
    public GameObject turretSelected = null;

    public void UpdateTurret(GameObject newTurret)
    {
        turretSelected = newTurret;
    }

    protected virtual void OnEnable()
    {
        controllerEvents.ButtonTwoPressed += ControllerEvents_ButtonTwoPressed;
        controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
    }

    protected virtual void OnDisable()
    {
        controllerEvents.ButtonTwoPressed -= ControllerEvents_ButtonTwoPressed;
        controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;
    }

    private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ControllerEvents_ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        if(turretSelected != null)
        {
            Destroy(turretSelected);
        }
    }
}
