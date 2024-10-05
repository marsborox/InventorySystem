using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InventoryManager inventory;
    /*private void Awake()
    {//he didnt had this we do
        inventory = FindObjectOfType<InventoryManager>();
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {//if there is some item to avoid null error
            if (inventory.selectedItem != null)
            {
                //use the item
                //since it needs player controller input parameter its this controller
                inventory.selectedItem.UseItem(this);
            }
            else 
            {
                Debug.Log("Slot Epmty");
            }
        }
    }
}
