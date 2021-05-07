using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }
    }
}
