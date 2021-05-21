using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentHealth = 0f;

        bool isDead = false;
        GameObject instigator;

        private void Start()
        {
            currentHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public void TakeDamage (float damage, GameObject instigator)
        {
            this.instigator = instigator;
            currentHealth = Mathf.Max(currentHealth -damage, 0);
            if (currentHealth == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("die");
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.RewardExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public bool IsDead()
        {
            return isDead;
        }

        public float GetHealthPercentage()
        {
            return 100 *(currentHealth/ GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (float)state;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
}
