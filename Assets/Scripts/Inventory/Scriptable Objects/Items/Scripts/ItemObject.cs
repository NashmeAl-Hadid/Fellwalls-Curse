using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ItemType
{
    Potion,
    Equipment,
    Default,
    Key
}

public enum Attributes
{
    Agillity,
    Intellect,
    Stamina,
    Strength,
    Key

}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{

    public Sprite uiDisplay;
    public TextMeshProUGUI _text;
    public bool stackable;
    public ItemType types;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();


    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }


}

[System.Serializable]
public class Item
{
    public string Name;
    public string Description;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        Description = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Description = item.description;
        
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifiers
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}