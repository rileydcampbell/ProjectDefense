using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClicks : MonoBehaviour {

    public AudioSource click;
    public bool isActive = false;

    private void OnEnable()
    {
        click.Play();
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    public bool returnState()
    {
        return isActive;
    }
}
