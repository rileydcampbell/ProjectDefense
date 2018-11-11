using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {

    Text lifeText;
    void Start()
    {
        lifeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = "Life: " + LifeManager.lifeManager.GetCurrentLife();
    }
}
