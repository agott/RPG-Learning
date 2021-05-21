using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experience;

        public void RewardExperience(float experienceAmount)
        {
            experience += experienceAmount;
        }

        public float GetExperiencePoints()
        {
            return experience;
        }

        public object CaptureState()
        {
            return experience;
        }

        public void RestoreState(object state)
        {
            experience = (float)state;
        }
    }
}
