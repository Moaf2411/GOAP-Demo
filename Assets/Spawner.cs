using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class SpawnEvent : UnityEvent
{
   
   
} 

public class Spawner : MonoBehaviour
{
    public event EventHandler<PatientPrefabEventArgs> SpawnPatient; 
    public GameObject prefab;
    private PatientPrefabEventArgs ppe;
    public SpawnEvent se;
    public int num;
    
    void Awake()
    {
        ppe=new PatientPrefabEventArgs();
        ppe.prefab = prefab;
        ppe.pos = this.transform.position;

        SpawnPatient += Instantiator.Patient;
       
    }
    

    public void Spawn()
    {
     SpawnPatient(this, ppe);
     se.Invoke();
    }

   
}
