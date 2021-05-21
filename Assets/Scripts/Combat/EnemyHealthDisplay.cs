using RPG.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text healthDisplay;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthDisplay = GetComponent<Text>();
        }

        private void Update()
        {
            if (fighter.CurrentTarget() == null)
            {
                healthDisplay.text = "N/A";
                return;
            }
            healthDisplay.text = String.Format("{0:0}%", fighter.CurrentTarget().GetHealthPercentage());
        }
    }
}
