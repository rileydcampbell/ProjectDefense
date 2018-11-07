using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float damage = 10f;
    public float fireRate = 2f;
    private float fireCountdown = 0;

    [Header("Setup Fields")]
    public string tower = "";
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform rotateAround = null;
    private float xRot = 0;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Repeats the update target function every 1/2 second, prevents calling the function 60+ times per second if the method was instead in the update function
        if (tower == "Missle")
            xRot = -90;
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
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(target == null)
        {
            return;
        }

        // Locking onto target and rotating
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateAround.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateAround.rotation = Quaternion.Euler(xRot , rotation.y, 0f);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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
}
