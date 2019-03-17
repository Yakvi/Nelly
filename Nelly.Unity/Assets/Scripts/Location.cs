using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Location", menuName = "Game/Location", order = 0)]
public class Location : ScriptableObject
{
    public string Title = "";
    // TODO: draggable gizmo
    public Vector2 Position;
    public Sprite Icon;
    public bool IsTemporary;
    
    public AudioClip Ambient;
    [Range(0.0f, 1.0f)]
    public float AmbientVolume = 1.0f;
}
