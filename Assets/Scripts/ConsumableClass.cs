using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    //specific data to consumable
    [Header("Consumable")]
    public float healthAdded;

    public override ConsumableClass GetConsumableClass() { return this; }

    public override void UseItem(PlayerController caller)
    {
        base.UseItem(caller);
        Debug.Log("Eaten Consumable");
        //substract from inventory but from selected slot
        caller.inventory.UseSelected();
        
    }
}
