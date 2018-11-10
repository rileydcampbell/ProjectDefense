using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    public int life = 50;

    public static LifeManager lifeManager;

    private void Start()
    {
        lifeManager = this;
    }

    public int GetCurrentLife()
    {
        return life;
    }

    public void ModifyLife(int mod)
    {
        life += mod;
    }
}
