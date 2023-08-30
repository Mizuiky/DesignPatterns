using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI points;
    public SOINT score;

    public void Start()
    {
        points.text = "";
    }

    public void Update()
    {
        points.text = score.value.ToString();
    }
}
