using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public GameObject impactEffect;

    public float explosionRadius = 0f;
    public float speed = 70f;
    public float damage = 0f;

    public float range = 20;
    public string enemyTag = "Enemy";

    
    // Receive target from turret
    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void PassDamage(float dmg)
    {
        damage = dmg;
    }

	void Start () {
		
	}
	

	void Update ()
    {
        if (target == null)
        {
            if(UpdateTarget() == null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = UpdateTarget();
            }
            
        }

        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(dir.magnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
        transform.LookAt(target);

	}

    Transform UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            return nearestEnemy.transform;
        }
        else
        {
            return null;
        }
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(effectIns, 4f);
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] collidedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidedObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage (Transform _enemy)
    {
        Enemy enemey = _enemy.GetComponent<Enemy>();
        enemey.TakeDamage(damage);
    }

}
