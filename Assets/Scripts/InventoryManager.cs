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

    //public List<SlotClass> items = new List<SlotClass>();
    //
    [SerializeField] private SlotClass[] _startingItems;//if we want to start with items instead of methods

    private SlotClass[] _items;


    //this array keep track of slots
    private GameObject[] _slots;

    private SlotClass _movingSlot;
    private SlotClass _tempSlot;
    private SlotClass _originalSlot;

    bool isMovingItem;


    private void Start()
    {
        _slots = new GameObject[_slotHolder.transform.childCount];
        _items = new SlotClass[_slots.Length];
        


        //set all the slots
        //this is if we dont want starting items explicitly

        //initialise slots
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i]=new SlotClass();
        }
        //starting items into inventory
        for (int i = 0; i < _startingItems.Length; i++)
        {
            _items[i] = _startingItems[i];
        }
        //set all slots
        for (int i = 0; i < _slotHolder.transform.childCount; i++)
        {
            _slots[i]=_slotHolder.transform.GetChild(i).gameObject;
        }

        RefreshUI();


        //just for testing
        Add(_itemToAdd);
        Remove(_itemToRemove);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//we clicked down
        { //find closest slot - could be done as Onclick event on slot
            Debug.Log(GetClosestSlot().GetItem());

            if (isMovingItem)
            {
                EndItemMove();
            //endItemMove
            }
            else
            {
                BeginItemMove();
            }
        }
    }
    #region Inventory Utilities
    public void RefreshUI()
    { //will get through inventory and check if its in inventory, if yes image is put int
        for (int i=0; i< _slots.Length;i++) 
        {
            try
            {
                //everytime child is image
                //child on position 0 i guess; it will set sprite as item icon
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].GetItem().itemIcon;
                if (_items[i].GetItem().isStackable)
                {
                    _slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _items[i].GetQuantity() + "";//***************
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
        {//check whole array
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].GetItem() == null)//this is empty slot
                {
                    _items[i].AddItem(item, 1);
                    break;
                }
            }
            
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
                //Debug.Log("item removed");
            }
            else
            {
                int slotToRemoveIndex = 0;
                for (int i=0; i<_items.Length;i++)
                {
                    if (_items[i].GetItem()==item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                _items[slotToRemoveIndex].Clear();
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
        for (int  i=0; i < _items.Length;i++) 
        {
            //Debug.Log(_items[i]);
            if (_items[i].GetItem() == item)
            { 
                return _items[i]; 
            }
        }
        return null;
    }
    #endregion Inventory Utilities 

    #region Moving Stuff
    private bool BeginItemMove()
    {
        //_originalSlot = new SlotClass(GetClosestSlot());
        _originalSlot = GetClosestSlot();
        if (_originalSlot == null || _originalSlot.GetItem() == null)
            return false;//no item to move
       
        
            _movingSlot = new SlotClass(_originalSlot);
            _originalSlot.Clear();
            isMovingItem = true;
            RefreshUI();
            return true;
        
    }
    private bool EndItemMove() 
    {
        _originalSlot=GetClosestSlot();
        if (_originalSlot.GetItem() != null)
        {//if item is already existing there
            if (_originalSlot.GetItem() == _movingSlot.GetItem()) //they are same item they should stack
            {//if they are same stack
                
            }
            else
            {//they not same swap

            }
        }
        else
        { 
            //place as usual
            _originalSlot.AddItem(_movingSlot.GetItem(),_movingSlot.GetQuantity());
            _movingSlot.Clear();
        }
        RefreshUI();
        return true;
    }
    private SlotClass GetClosestSlot()
    {//find cursor position
        Debug.Log(Input.mousePosition);
        //should be in relation in canvas
        for (int i = 0; i < _slots.Length; i++)
        {
            if (Vector2.Distance(_slots[i].transform.position, Input.mousePosition) <= 32)
                return _items[i];
        }
        return null;
    }


    #endregion
}
/*
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
*/
/*
    public SlotClass Contains(ItemClass item)
    {
        foreach (SlotClass slot in items)
        { 
            if (slot.GetItem() == item)
                return slot;
        }
        return null;
    }
*/
/*
void TestStartMethods()
{
    //just for testing, was in start

    Add(_itemToAdd);
    Remove(_itemToRemove);
}
*/


/* this should have stick size
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

/* this is method used when we used List
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
  
 */
