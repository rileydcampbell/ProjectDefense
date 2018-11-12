using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour {

    Text goldText;

    void Start () {
        goldText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        goldText.text = "Gold: " + GoldManager.goldManager.GetCurrentGold();
	}
}
