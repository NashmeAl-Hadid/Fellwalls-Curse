using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Item/Equipment")]
public class EquipmentObject : ItemObject
{
 //   public float atkBonus;
//    public float defenceBonus;

    public void Awake()
    {
        types = ItemType.Equipment;
    }

}
