using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float fightRange = 2f;

        Transform currentTarget = null;
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (currentTarget == null) return;

            if (IsInRange())
            {
                mover.CancelMovement();
            }
        }

        private bool IsInRange()
        {
            float distanceBetweenCharacters = Vector3.Distance(this.transform.position, currentTarget.transform.position);
            if (distanceBetweenCharacters < fightRange) {
                return true;
            }
            return false;
        }

        public void Attack(CombatTarget target)
        {
            currentTarget = target.transform;
            mover.MoveTo(currentTarget.position);
        }
    }
}
