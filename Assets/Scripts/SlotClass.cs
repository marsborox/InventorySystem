using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotClass
{
    [SerializeField] private ItemClass _item;
    [SerializeField] private int _quantity;

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
    public ItemClass GetItem () { return _item; }
    public int GetQuantity () { return _quantity;}
    public void AddQuantity(int quantity) { _quantity += quantity; }
}
