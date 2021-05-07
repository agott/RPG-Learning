using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Animator animator;
        NavMeshAgent navMeshAgent;
        Ray ray;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.MoveTo(hit.point);
                }
            } 
            else
            {
                print("Nothing to do");
            }

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            animator.SetFloat("Locomotion", localVelocity.z);
        }
    }
}
