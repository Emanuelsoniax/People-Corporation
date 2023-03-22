using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class DaylightCycle
{
    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    private Transform backgroundTransform;
    [SerializeField]
    private Vector2 displacementPos;

   public IEnumerator LerpColor(float time)
    {
        float startHue = 180;
        float S;
        float V;
        float endHue = 0;

        Color.RGBToHSV(globalLight.color, out startHue, out S, out V);

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            globalLight.color = Color.HSVToRGB(Mathf.Lerp(startHue, endHue, (elapsedTime / time)), S, V);
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
    }

    public IEnumerator ScrollBackground(float time)
    {
        Vector2 startPos = backgroundTransform.position;

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            backgroundTransform.position = Vector2.Lerp(startPos, displacementPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }


    }
}
