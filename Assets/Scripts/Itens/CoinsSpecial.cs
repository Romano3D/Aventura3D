using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;
public class CoinsSpecial : ItemCollactableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddByType(ItemType.COINSPECIAL);
    }
    }


