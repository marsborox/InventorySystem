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
                _slots[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = items[i].GetQuantity().ToString();//***************
                // this was above he changed it for some reason _slots[i].transform.GetChild(1).GetComponent<Text>().text = _items[i].GetQuantity().ToString();
                // this was above he changed it for some reason _slots[i].transform.GetChild(1).GetComponent<Text>().text = _items[i].GetQuantity()+"";
            }
            catch 
            {//either no item in slot or outside of index array
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;//no item
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;//get rid of white image
                //_slots[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = " ";//*****************
            }
        }
    }

    public void Add(ItemClass item)
    {
        //items.Add (item);
        //check if invenotry contains item
        SlotClass slot = Contains(item);
        if (slot != null)
        {//if yes we add +1
            slot.AddQuantity(1);
        }
        else
        {
            items.Add(new SlotClass(item, 1));
        }
        RefreshUI();
    }

    public void Remove(ItemClass item)
    {
        //items.Remove(item);
        SlotClass slotToRemove=new SlotClass();
        foreach (SlotClass slot in items)
        {
            if (slot.GetItem() == item)
            {
                slotToRemove = slot;
                break;
            }
        }
        items.Remove(slotToRemove);
        RefreshUI();
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
