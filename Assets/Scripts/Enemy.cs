using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float speed;
    public float startSpeed = 10f;

    public string enemyName;

    public float health = 50;
    private float startingHealth;

    private Transform target;
    private int wayPointIndex = 0;

    public int value = 10;

    public GameObject deathEffect;

    public Image healthBar;

	void Start ()
    {
        target = Waypoints.wayPoints[0];
        speed = startSpeed;
        startingHealth = health;
	}
	
	void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        speed = startSpeed;
	}

    public void SlowSpeed(float factor)
    {
        speed = startSpeed * (1f - factor);
    }

    void GetNextWayPoint()
    {
        if (wayPointIndex >= Waypoints.wayPoints.Length - 1)
        {
            LifeManager.lifeManager.ModifyLife(-1);
            Destroy(gameObject);
            return;
        }

        wayPointIndex++;
        target = Waypoints.wayPoints[wayPointIndex];
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
            return;
        }

        healthBar.fillAmount = health / startingHealth;
    }

    void Death()
    {
        GoldManager.goldManager.ModifyGold(value);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 4f);
        Destroy(gameObject);
    }
}
