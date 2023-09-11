using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{

    public TextMeshProUGUI position;

    public TextMeshProUGUI points;

    public void SetRankItem(string position, string points)
    {
        this.position.text = position;
        this.points.text = string.Format(points, "Pts");
    }
}
