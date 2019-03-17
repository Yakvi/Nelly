using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;

[System.Serializable]
[CreateAssetMenu(fileName = "Slide", menuName = "Game/Dialog/Slide", order = 0)]
public class Slide : Node, INarrative
{
    // TODO: Extract this even further? To produce a unique item which would be a color / text / image / combination of these
    [Input]
    public Sprite Image;
    [Input]
    public Location PlayerPosition;
    //public Location OtherPOI; TODO: Add logic linked to these POI

    public Color ImageTint = Color.white;

    #region NOTE : Sounds
    public AudioClip Sound;
    [Range(0.0f, 1.0f)]
    public float SoundVolume = 1.0f;
    
    #endregion

    [Multiline]
    public string ImageText = "";
    [Multiline]
    public string DialogText = "";
    [Input]
    public Choice PreviousChoice;
    [Output]
    public Choice[] Choices;
    // GetValue should be overridden to return a value for any specified output port
    public override object GetValue(NodePort port)
    {
        return Choices;
    }

    public Slide GetNextSlide() => this;
    public INarrative GetNextUnit() => null;

    public void Reset()
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
