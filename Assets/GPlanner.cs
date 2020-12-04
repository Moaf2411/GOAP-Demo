using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> state, GAction action)
    {
        this.parent = parent;
        this.parent = parent;
            
        this.cost = cost;
        this.state=new Dictionary<string, int>(state);
        this.action = action;
    }
    
    public Node(Node parent, float cost, Dictionary<string, int> state,Dictionary<string, int> beliefState, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state=new Dictionary<string, int>(state);
        foreach(KeyValuePair<string , int> b in beliefState)
            if(!this.state.ContainsKey(b.Key))
                this.state.Add(b.Key, b.Value); // we add the beliefs into the states
        
        this.action = action;
    }
}

public class GPlanner
{
    
    
    
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates)
    {
        List<GAction> useableActions=new List<GAction>();
        foreach (GAction a in actions)
        {
            if(a.IsAchiveable())
                useableActions.Add(a);
        }
        List<Node> leaves = new List<Node>();
        Node start = new Node(null,0,GWorld.Instance.GetWorld().GetStates(), beliefStates.GetStates(),null); // we pass the beliefs to the first node (beliefs are now added to the states as well)

        bool success = BuildGraph(start,leaves, useableActions, goal);

        if (!success)
        {
           // Debug.Log("No Plan");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else if (leaf.cost < cheapest.cost)
                cheapest = leaf; 
        }
        
        List<GAction> resault = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if(n.action != null)
                resault.Insert(0,n.action);
            n = n.parent;
        }
        
        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in resault)
        {
            queue.Enqueue(a);
        }
        
        //Debug.Log("The Plan Is: ");
        foreach (GAction a in queue)
        {
            //Debug.Log(a.actionName);
        }

        return queue;

    }




    int i = 0;
    private List<GAction> subSet;
    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> useableactions, Dictionary<string, int> goal)
    {
       
        
        bool foundPath = false;

        foreach (GAction action in useableactions)
        {
            i++;
            if (action.IsAchiveableGiven(parent.state))
            {
                Dictionary<string , int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string , int> eff in action.effects)
                {
                    if(!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key,eff.Value);
                }
                
                Node node=new Node(parent,parent.cost + action.cost , currentState , action); // we don't need to add the beliefs because there are already in the states (above we added them)

                if (GoalAcheaved(goal,currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    subSet = ActionSubset(useableactions, action); // a subset of useable actions which gets smaller and smaller(takes out actions that have been checked)
                    bool found = BuildGraph(node, leaves, subSet, goal);
                    if (found)
                       foundPath = true;

                }
                
            }
        }
        
       // Debug.Log(i);
       // i = 0;
        return foundPath;

    }



    private bool GoalAcheaved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string , int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }

        return true;
    }


    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subSet=new List<GAction>();
        foreach (GAction a in actions)
        {
            if(!a.Equals(removeMe))
                subSet.Add(a);
        }

        return subSet;
    }
    
    
    
    
    
    
    
}
