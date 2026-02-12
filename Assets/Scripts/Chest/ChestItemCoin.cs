using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinAmount = 5;
    public GameObject coinObject;
    public Vector2 randomRange = new Vector2(0.5f, 1.5f);
    public float tweenDuration = 1f;

    private List<GameObject> _coins = new List<GameObject>();


    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
        Collect();
    }

    void CreateItems()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var item = Instantiate(coinObject,
                transform.position + Vector3.up * Random.Range(randomRange.x, randomRange.y) + Vector3.forward * Random.Range(randomRange.x, randomRange.y), Quaternion.identity);
            item.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
            _coins.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach (var coin in _coins)
        {
            coin.transform.DOMoveY(2f, tweenDuration).SetRelative();
            coin.transform.DOScale(0, tweenDuration / 2).SetDelay(tweenDuration / 2);
            ItemManager.Instance.AddByType(ItemType.Coin);
        }
    }
}