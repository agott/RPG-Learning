using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Animator animator;
        NavMeshAgent navMeshAgent;
        Ray ray;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<CombatTarget>())
                {
                    if (Input.GetMouseButton(0))
                    {
                        fighter.Attack(hit.transform.GetComponent<CombatTarget>());
                        return true;
                    }
                }
            }
            return false;

        }


        private bool InteractWithMovement()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.MoveTo(hit.point);
                    return true;
                }
            }
            else
            {
                print("Nothing to do");
            }
            return false;
        }
    }
}
