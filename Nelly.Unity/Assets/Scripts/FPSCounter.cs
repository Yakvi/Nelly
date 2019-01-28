using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float[] frameCounters = new float[30];
    private int frameIndex = 0;

    void Update()
    {
        var counter = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        if (counter)
        {
            var msPerFrame = StoreValue(Time.deltaTime * 1000);
            var averageMS = calculateMS();
            var FPS = 1000 / averageMS;

            counter.text = averageMS.ToString("#.##") + " ms\n";
            counter.text += (int)FPS + " fps\n";
        }
    }

    float calculateMS()
    {
        float total = 0;
        foreach (var frame in frameCounters)
        {
            total += frame;
        }
        var average = total / frameCounters.Length;

        return average;
    }

    float StoreValue(float value)
    {
        frameCounters[frameIndex++] = value;
        if (frameIndex >= frameCounters.Length)
        {
            frameIndex = 0;
        }
        return value;
    }
}
