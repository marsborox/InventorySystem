using System.Collections;

using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    //data shared across every item
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;

    //we just define method but not implement its role
    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract MiscClass GetMiscClass();
    public abstract ConsumableClass GetConsumableClass();
}
