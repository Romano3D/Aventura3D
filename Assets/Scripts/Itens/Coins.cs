using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class Coins : ItemCollactableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddByType(ItemType.COIN); 
    }
}