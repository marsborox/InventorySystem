using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotHolder;
    [SerializeField] private ItemClass _itemToAdd;
    [SerializeField] private ItemClass _itemToRemove;

    //public List <ItemClass> items = new List<ItemClass> ();
    //public List<SlotClass> _items = new List<SlotClass>();
    public List<SlotClass> items = new List<SlotClass>();
    //public List<ItemClass> itemss = new List<ItemClass>();
    //this array keep track of slots
    private GameObject[] _slots;
    private void Start()
    {
        _slots = new GameObject[_slotHolder.transform.childCount];
        //set all the slots
        for (int i = 0; i < _slotHolder.transform.childCount; i++)
        {
            _slots[i]=_slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();
        //just for testing
        Add(_itemToAdd);
        Remove(_itemToRemove);
        Add(_itemToAdd);
        Add(_itemToAdd);
        Add(_itemToAdd);
        Add(_itemToAdd);
    }
    public void RefreshUI()
    { //will get through inventory and check if its in inventory, if yes image is put int
        for (int i=0; i< _slots.Length;i++) 
        {
            try
            {
                //everytime child is image
                //child on position 0 i guess; it will set sprite as item icon
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().isStackable)
                {
                    _slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity() + "";//***************
                }
                else { _slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""; }
                // this was above he changed it for some reason _slots[i].transform.GetChild(1).GetComponent<Text>().text = _items[i].GetQuantity().ToString();
                // this was above he changed it for some reason _slots[i].transform.GetChild(1).GetComponent<Text>().text = _items[i].GetQuantity()+"";
            }
            catch 
            {//either no item in slot or outside of index array
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;//no item
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;//get rid of white image
                _slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";//*****************
            }
        }
    }

    public bool Add(ItemClass item)
    {
        //items.Add (item);
        //check if invenotry contains item
        SlotClass slot = Contains(item);
        if (slot != null&&slot.GetItem().isStackable)
        {//if yes we add +1
            slot.AddQuantity(1);
        }
        else
        {
            if (items.Count < _slots.Length)
            {
                items.Add(new SlotClass(item, 1));
            }
            else { return false; }
        }
        RefreshUI();
        return true;//yes we succesfully added the item
    }

    public bool Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {//if yes we add +1
            if (temp.GetQuantity() > 1)
            {//if we have more of this item in inventory
                temp.SubQuantity(1);
            }
            else 
            {//we have just one item in inventory
                //items.Remove(item);
                SlotClass slotToRemove = new SlotClass();
                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }
                items.Remove(slotToRemove);
            }
        }
        else
        {//if we dont have that item in inventory
            return false;
        }
        
        RefreshUI();
        return true;
    }
    public SlotClass Contains(ItemClass item)
    {
        foreach (SlotClass slot in items)
        { 
            if (slot.GetItem() == item)
                return slot;
        }
        return null;
    }

    void TestStartMethods()
    {
        //just for testing, was in start

        Add(_itemToAdd);
        Remove(_itemToRemove);
    }
}


/* this should have sticke size
 * public SlotClass Contains(ItemClass item)
    {
        foreach(SlotClass slot in Items)
        {
            if (slot.GetItem() == item && slot.GetItem().stackSize > slot.GetQuantity())
                return slot;
        }
        return null;
    }
*/
