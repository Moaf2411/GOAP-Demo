using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    public void Start()
    {
        base.Awake();
        SubGoal s1= new SubGoal("treatPatient",1,false);
        goals.Add(s1,3);
        
        SubGoal s2= new SubGoal("rested",1,false); // if the third value is false the goal is not going to be removed from the goals list when it gets satisfied
        goals.Add(s2,1);
        
        Invoke("GetTired" , UnityEngine.Random.Range(10,15));

    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted" , 1);
        Invoke("GetTired" , UnityEngine.Random.Range(5,10));
    }

    
}
