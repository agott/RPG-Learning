using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControls;
            GetComponent<PlayableDirector>().stopped += EnableControls;
        }

        void DisableControls(PlayableDirector obj)
        {
            print("Disabled Controls");
        }

        void EnableControls(PlayableDirector obj)
        {
            print("Enabled Controls");
        }
    }
}
