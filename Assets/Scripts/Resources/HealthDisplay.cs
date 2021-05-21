using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text healthDisplay; 

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthDisplay = GetComponent<Text>();
        }

        private void Update()
        {
            healthDisplay.text = String.Format("{0:0}%", health.GetHealthPercentage());
        }
    }
}
