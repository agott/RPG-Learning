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
        [SerializeField] float timeBetweenAttacks = 2f;

        float timeSinceLastAttack = Mathf.Infinity;
        Health currentTarget;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (currentTarget == null) return;
            if (currentTarget.IsDead()) return;



            bool isInRange = Vector3.Distance(transform.position, currentTarget.transform.position) < weaponRange;
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(currentTarget.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(currentTarget.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttacking");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (currentTarget == null) return;
            currentTarget.TakeDamage(5f);
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            currentTarget = target.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            currentTarget = null;
            StopAttack();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("stopAttacking");
            GetComponent<Animator>().ResetTrigger("attack");
        }
    }
}
