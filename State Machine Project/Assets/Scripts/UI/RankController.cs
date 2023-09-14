using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RankController : MonoBehaviour
{
    public GameObject rankItem;

    public Transform rankItemContainer;

    public void Reset()
    {
        foreach(Transform child in rankItemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetRank(int [] rank)
    {

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
