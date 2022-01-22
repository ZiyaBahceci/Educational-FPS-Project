using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

    void Start()
    {
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }


    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)                //enemy AI (chase the player, if out of range, go to last position)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }


            }
            if(agent.remainingDistance < 0.25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);

            }
        }

        else
        {

            //transform.LookAt(targetPoint);

            //theRB.velocity = transform.forward * moveSpeed;

            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }
            else
            {
                agent.destination = transform.position;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)      //if out of range 
            {
                chasing = false;                   //stop chasing

                chaseCounter = keepChasingTime;
            }


            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if(shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else
            {

                shootTimeCounter -= Time.deltaTime;

                if (shootTimeCounter > 0)
                {

                    fireCount -= Time.deltaTime;

                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;

                        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                        if(Mathf.Abs(angle) < 30f)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);

                            anim.SetTrigger("fireShot");
                        }
                        else
                        {
                            shotWaitCounter = waitBetweenShots;

                        }

                        anim.SetBool("isMoving", false);    
                    }
                    agent.destination = transform.position;
                }   
                 else
                 {
                      shotWaitCounter = waitBetweenShots;
                 }
            }
        }
    }
}


