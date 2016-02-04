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
            node = cnv.NPCLineList.ElementAt(n);
            //Let's analyze the current node conditions/plot, If any
            plotID = node.ConditionPlotURI;
            if (plotID != 0)//If there is an actual condition
            {
                //Check to see if plot already exists, if not create one
                plot = xGameObjectMOD.instance.oPlots.Find(x => x.ResRefID == plotID);
                if (plot == null) //Not found
                {
                    //let's parse and create one
                    plot = xGameObjectMOD.instance.GetComponent<Engine>().ParsePlot(
                        xGameObjectMOD.instance.GetComponent<Engine>().GetResource("ID", plotID.ToString(), "Name"));
                }

                ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ConditionPlotFlag);
                if (ePlot != null && ePlot.pValue == Convert.ToInt32(node.ConditionResult))
                {
                    bStart = EngineConstants.TRUE;
                    lIndex = n;//set the found starting branch
                    break;
                }
            }
        }

        if (bStart == EngineConstants.TRUE) //We actually found the Starting conversation node
        {
            NPCLines(lIndex);
            //node = cnv.NPCLineList.ElementAt(lIndex);
        }
        else //nothing found?
        {
            throw new NotImplementedException();
        }



        Console.WriteLine();
    }

    public void NPCLines(int lineIndex)
    {
        xConversation cnv = oConversation;
        xConvNode node = cnv.NPCLineList.ElementAt(lineIndex);
        //Time to display the first branch, and if null, the player choices directly
        gameObject.SetActive(true);
        GameObject npcLine = gameObject.transform.Find("NPCLine").gameObject;

        Text ct = (Text)npcLine.GetComponent(typeof(Text));
        ct.text = node.text;

        //Get the list of player replies
        List<xConvNode> pReplies = new List<xConvNode>();
        foreach (Transition t in cnv.NPCLineList.ElementAt(lineIndex).TransitionList)
        {
            pReplies.Add(cnv.PlayerLineList.ElementAt(t.LineIndex));
        }

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
                    timer = 2.0f;
                    nextLine = pNode;
                    ResetLayout();
                    DelayLineChange();
                }
                else //it's end dialogue
                {
                    throw new NotImplementedException();
                }
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
        int bStart = EngineConstants.FALSE;
        int lIndex = 0;//Line index current
        int plotID;
        xPlot plot;
        xPlotElement ePlot;

        xConversation cnv = oConversation;
        xConvNode node = cnv.PlayerLineList.ElementAt(lineIndex);

        //Time to display the first branch, and if null, the player choices directly
        //gameObject.SetActive(true);
        //GameObject npcLine = gameObject.transform.Find("NPCLine").gameObject;

        //Text ct = (Text)npcLine.GetComponent(typeof(Text));
        //ct.text = node.text;

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
                plot = xGameObjectMOD.instance.oPlots.Find(x => x.ResRefID == plotID);
                if (plot == null) //Not found
                {
                    throw new NotImplementedException();//The plot should have been ready at this point
                }

                ePlot = plot.StatusList.Find(x => x.pNode.Flag == node.ConditionPlotFlag);
                if (ePlot != null && ePlot.pValue == Convert.ToInt32(node.ConditionResult))
                {
                    bStart = EngineConstants.TRUE;
                    lIndex = n;//set the found starting branch
                    break;
                }
            }
        }

        if (bStart == EngineConstants.TRUE) //We actually found the Starting conversation node
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
    }

    void ResetLayout()
    {
        //Reset the visual lines placeholders
        foreach (var l in pLines)
        {

            l.SetActive(false);
        }
    }

}
