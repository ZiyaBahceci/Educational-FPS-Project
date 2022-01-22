using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DamageEnemy(int damageAmount)
    {
        currentHealth-= damageAmount;            //decrease health

        if (currentHealth <= 0)      //if health drops to 0, destroy the target
        {
            Destroy(gameObject);
        }
    }
}
