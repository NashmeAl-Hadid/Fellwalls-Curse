//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;


//public class DisplayInventory : MonoBehaviour
//{


//    public GameObject inventoryPrefab;
//    public InventoryObject inventory;

//    public int X_START;
//    public int Y_START;

//    public int X_SPACE_BETWEEN_ITEM;
//    public int NUMBER_OF_COLUMNS;
//    public int Y_SPACE_BETWEEN_ITEMS;

//    Dictionary<GameObject,InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

//    private void Start()
//    {
//        CreateSlots();
//    }

//    private void Update()
//    {
//        UpdateSlots();
//    }

//    public void UpdateDisplay()
//    {

//    }
//    public void UpdateSlots()
//    {
//        foreach (KeyValuePair<GameObject,InventorySlot> _slot in itemsDisplayed)
//        {
//            if (_slot.Value.item.Id >= 0) 
//            {
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
//                _slot.Key.GetComponentInChildren <TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
//            }
//            else
//            {
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
//                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =  "";
//            }
//        }
//    }
//    public void CreateSlots()
//    {
//        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
//        for (int i = 0; i < inventory.Container.items.Length; i++)
//        {
//            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

//            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
//            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
//            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
//            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
//            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

//            itemsDisplayed.Add(obj, inventory.Container.items[i]);
//        }
//    }

//    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
//    {
//        EventTrigger trigger = obj.GetComponent<EventTrigger>();
//        var eventTrigger = new EventTrigger.Entry();
//        eventTrigger.eventID = type;
//        eventTrigger.callback.AddListener(action);
//        trigger.triggers.Add(eventTrigger);
//    }

//    public void OnEnter(GameObject obj)
//    {
//        mouseItem.hoverObj = obj;
//        if (itemsDisplayed.ContainsKey(obj))
//            mouseItem.hoverItem = itemsDisplayed[obj];
//    }
//    public void OnExit(GameObject obj)
//    {
//        mouseItem.hoverObj = null;
//        mouseItem.hoverItem = null;

//    }
//    public void OnDragStart(GameObject obj)
//    {
//        var mouseObject = new GameObject();
//        var rt = mouseObject.AddComponent<RectTransform>();
//        rt.sizeDelta = new Vector2(170, 100);
//        mouseObject.transform.SetParent(transform.parent);
//        if(itemsDisplayed[obj].item.Id >= 0)
//        {
//            var img = mouseObject.AddComponent<Image>();
//            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].item.Id].uiDisplay;
//            img.raycastTarget = false;
//        }
//        mouseItem.obj = mouseObject;
//        mouseItem.item = itemsDisplayed[obj];

//    }
//    public void OnDragEnd(GameObject obj)
//    {
//        if (mouseItem.hoverObj)
//        {
//            inventory.SwapItems(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
//        }
//        else
//        {
//            inventory.(itemsDisplayed[obj].item);
//        }
//        Destroy(mouseItem.obj);
//        mouseItem.item = null;
//    }
//    public void OnDrag(GameObject obj)
//    {
//        if(mouseItem.obj != null)
//        {
//            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
//        }

//    }


//    public Vector3 GetPosition(int i)
//    {
//        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
//    }

//}


