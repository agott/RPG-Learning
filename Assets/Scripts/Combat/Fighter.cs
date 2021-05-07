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

        Transform currentTarget = null;
        Mover mover;
        Animator animator;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (currentTarget == null) return;

            if (!IsInRange())
            {
                mover.MoveTo(currentTarget.position);
            } else
            {
                mover.Cancel();
                StartAttackBehavior();
            }
        }

        private void StartAttackBehavior()
        {
            //animator.ResetTrigger("stopAttacking");
            animator.SetTrigger("attack");
        }

        private bool IsInRange()
        {
            float distanceBetweenCharacters = Vector3.Distance(this.transform.position, currentTarget.transform.position);
            if (distanceBetweenCharacters < weaponRange) {
                return true;
            }
            return false;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            currentTarget = target.transform;
            mover.MoveTo(currentTarget.position);
        }

        void Hit()
        {
            if (currentTarget != null)
            {
                print("Doing Damage");
            }

        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttacking");
            //animator.ResetTrigger("attack");
        }
    }
}
