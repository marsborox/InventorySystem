using System.Collections;

using UnityEngine;

public abstract class ItemClass : ScriptableObject
{//abstract class
    //data shared across every item
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable;//can be default =true
    //we just define method but not implement its role

    public int stackSize = 64;//set 64 to all items by default, can be changed in inspector for each item
    //public SlotType slotType;
    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract MiscClass GetMiscClass();
    public abstract ConsumableClass GetConsumableClass();
}
