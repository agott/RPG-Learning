using RPG.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHeatSeeking = true;
        [SerializeField] GameObject impactEffect = null;
        [SerializeField] GameObject[] objectsToDestroy = null;
        [SerializeField] float timeTillDestroy = 2f;

        Health target = null;
        float givenDamage = 0;
        GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            StartCoroutine(DieAfterTime());
        }


        private void Update()
        {
            if (target == null) return;

            if (isHeatSeeking && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        public void SetTarget(Health target, float damage, GameObject instigator)
        {
            this.instigator = instigator;
            this.target = target;
            givenDamage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;

            if (target.IsDead()) return;

            target.TakeDamage(givenDamage, instigator);

            speed = 0;

            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            foreach (GameObject objectToDestroy in objectsToDestroy)
            {
                Destroy(objectToDestroy);
            }

            Destroy(gameObject, timeTillDestroy);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 1.4f;
        }


        private IEnumerator DieAfterTime()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
