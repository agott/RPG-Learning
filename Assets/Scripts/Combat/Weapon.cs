using RPG.Core;
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

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {

            if (equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHandTransform, leftHandTransform);
                Instantiate(equippedPrefab, handTransform);
            }
            if (animationOverride != null)
            {
                animator.runtimeAnimatorController = animationOverride;
            }
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