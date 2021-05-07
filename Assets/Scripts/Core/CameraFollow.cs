using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        Transform player;
        Vector3 offset;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            offset = this.transform.position - player.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            this.transform.position = player.transform.position + offset;
        }
    }
}
