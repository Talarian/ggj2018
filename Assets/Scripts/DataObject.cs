using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataObject : Indicator
{
    public string dataValue;
    public TextMeshProUGUI valueNormal;
    public TextMeshProUGUI valueHighlight;

    void Start()
    {
        normal = valueNormal.transform.parent.gameObject;
        normal.SetActive(true);
        highlight = valueHighlight.transform.parent.gameObject;
        highlight.SetActive(false);
    }

    public void ChangeValue(string newValue)
    {
        valueNormal.text = newValue;
        valueHighlight.text = newValue;
        DoFlashing();
    }
}