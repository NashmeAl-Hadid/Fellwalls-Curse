using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;


    public Attribute[] attributes;

    public bool key = false; 
    private void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].OnBeforeUpdated += OnBeforeSlotUpdate;
            inventory.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }


    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        
        if (_slot.ItemObject == null)
            return;
        print("Before Update");
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:


                print(string.Concat("Placed ", _slot.ItemObject, "On", _slot.parent.inventory.type, ",Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {

                    for (int j = 0; j < attributes.Length; j++)
                    {

                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }


                if (_slot.item.Id == 0)
                {

                }
                if (_slot.item.Id == 1)
                {

                }
                if (_slot.item.Id == 3)
                {
                    key = true;
                }


                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }




    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
 
                print(string.Concat("Remove", _slot.ItemObject, "On", _slot.parent.inventory.type, ",Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {

                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }

    }


    private void OnTriggerEnter2D(Collider2D other)


    { 
    
         var item = other.GetComponent<GroundItem>();

            if (item)
            {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }

            
            }



    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "NPC"&& Input.GetKey(KeyCode.T))
        {
            other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        if (other.gameObject.tag == "Door" && key == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                print("DoorDone");
                Destroy(other.gameObject);
            }


        }
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    inventory.Save();
        //}

        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    inventory.Load();
        //}
    }

    public void AttributeModifided(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, "was updated value isnow", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
      
        inventory.Clear();

    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;
    public static ModifiableInt BaseValuess;


    public void SetParent(Player _parent)
    {
        BaseValuess = value;
        parent = _parent;
        value = new ModifiableInt(AttributeModifided);
    }
    public void AttributeModifided()
    {
        parent.AttributeModifided(this);
    }
}
