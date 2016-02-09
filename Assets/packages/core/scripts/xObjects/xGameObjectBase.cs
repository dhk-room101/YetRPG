using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectBase : MonoBehaviour {

    Engine engine { get; set; }

    #region XML Resource header attributes
    public int ModuleID { get; set; }
    public int OwnerModuleID { get; set; }
    public int ResRefID { get; set; }
    public string Folder { get; set; }
    public string XResRefName { get; set; }
    public int IsCore { get; set; }
    public int OwnerIsCore { get; set; }
    public int Type { get; set; }
    public int State { get; set; }
    public string XObjectName { get; set; }//ObjectName
    public string Relationship { get; set; }//?
    public string ResType { get; set; }
    public string URI { get; set; }
    public int Requested { get; set; }
    #endregion

    #region pseudo-Boolean
    //pseudo-booleans,0/1 values only
    public int bDestroyable { get; set; }
    public int bPlot { get; set; }
    public int bImmortal { get; set; }
    public int bConjuring { get; set; }
    public int bDead { get; set; }
    public int bDying { get; set; }

    //DHK
    //Variable to be checked by core scripts if a custom script takes precedence
    //by default custom is  false, But if a custom script spawns, it turns true
    public int bCustom { get; set; }
    //If customer doesn't handle, redirects to its parent core
    public int bRedirected { get; set; }
    #endregion

    #region Integers
    //Integers
    public int nGender { get; set; }
    public int nRacialType { get; set; }
    public int nCoreClass { get; set; }
    public int nBackground { get; set; }
    public int nRank { get; set; }
    public int nObjectType { get; set; }
    public int nAppearanceType { get; set; }
    public int nTeamId { get; set; }
    public int nNumTactics { get; set; }
    #endregion

    public List<xEvent> qEvent { get; set; }

    //Commands
    public List<xCommand> qCommand { get; set; }//Command Queue
    public xCommand cCommand { get; set; }//The current command. If present, it's outside the command cue
    public xCommand pCommand { get; set; }//The previous command used for AI decisions
    public int cCommandResult { get; set; }
    public int pCommandResult { get; set; }

    // Use this for initialization
    void Awake () {
        engine = gameObject.GetComponent<Engine>();

        if (qEvent == null) qEvent = new List<xEvent>();
        if (qCommand == null) qCommand = new List<xCommand>();
        cCommand = pCommand = engine.Command(EngineConstants.COMMAND_TYPE_INVALID);
        cCommandResult = pCommandResult = EngineConstants.COMMAND_RESULT_INVALID;
    }
	
	// Update is called once per frame
	void Update () {

	
	}

    /*public void AddToQueue(xCommand xCmd)
    {
        //bool condition = false;
        switch (xCmd.nType)
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
    }    */
}
