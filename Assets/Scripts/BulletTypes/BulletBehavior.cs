using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public Bullets bullet;
    public Vector3 bulletDirection;
    public float bulletSpeed;
    public float playerNum;

    private float currLifeTime = 0;

    private void Start()
    {
        currLifeTime = bullet.bulletLifeTime;
    }

    private void Update()
    {
        currLifeTime -= Time.deltaTime;

        if (currLifeTime <= 0)
        {
            Destroy(gameObject);
        }

        bullet.Movement(gameObject, bulletDirection, bulletSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<BulletBehavior>().playerNum == playerNum){
            return;
        }

        Collider[] toBePushedObjs = Physics.OverlapSphere(collision.contacts[0].point, bullet.blastRadius);
        Collider[] selfBlastObjs = Physics.OverlapSphere(collision.contacts[0].point, bullet.selfBlastRadius);

        foreach (Collider hitObj in toBePushedObjs)
        {
            Rigidbody rb = hitObj.GetComponent<Rigidbody>();

            if (rb != null && hitObj.gameObject.name != "Player " + (playerNum).ToString()){
                rb.AddExplosionForce(bullet.blastForce * rb.mass, collision.contacts[0].point, bullet.blastRadius, 1, ForceMode.Impulse);
            }
        }

        foreach (Collider obj in selfBlastObjs)
        {
            if (obj.gameObject.name == "Player " + (playerNum).ToString())
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.AddExplosionForce(bullet.selfBlastForce * rb.mass, collision.contacts[0].point, bullet.selfBlastRadius, 1, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
