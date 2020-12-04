using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SubGoal
{
    public Dictionary<string, int> subGoals;
    public bool remove;

    public SubGoal(string s, int v, bool r)
    {
        subGoals=new Dictionary<string, int>();
        subGoals.Add(s,v);
        remove = r;
    }
}
public class GAgent : MonoBehaviour
{
    
    public List<GAction> actions =new List<GAction>();
    public Dictionary<SubGoal , int> goals = new Dictionary<SubGoal , int>();
    private GPlanner planner;
    
    Queue<GAction> actionQueue;
    public GAction currentAction;
    private SubGoal currentGoal;
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    private bool invoked = false;
    
    
    public void Awake()
    {
        GAction[] acts = GetComponents<GAction>();

        foreach (GAction a in acts)
        {
            actions.Add(a);
        }
    }


    IEnumerator CompleteAction(float z)
    {
        yield return new WaitForSeconds(z);
        currentAction.runnig = false;
        currentAction.PostPerform();
        invoked = false;
    }
    
    
    

    private void LateUpdate()
    {
        try
        {
            if (currentAction.runnig)
            {
                if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < 2f) 
                {
                    if (!invoked)
                    {
                        StartCoroutine(CompleteAction(currentAction.duration));
                        invoked = true;
                    }
                }

                return; 
            }
        }
        catch (Exception e)
        {
            
            
        }
        
       






        if (planner == null || actionQueue == null)
        {
            planner=new GPlanner();

            var sortedGoal = from entry in goals orderby entry.Value descending select entry; // sorts it by the Value (importance of satisfying the goal )
            foreach (KeyValuePair<SubGoal ,int> sg in sortedGoal)
            {
                actionQueue = planner.Plan(actions, sg.Key.subGoals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                //if (currentAction.target == null && currentAction.targetTag != "")
                   // currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                    currentAction.runnig = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                
            }
            else
            {
                actionQueue = null;
            }
        }
            
    }
}
