using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class NodeInteraction : MonoBehaviour {

    public VRTK_InteractableObject linkedObject;

	protected virtual void OnEnable()
    {
        print("Success");
    }

    protected virtual void OnDisable()
    {
        print("Off");
    }
}
