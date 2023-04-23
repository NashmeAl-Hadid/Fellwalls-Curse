using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Item/Potion")]
public class PotionObject : ItemObject
{
 //   public int restoreHealthValue;

    public void Awake()
    {
        types = ItemType.Potion;
    }
}
