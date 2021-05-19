using RPG.Core;
using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order =0)]
    public class Weapon: ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animationOverride = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHandTransform, leftHandTransform);
                GameObject instantiatedWeapon = Instantiate(equippedPrefab, handTransform);
                instantiatedWeapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (animationOverride != null)
            {
                animator.runtimeAnimatorController = animationOverride;
            } 
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(weaponName);

            if (oldWeapon == null)
            {
                oldWeapon = leftHandTransform.Find(weaponName);
            }

            if (oldWeapon == null) return;

            oldWeapon.name = "Destorying";

            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile instancedProjectile = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            instancedProjectile.SetTarget(target, weaponDamage);
        }

        public bool HasProjectile()
        {
            return projectile != null ;
        }


        private Transform GetHandTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            return handTransform;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}