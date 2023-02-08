using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletLifeTime = 10;
    private float blastRadius = 3f;
    private float blastForce = 20.0f;
    private float selfBlastRadius = 7f;
    private float selfBlastForce = 30f;
    public float playerNum;

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
        Collider[] selfBlastObjs = Physics.OverlapSphere(collision.contacts[0].point, selfBlastRadius);

        foreach (Collider hitObj in toBePushedObjs)
        {
            Rigidbody rb = hitObj.GetComponent<Rigidbody>();

            if (rb != null && hitObj.gameObject != gameObject && hitObj.gameObject.name != "Player " + (playerNum).ToString()){
                rb.AddExplosionForce(blastForce * rb.mass, collision.contacts[0].point, blastRadius, 1, ForceMode.Impulse);
            }
        }

        foreach (Collider obj in selfBlastObjs)
        {
            if (obj.gameObject.name == "Player " + (playerNum).ToString())
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.AddExplosionForce(selfBlastForce * rb.mass, collision.contacts[0].point, selfBlastRadius, 1, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
