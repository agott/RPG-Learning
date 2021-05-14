using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        float alpha = 0f;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = 1f;
            StartCoroutine(FadeIn(1f));
        }

        public IEnumerator FadeOut(float time)
        {
            while (GetComponent<CanvasGroup>().alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime/time;
                yield return null ;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            while (GetComponent<CanvasGroup>().alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
