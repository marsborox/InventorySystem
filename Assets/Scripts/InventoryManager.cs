using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemCursor;
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

    //SF only for torubleshooting
    [SerializeField] private SlotClass _movingSlot;
    [SerializeField] private SlotClass _tempSlot;
    [SerializeField] private SlotClass _originalSlot;

    [SerializeField] bool isMovingItem;


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
        Add(_itemToAdd,1);
        Remove(_itemToRemove);
    }

    private void Update()
    {
#region have item visible when moving

        _itemCursor.SetActive(isMovingItem);
        _itemCursor.transform.position=Input.mousePosition;
        if (isMovingItem)
        {
            _itemCursor.GetComponent<Image>().sprite = _movingSlot.item.itemIcon;
        }
#endregion
        if (Input.GetMouseButtonDown(0))//we left clicked down
        { //find closest slot - could be done as Onclick event on slot
            //cant use this debug will cause null reference and wont work
            //when clicked not on slot while moving
            //Debug.Log(GetClosestSlot().GetItem());

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
        else if (Input.GetMouseButton(1))//rightclick
        {
            if (isMovingItem)
            {   //when rightclick while holding item we put down 5 of stack
                EndItemMove_Single();
                //endItemMove
            }
            else
            {  // when righclick and not holding item pick half of stack

                BeginItemMove_Half();
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
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].item.itemIcon;
                if (_items[i].item.isStackable)
                {
                    _slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _items[i].quantity + "";//***************
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
    #endregion Inventory Utilities 
    public bool Add(ItemClass item, int quantity)
    {
        //items.Add (item);
        //check if invenotry contains item
        SlotClass slot = Contains(item);
        if (slot != null&&slot.item.isStackable)
        {//if yes we add +1
            slot.AddQuantity(quantity);
        }
        else
        {//check whole array
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].item == null)//this is empty slot
                {
                    _items[i].AddItem(item, quantity);
                    break;
                }
            }
            
        }
        RefreshUI();
        return true;//yes we succesfully added the item
    }
    public bool Remove(ItemClass item, int quantity =1)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {//if yes we add +1
            if (temp.quantity > 1)
            {//if we have more of this item in inventory
                temp.SubQuantity(1);
                //Debug.Log("item removed");
            }
            else
            {
                int slotToRemoveIndex = 0;
                for (int i=0; i<_items.Length;i++)
                {
                    if (_items[i].item==item)
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
    public void UseSelected()
    {
        
    }
    public bool IsFull()
    {

        return false;
    }
    public SlotClass Contains(ItemClass item)
    {
        for (int  i=0; i < _items.Length;i++) 
        {
            //Debug.Log(_items[i]);
            if (_items[i].item == item)
            { 
                return _items[i]; 
            }
        }
        return null;
    }
    

    #region Moving Stuff
    private bool BeginItemMove()
    {
        //_originalSlot = new SlotClass(GetClosestSlot());
        _originalSlot = GetClosestSlot();
        if (_originalSlot == null || _originalSlot.item == null)
            return false;//no item to move
       
        
            _movingSlot = new SlotClass(_originalSlot);
            _originalSlot.Clear();
            isMovingItem = true;
            RefreshUI();
            return true;
    }
    private bool BeginItemMove_Half()
    {//move half of stack roundUp
        //_originalSlot = new SlotClass(GetClosestSlot());
        _originalSlot = GetClosestSlot();
        if (_originalSlot == null || _originalSlot.item == null)
            return false;//no item to move
        //we will use constructor with defyning quantity
        _movingSlot = new SlotClass(_originalSlot.item,Mathf.CeilToInt(_originalSlot.quantity/2f));//will roundup
        _originalSlot.SubQuantity(Mathf.CeilToInt(_originalSlot.quantity / 2f));
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool EndItemMove() 
    {
        _originalSlot=GetClosestSlot();

        if (_originalSlot == null || _originalSlot.item == null)
            return false;//if we click offi nventory to drop we wont drop item
        //also when we click on slot with other item we wont drop it there


        if (_originalSlot == null)
        {//if we are moving item but click not on slot return item to original slot
            Add(_movingSlot.item, _movingSlot.quantity);
            _movingSlot.Clear();
        }
        else
        {
            if (_originalSlot.item != null)
            {//if item is already existing there
                if (_originalSlot.item == _movingSlot.item) //they are same item they should stack
                {//if they are same stack combine them
                    if (_originalSlot.item.isStackable)
                    {
                        _originalSlot.AddQuantity(_movingSlot.quantity);
                        _movingSlot.Clear();
                    }
                    else { return false; }
                }
                else
                {//they not same item - we will swap
                    _tempSlot = new SlotClass(_originalSlot);
                    _originalSlot.AddItem(_movingSlot.item, _movingSlot.quantity);
                    _movingSlot.AddItem(_tempSlot.item, _tempSlot.quantity);
                    RefreshUI();
                    return true;
                }
            }
            else
            {
                //place as usual
                _originalSlot.AddItem(_movingSlot.item, _movingSlot.quantity);
                _movingSlot.Clear();
            }
        }
        isMovingItem=false;
        RefreshUI();
        return true;
    }
    private bool EndItemMove_Single()
    {
        _originalSlot = GetClosestSlot();

        if (_originalSlot == null)
        {//if we are moving item but click not on slot return item to original slot
            Add(_movingSlot.item, _movingSlot.quantity);
            _movingSlot.Clear();
        }
        else
        {
            if (_originalSlot.item != null)
            {//if item is already existing there
                if (_originalSlot.item == _movingSlot.item) //they are same item they should stack
                {//if they are same stack combine them
                    if (_originalSlot.item.isStackable)
                    {
                        _originalSlot.AddQuantity(_movingSlot.quantity);
                        _movingSlot.Clear();
                    }
                    else { return false; }
                }
                else
                {//they not same item - we will swap
                    _tempSlot = new SlotClass(_originalSlot);
                    _originalSlot.AddItem(_movingSlot.item, _movingSlot.quantity);
                    _movingSlot.AddItem(_tempSlot.item, _tempSlot.quantity);
                    RefreshUI();
                    return true;
                }
            }
            else
            {
                //place as usual
                _originalSlot.AddItem(_movingSlot.item, _movingSlot.quantity);
                _movingSlot.Clear();
            }
        }
        isMovingItem = false;
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

