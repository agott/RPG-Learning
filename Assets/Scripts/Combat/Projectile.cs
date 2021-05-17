using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHeatSeeking = true;

        Health target = null;
        float givenDamage = 0;
        bool hasTarget = false;

        private void Update()
        {
            if (target == null) return;

            if (hasTarget && !isHeatSeeking)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                return;
            }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            givenDamage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;

            target.TakeDamage(givenDamage);
            Destroy(gameObject);
        }

        private Vector3 GetAimLocation()
        {
            hasTarget = true;
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 1.4f;
        }
    }
}
