using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        Text healthDisplay;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            healthDisplay = GetComponent<Text>();
        }

        private void Update()
        {
            healthDisplay.text = String.Format("{0:0}", experience.GetExperiencePoints());
        }
    }
}
