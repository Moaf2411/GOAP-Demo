using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld 
{
   private static readonly GWorld instance = new GWorld();
   private static WorldStates world;
   private static Queue<GameObject> patientsQueue;
   private static Queue<GameObject> cubiclesQueue;

   static GWorld()
   {
      world=new WorldStates();
      patientsQueue=new Queue<GameObject>();
      cubiclesQueue=new Queue<GameObject>();

      GameObject[] cubicles = GameObject.FindGameObjectsWithTag("Cubicle");
      foreach (GameObject c in cubicles)
      {
         cubiclesQueue.Enqueue(c.gameObject);
      }
      
      if(cubicles.Length > 0)
         world.ModifyState("FreeCubicle", cubicles.Length);

      Time.timeScale = 5;
   }

   private GWorld()
   {
   }

   public static GWorld Instance
   {
      get { return instance; }
   }

   public WorldStates GetWorld()
   {
      return world;
   }

   public void AddPatient(GameObject p)
   {
      patientsQueue.Enqueue(p);
   }

   public GameObject RemovePatient()
   {
      if (patientsQueue.Count == 0)
         return null;

      return patientsQueue.Dequeue();
   }
   
   
   
   
   
   
   public void AddCubicle(GameObject c)
   {
      cubiclesQueue.Enqueue(c);
   }

   public GameObject RemoveCubicle()
   {
      if (cubiclesQueue.Count == 0)
         return null;

      return cubiclesQueue.Dequeue();
   }
   
   
   
   
}
