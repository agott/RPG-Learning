using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using RPG.Resources;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 1f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float withinRangeOfWaypoint = .5f;
        [SerializeField] float dwellTime = 2f;
        [SerializeField] float attackSpeed = 3f;
        [SerializeField] float patrolSpeed = 1.5f;

        Mover mover;
        Fighter fighter;
        Health health;
        GameObject player;
        NavMeshAgent navMeshAgent;
        Vector3 guardPosition;
        int currentWaypoint = 0;
        float timeSinceLastSeenPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            navMeshAgent = GetComponent<NavMeshAgent>();
            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange(player) && fighter.CanAttack(player))
            {
                AttackBehavior();
            }
            else if (timeSinceLastSeenPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSeenPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSinceLastSeenPlayer = 0;
            fighter.Attack(player.gameObject);
            navMeshAgent.speed = attackSpeed;
        }

        private void PatrolBehavior()
        {
            navMeshAgent.speed = patrolSpeed;
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint(currentWaypoint))
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

                if (SufficientTimeWaited())
                {
                    mover.StartMoveAction(nextPosition);
                }
            }
        }

        private bool SufficientTimeWaited()
        {
            return timeSinceArrivedAtWaypoint >= dwellTime;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypoint);
        }

        private void CycleWaypoint()
        {
            currentWaypoint = patrolPath.GetNextWaypoint(currentWaypoint);
        }

        private bool AtWaypoint(int waypoint)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, patrolPath.GetWaypoint(waypoint));
            return distanceToWaypoint < withinRangeOfWaypoint;
        }

        private bool InAttackRange(GameObject player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
