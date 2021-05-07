using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100;

        bool isDead = false;

        public void TakeDamage (float damage)
        {
            health = Mathf.Max(health -damage, 0);
            if (health == 0)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
