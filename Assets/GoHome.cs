using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : GAction
{
    public override bool PrePerform()
    {
      
        
        
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("isCured" , -1);
        Destroy(this.gameObject);
        return true;
    }
}
