using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float cubeSize = .2f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(GetWaypoint(i), new Vector3(cubeSize, cubeSize, cubeSize));
                Gizmos.color = Color.white;
                int j = GetNextWaypoint(i);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }

        }

        public int GetNextWaypoint(int i)
        {
            if (i >= transform.childCount - 1)
            {
                return 0;
            }
            else
            {
                return i+1;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
