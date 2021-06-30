using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    public int damage = 1;

    //public bool damageEnemy, damagePlayer;

    void Start()
    {
        
    }

    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;         

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)           //destroy bullets after a certain time
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)                 
    {
        if(other.gameObject.tag == "Enemy")             //if bullet hits enemy
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        if(other.gameObject.tag == "Headshot")
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 3);
            Debug.Log("Headshot");
        }


        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player at " + transform.position);
        }


        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation); //Bullet Hit Effect
        Destroy(gameObject); //destroy bullet upon contact            
    }
}
