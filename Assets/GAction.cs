using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1;// the plan with the lowest cost will be executed
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;
    public GInventory inventory;
    public WorldStates beliefs;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldState agentBeliefs;

    public bool runnig = false;


    public GAction()
    {
        preconditions=new Dictionary<string, int>();
        effects=new Dictionary<string, int>();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key , w.value);
            }
        }

        if (afterEffects!=null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key,w.value);
            }
        }
        
        if (target == null && targetTag != "")
             target = GameObject.FindWithTag(targetTag);

        inventory = GetComponent<GAgent>().inventory;
        beliefs = GetComponent<GAgent>().beliefs;
    }

    public bool IsAchiveable()
    {
        return true;
    }

    public bool IsAchiveableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string , int> k in preconditions)
        {
            if (!conditions.ContainsKey(k.Key))
                return false;
        }

        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();




















}
