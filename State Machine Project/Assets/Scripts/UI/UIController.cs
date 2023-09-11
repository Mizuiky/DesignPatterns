using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI points;
    public SOINT score;

    public RankController rankController;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        points.text = score.value.ToString();
    }

    private void Init()
    {
        points.text = "";
        rankController.gameObject.SetActive(false);
    }

    public void OpenRankScreen(int [] rank)
    {
        rankController.gameObject.SetActive(true);

        rankController.SetRank(rank);
    }
}
