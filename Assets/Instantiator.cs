using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientPrefabEventArgs : EventArgs
{
    public GameObject prefab;
    public Vector3 pos;
}
public class Instantiator
{


    public static void Patient(object s, PatientPrefabEventArgs e)
    {
        UnityEngine.MonoBehaviour.Instantiate(e.prefab, e.pos, Quaternion.identity);
    }
    



}
