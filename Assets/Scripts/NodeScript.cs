using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NodeScript : MonoBehaviour {

    public Color hoverColor;
    private Color startColor;

    private GameObject turret = null;

    private Renderer rend;
    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void ControllerStartUseInteractableObject()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (turret != null){
            //Cant build here
            return;
        }
    }

}
