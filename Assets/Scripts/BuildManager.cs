using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject standardTurretPrefab;

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public void setTurret(GameObject newTurret)
    {
        turretToBuild = newTurret;
    }

    public GameObject getTurretToBuild()
    {
        return turretToBuild;
    }

}
