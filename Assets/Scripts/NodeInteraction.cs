using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NodeInteraction : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;

    private GameObject turret;
    public bool isHighlighted = false;
    private bool newIsHighlighted = false;

    public GameObject previewPrefab;

	protected virtual void OnEnable()
    {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            linkedObject.InteractableObjectTouched += LinkedObject_InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += LinkedObject_InteractableObjectUntouched;
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            linkedObject.InteractableObjectTouched -= LinkedObject_InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched -= LinkedObject_InteractableObjectUntouched;
        }

    }

    private void LinkedObject_InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        print("Untouched");
        previewPrefab.SetActive(false);
    }

    private void LinkedObject_InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        print("Touched");
        previewPrefab.SetActive(true);
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
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        OnDisable();
    }

    public void SetHighlightState(bool state)
    {
        newIsHighlighted = state;
    }

    private void Update()
    {
        /**if(highlighterObject.GetAffectingObject() == this)
        {
            previewPrefab.SetActive(true);
        }**/
    }
}
