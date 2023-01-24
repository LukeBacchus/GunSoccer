using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletLifeTime = 10;
    private float blastRadius = 5f;
    private float blastForce = 20.0f;

    private void Update()
    {
        bulletLifeTime -= Time.deltaTime;

        if (bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] toBePushedObjs = Physics.OverlapSphere(collision.contacts[0].point, blastRadius);

        foreach (Collider hitObj in toBePushedObjs)
        {
            Rigidbody rb = hitObj.GetComponent<Rigidbody>();

            if (rb != null && hitObj.gameObject != gameObject){
                Debug.Log(hitObj.gameObject.name);
                rb.AddExplosionForce(blastForce * rb.mass, collision.contacts[0].point, blastRadius, 1, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
