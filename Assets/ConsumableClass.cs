using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    //specific data to consumable
    [Header("Consumable")]
    public float healthAdded;
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }

    public override MiscClass GetMiscClass() { return null; }
    public override ConsumableClass GetConsumableClass() { return this; }
}
