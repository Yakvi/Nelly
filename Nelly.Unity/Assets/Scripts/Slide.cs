using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Slide", menuName = "Dialog/Slide", order = 0)]
public class Slide : ScriptableObject
{
    // TODO: Extract this even further? To produce a unique item which would be a color / text / image / combination of these
    public Sprite Image;
    public Color ImageTint = Color.white;
    public string ImageText = "";

    public string DialogText = "";
    public Choice[] Choices;

    public bool IsLinear()
    {
        var result = false;

        if (Choices != null)
        {
            var found = 0;
            foreach (var choice in Choices)
            {
                if (!String.IsNullOrWhiteSpace(choice.Text))
                {
                    ++found;
                }
            }

            if (found < 2)
            {
                result = true;
            }
            
        }

        return result;
    }
}
