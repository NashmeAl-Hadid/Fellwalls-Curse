using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Item/Default")]

public class DefaultObject : ItemObject
{
    public void Awake()
    {
        types = ItemType.Default;
    }
}
