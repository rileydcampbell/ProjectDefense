using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{

    private Transform target;
    public GameObject impactEffect;

    public float explosionRadius = 10f;
    public float speed = 70f;
    public float damage = 0f;
    public float TargetRadius = 20f;
    public float LaunchAngle = 60f;
    public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    // cache
    private Rigidbody rigid;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    // Receive target from turret
    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void PassDamage(float dmg)
    {
        damage = dmg;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        Launch();
    }


    void Update()
    {

        //transform.rotation = Quaternion.LookRotation(rigid.velocity) * initialRotation;

    }
    /**
    void OnCollisionEnter()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        Collider[] collidedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidedObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

        Destroy(effectIns, 4f);
        Destroy(gameObject);
    }
    **/
    void Damage(Transform _enemy)
    {
        Enemy enemey = _enemy.GetComponent<Enemy>();
        enemey.TakeDamage(damage);
    }

    void Launch()
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(target.position.x, 0.0f, target.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (target.position.y + GetPlatformOffset()) - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigid.velocity = globalVelocity;
    }

    float GetPlatformOffset()
    {
        float platformOffset = 0.0f;

        /** we're iterating through Mark (Sprite) and Platform (Cube) Transforms. 
        foreach (Transform childTransform in target.GetComponentsInChildren<Transform>())
        {
            // take into account the y-offset of the Mark gameobject, which essentially
            // is (y-offset + y-scale/2) of the Platform as we've set earlier through the editor.
            if (childTransform.name == "Mark")
            {
                platformOffset = childTransform.localPosition.y;
                break;
            }
        }
    **/
        return platformOffset;
    }



}
