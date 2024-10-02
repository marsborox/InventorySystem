using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotClass
{
    [field: SerializeField] public ItemClass item { get; private set; }
    [field: SerializeField] public int quantity { get; private set; }

    //public SlotType slotType { get; private set; } = SlotType.def; its some enum apparently not used 
    private SlotClass slotClass;

    public SlotClass()
    {
        item = null;
        quantity = 0;
    }
    /*
    public SlotClass(SlotType slotType)
    {
        item = null;
        quantity=0;
        slotType = slotType;
    }*/
    public SlotClass (ItemClass item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public SlotClass(SlotClass slotClass)
    {//he didnt added it but to fix bug should work
        this.item = slotClass.item;
        this.quantity= slotClass.quantity;
    }

    public void Clear()
    {
        this.item = null;
        this.quantity = 0;
    
    }
    //public ItemClass GetItem () { return item; } //in other script its replaced by "item" and "quantity"
    //public int GetQuantity () { return quantity;} //not needed anymore he changed it by setting it to public
    public void AddQuantity(int quantity) { this.quantity += quantity; }
    public void SubQuantity(int quantity) { this.quantity -= quantity; }
    public void AddItem(ItemClass item, int quantity) 
    {
        this.item= item;
        this.quantity= quantity;
    }
}
