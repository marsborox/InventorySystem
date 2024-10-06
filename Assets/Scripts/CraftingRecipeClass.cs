using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newCraftingRecipe",menuName = "Crafting/Recipe")]
public class CraftingRecipeClass : ScriptableObject
{

    public SlotClass[] inputItems;
    public SlotClass outputItem;

    public bool CanCraft(InventoryManager inventory)
    {
        //check if we have free inventory slot
        if (inventory.IsFull())
        {
            Debug.Log("Inventory is full (crafting recipe class)");
            return false;
        }//return cant craft

        //return if inventory has input items
        for (int i = 0; i < inputItems.Length; i++)
        {//we wil lcheck every item in our inventory and its quantity
            //if it doesnt contain item of that quantity we will get false
            if (!inventory.Contains(inputItems[i].item, inputItems[i].quantity))
            {
                Debug.Log("Not enough materials");
                return false;
            }
        }
        return true;
    }
    public void Craft(InventoryManager inventory)
    {
        //remove input to inventory
        for (int i = 0; i < inputItems.Length; i++)
        {
            inventory.Remove(inputItems[i].item, inputItems[i].quantity);
        }
        //add output into inventory
        inventory.Add(outputItem.item,outputItem.quantity);
    }
}
