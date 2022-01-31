using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 10f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
