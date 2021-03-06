using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] Progression progression;
        [SerializeField] CharacterClasses characterClass;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(characterClass, stat, startingLevel);
        }
    }
}
