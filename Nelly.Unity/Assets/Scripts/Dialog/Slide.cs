using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Slide", menuName = "Game/Dialog/Slide", order = 0)]
public class Slide : Narrative
{
    // TODO: Extract this even further? To produce a unique item which would be a color / text / image / combination of these
    public Sprite Image;
    public Location PlayerPosition;
    //public Location OtherPOI; TODO: Add logic linked to these POI

    public Color ImageTint = Color.white;

    #region NOTE: Sounds
    public AudioClip Sound;
    [Range(0.0f, 1.0f)]
    public float SoundVolume = 1.0f;

    public AudioClip Ambient;
    [Range(0.0f, 1.0f)]
    public float AmbientVolume = 1.0f;
    #endregion

    [Multiline]
    public string ImageText = "";
    [Multiline]
    public string DialogText = "";
    public Choice[] Choices;

    public override Slide GetNextSlide() => this;
    public override Narrative GetNextUnit() => null;
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
