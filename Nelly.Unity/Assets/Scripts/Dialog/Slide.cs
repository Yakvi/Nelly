using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Slide", menuName = "Dialog/Slide", order = 0)]
public class Slide : Narrative
{
    // TODO: Extract this even further? To produce a unique item which would be a color / text / image / combination of these
    public Sprite Image;
    public Color ImageTint = Color.white;
    public AudioClip Sound;
    public string ImageText = "";

    public string DialogText = "";
    public Choice[] Choices;

    public override Slide GetNextSlide() => this;
    public override Unit GetNextUnit() => null;
    public override void Reset()
    {
        // No action needed for now
    }

    public bool IsLinear()
    {
        var result = false;

        if (Choices != null)
        {
            var found = 0;
            foreach (var choice in Choices)
            {
                if (choice != null && !String.IsNullOrWhiteSpace(choice.Text))
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
