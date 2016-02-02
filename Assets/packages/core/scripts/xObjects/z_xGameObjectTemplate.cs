using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//game object extension
public class z_xGameObjectTemplate : MonoBehaviour
{
     //Dictionaries and lists/arrays
     public Dictionary<string, string> sLocals { get; set; }
     public List<xThreat> oThreats { get; set; }
     public List<xProperty> oProperties { get; set; }
     public List<int> oAbilities { get; set; }
    public List<GameObject> oGear { get; set; }//This is the active gear, not the inventory
    public GameObject[] oWeaponSet = { null, null };//This is the weapon set, maximum 2

     public GameObject oCurrentArea { get; set; }

     //pseudo-booleans,0/1 values only
     public int bDestroyable { get; set; }
     public int bPlot { get; set; }
     public int bImmortal { get; set; }
     public int bConjuring { get; set; }
     public int bDead { get; set; }
     public int bDying { get; set; }

     //Integers
     public int nGender { get; set; }
     public int nRacialType { get; set; }
     public int nCoreClass { get; set; }
     public int nBackground { get; set; }
     public int nRank { get; set; }
     public int nObjectType { get; set; }
     public int nAppearanceType { get; set; }
     public int nTeamId { get; set; }

     //sometimes some values are needed before Start runs, so I made Initialize a public standalone function to be executed manually if/when needed
     void Start() { Initialize();}

     // Use this for initialization
     public void Initialize()
     {
          //create only if null
          if(sLocals==null)        sLocals = new Dictionary<string, string>();
          if(oThreats==null)       oThreats = new List<xThreat>();
          if(oProperties==null)    oProperties = new List<xProperty>();
          if(oAbilities==null)     oAbilities = new List<int>();
        if (oGear == null) oGear = new List<GameObject>();

        if (oCurrentArea==null)   oCurrentArea = GameObject.FindGameObjectWithTag("Area");
          
          //Debug.Log(gameObject.name + " | " + currentArea.name);
	}
	
	// Update is called once per frame
	void Update () {
          //TO DO threat decay overtime
     }

     /*public void AddToQueue(xCommand xCmd)
     {
          //bool condition = false;
          switch(xCmd.nType)
          {
               case EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION:
                    {
                         //commandQueue.Enqueue(Commands.MoveTo(gameObject, new Vector3(5.0f, 0.0f, 0.0f), 5, Ease.Linear()));
                         Debug.Log(gameObject.name + " is moving to location");
                         break;
                    }
               case EngineConstants.COMMAND_TYPE_WAIT:
               {
                    Debug.Log(gameObject.name + " is waiting");
                         break;
                    }
               default:
                    {
                         Debug.LogError("something went wrong with the commands switch");
                         break;
                    }
          }
     }*/
}
