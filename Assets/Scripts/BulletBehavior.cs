using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletLifeTime = 10;
    [SerializeField] private float blastRadius = 15.0f;
    [SerializeField] private float blastForce = 50.0f;

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
        Collider[] toBePushedObjs = Physics.OverlapSphere(this.transform.position, blastRadius);
        foreach (Collider hitObj in toBePushedObjs)
        {
            Rigidbody rb = hitObj.GetComponent<Rigidbody>();

            if (rb != null){
                Debug.Log(hitObj.gameObject.name);
                rb.AddExplosionForce(blastForce, this.transform.position, blastRadius, 1.0F, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
