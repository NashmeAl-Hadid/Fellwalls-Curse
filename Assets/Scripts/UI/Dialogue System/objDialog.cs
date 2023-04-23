using UnityEngine;

[System.Serializable]
public class objDialog 
{
    //Will hold all dialogue
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
}
