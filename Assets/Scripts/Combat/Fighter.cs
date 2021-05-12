using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health currentTarget = null;
        Health health;
        Mover mover;
        Animator animator;
        float timeSinceLastAttacked = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            timeSinceLastAttacked += Time.deltaTime;

            if (currentTarget == null) return;
            if (currentTarget.IsDead())
            {
                Cancel();
                return;
            }

            if (!GetIsInRange())
            {
                mover.MoveTo(currentTarget.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }


        }

        private void AttackBehavior()
        {
            transform.LookAt(currentTarget.transform.position);
            if (timeSinceLastAttacked > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttacked = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(this.transform.position, currentTarget.transform.position) < weaponRange;

        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            currentTarget = combatTarget.GetComponent<Health>();
        }

        void Hit()
        {
            if (currentTarget == null) return;  
            currentTarget.TakeDamage(weaponDamage);
        }

        public void Cancel()
        {
            StopAttack();
            currentTarget = null;
            mover.Cancel();
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}
