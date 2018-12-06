using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public bool buildModeActive = true;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!buildModeActive && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
	}
}
