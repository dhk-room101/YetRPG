#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectBase : MonoBehaviour
{

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
    public int nNumTactics { get; set; }
    #endregion

    public List<xEvent> qEvent { get; set; }

    //Commands
    public List<xCommand> qCommand { get; set; }//Command Queue
    public xCommand currentCommand { get; set; }//The current command. If present, it's outside the command cue
    public xCommand lastCommand { get; set; }//The last command used for AI decisions

    int counter { get; set; }

    // Use this for initialization
    void Awake()
    {
        engine = gameObject.GetComponent<Engine>();

        if (qEvent == null) qEvent = new List<xEvent>();
        if (qCommand == null) qCommand = new List<xCommand>();
        currentCommand = lastCommand = engine.Command(EngineConstants.COMMAND_TYPE_INVALID);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (10 - counter < 0)
        {
            //engine.Warning(" increment update " + counter);
            counter = 0;
            //Let's take a look at commands, specifically to the current one
            //If current command is invalid, it means that there is no command to process
            if (currentCommand.nType == EngineConstants.COMMAND_TYPE_INVALID)
            {
                //Let's take a look at the command queue, maybe there's something there
                if (qCommand.Count > 0)
                {
                    //There is something, so snatch the first element, make It current, And removed from list
                    currentCommand = qCommand[0];
                    qCommand.RemoveAt(0);
                    HandleCommand(currentCommand);
                }
                else
                {
                    //Nothing in queue, so nothing to do :-)
                }
            }
        }
    }

    void HandleCommand(xCommand cCommand)
    {
        switch (cCommand.nType)
        {
            case EngineConstants.COMMAND_TYPE_WAIT:
                {
                    StartCoroutine(
                        HandleCommandWait(cCommand, () =>
                        {
                            UpdateCommandRef(ref cCommand);
                        })
                        );

                    break;
                }
            case EngineConstants.COMMAND_TYPE_MOVE_TO_OBJECT:
                {
                    StartCoroutine(
                        HandleCommandMoveToObject(cCommand, () =>
                        {
                            UpdateCommandRef(ref cCommand);
                        })
                        );

                    break;
                }
            case EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION:
                {
                    StartCoroutine(
                        HandleCommandMoveToLocation(cCommand, () =>
                        {
                            UpdateCommandRef(ref cCommand);
                        })
                        );

                    break;
                }
            case EngineConstants.COMMAND_TYPE_USE_OBJECT:
                {
                    StartCoroutine(
                        HandleCommandUseObject(cCommand, () =>
                        {
                            UpdateCommandRef(ref cCommand);
                        })
                        );

                    break;
                }
            case EngineConstants.COMMAND_TYPE_START_CONVERSATION:
                {
                    StartCoroutine(
                        HandleCommandStartConversation(cCommand, () =>
                        {
                            UpdateCommandRef(ref cCommand);
                        })
                        );

                    break;
                }
            default: throw new NotImplementedException();
        }
    }

    void UpdateCommandRef(ref xCommand cCommand)
    {
        lastCommand = cCommand;
        currentCommand = engine.Command(EngineConstants.COMMAND_TYPE_INVALID);

        xEvent ev = new xEvent(EngineConstants.EVENT_TYPE_COMMAND_COMPLETE);
        engine.SetEventIntegerRef(ref ev, 0, cCommand.nType);
        engine.SetEventIntegerRef(ref ev, 1, EngineConstants.COMMAND_RESULT_SUCCESS);
        engine.SetEventObjectRef(ref ev, 0, gameObject);
        engine.SignalEvent(gameObject, ev);
    }

    IEnumerator HandleCommandWait(xCommand cCommand, Action onComplete)
    {
        yield return new WaitForSeconds(engine.GetCommandFloatRef(ref cCommand));

        onComplete();
    }

    IEnumerator HandleCommandMoveToObject(xCommand cCommand, Action onComplete)
    {
        GameObject oTarget = engine.GetCommandObjectRef(ref cCommand);
        if (oTarget != null)
        {
            Vector3 vObject = gameObject.transform.position;
            Vector3 vTarget = oTarget.transform.position;
            //Check to make sure that the object target referenced in command still exists in the scene
            GameObject ot = GameObject.Find(oTarget.name);

            float distance = Mathf.Abs(vObject.sqrMagnitude - vTarget.sqrMagnitude);
            while (distance > 0)
            //while (bTargetReached == EngineConstants.FALSE)
            {
                vObject = Vector3.MoveTowards(vObject, vTarget, Time.deltaTime * 3);
                gameObject.transform.position = vObject;
                Camera.main.transform.position = new Vector3(vObject.x, Camera.main.transform.position.y, vObject.z);
                distance = Mathf.Abs(vObject.sqrMagnitude - vTarget.sqrMagnitude);

                yield return null;
            }
        }
        onComplete();

    }

    IEnumerator HandleCommandMoveToLocation(xCommand cCommand, Action onComplete)
    {
        Vector3 vTarget = engine.GetCommandLocationRef(ref cCommand);
        Vector3 vObject = gameObject.transform.position;
        
        float distance = Mathf.Abs(vObject.sqrMagnitude - vTarget.sqrMagnitude);
        while (distance > 0)
        {
            vObject = Vector3.MoveTowards(vObject, vTarget, Time.deltaTime * 3);
            gameObject.transform.position = vObject;
            Camera.main.transform.position = new Vector3(vObject.x, Camera.main.transform.position.y, vObject.z);
            distance = Mathf.Abs(vObject.sqrMagnitude - vTarget.sqrMagnitude);

            yield return null;
        }

        onComplete();
    }

    IEnumerator HandleCommandUseObject(xCommand cCommand, Action onComplete)
    {
        //Let's check what type of Use can we get out of it, starting with area transition
        //TO DO WorldMap
        GameObject oTarget = engine.GetCommandObjectRef(ref cCommand);

        #region Area Transition
        if (oTarget != null && oTarget.tag == "AreaTransition")
        {
            string sArea = oTarget.GetComponent<xGameObjectUTP>().PLC_AT_DEST_AREA_TAG;
            string sWP = oTarget.GetComponent<xGameObjectUTP>().PLC_AT_DEST_TAG;
            if (sArea != "" && sWP != "")
            {
                xGameObjectMOD.instance.bTransitioning = EngineConstants.TRUE;

                //engine.Warning(w.name + " was clicked");
                GameObject _player = engine.GetHero();

                //Set placeable action as area transition
                engine.UpdateGameObjectProperty(oTarget, "PLC_ACTION", EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION.ToString());

                xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_USE);
                engine.SetEventCreatorRef(ref ev, _player);
                engine.SignalEvent(oTarget, ev);
            }
        }
        #endregion

        yield return null;
        onComplete();
    }

    IEnumerator HandleCommandStartConversation(xCommand cCommand, Action onComplete)
    {
        GameObject oTarget = engine.GetCommandObjectRef(ref cCommand);

        xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_CONVERSATION);
        engine.SetEventCreatorRef(ref ev, engine.GetHero());
        engine.SetEventResourceRef(ref ev, 0, engine.GetCommandStringRef(ref cCommand));
        engine.SignalEvent(oTarget, ev);

        yield return null;
        onComplete();
    }
}
