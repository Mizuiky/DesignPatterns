using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI points;

    public void Start()
    {
        UpdatePoints("0");
    }

    public void UpdatePoints(string value)
    {
        points.text = value;
    }
}
