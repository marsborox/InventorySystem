using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/Tool")]
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
    
    public override ToolClass GetTool() { return this; }
    //cant return this
    public override void UseItem(PlayerController caller)
    {//this will call original method and do this stuff
        base.UseItem(caller);
        Debug.Log("Swing TOOL");
    }
}
