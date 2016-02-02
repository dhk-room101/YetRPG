using UnityEngine;
using System.Collections;

public class xProperty
{
     public int nID { get; set; }
     //public float fValue { get; set; }
     public int nType { get; set; }//attribute, simple, depletable, derived
    
    //Value types: base, current, modifier, total
    public float fValueTypeBase { get; set; }
    public float fValueTypeCurrent { get; set; }
    public float fValueTypeModifier { get; set; }
    public float fValueTypeTotal { get; set; }

    public xProperty(int nID, float fValue, int nType, int nValueType)
     {
          this.nID = nID;
          //this.fValue = fValue;
          this.nType = nType;
        if(nValueType== EngineConstants.PROPERTY_VALUE_BASE)
        {
            fValueTypeBase = fValue;
        }
        if (nValueType == EngineConstants.PROPERTY_VALUE_CURRENT)
        {
            fValueTypeCurrent = fValue;
        }
        if (nValueType == EngineConstants.PROPERTY_VALUE_MODIFIER)
        {
            fValueTypeModifier = fValue;
        }
        if (nValueType == EngineConstants.PROPERTY_VALUE_TOTAL)
        {
            fValueTypeTotal = fValue;
        }
    }
}