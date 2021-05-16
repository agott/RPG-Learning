using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100;

        bool isDead = false;

        public void TakeDamage (float damage)
        {
            health = Mathf.Max(health -damage, 0);
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("die");
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
