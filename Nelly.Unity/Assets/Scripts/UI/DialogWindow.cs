using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Window, ISlideUI, IButtons
{
    public int ButtonCount;

    public Image Image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Subtitle;
    public TMButton[] Buttons;

    public override void Awake()
    {
        base.Awake();
        
        // Image = GameObject.Find("OutputImage").GetComponent<Image>();
        // Title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        // Subtitle = GameObject.Find("Subtitle").GetComponent<TextMeshProUGUI>();

        // ButtonCount = (int) Selection.Count;

        // Buttons = new TMButton[ButtonCount];
        // for (int i = 0; i < ButtonCount; i++)
        // {
        //     Buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        // }
    }

    private void SetText(TextMeshProUGUI dest, string text)
    {
        dest.text = text;
    }

    public TMButton GetButton(int index)
    {
        TMButton result = null;

        if (Buttons[index].IsActive)
        {
            result = Buttons[index];
        }

        return result;
    }

    public void SetButtonText(string text, int pos = 3)
    {
        Buttons[pos].SetText(text);
    }

    public void Clear()
    {
        // TODO: Do we want to preserve previous image or reset to nothing? 
        //       We reset previous for now
        Image.sprite = null;
        foreach (var button in Buttons)
        {
            button.SetText("");
        }
    }

    public void SetTitle(string text)
    {
        SetText(Title, text);
    }

    public void SetSubtitle(string text)
    {
        SetText(Subtitle, text);
    }

    public void SetPicture(Sprite newSprite, Color tint)
    {
        if (newSprite) Image.sprite = newSprite;
        if (tint != Color.white) Image.color = tint;
    }
}
