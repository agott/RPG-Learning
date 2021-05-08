using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 1f;
        [SerializeField] float suspicionTime = 2f;

        Mover mover;
        Fighter fighter;
        Health health;
        GameObject player;
        Vector3 guardPosition;
        float timeSinceLastSeenPlayer = Mathf.Infinity;

        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange(player) && fighter.CanAttack(player))
            {
                timeSinceLastSeenPlayer = 0;
                AttackBehavior();
            }
            else if(timeSinceLastSeenPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player.gameObject);
        }

        private void GuardBehavior()
        {
            mover.StartMoveAction(guardPosition);
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
