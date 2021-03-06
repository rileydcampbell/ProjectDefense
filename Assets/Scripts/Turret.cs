﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float damage = 10f;
    public float fireRate = 2f;
    public int towerCost;
    public int towerLevel = 1;
    public int upgradeCost = 150;

    [Header("Setup Fields")]
    public string tower = "";
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform rotateAround = null;
    private float xRot = 0;
    private float fireCountdown = 0;
    public AudioSource fireSound;

    [Header("Firing Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;

    public bool menuState = false;
    public bool newMenuState = false;
    public GameObject menu;
    public GameObject rangeDisplay;

    [Header("Laser Setup")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public float slowFactor = 0.5f;
    public ParticleSystem impactEffect;
    public Light impactLight;
    private Enemy targetEnemy;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Repeats the update target function every 1/2 second, prevents calling the function 60+ times per second if the method was instead in the update function
        if (tower == "Missle")
            xRot = -90;
        fireSound = GetComponent<AudioSource>();
        rangeDisplay.SetActive(false);
	}

    public void SetMenuState(bool state)
    {
        newMenuState = state;
    }

    // Method that iterates through all enemies in range, finds the closest one, and sets it as the target
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (newMenuState != menuState)
        {
            rangeDisplay.SetActive(newMenuState);
            if(menu != null)
            {
                menu.SetActive(newMenuState);
            }
            menuState = newMenuState;
        }

        if (target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
                fireSound.Stop();
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateAround.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateAround.rotation = Quaternion.Euler(xRot, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.SlowSpeed(slowFactor);
        targetEnemy.TakeDamage(damage * Time.deltaTime);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
            fireSound.Play();
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        
    }

    void Shoot()
    {
        GameObject _bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = _bullet.GetComponent<Bullet>();
        Bullet bullet1 = null;
        Bullet bullet2 = null;
        
        if (tower == "Missle")
        {
            GameObject _bullet1 = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            bullet1 = _bullet1.GetComponent<Bullet>();
            GameObject _bullet2 = (GameObject)Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            bullet2 = _bullet2.GetComponent<Bullet>();
            fireSound.Play();
        }
        else
        {
            fireSound.Play();
        }

        if (bullet != null)
        {
            bullet.Seek(target);
            bullet.PassDamage(damage);
        }
        if (bullet1 != null)
        {
            bullet1.Seek(target);
            bullet1.PassDamage(damage);
        }
        if (bullet2 != null)
        {
            bullet2.Seek(target);
            bullet2.PassDamage(damage);
        }
        
    }

    //Visual representation of the range in the scene
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public int GetTowerCost()
    {
        return towerCost;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public int GetLevel()
    {
        return towerLevel;
    }

    public string GetTurretType()
    {
        return tower;
    }

}
