using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NodeInteraction : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;
    public GameObject turret = null;



	protected virtual void OnEnable()
    {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }

    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (turret != null)
        {
            print("Can't build here");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        if (GoldManager.goldManager.GetCurrentGold() >= turretToBuild.GetComponent<Turret>().GetTowerCost())
        {
            GoldManager.goldManager.ModifyGold(-turretToBuild.GetComponent<Turret>().GetTowerCost());
            turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0f, 0.50f, 0f), transform.rotation);
        }
        else
        {
            turret = null;
        }
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        OnDisable();
    }


}
