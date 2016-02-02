using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xEvent
{
    public GameObject oCreator { get; set; }
     public List<int> nList { get; set; }
     public List<float> fList { get; set; }
     public List<GameObject> oList { get; set; }
     public List<string> sList { get; set; }
     public List<string> rList { get; set; }
     public List<Vector3> lList { get; set; }
     public int nType { get; set; }
     public float delay { get; set; }
     public string scriptname { get; set; }

     public xEvent(int nType)
     {
          this.nType = nType;
          //initialize lists
          this.nList = new List<int>();
          this.fList = new List<float>();
          this.oList = new List<GameObject>();
          this.sList = new List<string>();
          this.rList = new List<string>();
          this.lList = new List<Vector3>();
     }
}