using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;

    public float health = 50;

    private Transform target;
    private int wayPointIndex = 0;

	void Start ()
    {
        target = Waypoints.wayPoints[0];
	}
	
	// Test
	void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        if(health <= 0)
        {
            Death();
        }
	}

    void GetNextWayPoint()
    {
        if (wayPointIndex >= Waypoints.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wayPointIndex++;
        target = Waypoints.wayPoints[wayPointIndex];
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
