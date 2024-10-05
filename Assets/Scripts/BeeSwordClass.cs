using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/BeeSword")]
public class BeeSwordClass : ToolClass
{
    public GameObject beeObject;
    public override void UseItem(PlayerController caller)
    {
        base.UseItem(caller);//it inherits from tool class 
        //but since it has base.UseItem as well it will trigger ItemClass method as well

        Debug.Log("BeeSword Attacks");
        Instantiate(beeObject,caller.transform.position,Quaternion.identity);
    }
}
