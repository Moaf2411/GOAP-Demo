using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class updateWorld : MonoBehaviour
{
    public Text states;


    private void LateUpdate()
    {
        Dictionary<string, int> worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text="";
        foreach (KeyValuePair<string , int> ws in worldStates)
        {
            states.text += ws.Key + "\n";
        }
    }
}
