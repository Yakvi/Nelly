using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Window, ISlideUI, IButtons
{
    public int ButtonCount;

    private Image image;
    private TextMeshProUGUI subtitle;
    private TextMeshProUGUI title;
    private TMButton[] buttons;

    public override void Awake()
    {
        base.Awake();
        // TODO: change this to allow for multiple UI instances!
        image = GameObject.Find("OutputImage").GetComponent<Image>();
        title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        subtitle = GameObject.Find("Subtitle").GetComponent<TextMeshProUGUI>();

        ButtonCount = (int) Selection.Count;

        buttons = new TMButton[ButtonCount];
        for (int i = 0; i < ButtonCount; i++)
        {
            buttons[i] = GameObject.Find($"Button{i}").GetComponent<TMButton>();
        }
    }

    private void SetText(TextMeshProUGUI dest, string text)
    {
        dest.text = text;
    }

    public TMButton GetButton(int index)
    {
        TMButton result = null;

        if (buttons[index].IsActive)
        {
            result = buttons[index];
        }

        return result;
    }

    public void SetButtonText(string text, int pos = 3)
    {
        buttons[pos].SetText(text);
    }

    public void Clear()
    {
        // TODO: Do we want to preserve previous image or reset to nothing? 
        //       We reset previous for now
        image.sprite = null;
        foreach (var button in buttons)
        {
            button.SetText("");
        }
    }


    public void SetTitle(string text)
    {
        SetText(title, text);
    }

    public void SetSubtitle(string text)
    {
        SetText(subtitle, text);
    }

    public void SetPicture(Sprite newSprite, Color tint)
    {
        if (newSprite) image.sprite = newSprite;
        if (tint != Color.white) image.color = tint;
    }
}
