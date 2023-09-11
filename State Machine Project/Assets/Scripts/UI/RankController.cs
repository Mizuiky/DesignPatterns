using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RankController : MonoBehaviour
{
    public GameObject rankItem;

    public Transform rankItemContainer;

    private int [] rank;

    public void SetRank(int [] rank)
    {
        this.rank = rank;

        for (int i = 0; i < rank.Length; i++)
        {
            var obj = Instantiate(rankItem, rankItemContainer);

            if (obj != null)
            {
                RankItem item = obj.GetComponent<RankItem>();

                if (item != null)
                {
                    var count = i + 1;

                    item.SetRankItem(count.ToString(), rank[i].ToString());

                    item.transform.DOScale(1f, 0.5f).From(0.3f).SetEase(Ease.OutBack);
                }
            }
        }            
    }
}
