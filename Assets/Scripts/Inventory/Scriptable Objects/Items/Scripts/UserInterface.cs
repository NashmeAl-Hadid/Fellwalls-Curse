using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory;
    public DynamicInteface g_script;
    private int valuess;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        g_script = GameObject.Find("Panel").GetComponent<DynamicInteface>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }

        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate {OnEnterInferace(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate {OnExitInferace(gameObject); });
     
    }

    private void OnSlotUpdate(InventorySlot _slot)
    {

        if (_slot.item.Id >= 0)
        {
            _slot.storedSlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
            _slot.storedSlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.storedSlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
        }
        else
        {
            _slot.storedSlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.storedSlotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.storedSlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }


    }





    
    public abstract void CreateSlots();
   // public abstract void DisplayItemInfo(string itemname, Vector2 buttonpos);
   // public abstract void DestroyItemInfo();



    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {

        
        MouseData.SlotHoveredOver = obj;
        if (MouseData.SlotHoveredOver&&slotsOnInterface[obj].item.Id >= 0)  // checks if hovered over slot
        {


            //ItemBuff[] buffs = slotsOnInterface[obj].item.buffs;
            
            //for (int i = 0; i < buffs.Length; i++)
            //{
            //    buffs[i] = new ItemBuff(slotsOnInterface[obj].item.buffs[i].min, slotsOnInterface[obj].item.buffs[i].max)
            //    {
            //        attribute = slotsOnInterface[obj].item.buffs[i].attribute
                    
            //    };
            //}
            //int s = 0;
            g_script.DisplayItemInfo(slotsOnInterface[obj].item.Name, slotsOnInterface[obj].item.Description, slotsOnInterface[obj].item.buffs[0].value.ToString(), transform.position);
                MouseData.HoveringOverItem = true;


        }
    }
    public void OnExit(GameObject obj)
    {
        MouseData.SlotHoveredOver = null;
        if (MouseData.HoveringOverItem == true)
        {
            g_script.DestroyItemInfo();

            MouseData.HoveringOverItem = false;

        }



    }
    public void OnExitInferace(GameObject obj)
    {

        MouseData.interfaceMouseIsOver = null;

    }
    public void OnEnterInferace(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        

    }
    public void OnDragStart(GameObject obj)
    {
        
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;

          //  MouseData.hoverItem.storedSlotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = MouseData.hoverItem.amount.ToString();
        }
        return tempItem;


    }


    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.SlotHoveredOver) // checks if hovered over slot
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.SlotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            
        }

    }
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)

        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

     public void OnClick(GameObject obj)
     {
        Destroy(MouseData.tempItemBeingDragged);
       
        if (MouseData.LeftCLick == true)
        {
            slotsOnInterface[obj].RemoveItem();
            MouseData.LeftCLick = false;
        }    
     }
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MouseData.interfaceMouseIsOver)
        {
           MouseData.LeftCLick = true;
        }
    }

}





public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static InventorySlot item;
    public static InventorySlot hoverItem;
    public static GameObject SlotHoveredOver;
    public static bool LeftCLick = false;
    public static bool HoveringOverItem = false;
}




