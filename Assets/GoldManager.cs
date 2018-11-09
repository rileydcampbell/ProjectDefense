using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{

    public static GoldManager goldManager;

    private void Awake()
    {
        goldManager = this;
    }

    public int gold = 1000;

    public int GetCurrentGold()
    {
        return gold;
    }

    public void ModifyGold(int cost)
    {
        gold += cost;
    }
}
