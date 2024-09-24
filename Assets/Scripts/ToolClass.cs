using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool")]
public class ToolClass : ItemClass
{
    
    //specific data to tool
    [Header("Tool")]
    public ToolType toolType;
    public enum ToolType
    { 
        weapon,
        pickaxe,
        hammer,
        axe
    }
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return this; }
    //cant return this
    public override MiscClass GetMiscClass() { return null; }
    public override ConsumableClass GetConsumableClass() { return null; }
}
