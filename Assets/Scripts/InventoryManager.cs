using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotHolder;
    [SerializeField] private ItemClass _itemToAdd;
    [SerializeField] private ItemClass _itemToRemove;

    public List <ItemClass> items = new List<ItemClass> ();

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
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemIcon;
            }
            catch 
            {//either no item in slot or outside of index array
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;//no item
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;//get rid of white image
            }
        }
    }

    public void Add(ItemClass item)
    { 
        items.Add (item);
        RefreshUI();
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item);
        RefreshUI();
    }

    void TestStartMethods()
    {
        //just for testing, was in start
        Add(_itemToAdd);
        Remove(_itemToRemove);
    }
}
