using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Item/Database")]

public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObjects;
  //  public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();


    [ContextMenu("Update ID'S")]
    public void UpdateId()
    {

        for (int i = 0; i < ItemObjects.Length; i++)
        {
            if(ItemObjects[i].data.Id != i)
            {
                ItemObjects[i].data.Id = i;
            }
            
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateId();
    }




    public void OnBeforeSerialize()
    {
       // GetItem = new Dictionary<int, ItemObject>();
    }
}
