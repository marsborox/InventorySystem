using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Misc Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    //specific data to misc
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }
    
    public override MiscClass GetMiscClass() { return this; }
    public override ConsumableClass GetConsumableClass() { return null; }
}
