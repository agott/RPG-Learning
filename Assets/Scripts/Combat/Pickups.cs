using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Pickups : MonoBehaviour
    {
        [SerializeField] Weapon weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player"){
                other.GetComponent<Fighter>().EquipWeapon(weapon.name);
                StartCoroutine(PickUpRespawn());
            }
        }

        private IEnumerator PickUpRespawn()
        {
            HidePickups();
            yield return new WaitForSeconds(2f);
            ShowPickups();
        }

        private void HidePickups()
        {
            print("Coroutine Started");
            GetComponent<BoxCollider>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        private void ShowPickups()
        {
            GetComponent<BoxCollider>().enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
