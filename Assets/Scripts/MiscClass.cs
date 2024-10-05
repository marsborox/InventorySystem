using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Misc Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    //specific data to misc
  
    public override MiscClass GetMiscClass() { return this; }

    public override void UseItem(PlayerController caller)
    {
        //base.UseItem(caller);
        Debug.Log("Misc item does not do anything");
    }
}
