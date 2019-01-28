using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TMButton : MonoBehaviour
{
    public string Text;
    public GameObject This;
    private TextMeshProUGUI TextMesh;
    // Start is called before the first frame update
    void Start()
    {
        TextMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        This = gameObject;
    }

    void Update() {
        TextMesh.text = Text;
    }
}
