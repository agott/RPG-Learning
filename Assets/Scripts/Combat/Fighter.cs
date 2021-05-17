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
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health currentTarget = null;
        Health health;
        Mover mover;
        Animator animator;
        Weapon currentWeapon = null;
        float timeSinceLastAttacked = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weaponToSpawn)
        {
            currentWeapon = weaponToSpawn;
            weaponToSpawn.Spawn(rightHandTransform, leftHandTransform, animator);
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
            return Vector3.Distance(this.transform.position, currentTarget.transform.position) < currentWeapon.GetWeaponRange();

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

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, currentTarget);
            }
            else
            {
                currentTarget.TakeDamage(currentWeapon.GetWeaponDamage());
            }
        }

        void Shoot()
        {
            Hit();
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
