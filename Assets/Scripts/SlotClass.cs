using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotClass
{
    [SerializeField] private ItemClass _item;
    [SerializeField] private int _quantity;
    private SlotClass slotClass;

    public SlotClass()
    {
        _item = null;
        _quantity = 0;
    }

    public SlotClass (ItemClass item, int quantity)
    {
        this._item = item;
        this._quantity = quantity;
    }

    public SlotClass(SlotClass slotClass)
    {//he didnt added it but to fix bug should work
        this._item = slotClass._item;
        this._quantity= slotClass._quantity;
    }

    public void Clear()
    {
        this._item = null;
        this._quantity = 0;
    
    }
    public ItemClass GetItem () { return _item; }
    public int GetQuantity () { return _quantity;}
    public void AddQuantity(int quantity) { _quantity += quantity; }
    public void SubQuantity(int quantity) { _quantity -= quantity; }
    public void AddItem(ItemClass item, int quantity) 
    {
        this._item= item;
        this._quantity= quantity;
    }
}
