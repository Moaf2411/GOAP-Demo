using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patient : GAgent
{
    
    public void Start()
    {
        base.Awake();
        SubGoal s1= new SubGoal("isWaiting",1,true);
        goals.Add(s1,3);
        
        SubGoal s2= new SubGoal("isTreated",1,true);
        goals.Add(s2,5);
        
        SubGoal s3= new SubGoal("goHome",1,true);
        goals.Add(s3,3);

        

        
            
        
    }


}
