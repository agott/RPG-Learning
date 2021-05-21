using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        Health currentTarget = null;
        Mover mover;
        Animator animator;
        Weapon currentWeapon = null;
        float timeSinceLastAttacked = Mathf.Infinity;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeaponName);
            }
        }

        public void EquipWeapon(string weaponName)
        {
            Weapon weaponToSpawn = UnityEngine.Resources.Load<Weapon>(weaponName);
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
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, currentTarget, this.gameObject);
            }
            else
            {
                currentTarget.TakeDamage(currentWeapon.GetWeaponDamage(), this.gameObject);
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

        public Health CurrentTarget()
        {
            return currentTarget;
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            EquipWeapon(weaponName);
        }
    }
}
