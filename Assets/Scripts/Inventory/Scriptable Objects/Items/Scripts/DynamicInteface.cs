using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DynamicInteface : UserInterface
{
    public GameObject inventoryPrefab;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;


    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentIteminfo = null;
    public float moveX = 0f;
    public float moveY = 0f;

    public void DisplayItemInfo(string itemname, string itemDescription,string itemValue, Vector2 buttonpos)
    {
        if (currentIteminfo != null)
        {
            Destroy(currentIteminfo.gameObject);
        }
      //  itemInfoPrefab.gameObject.transform.position = buttonpos;
      //  buttonpos = new Vector3(moveX, ,0);
         buttonpos.x += moveX;
         buttonpos.y += moveY;


        currentIteminfo = Instantiate(itemInfoPrefab, buttonpos, Quaternion.identity, canvas);
        currentIteminfo.GetComponent<ItemInfo>().Setup(itemname,itemDescription, itemValue);
    }
    public void DestroyItemInfo()
    {

        if (currentIteminfo != null)
        {
            Destroy(currentIteminfo.gameObject);
        }
    }
    public override void CreateSlots()
    {

        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            


            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            

            inventory.GetSlots[i].storedSlotDisplay = obj;



            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }


 

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    internal void DisplayItemInfo(string name, string description, ItemBuff itemBuff, Vector3 position)
    {
        throw new NotImplementedException();
    }
}
