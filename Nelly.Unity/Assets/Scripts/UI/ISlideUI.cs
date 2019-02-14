using UnityEngine;

public interface ISlideUI
{
    void Clear();
    void SetTitle(string text);
    void SetSubtitle(string text);
    void SetPicture(Sprite newSprite, Color tint);
}
