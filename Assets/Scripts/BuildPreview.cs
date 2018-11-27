using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {

	void Start () {
        float desiredTransparency = 0.5f;
        // Find all the renderers on the object, including the children
        var renderers = GetComponentsInChildren<Renderer>(true);
        foreach (var renderer in renderers)
        {
            var color = renderer.material.color;
            renderer.material.color = new Color(0, 1, 1, 0.2f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
