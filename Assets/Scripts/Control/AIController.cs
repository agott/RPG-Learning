using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float detectRange = 10f;

        Mover mover;
        Fighter fighter;
        Animator animator;
        NavMeshAgent navMeshAgent;
        GameObject player;
        Vector3 startLocation;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player");
            startLocation = transform.position;
        }

        void Update()
        {
            if (WithinRange())
            {
                fighter.Attack(player.GetComponent<Health>());
            }else
            {
                mover.StartMoveAction(startLocation);
            }
        }

        private bool WithinRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) < detectRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
    }
}
