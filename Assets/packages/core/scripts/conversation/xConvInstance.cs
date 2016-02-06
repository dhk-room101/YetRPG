#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class xConvInstance : MonoBehaviour
{
    public xConversation oConversation { get; set; }
    List<GameObject> pLines { get; set; }
    IEnumerator delay { get; set; }
    float timer { get; set; }
    xConvNode nextLine { get; set; }
    bool end { get; set; }
    // Use this for initialization
    void Awake()
    {
        if (oConversation == null) oConversation = new xConversation();
        if (pLines == null) pLines = new List<GameObject>();

        //Prepare an array of text lines, to be visible or not based on need
        GameObject line;
        line = gameObject.transform.Find("0").gameObject;
        pLines.Add(line);
        line = gameObject.transform.Find("1").gameObject;
        pLines.Add(line);
        line = gameObject.transform.Find("2").gameObject;
        pLines.Add(line);
        line = gameObject.transform.Find("3").gameObject;
        pLines.Add(line);
        line = gameObject.transform.Find("4").gameObject;
        pLines.Add(line);
        line = gameObject.transform.Find("5").gameObject;
        pLines.Add(line);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartConversation()
    {
        //Reset end
        end = false;

        //xConversation cnv = GetConversation();
        xConversation cnv = oConversation;

        if (cnv == null)
        {
            throw new NotImplementedException();
        }

        //1st analyze the start list conditions to see which is the current branch to initiate
        int bStart = EngineConstants.FALSE;
        int lIndex = 0;//Line index current
        xConvNode node;
        int plotID;
        xPlot plot;
        xPlotElement ePlot;

        foreach (int n in cnv.StartList)
        {
            Engine engine = xGameObjectMOD.instance.GetComponent<Engine>()
                .GetParty().GetComponent<Engine>();

            node = cnv.NPCLineList.ElementAt(n);
            //Let's analyze the current node conditions/plot, If any
            plotID = node.ConditionPlotURI;
            if (plotID != 0)//If there is an actual condition
            {
                //Check to see if plot already exists, if not create one
                xGameObjectPTY oParty = engine.GetParty().
                    GetComponent<xGameObjectPTY>();
                plot = oParty.oPlots.Find(x => x.ResRefID == plotID);
                if (plot == null) //Not found
                {
                    //let's parse and create one
                    plot = engine.ParsePlot(
                        engine.GetResource("ID", plotID.ToString(), "Name"));
                    //gameObject.AddComponent(Type.GetType(Script));
                    //oParty.gameObject.AddComponent(Type.GetType(plot.ResRefName));
                }

                //Check defined flag versus regular flag
                if (node.ConditionPlotFlag > 255) //Defined flag, That is AND/OR combination of regular flags
                {
                    if (engine.PlotEvent(EngineConstants.EVENT_TYPE_GET_PLOT,
                    plot.GUID.ToString(),
                    node.ConditionPlotFlag,
                    engine.GetParty(),
                    engine.GetLocalObject(engine.GetModule(), "CONVERSATION_SPEAKER"))
                    == EngineConstants.TRUE)
                    {
                        bStart = EngineConstants.TRUE;
                        lIndex = n;//set the found starting branch
                        break;
                    }
                }
                else //Regular flag, check directly
                {
                    ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ConditionPlotFlag);
                    if (ePlot != null && ePlot.pValue == Convert.ToInt32(node.ConditionResult))
                    {
                        bStart = EngineConstants.TRUE;
                        lIndex = n;//set the found starting branch
                        break;
                    }
                }
            }
        }

        if (bStart == EngineConstants.TRUE) //We actually found the Starting conversation node
        {
            NPCLines(lIndex);
        }
        else //nothing found?
        {
            throw new NotImplementedException();
        }
    }

    public void NPCLines(int lineIndex)
    {
        Engine engine = xGameObjectMOD.instance.GetComponent<Engine>()
    .GetParty().GetComponent<Engine>();

        xConversation cnv = oConversation;
        xConvNode node = cnv.NPCLineList.ElementAt(lineIndex);
        GameObject npcLine = gameObject.transform.Find("NPCLine").gameObject;

        Text ct = (Text)npcLine.GetComponent(typeof(Text));
        ct.text = node.text;

        if (node.Ambient)//If ambient, we skip the conversation mode
        {
            engine.DisplayFloatyMessage(
                        xGameObjectMOD.instance.CONVERSATION_SPEAKER, node.text, 0, 12345, 2);
            //Switchback to game mode explore, or whatever
            GameObject gConversation = GameObject.Find("Canvas").transform.Find("convPanel").gameObject;
            Destroy(gConversation);
            engine.EndConversation();
        }
        else
        {
            //Time to display the first branch, and if null, the player choices directly
            gameObject.SetActive(true);

            //Check for actions that needs to be set
            int plotID;
            xPlot plot;
            xPlotElement ePlot;
            plotID = node.ActionPlotURI;
            if (plotID != 0)//If there is an actual condition
            {
                //Check to see if plot already exists, if not create one
                xGameObjectPTY oParty = engine.GetParty().
                    GetComponent<xGameObjectPTY>();
                plot = oParty.oPlots.Find(x => x.ResRefID == plotID);
                if (plot == null) //Not found
                {
                    //let's parse and create one
                    plot = engine.ParsePlot(
                        engine.GetResource("ID", plotID.ToString(), "Name"));
                    //gameObject.AddComponent(Type.GetType(Script));
                    //oParty.gameObject.AddComponent(Type.GetType(plot.ResRefName));
                }

                if (node.ActionPlotFlag <= 255) //Only regular flags can be set directly
                {
                    engine.PlotEvent(EngineConstants.EVENT_TYPE_SET_PLOT,
                        plot.GUID.ToString(),
                        node.ActionPlotFlag,
                        engine.GetParty(),
                        engine.GetLocalObject(engine.GetModule(), "CONVERSATION_SPEAKER"));

                    ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ActionPlotFlag);
                    if (ePlot != null)
                    {
                        ePlot.pValue = Convert.ToInt32(node.ActionResult);
                        engine.DisplayFloatyMessage(
                            engine.GetHero(), ePlot.pNode.xname, 0, 12345, 2);
                    }
                }
                //Something tries to directly set a defined flag...
                else throw new NotImplementedException();
            }
            //Get the list of player replies
            List<xConvNode> pReplies = new List<xConvNode>();
            foreach (Transition t in cnv.NPCLineList.ElementAt(lineIndex).TransitionList)
            {
                pReplies.Add(cnv.PlayerLineList.ElementAt(t.LineIndex));
            }

            //If an actual multinode conversation
            if (pReplies.Count > 0)
            {
                //activate text lines
                foreach (xConvNode pNode in pReplies)
                {
                    if (pNode.text != null) //If null, It's either or CONTINUE or End Dialogue
                    {
                        char c = (char)124;//'|' Delimiter
                        string[] split = pNode.text.Split(c);
                        pNode.text = split[1];
                        string lineLocation = split[0][0].ToString();
                        string iconID = split[0][1].ToString();
                        GameObject lReply = pLines.ElementAt(int.Parse(lineLocation));
                        lReply.GetComponent<Text>().text = pNode.text;
                        lReply.GetComponent<xConvTouch>().lineLocation = int.Parse(lineLocation);
                        lReply.GetComponent<xConvTouch>().iconID = iconID;
                        lReply.GetComponent<xConvTouch>().lineIndex = pNode.lineIndex;
                        lReply.SetActive(true);
                    }
                    else
                    {
                        //Check to see if it's CONTINUE
                        if (cnv.PlayerLineList.ElementAt(pNode.lineIndex).TransitionList.Count != 0)
                        {
                            //It's a continue connector, Run it after a short delay
                            //This is a static hack, 
                            //correctly would be to set up a yield waiting for the sound file to stop playing
                            timer = 2.0f;
                            nextLine = pNode;
                            ResetLayout();
                            DelayLineChange();
                        }
                        else //it's end dialogue
                        {
                            end = true;
                            timer = 2.0f;
                            nextLine = pNode;
                            ResetLayout();
                            DelayLineChange();
                        }
                    }
                }
            }
            else //it's a one-liner
            {
                end = true;
                timer = 2.0f;
                ResetLayout();
                DelayLineChange();
            }
        }
    }

    public void NextLine(int lineIndex)
    {
        ResetLayout();

        PlayerLines(lineIndex);
    }

    public void PlayerLines(int lineIndex)
    {
        Engine engine = xGameObjectMOD.instance.GetComponent<Engine>()
    .GetParty().GetComponent<Engine>();

        int bStart = EngineConstants.FALSE;
        int lIndex = 0;//Line index current
        int plotID;
        xPlot plot;
        xPlotElement ePlot;

        xConversation cnv = oConversation;
        xConvNode node = cnv.PlayerLineList.ElementAt(lineIndex);

        //Check for actions that needs to be set
        plotID = node.ActionPlotURI;
        if (plotID != 0)//If there is an actual condition
        {
            //Check to see if plot already exists, if not create one
            xGameObjectPTY oParty = engine.GetParty().
                    GetComponent<xGameObjectPTY>();
            plot = oParty.oPlots.Find(x => x.ResRefID == plotID);
            if (plot == null) //Not found
            {
                //let's parse and create one
                plot = engine.ParsePlot(
                    engine.GetResource("ID", plotID.ToString(), "Name"));
                //gameObject.AddComponent(Type.GetType(Script));
                //oParty.gameObject.AddComponent(Type.GetType(plot.ResRefName));
            }

            if (node.ActionPlotFlag <= 255) //Regular flag
            {
                engine.PlotEvent(EngineConstants.EVENT_TYPE_SET_PLOT,
                        plot.GUID.ToString(),
                        node.ActionPlotFlag,
                        engine.GetParty(),
                        engine.GetLocalObject(engine.GetModule(), "CONVERSATION_SPEAKER"));

                ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ActionPlotFlag);
                if (ePlot != null)
                {
                    ePlot.pValue = Convert.ToInt32(node.ActionResult);
                    engine.DisplayFloatyMessage(
                        engine.GetHero(), ePlot.pNode.xname, 0, 12345, 2);
                }
            }
            //Something tries to directly set the defined flag...
            else throw new NotImplementedException();
        }
        //Get the list of NPC replies
        List<xConvNode> npcReplies = new List<xConvNode>();
        List<int> npcRepliesIndex = new List<int>();
        foreach (Transition t in cnv.PlayerLineList.ElementAt(lineIndex).TransitionList)
        {
            npcReplies.Add(cnv.PlayerLineList.ElementAt(t.LineIndex));
            npcRepliesIndex.Add(t.LineIndex);
        }

        //If more than one line check to see which condition matches first
        foreach (int n in npcRepliesIndex)
        {
            node = cnv.NPCLineList.ElementAt(n);
            //Let's analyze the current node conditions/plot, If any
            plotID = node.ConditionPlotURI;
            if (plotID != 0)//If there is an actual condition
            {
                //Check to see if plot already exists, if not create one
                xGameObjectPTY oParty = engine.GetParty().
                    GetComponent<xGameObjectPTY>();
                plot = oParty.oPlots.Find(x => x.ResRefID == plotID);
                if (plot == null) //Not found
                {
                    //let's parse and create one
                    plot = engine.ParsePlot(
                        engine.GetResource("ID", plotID.ToString(), "Name"));
                    //gameObject.AddComponent(Type.GetType(Script));
                    //oParty.gameObject.AddComponent(Type.GetType(plot.ResRefName));
                }

                if (node.ConditionPlotFlag > 255) //Defined flag, That is AND/OR combination of regular flags
                {
                    if (engine.PlotEvent(EngineConstants.EVENT_TYPE_GET_PLOT,
                    plot.GUID.ToString(),
                    node.ConditionPlotFlag,
                    engine.GetParty(),
                    engine.GetLocalObject(engine.GetModule(), "CONVERSATION_SPEAKER"))
                    == EngineConstants.TRUE)
                    {
                        bStart = EngineConstants.TRUE;
                        lIndex = n;//set the found starting branch
                        break;
                    }
                }
                else
                {
                    ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ConditionPlotFlag);
                    if (ePlot != null && ePlot.pValue == Convert.ToInt32(node.ConditionResult))
                    {
                        bStart = EngineConstants.TRUE;
                        lIndex = n;//set the found Next NPC branch
                        break;
                    }
                }
            }
        }

        if (bStart == EngineConstants.TRUE) //We actually found the Next NPC conversation node
        {
            NPCLines(lIndex);
            //node = cnv.NPCLineList.ElementAt(lIndex);
        }
        else //nothing found?
        {
            //If the transition list has one and only one entry, set it
            if (npcRepliesIndex.Count == 1)
            {
                NPCLines(npcRepliesIndex[0]);
            }
            else if (end)
            {

            }
            else throw new NotImplementedException();
        }

    }

    void DelayLineChange()
    {
        if (delay != null)
        {
            StopCoroutine(delay);
        }
        delay = DelayCoroutine();
        StartCoroutine(delay);
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(timer);
        PlayerLines(nextLine.lineIndex);
        if (end)
        {
            Engine engine = xGameObjectMOD.instance.GetComponent<Engine>()
    .GetParty().GetComponent<Engine>();
            engine.EndConversation();

            //Switchback to game mode explore, or whatever
            GameObject gConversation = GameObject.Find("Canvas").transform.Find("convPanel").gameObject;
            //gConversation.SetActive(false);
            Destroy(gConversation);
        }
    }

    void ResetLayout()
    {
        //Hide the mood icon
        Transform _mood = gameObject.transform.Find("moodCircle");
        if (_mood != null && _mood.gameObject != null) _mood.gameObject.SetActive(false);

        //Reset the visual lines placeholders
        foreach (var l in pLines)
        {
            l.SetActive(false);
        }
    }

}
