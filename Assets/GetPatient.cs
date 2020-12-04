using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject resource;
    public override bool PrePerform()
    {
        
        
        target = GWorld.Instance.RemovePatient();
        if (target == null) 
            return false;

        resource = GWorld.Instance.RemoveCubicle();
        if(resource!=null)
            inventory.AddItem(resource); // add this cubicle into nurse's inventory
        if (resource == null)
        {
            GWorld.Instance.AddPatient(target); // release the patient (there is no free cubicles)
            target = null;
            return false; // plan fails , recalculate the plan
        }
        
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle" , -1);

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting" , -1);
        
        if(target != null)
            target.GetComponent<GAgent>().inventory.AddItem(resource); // we are adding the same cubicle as the nurse's to the inventory of the patient
            
        return true;
    }
}
