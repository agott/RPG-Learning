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
        Mover mover;
        Animator animator;
        float timeSinceLastAttacked = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
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
                transform.LookAt(currentTarget.transform.position);
                StartAttackBehavior();
            }


        }

        private void StartAttackBehavior()
        {
            if (timeSinceLastAttacked > timeBetweenAttacks)
            {
                StartAttack();
                timeSinceLastAttacked = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(this.transform.position, currentTarget.transform.position) < weaponRange;

        }

        public void Attack(Health combatTarget)
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
        }

        private void StartAttack()
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
