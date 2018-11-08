using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NodeInteraction : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;

    private GameObject turret;

	protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if(linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }

        print("On");
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
        else

        print("Off");
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (turret != null)
        {
            print("Can't build here");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0f,0.50f,0f), transform.rotation);
        print("turret built");
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        OnDisable();
        print("test2");
    }
}
