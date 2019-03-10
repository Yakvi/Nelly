using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [Range(0, 12)]
    public int Minutes;
    [Range(0, 24)]
    public int Hours;
    public int Days;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateHoursPass(12);

        if (Hours > 24)
        {
            Hours = 0;
            ++Days;
        }
    }

    private void CalculateHoursPass(int hourLength)
    {
        if (Minutes == hourLength)
        {
            Minutes = 0;
            ++Hours;
        }
        else if (Minutes > hourLength)
        {
            var hoursPassed = (int) Minutes / hourLength;
            Hours += hoursPassed;
            Minutes -= hourLength;
            CalculateHoursPass(hourLength);
        }
        else if (Minutes < 0)
        {
            Minutes = 0;
        }
    }
}
