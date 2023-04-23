using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    public Text itemname;
    public Text itemDescription;
    public Text itemValue;


    public void Setup(string name , string description, string value)
    {
        itemname.text = name;
        itemDescription.text = description;
        itemValue.text = value;
    }
}
