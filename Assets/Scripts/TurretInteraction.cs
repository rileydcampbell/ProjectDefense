using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TurretInteraction : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;
    public GameObject turretToBuild;
    public GameObject currentTurret;
    public int turretLevel;


    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }

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
        if(turretLevel < 3 && GoldManager.goldManager.GetCurrentGold() >= currentTurret.GetComponent<Turret>().GetUpgradeCost())
        {
            print("Destroying and Upgrading");
            Instantiate(turretToBuild, transform.position + new Vector3(0f, 0f, 0f), transform.rotation);
            GoldManager.goldManager.ModifyGold(-currentTurret.GetComponent<Turret>().GetUpgradeCost());
            Destroy(this.gameObject);
        }
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        print("Debug");
    }

}
