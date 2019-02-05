using UnityEngine;

public abstract class Narrative : ScriptableObject
{
    public abstract Slide GetNextSlide();
    public abstract Unit GetNextUnit();
    public abstract void Reset();
}
