using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

using UnityEngine;

public class ItemClass : ScriptableObject
{//abstract class
    //data shared across every item
    [Header("Item")]
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable;//can be default =true
    //we just define method but not implement its role

    public int stackSize = 64;//set 64 to all items by default, can be changed in inspector for each item
    //public SlotType slotType;
    public virtual ItemClass GetItem() { return this; }
    public virtual ToolClass GetTool() { return null; }
    public virtual MiscClass GetMiscClass() { return null; }
    public virtual ConsumableClass GetConsumableClass() { return null; }
    public virtual void UseItem(PlayerController caller)
    {
        Debug.Log("Used Item");
    }
}
