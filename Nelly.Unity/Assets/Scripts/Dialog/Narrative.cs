using UnityEngine;

public abstract class Narrative : ScriptableObject
{
    public abstract Slide GetNextSlide();
    public abstract Narrative GetNextUnit();
    public abstract void Reset();
}
