using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class ItemCollactableCoin : ItemCollactableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
    }
}
