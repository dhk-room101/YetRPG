//Warning("debug mode: 3-D coordinates in complete!");

#region Design Choices
/*
- created xTarget script with oTargets List of Game Object public property to be added by default on all game objects
this way we can check which object has which target/s, such as enemies, triggers, placeable etc.
- The LDF directives are mainly Dragon Age 2, using 'ref' format for example, which was not used in DA:O
- Also, the idea is to follow DA2 design merging dialogue and cut scenes into one conversation class
*/
#endregion

//TO DO: string and location
//event ref: update functions from DA:O format to DA2

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

#region DEFINES
#define ENGINE_NUM_STRUCTURES   //6
#define ENGINE_STRUCTURE_0  //event
#define ENGINE_STRUCTURE_1  //location
#define ENGINE_STRUCTURE_2  //command
#define ENGINE_STRUCTURE_3  //effect
#define ENGINE_STRUCTURE_4  //itemproperty
#define ENGINE_STRUCTURE_5  //player

#define SOURCE_FILE_EXTENSION   //nss
#define COMPILED_FILE_EXTENSION //ncs
#define DEBUGGER_FILE_EXTENSION //ndb
#define DEBUG
#endregion

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEditor;
using Random = UnityEngine.Random;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
using System.Reflection;
using UnityEngine.UI;

public partial class Engine
{
    int increment;
    //#region Engine Functions
    /***************************************************************/
    // Prints / Logs
    /***************************************************************/
    /* @addtogroup printlog Print & Log Functions
    *
    * Functions to print to the log and to print specific value types.
    */
    /* @{*/

    /* @brief Prints an integer to the log file.
    *
    * Prints the integer nInteger to the log file. If bPrepend is TRUE, then 'PRINTINTEGER'
    * will be prepended onto the text outputted to the log file.
    *
    * @param nInteger - The integer value to print out to the log.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintFloat(), PrintString(), PrintObject(), PrintVector(), PrintToLog()
    * @author Brenon
    */
    public void PrintInteger(int nInteger, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print integer: " + nInteger);
    }

    /* @brief Prints a float to the log file.
    *
    * Prints the float fFloat to the log file based on the nWidth and nDecimal specifications.
    * If bPrepend is TRUE, then 'PRINTFLOAT' will be prepended onto the text outputted to the
    * log file.
    *
    * @param fFloat - The float value to print out to the log.
    * @param nWidth - The size of the value before the decimal. Must be a value between 0 and 18 inclusive.
    * @param nDecimals - The number of decimal places. Must be a value between 0 and 9 inclusive.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintInteger(), PrintString(), PrintObject(), PrintVector(), PrintToLog()
    * @author Brenon
    */
    public void PrintFloat(float fFloat, int nWidth = 18, int nDecimals = 9, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print float: " + fFloat + " TO DO: width, decimals");
    }

    /* @brief Prints a string to the log file.
    *
    * Prints the string sString to the log file. If bPrepend is TRUE, then 'PRINTSTRING' will
    * be prepended onto the text outputted to the log file.
    *
    * @param sString - The string value to print out to the log.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintInteger(), PrintFloat(), PrintObject(), PrintVector(), PrintToLog()
    * @author Brenon
    */
    public void PrintString(string sString, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print string: " + sString);
    }

    /* @brief Prints an GameObject to the log file.
    *
    * Prints the GameObject oObject's unique ID to the log file. If bPrepend is TRUE, then
    * 'PRINTOBJECT' will be prepended onto the text outputted to the log file.
    *
    * @param oObject - The GameObject to be printed to the log.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintInteger(), PrintFloat(), PrintString(), PrintVector(), PrintToLog()
    * @author Brenon
    */
    public void PrintObject(GameObject oObject, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print object: " + oObject);
    }

    /* @brief Prints a Vector3 to the log file.
    *
    * Prints the Vector3 vVector to the log file. If bPrepend is TRUE then 'PRINTVECTOR'
    * will be prepended onto the text outputted to the log file.
    *
    * @param vVector - The Vector3 to be printed to the log.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintInteger(), PrintFloat(), PrintString(), PrintObject(), PrintToLog()
    * @author Brenon
    */
    public void PrintVector(Vector3 vVector, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print vector: " + vVector);
    }

    /* @brief Prints a timestamped entry to the log file.
    *
    * Prints a timestamped string entry to the log file. Note this will only be visible if Script logging is enabled. To enable, edit \\tag\main\build\bin_release\ECLog.ini and set Script=1 in the [LogTypes] section.
    *
    * @param sLogEntry - The string entry to print to the log file.
    * @sa PrintInteger(), PrintFloat(), PrintString(), PrintObject(), PrintVector()
    * @author Brenon
    */
    public void PrintToLog(string sMessage)
    {
        Debug.Log(sMessage + " | " + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
    }

    /* @brief DejaInsight Enabled log writer
    *
    * Prints a timestamped string entry to the log file.
    *
    * @param nChannel  - EngineConstants.LOG_CHANNEL_* constant for use with Deja
    * @param sLogEntry - The string entry to print to the log file.
    * @param oTarget - The target of the debugged function.
    * @author Georg
    */
    public void LogTrace(int nChannel = EngineConstants.LOG_CHANNEL_GENERAL, string sLogEntry = "", GameObject oTarget = null)
    {
        string sString = nChannel + " | " + sLogEntry + " | " + oTarget;
        Debug.Log("log trace: " + sString);
    }

    /* @brief Prints a string to the log file.
    *
    * Prints the string rResource to the log file. If bPrepend is TRUE, then 'PRINTRESOURCE' will
    * be prepended onto the text outputted to the log file.
    *
    * @param rResource - The string value to print out to the log.
    * @param bPrepend - Specifies if the type of value should be prepended on to the log string.
    * @sa PrintInteger(), PrintFloat(), PrintObject(), PrintVector(), PrintToLog(), PrintString()
    * @author Paul
    */
    public void PrintResource(string rResource, int bPrepend = EngineConstants.FALSE)
    {
        if (bPrepend != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        Debug.Log("print string: " + rResource);
    }

    /* @brief Prints a timestamped entry to the log file and flushes it. Very expensive.
    *
    * @param sLogEntry - The string entry to print to the log file.
    * @sa PrintToLog
    * @author Jacques Lebrun
    */
    public void PrintToLogAndFlush(string sLogEntry)
    {
        Debug.Log(sLogEntry + " | " + DateTime.Now.ToString("yyyyMMddHHmmssfff" + " | TO DO Flush"));
    }

    /* @brief Shows a warning.
    *
    * Shows a warning with the specified string.
    *
    * @param sWarning - The warning string.
    * @author Jose
    */
    public void Warning(string sWarning)
    {
        Debug.LogWarning("warning: " + sWarning);
    }

    /* @brief Sends a string to the probe system.
    *
    * Sends a string to the probe system.
    *
    * @param sOutput - name of the stat to track
    * @param fValue - The value of stat to track (optional)
    * @author Jose
    */
    public void Probe(string sOutput, float fValue = 0)
    {
        Debug.Log("Probe: " + sOutput + "|" + fValue);
    }

    /* @brief Gets the current world time - used for logging 
    *
    * Gets the current world time 
    *
    * @author Sophia
    */
    public int GetTime()
    {
        return Convert.ToInt32(DateTime.Now.ToOADate());
    }

    /* @brief Gets the current system tick count.
    *
    * Gets an int32 representing the system tick count. This is useful to create unique timestamps for
    * logging purposes
    *
    * @author Georg
    */
    public int GetLowResTimer()
    {
        return Convert.ToInt32(DateTime.Now.ToOADate());
    }

    /* @brief Prints a string to all the client screens.
    *
    * Prints the string sString to the screen in all the available clients.
    *
    * @param sString - The string value to print out to the screen.
    * @param nPosFromTop - Where to output the string on the client's screen.
    * @param fLife - life for the string in seconds
    * @author Adriana
    */
    public void DEBUG_PrintToScreen(string sString, int nPosFromTop = 10, float fLife = 10.0f)
    {
        Debug.Log("TO DO PrintToScreen: " + sString);
    }

    /* @brief Prints a string to the onscreen log window provided by the GUI.
    *
    * Prints a string to the onscreen log window provided by the GUI.
    *
    * @param sString - The string to print.
    * @author Henry
    */
    public void PrintToLogWindow(string sString, string sLogType = "LogWindow")
    {
        Debug.Log("TO DO print to log window: " + sLogType + " | " + sString);
    }

    /* @brief Sends a message to the Designer Run Database.
    *
    * If the string is less than 960 characters, this will send it as a message
    * to the database. Be sure to format it in such a way that it makes sence
    * to the db itself.
    * If your name is not Georg, you should not use this function.
    *
    * @param sString - The string to print.
    * @author Paul
    */
    public void SendToRunDatabase(string sString)
    {
        Debug.Log("send to run | LogToDatabase: " + sString);
    }

    /* @brief Submits a game xEvent to the Designer Run Database
    *
    * Submits a 'game event' to the designer run database
    *
    * If your name is not Georg, you should not use this function.
    *
    * @param nEventId - The xEvent to submit.
    * @param oidObject - The xEvent owner
    * @param oidTarget - The xEvent target
    * @param nParam1  - optional Event Parameter 1
    * @param nParam2  - optional Event Parameter 2
    * @param sParam1  - optional String Parameter 1
    * @param sParam2  - optional String Parameter 2
    * @param sType   -  optional Message Type override
    *
    * @author Georg
    */
    public void SendGameEventRunDatabase(int nEventId, GameObject oidObject = null, GameObject oidTarget = null, int nParam1 = 0, int nParam2 = 0, string sParam1 = "", string sParam2 = "", string sType = "xE")
    {
        if (oidObject == null) oidObject = gameObject;//gameObject
        string sString = "event ID: " + nEventId + " game object: " + oidObject + " target: " + oidTarget;
        Debug.Log("send to game event run database: " + sString);
    }

    /* @brief Saves a screenshot
    *
    *
    * @param sPath - optional path. This uses forward slashes, not backslashes(as used in windows).  Example: c:\screenshots\ or \\bioware\share\da
    * @param sFileName -  optional FileName
    *
    * @author Sam
    */
    public void Screenshot(string sFileName = "", string sPath = "", int bHideConsole = EngineConstants.FALSE)
    {
        if (bHideConsole != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        string s = sPath + sFileName;
        Application.CaptureScreenshot(s);
        Debug.Log("Screenshot: " + s);
    }

    /* @brief saves a screenshot to the screenshots directory on the players hard drive
*
* @author EricP
*
* @param bHideInterface	- optional boolean to hide the interface or not
* @param nTitleStrRef 		- optional strref for the screenshot's title
* @param nDescriptionStrRef 	- optional strref for the screenshot's description
* @param nType 			- optional int Screenshot type (auto-screenshot, achievement screenshot, etc)
* @param sId 			- optional string screenshot Id for web journal positioning.
*/

    public void TakeScreenshot(int bHideInterface = 0, int nTitleStrRef = 0, int nDescriptionStrRef = 0, int nType = 0, string sId = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }


    /* @brief Returns 1 if debug helpers are turned on.
    *
    * @author Jacques
    */
    public int GetDebugHelpersEnabled()
    {
        Debug.Log("get debug helpers enabled: TO DO");
        return EngineConstants.TRUE;
    }

    /* zDA2 @brief Returns 1 if ((nInt & nFlag) == nFlag)
    *
    * @author Noel
    */
    public int hasflag(int nInt, int nFlag)
    {
        return ((nInt & nFlag) == nFlag) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    public int min(int a, int b)
    {
        return Math.Min(a, b);
    }

    public int max(int a, int b)
    {
        return Math.Max(a, b);
    }

    public float minf(float a, float b)
    {
        return Mathf.Min(a, b);
    }

    public float maxf(float a, float b)
    {
        return Mathf.Max(a, b);
    }

    /* @} */
    /***************************************************************/

    /***************************************************************/
    // Actions / System
    /***************************************************************/
    /* @addtogroup actionsys Action & System Functions
    *
    * Functions to add and delay actions to objects as well as generic
    * system functions.
    */
    /* @{*/

    /* @brief Assigns the target GameObject to run the specified function.
    *
    * Assigns the target GameObject to run the specified function. It should
    * be noted that a 'function' is any void returning function, including user
    * defined functions.
    *
    * @param oTarget - Specifies which GameObject the function should be run on
    * @param fnFunction - The void returning function to run on the target object
    * @sa DelayFunction()
    * @remarks If the GameObject does not exist or the specified function is not a
    * void returning function, this call will fail. If a non-void returning
    * function is specified an error will be generated.
    * @author Brenon
    */
    //void AssignFunction(GameObject oTarget, function fnFunction) 

    /* @brief Delays a function call.
    *
    * Calls the function fnFunction after a delay of float fSeconds. The function fnFunction
    * can be any void returning function including user defined functions.
    *
    * @param fSeconds - The amount of time in seconds to delay the function call by.
    * @param fnFunction - The void returning function to run after the delay.
    * @sa AssignFunction()
    * @remarks If the time value is equal to or smaller than 0, the action will run on the next AI update.
    * If the function specified is not a void returning function then DelayFunction() will fail.
    * If a non-void returning function is specified an error will be generated.
    * It should be noted that this scripting function is not blocking, execution will continue and
    * the specified fnFunction will be run separately after the appropriate amount of time has elapsed.
    * @author Brenon
    */
    //void DelayFunction(float fSeconds, function fnFunction) 

    /* @brief Executes a script.
    *
    * Deprecated, do not use -- Georg. 
    * Runs the script with the name specified by the string sScript on the GameObject oTarget. 
    * Note that this will happen immediately from the execution point in the current script.
    * The current script will continue only after the new script has finished executing. 
    *
    * @param rScript - The name of the script to run (*.ncs)
    * @param oTarget - The GameObject to run the script on.
    * @remarks If the specified script or GameObject does not exist then the call to ExecuteScript() will fail.
    * @author Brenon
    */
    //Deprecated_ExecuteScript(string rScript, GameObject oTarget)

    /* @brief Returns a value from a 2DA in string format.
    *
    * Returns a 2DA string based on the specified Row and Column values.
    *
    * @param n2DA - The 2DA to access
    * @param sColumn - The name of the column to access
    * @param nRow - The 0 based index of the row to access
    * @param s2DA - (optional) if n2da is -1 and this is a valid string, it will retrieve
    *       the 2da based on the name instead of the index. Note that this should be avoided
    *       when possible.  
    * @return Returns the string specified by the parameters. Returns an empty string on error.
    * @author Brenon, Georg
    */
    public string GetM2DAString(int n2DA, string sColumn, int nRow, string s2DA = "")
    {
        XmlDocument x2da = GetXML(n2DA);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(x2da));
        XElement root = xDoc.Root;
        string cell = (string)
            (from el in root.Elements(GetNodeName(n2DA))
             where (int)el.Element("ID") == nRow
             select el).First().Element(sColumn).Value;

        return cell;
    }

    /* @brief Returns a string from a 2DA.
    *
    * Returns a 2DA string on the specified Row and Column values.
    *
    * @param n2DA - The 2DA to access
    * @param sColumn - The name of the column to access
    * @param nRow - The 0 based index of the row to access
    * @param s2DA - (optional) if n2da is -1 and this is a valid string, it will retrieve
    *       the 2da based on the name instead of the index. Note that this should be avoided
    *       when possible.  

    * @return Returns a resrouce specified by the parameters. 
    * @author Georg
    */
    public string GetM2DAResource(int n2DA, string sColumn, int nRow, string s2DA = "")
    {
        string s = GetM2DAString(n2DA, sColumn, nRow, s2DA);
        //string r = new string();
        throw new NotImplementedException();
    }

    /* @brief Returns a 2DA value in integer format.
    *
    * Returns a 2DA integer based on the specified Row and Column values.
    *
    * @param n2DA - The 2DA to access
    * @param sColumn - The name of the column to access
    * @param nRow - The 0 based index of the row to access
    * @param s2DA - (optional) if n2da is -1 and this is a valid string, it will retrieve
    *       the 2da based on the name instead of the index. Note that this should be avoided
    *       when possible.  
    * @return Returns the int specified by the parameters. Returns 0.
    * @sa Set2DAInt()
    * @author Brenon, Georg
    */
    public int GetM2DAInt(int n2DA, string sColumn, int nRow, string s2DA = "")
    {
        XmlDocument x2da = GetXML(n2DA);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(x2da));
        XElement root = xDoc.Root;

        string sCell = (from el in root.Elements(GetNodeName(n2DA))
                        where (int)el.Element("ID") == nRow
                        select el).First().Element(sColumn).Value;

        int cell;

        //Ignore if 4* or Empty
        if (sCell == "****" || sCell == null || sCell == string.Empty)
        {
            return 0;
        }
        //Try to parse it
        else if (int.TryParse(sCell, out cell))
        {
            return cell;
        }
        //Perhaps is hexadecimal
        else if (sCell.IndexOf("0x") != -1)
        {
            return int.Parse(sCell.Split('x')[1], System.Globalization.NumberStyles.HexNumber);
        }
        else {
#if DEBUG
            Warning("table " + n2DA + " cannot parse " + sCell + " into an integer, returning 0!");
#endif
            throw new NotImplementedException();
            //return 0;
        }
    }

    /* @brief Returns a 2DA value in float format.
    *
    * Returns a 2DA integer based on the specified Row and Column values.
    *
    * @param n2DA - The 2DA to access
    * @param sColumn - The name of the column to access
    * @param nRow - The 0 based index of the row to access
    * @param s2DA - (optional) if n2da is -1 and this is a valid string, it will retrieve
    *       the 2da based on the name instead of the index. Note that this should be avoided
    *       when possible.  
    * @return Returns the float specified by the parameters. 
    * @author Georg
    */
    public float GetM2DAFloat(int n2DA, string sColumn, int nRow, string s2DA = "")
    {
        XmlDocument x2da = GetXML(n2DA);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(x2da));
        XElement root = xDoc.Root;
        string sCell = (from el in root.Elements(GetNodeName(n2DA))
                        where (int)el.Element("ID") == nRow
                        select el).First().Element(sColumn).Value;
        float cell;
        //Ignore if 4* or Empty
        if (sCell == "****" || sCell == null || sCell == string.Empty)
        {
            return 0;
        }
        //try and parse it
        else if (float.TryParse(sCell, out cell))
            return cell;
        else {
#if DEBUG
            Warning("table " + n2DA + " cannot parse " + sCell + " into a float, returning 0!");
#endif
            throw new NotImplementedException();
            //return 0f;
        }
    }

    /* @brief Returns the number of rows in the specified 2da.
    *
    * Returns the number of rows in the specified 2da.
    *
    * @param n2DA - The 2DA to access.
    * @param s2DA - (optional) if n2da is -1 and this is a valid string, it will retrieve
    *       the 2da based on the name instead of the index. Note that this should be avoided
    *       when possible.  
    * @returns Returns the number of rows in the 2DA, returns 0 on error.
    * @author Brenon, Georg
    */
    public int GetM2DARows(int n2DA, string s2DA = "")
    {
        XmlDocument x2da = GetXML(n2DA);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(x2da));
        XElement root = xDoc.Root;
        int i = 0;
        foreach (var xe in root.Elements()) i++;
        return i;
    }

    /* @brief Returns the number of columns in the specified 2da.
    *
    * Returns the number of columns in the specified 2da.
    *
    * @param n2DA - The 2DA to access.
    * @returns Returns the number of columns in the 2DA, returns 0 on error.
    * @sa Get2DARows()
    * @author Brenon
    */
    public int GetM2DAColumns(int n2DA)
    {
        //Debug.Log("get m2da columns: " + GetDataCount(n2DA, true));
        throw new NotImplementedException();
    }

    /* @brief Returns a 2DA value (by hashed column and row) in integer format.
    *
    * Returns a 2DA integer based on the specified Row and Column values.  Use GetM2DAInt 
    * if you don't know the hashed value of the column.
    *
    * @param n2DA - The 2DA to access
    * @param nColumn - The hashed name of the column
    * @param nRow - The 0 based index of the row to access
    * @return Returns the int specified by the parameters. Returns 0 if row/column is bad or not an integer.
    * @sa GetHashedM2DAInt() 
    * @author MarkB
    */
    public int GetHashedM2DAInt(int n2DA, int nColumnHash, int nRow)
    {
        throw new NotImplementedException();
    }

    /* @brief Returns a bool 2DA value (by hashed column and row) in integer format.
    *
    * Returns a bool 2DA as integer (1 = true, 0 = false) based on the specified Row and Column values.
    *
    * @param n2DA - The 2DA to access
    * @param nColumn - The hashed name of the column
    * @param nRow - The 0 based index of the row to access
    * @return Returns the int specified by the parameters. Returns 0 if row/column is bad or not an integer.
    * @sa GetHashedM2DABool() 
    * @author NicolasN
    */
    public int GetHashedM2DABool(int n2DA, int nColumnHash, int nRow)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns a 2DA value (by hashed column and row) in float format.
    *
    * Returns a 2DA float based on the specified Row and Column values.  Use GetM2DAFloat
    * if you don't know the hashed value of the column.
    *
    * @param n2DA - The 2DA to access
    * @param nColumn - The hashed name of the column
    * @param nRow - The 0 based index of the row to access
    * @return Returns the float specified by the parameters. Returns 0.0f if the row/column is bad or not a float.
    * @sa GetHashedM2DAFloat() 
    * @author MarkB
    */
    public float GetHashedM2DAFloat(int n2DA, int nColumnHash, int nRow)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns the combined 2DA value of this abillity and its upgrades
    *
    * Returns the combined 2DA value of this abillity and its upgrades if the target creature has them
    *
    * @author NicolasN
    */
    public int GetUpgradableAbilityHashedM2DAInt(GameObject oTarget, int nColumnHash, int nAbilityId)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns the combined 2DA value of this abillity and its upgrades
    *
    * Returns the combined 2DA value of this abillity and its upgrades if the target creature has them
    *
    * @author NicolasN
    */
    public float GetUpgradableAbilityHashedM2DAFloat(GameObject oTarget, int nColumnHash, int nAbilityId)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns a 2DA value (by hashed column and row) as a string.
    *
    * Returns a string based on the specified Row and Column values.  Use GetM2DAString
    * if you don't know the hashed value of the column.
    *
    * @param n2DA - The 2DA to access
    * @param nColumn - The hashed name of the column
    * @param nRow - The 0 based index of the row to access
    * @return Returns the string specified by the parameters. Returns "" if the row/column is bad or not a string.
    * @sa GetHashedM2DAString() 
    * @author MarkB
    */
    public string GetHashedM2DAString(int n2DA, int nColumnHash, int nRow)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns a 2DA value (by hashed column and row) as a string.
    *
    * Returns a string based on the specified Row and Column values.  Use GetM2DAResource
    * if you don't know the hashed value of the column.
    *
    * @param n2DA - The 2DA to access
    * @param nColumn - The hashed name of the column
    * @param nRow - The 0 based index of the row to access
    * @return Returns the string specified by the parameters. Returns "" if the Row/column is bad or not a resrouce.
    * @sa GetHashedM2DAResource() 
    * @author MarkB
    */
    public string GetHashedM2DAResource(int n2DA, int nColumnHash, int nRow)
    {
        throw new NotImplementedException();
    }

    /* @brief Returns the row ID for the given row index
    *
    * Returns the value of the ID column for the given 2DA row index
    *
    * @param n2DA - The 2DA to access
    * @param nRowIndex - Index in the 2DA for which we want to know the ID
    * @author Nicolas Ng Man Sun
    */
    public int GetM2DARowIdFromRowIndex(int n2DA, int nRowIndex, string s2DA = "")
    {
        //Exclude zero
        if (nRowIndex == 0) nRowIndex++;

        XmlDocument x2da = GetXML(n2DA);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(x2da));
        XElement root = xDoc.Root;
        int i = 0;
        foreach (var xe in root.Elements())
        {
            i++;
            if (i == nRowIndex) return int.Parse(xe.Element("ID").Value);
        }
        throw new NotImplementedException();
    }

    /* @brief Returns a string from the tlk table.
    *
    * Returns a string from the tlk table based on the specified
    * strref (String reference) and gender. It should be noted
    * that there may be multiple genders in the tlk table depending
    * on how the specific tlk table is generated.
    *
    * @param nStrRef - The string reference to get from the tlk table
    * @param nGender - The gender variation of the string
    * @return Returns the string specified by the string reference. Returns an empty string on error.
    * @author Brenon
    */
    //string GetTlkTableString(int nStrRef, int nGender = EngineConstants.GENDER_MALE)
    public string GetTlkTableString(int nStrRef)
    {
        //Debug.Log("get talked able to string: ");
        XmlDocument xmldoc = GetXML(EngineConstants.TABLE_TALK);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        string tlkString = (string)
            (from el in root.Elements("tlkElement")
             where (int)el.Element("tlkID") == nStrRef
             select el).First().Element("tlkString").Value;

        return tlkString;
    }

    /* @brief Returns the current difficulty of the game.
    *
    * Returns the current difficulty of the game.
    *
    * @returns Returns a game difficulty constant EngineConstants.GAME_DIFFICULTY_*
    * @author Brenon
    */
    public int GetGameDifficulty()
    {
        return GetGameSettings("GameDifficulty");
    }

    /* @brief Returns the graphics detail level of the currently running session.
    *
    * Returns the graphics detail level of the currently running session.
    *
    * @returns Returns a game difficulty constant GRAPHICS_DETAIL_LEVEL_*
    * @author MarkB
    */
    public int GetGraphicsDetailLevel()
    {
        return GetGameSettings("GraphicsDetailLevel");
    }

    /* @brief Debug xCommand to spawn the script debugger.
    *
    * Debug xCommand to spawn the script debugger. If the script
    * has not been compiled with debug information, then this function
    * will do nothing.
    *
    * @remarks To use this function you must have generated debug information for
    * the script you are attempting to debug.
    * @author Brenon
    */
    public void DebugSpawnScriptDebugger()
    {
        throw new NotImplementedException();
    }

    /* @brief Execute console command.
    *
    * Execute a given console command.
    *
    * @remarks This is debug only as it executes console xCommand directly instead of sending a message to the client
    * @param sArg - The console xCommand to execute, including arguments.
    * @return Returns the string that the console xCommand returns.
    * @author Yuri
    */
    public string DEBUG_ConsoleCommand(string sArg)
    {
        Debug.Log("console command: ");
        //split the string by white spaces
        string[] args = sArg.Split(null);
        if (args.Length == 0)//nothing was passed
        {
            return "";
        }
        if (args[0] != "runscript")//check for runscript command
        {
            throw new NotImplementedException();
        }
        if (args.Length > 1) //if there is actually a script name variable passed
        {
            string script = args[1];
            Debug.Log("scripts to run: " + script);
        }
        if (args.Length > 2) //if there are arguments for the script
        {
            string sVar = "";
            for (int i = 2; i < args.Length; i++)
            {
                sVar += args[i];
                sVar += " ";
            }
            //remove the last white space
            sVar = sVar.TrimEnd(' ');
            //Debug.Log("script variables: " + sVar);

            //adds arguments for future use on Area GameObject on scene
            SetLocalString(GameObject.FindGameObjectWithTag("Area"), "RUNSCRIPT_VAR", sVar);

            //adds script to run with arguments
            GameObject.FindGameObjectWithTag("Area").AddComponent<xAddTalent>();

            //TO DO error check and such
            return "OK!";
        }
        return "";
    }

    /* @brief Returns the name of the currently executing script
    *
    * Returns the name of the currently executing script for debugging purposes.
    *
    * @return string with the name of the current script.
    * @author Georg Zoeller
    */
    public string GetCurrentScriptName(string s = null)
    {
        if (s != null) return s;
        else return String.Empty;
    }

    /* @brief Returns a string matching the currently running ncs.
    *
    * Returns the currently executing script string .
    *
    * @return ncs string for current script.
    * @author Georg Zoeller
    */
    public string GetCurrentScriptResource()
    {
        throw new NotImplementedException();
    }

    /* @} */
    /***************************************************************/

    /***************************************************************/
    // Position / Orientation
    /***************************************************************/
    /* @addtogroup posorient Position & Orientation Functions
    *
    * Functions to modify and access position and orientation.
    */
    /* @{ */

    /* @brief Returns the direction an GameObject is facing.
    *
    * Returns the direction the GameObject oTarget is currently facing in degrees. The direction
    * an GameObject is facing is expressed as an incrementing clockwise degree value
    * starting from the south. Related constants are: DIRECTION_NORTH, DIRECTION_EAST,
    * DIRECTION_SOUTH and DIRECTION_WEST.
    *
    * @param oTarget - The GameObject to get the facing direction of.
    * @return Returns the direction the GameObject is facing in degrees. Returns -1.0f on error.
    * @sa SetFacing(), SetFacingPosition(), SetFacingObject(), SetOrientation(), GetOrientation()
    * @author Brenon
    */
    public float GetFacing(GameObject oTarget, int bMirrorYAxis = EngineConstants.TRUE)
    {
        Debug.LogWarning("get facing, ensure that right is correct, instead of down, or something else");
        float fFacing = Mathf.Atan2(oTarget.transform.position.z, oTarget.transform.position.x) * Mathf.Rad2Deg;
        if (bMirrorYAxis != EngineConstants.FALSE)
        {
            //the unity way
            if (fFacing > 180f)
            {
                fFacing -= 360f;
            }
            /*//the Dragon age way
            if (fFacing > 180.0f)
            {
                 fFacing = 360.0f - fFacing;
            }*/
        }
        return fFacing;
    }

    /* @brief Sets the direction an GameObject is facing.
    *
    * Sets the direction the GameObject oTarget is currently facing in degrees. The direction
    * an GameObject is facing is expressed as an incrementing clockwise degree value
    * starting from the south. Related constants are: DIRECTION_NORTH, DIRECTION_EAST,
    * DIRECTION_SOUTH and DIRECTION_WEST.
    *
    * @param oTarget - The Object to set the facing direction of.
    * @param fFacing - The direction the GameObject will face.
    * @remarks If the specified GameObject is invalid or does not have an orientation then the
    * function will fail.
    * @sa GetFacing(), SetFacingPosition(), SetFacingObject(), SetOrientation(), GetOrientation()
    * @author Brenon
    */
    public void SetFacing(GameObject oTarget, float fFacing, int bMirrorYAxis = EngineConstants.TRUE)
    {
        Debug.Log("set facing: ensure the correct rotation");
        if (bMirrorYAxis != EngineConstants.FALSE)
        {
            //the unity way
            if (fFacing > 180f)
            {
                fFacing -= 360f;
            }
            /*//the Dragon age way
            if (fFacing > 180.0f)
            {
                 fFacing = 360.0f - fFacing;
            }*/
        }

        oTarget.transform.forward = fFacing * Vector3.forward;
    }

    /* @brief Returns the orientation of the object.
    *
    * Returns the orientation of the GameObject in Vector3 format.
    *
    * @param oTarget - Object to get the orientation from.
    * @returns Returns a Vector3 representing the orientation of the target object. Returns an empty Vector3 on error.
    * @sa SetOrientation(), SetFacing(), GetFacing(), SetFacingPosition(), SetFacingObject()
    * @author Brenon
    */
    public Vector3 GetOrientation(GameObject oTarget)
    {
        return oTarget.transform.rotation.eulerAngles;
    }

    /* @brief Sets the orientation of the target object.
    *
    * Sets the orientation of the target object.
    *
    * @param oTarget - Object to set the orientation for.
    * @param vOrientation - Vector orientation to set for the object.
    * @remarks If the target GameObject is invalid or does not have an orientation then the function
    * will fail.
    * @sa GetOrientation(), SetFacing(), GetFacing(), SetFacingPosition(), SetFacingObject()
    * @author Brenon
    */
    public void SetOrientation(GameObject oTarget, Vector3 vOrientation)
    {
        oTarget.transform.LookAt(vOrientation);
    }

    /* @brief Returns the position of an object.
    *
    * Returns a position Vector3 containting the xyz coordinates of the GameObject oTarget.
    *
    * @param oTarget - The Object to get the position of.
    * @return Returns a Vector3 containing the position of the specified object. Returns an empty Vector3 on error.
    * @sa SetPosition()
    * @author Brenon
    */
    public Vector3 GetPosition(GameObject oTarget)
    {
        return oTarget.transform.position;
    }

    /* @brief Sets the position of an object.
    *
    * Sets the position of the GameObject oObject to the xyz coordinates contained in the vector
    * vPosition. If bSafePosition is set to TRUE, then a safe position will be computed based
    * off of the Vector3 vPosition and this position will be used. A safe position is a valid
    * position on the nearest walkable mesh.
    *
    * @param oObject - The Object to set the position of.
    * @param vPosition - The position Vector3 containing the new coordinates.
    * @param bSafePosition - Specifies whether a safe position should be calculated and used.
    * @param bForceCameraReset - Force a camera position reset to the new creature position; ignored
    *                            if oObject is not the player controlled creature.
    * @remarks If an invalid GameObject is specified then the function will fail.
    * @remarks If a non-creature GameObject is specified, a safe position can not be computed
    * for the GameObject so vPosition will be used, regardless of whether or not it is safe.
    * @remarks If a safe position cannot be found and the bSafePosition flag is set to TRUE,
    * the function will fail.
    * @sa GetPosition()
    * @author Brenon
    */
    public void SetPosition(GameObject oObject, Vector3 vPosition, int bSafePosition = EngineConstants.TRUE)
    {
        Debug.Log("set position: TO DO handle safe navigation mesh");
        oObject.transform.position = vPosition;
    }

    /* @brief Sets an GameObject to face a given position.
    *
    * Sets the GameObject oObject to face the position contained in Vector3 vPosition.
    *
    * @param oObject - Object to set the facing for.
    * @param vPosition - Vector position to face towards.
    * @remarks If the target GameObject is invalid the function will fail.
    * @sa SetFacing(), GetFacing(), SetFacingObject()
    * @author Brenon
    */
    public void SetFacingPosition(GameObject oObject, Vector3 vPosition)
    {
        oObject.transform.LookAt(vPosition);
    }

    /* @brief Sets the target to face a given object.
    *
    * Sets the target GameObject to face a given object.
    *
    * @param oObject - Object to set the facing for.
    * @param oTarget - Object to face.
    * @param bInvert - This flag is very confusing, currently when set to FALSE it will invert. Why?
    * @param bInstant - By default an GameObject will change its facing gradually, set this to true
    *                   to change facing immediately.
    * @remarks If the GameObject or the target GameObject are invalid, the function will fail.
    * @sa SetFacing(), GetFacing(), SetFacingPosition()
    * @author Brenon
    */
    public void SetFacingObject(GameObject oObject, GameObject oTarget, int bInvert = EngineConstants.TRUE)
    {
        oObject.transform.LookAt(oTarget.transform.position);
    }

    /* @brief Gets the angle between two objects
    *
    * Returns the angle (in degrees) between the direction objectA is facing and 
    * the Vector3 formed between objectA and objectB. The angle follows a 
    * counterclockwise sequence, starting from the front. If a objectB is derectly in front
    * of objectA, the angle will be 0. If GameObject B is exactly to the left, the angle will be
    * 90 degrees. If GameObject B is behind GameObject A, the angle will be 180, and if GameObject B is
    * to the right, the angle will be 270.
    *
    * @param oObjectA - Object that has a facing direction.
    * @param oObjectB - Object that is positioned relative to GameObject A.
    * @author Gabo.
    */
    public float GetAngleBetweenObjects(GameObject oObjectA, GameObject oObjectB)
    {
        return Vector3.Angle(oObjectA.transform.position, oObjectB.transform.position);
    }

    //DHK Math functions section, removing, using instead C#/Unity

    /* @brief Returns the distance between two objects.
    *
    * Returns the distance between GameObject oObjectA and GameObject oObjectB. Distance is measured through
    * three dimmensional space and not just along the xy plane, so the height component of each object's location
    * is relevant.  If creatures, we assume that A is attacking B, so we use the closest interaction point on B relative to A.
    *
    * @param oObjectA - The first object. (attacker)
    * @param oObjectB - The second object. (being attacked by B)
    * @param bSubtractPersonalSpace - Removes personal spaces of A and B from the returned distance
    * @returns Returns the distance between the two objects. Returns -1.0f on error.
    * @sa GetDistanceBetweenLocations()
    * @author Nicolas
    */
    public float GetDistanceBetween(GameObject oObjectA, GameObject oObjectB, int bSubtractPersonalSpace = EngineConstants.FALSE)
    {
        Debug.Log("get distance between: TO DO subtract personal space");//zDA2
        return Vector3.Distance(oObjectA.transform.position, oObjectB.transform.position);
    }

    /* @brief Returns the distance between two locations.
    *
    * Returns the distance between Vector3 lLocationA and Vector3 lLocationB Distance is measured through
    * three dimmensional space and not just along the xy plane, so the hieght component of each object's location
    * is relevant.
    *
    * @param lLocationA - The first location.
    * @param lLocationB - The second location.
    * @returns Returns the distance between the two locations. Returns -1.0f on error.
    * @sa GetDistanceBetween()
    * @author Brenon
    */
    public float GetDistanceBetweenLocations(Vector3 lLocationA, Vector3 lLocationB)
    {
        return Vector3.Distance(lLocationA, lLocationB);
    }

    /* @brief Determines if an GameObject is in a trigger.
    *
    * Returns TRUE if the GameObject oObject is within the trigger oTrigger, otherwise returns FALSE.
    *
    * @param oObject - The GameObject which may be within the trigger.
    * @param oTrigger - The trigger the GameObject may be within.
    * @returns TRUE if the GameObject oObject is in the trigger oTrigger.
    * @author Brenon
    */
    public int IsInTrigger(GameObject oObject, GameObject oTrigger)
    {
        Debug.Log("is in trigger: double check");
        int rRet = EngineConstants.FALSE;

        xTrigger _xTrigger = oTrigger.GetComponent<xTrigger>();//get trigger script
        if (_xTrigger != null)//if found
        {
            return rRet = ((_xTrigger.gameObject.GetComponent<xGameObjectUTT>().oThreats
                 .Find(threat => threat.oTarget = oObject)) != null) ? EngineConstants.TRUE : EngineConstants.FALSE;
        }
        return rRet;
    }

    /* @brief Determines if a Vector3 is empty.
    *
    * Returns TRUE if the Vector3 vVector is empty, otherwise returns FALSE. An empty Vector3 is a Vector3 with
    * x, y and z values of 0.0f.
    *
    * @param vVector - The Vector3 which may be empty.
    * @returns Returns TRUE if the Vector3 vVector is empty, FALSE if it is not.
    * @author Brenon
    */
    public int IsVectorEmpty(Vector3 vVector)
    {
        return (vVector == Vector3.zero) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Creates a vector.
    *
    * Returns a new Vector3 with the specified x,y and z values.
    *
    * @param x - x value for the vector.
    * @param y - y value for the vector.
    * @param z - z value for the vector.
    * @returns Returns a Vector3 with the specified x, y, and z values.
    * @sa GetVectorMagnitude(), GetVectorNormalize()
    * @author Brenon
    */
    public Vector3 Vector(float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        return new Vector3(x, y, z);
    }

    /* @brief Returns the magnitude of a vector.
    *
    * Returns the magnitude of the Vector3 vVector. This is also known as the length of the vector.
    *
    * @param vVector - The Vector3 to calculate the magnitude of.
    * @returns Returns the floating point magnitude of the vector.
    * @sa Vector(), GetVectorNormalize()
    * @author Brenon
    */
    public float GetVectorMagnitude(Vector3 vVector)
    {
        return vVector.magnitude;
    }

    /* @brief Normalizes a vector.
    *
    * Returns a normalized version of the Vector3 vVector.
    *
    * @param vVector - The Vector3 to normalize.
    * @returns Returns a normalized vector. Returns an empty Vector3 on error.
    * @sa Vector(), GetVectorMagnitude()
    * @author Brenon
    */
    public Vector3 GetVectorNormalize(Vector3 vVector)
    {
        return vVector.normalized;
    }

    /* @brief Converts an angle to a vector.
    *
    * Converts the specified angle (in degrees) to a vector.
    *
    * @param fAngle - The angle to convert to a vector.
    * @returns Returns a Vector3 representation of the angle specified.
    * @sa VectorToAngle()
    * @author Brenon
    */
    public Vector3 AngleToVector(float fAngle)
    {
        Debug.LogWarning("angle to Vector, check that Vector down is zero angle");
        return Quaternion.AngleAxis(fAngle, Vector3.forward) * Vector3.down;
    }

    /* @brief Converts a Vector3 to an angle.
    *
    * Converts the specified Vector3 to an angle (in degrees).
    *
    * @param vVector - The Vector3 to convert to an angle.
    * @returns Returns a floating point degree value.
    * @sa AngleToVector()
    * @author Brenon
    */
    public float VectorToAngle(Vector3 vVector)
    {
        Debug.Log("Vector3 to angle:" + Mathf.Atan2(vVector.z, vVector.x) * Mathf.Rad2Deg);
        return Mathf.Atan2(vVector.z, vVector.x) * Mathf.Rad2Deg;
    }

    /* @brief Determines if there is a line of sight between two objects.
    *
    * Returns TRUE if there is a clear line of sight between GameObject oSource and GameObject oTarget, otherwise returns FALSE.
    * If the line is occluded by geometry then the function will return FALSE. If the two objects are not in the same
    * area or if either GameObject is invalid this function will return FALSE.
    *
    * @param oSource - The first object.
    * @param oTarget - The second object.
    * @returns Returns TRUE if there is a clear line of sight between the two objects. Returns FALSE if there is not.
    * @remarks It should be noted that the function is somewhat expensive and should not be called in an inner loop of your script.
    * @sa CheckLineOfSightVector()
    * @author Brenon
    */
    public int CheckLineOfSightObject(GameObject oSource, GameObject oTarget)
    {
        Debug.LogWarning("checked line of sight object: TO DO");
        //make field-of-view a public variable, and some other things, like check for obstacles in between :-)
        // If the player has entered the trigger sphere...
        // By default the player is not in sight.
        int playerInSight = EngineConstants.FALSE;
        float fieldOfViewAngle = 110f;
        SphereCollider col = oSource.GetComponent<SphereCollider>();
        GameObject player = GameObject.Find("oPlayerMesh");
        // Create a Vector3 from the enemy to the player and store the angle between it and forward.
        Vector3 direction = oTarget.transform.position - oSource.transform.position;
        float angle = Vector3.Angle(direction, oSource.transform.forward);

        // If the angle between forward and where the player is, is less than half the angle of view...
        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            // ... and if a raycast towards the player hits something...
            if (Physics.Raycast(oSource.transform.position, direction, out hit, col.radius))
            {
                // ... and if the raycast hits the player...
                if (hit.collider.gameObject == player)
                {
                    // ... the player is in sight.
                    playerInSight = EngineConstants.TRUE;
                }
            }
        }
        return playerInSight;
    }

    /* @brief Determines if there is a line of sight between two positions.
    *
    * Returns TRUE if there is a line of sight between the position contained in Vector3 vSource and the position contained
    * in Vector3 vTarget, otherwise returns FALSE. If the line is occluded by geometry then the function will return FALSE.
    * Both vectors are assumed to refer to positions in the area the GameObject running this function is currently in, therefore
    * this function must be run on an GameObject which is in the area in which you wish to test the line of sight.
    *
    * @param vSource - The first position vector.
    * @param vTarget - The second position vector.
    * @returns Returns TRUE if there is a clear line of sight between the two positions. Returns FALSE if there is not.
    * @remarks This function must be run on a valid GameObject in the area, ie: a creature, placeable... etc.
    * @remarks It should be noted that the function is somewhat expensive and should not be called in an inner loop of your script.
    * @sa CheckLineOfSightObject()
    * @author Brenon
    */
    //int CheckLineOfSightVector(Vector3 vSource, Vector3 vTarget)

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Conversions
    /***************************************************************/
    /* @addtogroup conversions Conversion Functions
    *
    * Functions to convert from one data type or unit of measurement
    * to another.
    */
    /* @{*/

    /* @brief Converts an integer to a floating point number.
    *
    * Returns the integer nInteger in floating point format.
    *
    * @param nInteger - The integer to convert.
    * @returns A floating point representation of the integer nInteger.
    * @sa FloatToInt(), IntToString(), IntToString(), IntToHexString(), IntToChar()
    * @author Brenon
    */
    public float IntToFloat(int nInteger)
    {
        return Convert.ToSingle(nInteger);
    }

    /* @brief Converts a floating point number to an integer.
    *
    * Returns the float fFloat in integer format. Any digits after the decimal point are dropped.
    *
    * @param fFloat - The floating point number to convert.
    * @returns An integer representation of the float fFloat.
    * @sa IntToFloat(), FloatToString()
    * @author Brenon
    */
    public int FloatToInt(float fFloat)
    {
        return Convert.ToInt32(fFloat);
    }

    /* @brief Converts an integer to a string.
    *
    * Returns the integer nInteger in string format.
    *
    * @param nInteger - The integer to convert.
    * @returns A string representation of the specified integer. Returns an empty string on error.
    * @sa StringToInt(), IntToFloat(), IntToString(), IntToHexString(), IntToChar()
    * @author Brenon
    */
    public string IntToString(int nInteger)
    {
        return nInteger.ToString();
    }

    /* @brief Converts any int, float or GameObject into a string
    *
    * Objects passed in will be converted into their tag
    *
    * @param anyData - The variable to convert.
    * @returns A string representation of the specified variable
    * @sa StringToInt(), IntToFloat(), IntToString(), IntToHexString(), IntToChar()
    * @author Georg Zoeller
    */
    public string ToString(object anyData) //any
    {
        return anyData.ToString();
    }

    /* @brief Converts a string to an integer.
    *
    * Returns the integer at the start of the string sNumber. This function will stop parsing the string
    * when it encounters a non-numeric character.
    *
    * @param sNumber - The string to convert.
    * @returns Returns the integer value in the specified string.
    * @sa IntToString(), StringToFloat(), StringToVector()
    * @author Brenon
    */
    public int StringToInt(string sNumber)
    {
        return Convert.ToInt32(sNumber);
    }

    /* @brief Converts a floating point number to a string.
    *
    * Returns the float fFloat in string format. The string Will include nWidth number of digits before the decimal
    * place and nDecimals number of digits after the decimal place.
    *
    * @param fFloat - The float value to convert.
    * @param nWidth - The size of the value before the decimal, must be a value between 0 and 18 inclusive.
    * @param nDecimals - The number of decimal places, must be a value between 0 and 9 inclusive.
    * @returns Returns a string representation of the specified floating point number.
    * @sa StringToFloat(), FloatToInt()
    * @author Brenon
    */
    public string FloatToString(float fFloat, int nWidth = 18, int nDecimals = 9)
    {
        Debug.Log("float to string may need display on-screen");
        return fFloat.ToString();
    }

    /* @brief Converts a string to a floating point number.
    *
    * Converts a string to a floating point number.
    *
    * @param sNumber - The string to convert.
    * @returns Returns the floating point value in the specified string.
    * @sa FloatToString(), StringToInt(), StringToVector()
    * @author Brenon
    */
    public float StringToFloat(string sNumber)
    {
        return Convert.ToSingle(sNumber);
    }

    /* @brief Converts an GameObject to a string.
    *
    * Returns the unique ID number of the GameObject oObject in string form.
    *
    * @param oObject - The GameObject to convert.
    * @returns Returns the string representation of the object. Returns an empty string on error.
    * @author Brenon
    */
    public string ObjectToString(GameObject oObject)
    {
        Debug.Log("object to string: why?");
        return oObject.ToString();
    }

    /* @brief Converts a Vector3 to a string.
    *
    * Returns the Vector3 vVector in string format. This string will be of the form "x y z" where x,y and z
    * are the x,y and z floats contained in the Vector3 vVector.
    *
    * @param vVector - The Vector3 to convert.
    * @returns Returns the string representation of the vector. Returns an empty string on error.
    * @sa StringToVector()
    * @author Brenon
    */
    public string VectorToString(Vector3 vVector)
    {
        Debug.Log("Vector3 to string: " + vVector.x + " " + vVector.y + " " + vVector.z);
        return vVector.x + " " + vVector.y + " " + vVector.z;
    }

    /* @brief Converts a string to a vector.
    *
    * Returns a new Vector3 from the string sString. The format must be "x y z" where x, y and z are floating point numbers.
    *
    * @param sString - The string to convert.
    * @returns Returns the Vector3 contained in the string. Returns an empty Vector3 on error.
    * @sa VectorToString(), StringToInt(), StringToFloat()
    * @author Brenon
    */
    public Vector3 StringToVector(string sString)
    {
        Debug.Log("string to vector: TO DO double check");
        string[] sArr = sString.Split(null);
        return new Vector3(Convert.ToInt32(sArr[0]), Convert.ToInt32(sArr[1]), Convert.ToInt32(sArr[2]));
    }

    /* @brief Converts an integer to a hexadecimal string.
    *
    * Returns the integer nInteger int hexadecimal string form.
    *
    * @param nInteger - The integer to convert.
    * @returns Returns a hexadecimal string representation of the specified integer.
    * @sa HexStringToInt(), IntToFloat(), IntToString(), IntToString(), IntToChar()
    * @author Brenon
    */
    public string IntToHexString(int nInteger)
    {
        return nInteger.ToString("X");
    }

    /* @brief Converts a hexadecimal string to an integer.
    *
    * Converts a hexadecimal string to an integer.
    *
    * @param sString - The string to convert.
    * @returns Returns the integer representation of the hexadecimal value in the string.
    * @sa IntToHexString()
    * @author Brenon
    */
    public int HexStringToInt(string sString)
    {
        return int.Parse(sString, System.Globalization.NumberStyles.HexNumber);
    }

    /* @brief Converts a character to an integer value.
    *
    * Returns the ascii value of the first character in the string string.
    *
    * @param sString - The string character to convert.
    * @returns Returns the integer value of the character in the string.
    * @remarks If a string with more than one character is passed in, the function will only convert the first character in the string.
    * @sa IntToChar()
    * @author Brenon
    */
    public int CharToInt(string sString)
    {
        return Convert.ToInt32(sString);
    }

    /* @brief Converts an integer to a character.
    *
    * Returns a string containing a single character for which nInteger is the ascii value. Any bits in the binary representation
    * of nInteger beyond the lowest 8 bits are ignored.
    *
    * @param nInteger - The integer to convert.
    * @returns Returns a single character in the string, representing the specified integer.
    * @sa CharToInt(), IntToFloat(), IntToString(), IntToString(), IntToHexString()
    * @author Brenon
    */
    public string IntToChar(int nInteger)
    {
        return nInteger.ToString();
    }

    /* @brief Converts feet to meters.
    *
    * Returns the float fFeet in meters. There are 0.3048 meters in a foot.
    *
    * @param fFeet - The number of feet to convert.
    * @returns Returns fFeet in meters.
    * @sa YardsToMeters()
    * @author Brenon
    */
    public float FeetToMeters(float fFeet)
    {
        return Convert.ToSingle(fFeet * 0.348);
    }

    /* @brief Converts yards to meters.
    *
    * Returns the float fYards in meters. There are 0.9114 meters in a yard.
    *
    * @param fYards - The number of yards to convert.
    * @returns Returns meters.
    * @sa FeetToMeters()
    * @author Brenon
    */
    public float YardsToMeters(float fYards)
    {
        return Convert.ToSingle(fYards * 0.9144);
    }

    /* @brief Converts a string type into a string.
    *
    * Returns the string.
    *
    * @param sResource - The string to convert.
    * @returns Returns a string.
    * @author Paul
    */
    public string ResourceToString(string rRes)
    {
        //may not be needed as a resource is already a string
        return rRes;
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Vars
    /***************************************************************/
    /* @addtogroup vars Variable Functions
    *
    * Functions to manage variables on objects.
    */
    /* @{*/

    /* @brief Retrieves a local integer variable stored on an object.
    *
    * Returns the value of the integer variable with the name sVarName which is stored
    * on the GameObject oObject. If no such variable is stored on the GameObject oObject, 0 is
    * returned. To change the value of a local integer value, use SetLocalInt().
    *
    * @param oObject - The Object the variable is stored on.
    * @param nVarName - The ID of the integer variable to retrieve.
    * @returns Returns the value of the integer variable. Returns 0 on error.
    * @sa SetLocalInt()
    * @author Brenon
    */
    public int GetLocalInt(GameObject oObject, string sVarName)
    {
        var o = GetGameObjectType(oObject);
        //piInstance.SetValue(exam, 37, null);
        //return src.GetType().GetProperty(propName).GetValue(src, null);
        return int.Parse(o.GetType().GetProperty(sVarName).GetValue(o, null).ToString());
    }

    /* @brief Stores a local integer variable on an object.
    *
    * Sets the value of the local integer variable with the name sVarName on the object
    * oObject to the integer value nValue. 
    *
    * @param oObject - Object to store the var on.
    * @param nVarName - The ID of the integer variable to store.
    * @param nValue - The value of the integer to store.
    * @sa GetLocalInt()
    * @author Brenon
    */
    public void SetLocalInt(GameObject oObject, string sVarName, int nValue)
    {
        UpdateGameObjectProperty(oObject, sVarName, nValue.ToString());
    }

    /* zDA2 @brief Retrieves a global integer variable stored in the user's profile.
    *
    * Returns the value of the integer variable with the given ID which is stored
    * in the user's profile. To change the value of a local integer value, use SetGlobalMaxInt().
    *
    * @param nID - The id of the integer variable to retrieve.
    * @returns Returns the value of the integer variable. Returns 0 on error.
    * @sa SetGlobalMaxInt()
    * @author Owen
    */
    public int GetGlobalMaxInt(int nID)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Stores a global integer variable in the user's profile.
    *
    * Sets the value of the global integer variable with the given ID in the user's
    * profile to the integer value nValue as long as nValue is larget than the stored
    * value. 
    *
    * @param nID - The id of the integer variable to store.
    * @param nValue - The value of the integer to store.
    * @sa GetGlobalMaxInt()
    * @author Owen
    */
    public void SetGlobalMaxInt(int nID, int nValue)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Increment a global integer variable stored in the user's profile.
    *
    * Increments and returns the new value of the global integer with given ID 
    * which is stored in the user's profile.
    *
    * @param nID - The id of the integer variable to retrieve.
    * @returns Returns the value of the integer variable. Returns 0 on error.
    * @sa GetGlobalIncrementalInt()
    * @author Owen
    */
    public int IncrementGlobalInt(int nID, int nByValue)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Retrieves a global integer variable stored in the user's profile.
    *
    * Returns the value of the integer variable with the given ID which is stored
    * in the user's profile. To change the value of a local integer value, use SetGlobalMaxInt().
    *
    * @param nID - The id of the integer variable to retrieve.
    * @returns Returns the value of the integer variable. Returns 0 on error.
    * @sa IncrementGlobalInt()
    * @author Owen
    */
    public int GetGlobalIncrementalInt(int nID)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Retrieves a global unlock from the user's profile.
    *
    * Returns the state of the global unlock with the given ID which is stored
    * in the user's profile. To set the global unlock, use SetGlobalUnlock().
    *
    * @param nID - The id of global unlock to retrieve.
    * @returns Returns the state of the global unlock. Returns 0 on error.
    * @sa SetGlobalUnlock()
    * @author Owen
    */
    public int GetGlobalUnlock(int nID)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Sets a global unlock in the user's profile.
    *
    * Sets the state to true for the global unlock with the given ID in the user's profile. 
    *
    * @param nID - The id of the global unlock to set.
    * @sa GetGlobalUnlock()
    * @author Owen
    */
    public void SetGlobalUnlock(int nID)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Retrieves the playthrough ID.
    *
    * Returns the playthrough ID for the current game
    *
    * @returns Returns the playthrough ID.
    * @author Owen
    */
    public int GetPlaythroughID()
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Returns 1 if the user has been online and owns the Entitlement.
    *
    * Returns true if the user has been online and owns the Entitlement.
    *
    * @param sGroup - The group for the entitlement
    * @param sName - The name of the entitlement
    * @returns Returns 1 if the user has been online and owns the Entitlement
    * @sa SetEntitlement(Group, Name)
    * @author Owen
    */
    public int GetEntitlement(string sGroup, string sName)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Tries to set an Entitlement on the player
    *
    * Tries to grant an entitlement to the user.  User must be online and Entitlement
    * must be in the Whitelisted group 
    *
    * @param sGroup - The group for the entitlement
    * @param sName - The name of the entitlement
    * @sa GetEntitlement()
    * @author Owen
    */
    public void SetEntitlement(string sGroup, string sName)
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local floating point variable off of an object.
    *
    * Gets a local floating point variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the var on.
    * @param nVarName - The ID of the floating point variable to retrieve.
    * @returns Returns the floating point variable, returns 0.0f on error.
    * @sa SetLocalFloat()
    * @author Brenon
    */
    public float GetLocalFloat(GameObject oObject, string sVarName)
    {
        var o = GetGameObjectType(oObject);
        return float.Parse(o.GetType().GetProperty(sVarName).GetValue(o, null).ToString());
    }

    /* @brief Sets a local floating point variable on an object.
    *
    * Sets a local floating point variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the floating point variable to set.
    * @param fValue - The value of the variable to set.
    * @sa GetLocalFloat()
    * @author Brenon
    */
    public void SetLocalFloat(GameObject oObject, string sVarName, float fValue)
    {
        UpdateGameObjectProperty(oObject, sVarName, fValue.ToString());
    }

    /* @brief Gets a local string variable on an object.
    *
    * Gets a local string variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the string variable to retrieve.
    * @returns Returns the string variable, returns an empty string on error.
    * @sa SetLocalString()
    * @author Brenon
    */
    public string GetLocalString(GameObject oObject, string sVarName)
    {
        var o = GetGameObjectType(oObject);
        return o.GetType().GetProperty(sVarName).GetValue(o, null).ToString();
    }

    /* @brief Sets a local string variable on an object.
    *
    * Sets a local string variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the string variable to set.
    * @param sValue - The value of the string variable.
    * @sa GetLocalString()
    * @author Brenon
    */
    public void SetLocalString(GameObject oObject, string sVarName, string sValue)
    {
        UpdateGameObjectProperty(oObject, sVarName, sValue);
    }

    /* @brief Gets a local GameObject variable on an object.
    *
    * Gets a local GameObject variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the GameObject variable to retrieve.
    * @returns Returns the GameObject variable, returns an invalid GameObject on error.
    * @sa SetLocalObject()
    * @author Brenon
    */
    public GameObject GetLocalObject(GameObject oObject, string sVarName)
    {
        /*Debug.Log("get local object: TO DO double check");
        Type t = Type.GetType(sVarName);
        return oObject.GetComponent(t).gameObject;*/
        var o = GetGameObjectType(oObject);
        return (GameObject)o.GetType().GetProperty(sVarName).GetValue(o, null);
    }

    /* @brief Sets a local GameObject variable on an object.
    *
    * Sets a local GameObject variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the GameObject variable to set.
    * @param oValue - The value of the GameObject variable.
    * @sa GetLocalObject()
    * @author Brenon
    */
    public void SetLocalObject(GameObject oObject, string sVarName, GameObject oValue)
    {
        /*Debug.LogWarning("set local object: TO DO");
        //basically add GameObject oValue with name sVarName on GameObject oObject
        Type t = Type.GetType(oValue.name);
        oObject.AddComponent(t);
        oObject.GetComponent(t).gameObject.name = sVarName;*/
        UpdateGameObjectProperty(oObject, sVarName, oValue.ToString());
    }

    /* @brief Gets a local Vector3 variable on an object.
    *
    * Gets a local Vector3 variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns the Vector3 variable, returns an invalid Vector3 on error.
    * @sa SetLocalLocation()
    * @author Brenon
    */
    public Vector3 GetLocalLocation(GameObject oObject, int nVarName)
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local Vector3 variable on an object.
    *
    * Sets a local Vector3 variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param lValue - The value of the Vector3 variable.
    * @sa GetLocalLocation()
    * @author Brenon
    */
    public void SetLocalLocation(GameObject oObject, int nVarName, Vector3 lValue)
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local player variable on an object.
    *
    * Gets a local player variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns the player variable, returns an invalid player on error.
    * @sa SetLocalPlayer()
    *
    * @warning It is very important to note that a player is not a reference to a specific
    * player, but rather is a reference to a player slot (player1, player2, player3... etc). So the
    * values will not necessarily refer to the same players across a save/load.
    * @author Brenon
    */
    public GameObject GetLocalPlayer(GameObject oObject, int nVarName)//player
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local player variable on an object.
    *
    * Sets a local player variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param pPlayer - The value of the player variable.
    * @sa GetLocalPlayer()
    *
    * @warning It is very important to note that a player is not a reference to a specific
    * player, but rather is a reference to a player slot (player1, player2, player3... etc). So the
    * values will not necessarily refer to the same players across a save/load.
    * @author Brenon
    */
    public void SetLocalPlayer(GameObject oObject, int nVarName, GameObject pPlayer)//player
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local xEvent variable on an object.
    *
    * Gets a local xEvent variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns the xEvent variable, returns an invalid xEvent on error.
    * @sa SetLocalEvent()
    * @author Brenon
    */
    public xEvent GetLocalEvent(GameObject oObject, int nVarName)
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local xEvent variable on an object.
    *
    * Sets a local xEvent variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param evEvent - The value of the xEvent variable to set.
    * @sa GetLocalEvent()
    * @author Brenon
    */
    public void SetLocalEvent(GameObject oObject, int nVarName, xEvent evEvent)
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local xCommand variable on an object.
    *
    * Gets a local xCommand variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns a xCommand variable, returns an invalid xCommand on error.
    * @sa SetLocalCommand()
    * @author Brenon
    */
    public xCommand GetLocalCommand(GameObject oObject, int nVarName)
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local xCommand variable on an object.
    *
    * Sets a local xCommand variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param cCommand - The value of the variable to set.
    * @sa GetLocalCommand()
    * @author Brenon
    */
    public void SetLocalCommand(GameObject oObject, int nVarName, xCommand cCommand)
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local xEffect variable on an object.
    *
    * Gets a local xEffect variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns an xEffect variable, returns an invalid xEffect on error.
    * @sa SetLocalEffect()
    * @remarks It should be noted that effects taken from current effects on an GameObject are
    * just copies of those effects, not references to them.
    * @author Brenon
    */
    public xEffect GetLocalEffect(GameObject oObject, int nVarName)
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local xEffect variable on an object.
    *
    * Sets a local xEffect variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param eEffect - The value of the xEffect variable to set.
    * @sa GetLocalEffect()
    * @remarks It should be noted that effects taken from current effects on an GameObject are
    * just copies of those effects, not references to them.
    * @author Brenon
    */
    public void SetLocalEffect(GameObject oObject, int nVarName, xEffect eEffect)
    {
        throw new NotImplementedException();
    }

    /* @brief Gets a local itemproperty variable on an object.
    *
    * Gets a local itemproperty variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to get the variable from.
    * @param nVarName - The ID of the variable to retrieve.
    * @returns Returns an itemproperty variable, returns an invalid itemproperty on error.
    * @sa SetLocalItemProperty()
    * @remarks It should be noted that item properties taken from current list on an GameObject are
    * just copies of those item properties, not references to them.
    * @author Brenon
    */
    public float GetLocalItemProperty(GameObject oObject, int nVarName)//itemproperty
    {
        throw new NotImplementedException();
    }

    /* @brief Sets a local itemproperty variable on an object.
    *
    * Sets a local itemproperty variable sVarName on the specified
    * object.
    *
    * @param oObject - Object to set the variable on.
    * @param nVarName - The ID of the variable to set.
    * @param iItemProperty - The value of the itemproperty variable to set.
    * @sa GetLocalItemProperty()
    * @remarks It should be noted that item properties taken from current list on an GameObject are
    * just copies of those item properties, not references to them.
    * @author Brenon
    */
    public void SetLocalItemProperty(GameObject oObject, int nVarName, float iItemProperty)//itemproperty
    {
        throw new NotImplementedException();
    }

    /* @brief Get a local variable of type string on an object
    * 
    * Gets a local variable of type string on the specified object
    *
    * @param oObject The GameObject from which to get the variable
    * @param nVarName The ID of the variable
    * @returns Returns the value of the variable or an empty string name on error.
    * @sa SetLocalResource()
    * @author Hesky
    */
    public string GetLocalResource(GameObject oObject, /*int*/ string nVarName)
    {
        //String and resource are identical in this porting
        return GetLocalString(oObject, nVarName);
    }

    /* @brief Sets a local variable of type string on an object
    *
    * Sets the value of a local variable of type string on the specified object
    *
    * @param oObject The GameObject on which to set the variable
    * @param nVarName The ID of the variable
    * @param sResource The new value of the variable
    * @sa GetLocalResource()
    * @author Hesky
    */
    public void SetLocalResource(GameObject oObject, /*int*/ string nVarName, string sResource)
    {
        Debug.Log("set local string: TO DO: replace string with integer");
        //check the VAR string for XML_RESOURCE_
        /*if (!sVarName.Contains("XML_RESOURCE_"))
        {
             sVarName = "XML_RESOURCE_" + sVarName;
        }
        SetLocal(oObject, sVarName, sResource);*/
        throw new NotImplementedException();
    }

    /* zDA2 @brief Gets an integer (or boolean) value from the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public int GetVaultInt(int nVaultId)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Gets a float value from the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public float GetVaultFloat(int nVaultId)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Gets a string value from the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public string GetVaultString(int nVaultId)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Sets an integer value in the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public void SetVaultInt(int nVaultId, int nValue)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Sets a float value in the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public void SetVaultFloat(int nVaultId, float fValue)
    {
        throw new NotImplementedException();
    }

    /* zDA2 @brief Sets a string value in the world vault
    *
    * @param nVaultId The VaultID of the variable that you want.
    * @author David Robinson
    */
    public void SetVaultString(int nVaultId, string sValue)
    {
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Strings
    /***************************************************************/
    /* @addtogroup strings String Functions
    *
    * Functions to manipulate strings.
    */
    /* @{*/

    /* @brief Returns the length of a string.
    *
    * Returns the number of characters in the string sString.
    *
    * @param sString - The string to get the length of.
    * @returns Returns the length of the string. Returns -1 on error.
    * @author Brenon
    */
    public int GetStringLength(string sString)
    {
        Debug.LogWarning("get string length: return on error should be 0 or -1?");
        return (sString.Length > 0) ? sString.Length : EngineConstants.FALSE;
    }

    /* @brief Converts a string to uppercase.
    *
    * Returns a string that is exactly the same as sString except all letters are converted to upper case.
    *
    * @param sString - The string to convert to upper case.
    * @returns Returns the string converted to uppercase. Returns an empty string on error.
    * @author Brenon
    */
    public string StringUpperCase(string sString)
    {
        return sString.ToUpper();
    }

    /* @brief Converts a string to lowercase.
    *
    * Returns a string that is exactly the same as sString except all letters are converted to lower case.
    *
    * @param sString - The string to convert.
    * @returns Returns the string converted to lowercase, returns an empty string on error.
    * @author Brenon
    */
    public string StringLowerCase(string sString)
    {
        return sString.ToLower();
    }

    /* @brief Truncates a string from the right.
    *
    * Returns a string that is the first nCount characters of the string sString from the right.
    *
    * @param sString - The string to truncate.
    * @param nCount - The number of letters to truncate the string to.
    * @returns Returns the truncated string, returns an empty string on error.
    * @author Brenon
    */
    public string StringRight(string sString, int nCount)
    {
        string sRet = "";
        return sRet = (nCount < sString.Length) ? sString.Substring(sString.Length - nCount) : "";
    }

    /* @brief Truncates the string from the left.
    *
    * Returns a string that is the last nCount characters of the string sString from the left.
    *
    * @param sString - The string to truncate.
    * @param nCount - The number of letters to truncate the string to.
    * @returns Returns the truncated string, returns an empty string on error.
    * @author Brenon
    */
    public string StringLeft(string sString, int nCount)
    {
        return sString.Substring(0, Math.Min(sString.Length, nCount));
    }

    /* @brief Inserts a string into another string.
    *
    * Returns a string which is the string sDestination with the string sString insered at the position index
    * nPosition. This index is 0 indexed, so if nPosition = 0 the string sString will be added to the front of
    * the string sDestination.
    *
    * @param sDestination - The destination string.
    * @param sString - The string to insert.
    * @param nPosition - The position in the destination string to insert at.
    * @returns The modified string. Returns an empty string on error.
    * @author Brenon.
    */
    public string InsertString(string sDestination, string sString, int nPosition)
    {
        return (nPosition < sDestination.Length) ? sDestination.Insert(nPosition, sString) : "";
    }

    /* @brief Returns a substring of a string.
    *
    * Returns nCount number of sequential characters from the string sString starting at character number nStart.
    * Like all string operations, the index is 0 based.
    *
    * @param sString - The string to extract the substring from.
    * @param nStart - The start of the range to extract.
    * @param nCount - The number of letters to use in the substring.
    * @returns Returns the specified substring, returns an empty string on error.
    * @author Brenon
    */
    public string SubString(string sString, int nStart, int nCount)
    {
        return (nStart < sString.Length && nCount < sString.Length) ? sString.Substring(nStart, Math.Min(sString.Length, nCount)) : "";
    }

    /* @brief Returns the position of a substring within a string.
    *
    * Returns the starting index of the first occurence of the substring sSubstring within the string sString
    * beginning at position nStart. Returns -1 if the string sSubstring does not occur in the string sString
    * after position nStart. The result is 0 indexed, meaning this function will return 0 if the substring 
    * occurs at the very beginning of the string.
    *
    * @param sString - The string to search.
    * @param sSubString - The substring to search for.
    * @param nStart - The position to start the search from.
    * @returns Returns the position of the substring, returns -1 on error.
    * @author Brenon
    */
    public int FindSubString(string sString, string sSubString, int nStart = 0)
    {
        //nStart is ignored
        Debug.Log("find sub string");
        return sString.IndexOf(sSubString);
    }

    /* @brief Determines if a string is empty.
    *
    * Returns TRUE if the string sString is an empty string.
    *
    * @param sString - The string which may be empty.
    * @returns Returns TRUE if the string is an empty string, FALSE otherwise.
    * @author Brenon
    */
    public int IsStringEmpty(string sString)
    {
        return (sString.Length > 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Returns a string matching a string id
    *
    * Returns a string matching a string id or an empty string if the string id is not used.
    * Note that this will always get the 'male' string for talktables that have additional      
    * female entries in the talktable 
    *
    * @param nId The String Id.
    * @returns String.
    * @author Georg
    */
    public string GetStringByStringId(int nId)
    {
        //Warning("get Talk string by Talk ID " + nId);
        return GetTlkTableString(nId);
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Events
    /***************************************************************/
    /* @addtogroup events Event Functions
    *
    * Functions to manipulate and handle events.
    */
    /* @{*/

    /* @brief Signals the xEvent to the specified object.
    *
    * Signals the specified xEvent to the target object.
    *
    * @param oObject - The GameObject to signal the xEvent to.
    * @param evEvent - The xEvent to signal.
    * @sa SignalEventToDataSet()
    * @author Brenon
    */
    public void SignalEvent(GameObject oObject, xEvent evEvent, int bProcessImmediate = EngineConstants.FALSE)
    {
        //TO DO process immediate should insert in the beginning of the queue
        oObject.GetComponent<xGameObjectBase>().qEvent.Add(evEvent);
        //Debug.LogWarning("signal event " + evEvent.nType + " on object with tag " + oObject.tag);
    }

    /* @brief Signals a delayed xEvent to the specified object.
    *
    * Signals a delayed xEvent to the target object.
    *
    * @param fSeconds - The number of seconds for which to delay the event.
    * @param oObject - The GameObject to signal the xEvent to.
    * @param evEvent - The xEvent to signal.
    * @param scriptname - If specified overides the default script
    * @remarks With a negative or zero time in fSeconds, the xEvent will run on the next AI update.
    *     
    * @author MarkB
    */
    public void DelayEvent(float fSeconds, GameObject oObject, xEvent evEvent, string scriptname = "")
    {
        Debug.LogWarning("delay event");
        evEvent.delay = fSeconds;
        evEvent.scriptname = scriptname;
        SignalEvent(oObject, evEvent);
    }

    /* zDA2 @brief Builds an IndividualAbilityImpact from a SpellImpact struct and signals it
    *
    * Signals a delayed xEvent to the target object.
    *
    * @param fSeconds - The number of seconds for which to delay the event.
    * @param oObject - The GameObject to signal the xEvent to.
    * @param evEvent - The xEvent to signal.
    * @param scriptname - If specified overides the default script
    * @param nEventType - Type of event
    * @param oCaster - Caster using the ability.
    * @param oTarget - Target of the Ability.
    * @param nAbility - The Ability ID.
    *     
    * @author Carson Fee
    */
    public void ConstructAbilityImpactandDelayEvent(float fSeconds, GameObject oObject, string a_scriptname, int nEventType, GameObject oCaster, GameObject oTarget, GameObject oItem, GameObject oTarget2, float fDamage, int nAbility, int nCombatResult, int nHit, int nHand, int nDamage, Vector3 lTarget)
    {
        throw new NotImplementedException();
    }

    /* @brief Creates an xEvent of the specified type.
    *
    * Creates an xEvent of the specified type.
    *
    * @param nEventType - The type of xEvent to create.
    * @returns Returns an xEvent of the specified type, returns an invalid xEvent on error.
    * @author Brenon
    */
    public xEvent Event(int nEventType)
    {
        //Debug.LogWarning("new event of type: " + nEventType);
        return new xEvent(nEventType);
    }

    /* @brief Returns TRUE if the xEvent is valid.
    *
    * Returns TRUE if the specified xEvent is a valid
    * event.
    *
    * @param evEvent - The xEvent to test.
    * @returns Returns TRUE if the xEvent is valid, FALSE if it is not.
    * @author Brenon
    */
    public int IsEventValid(xEvent evEvent)
    {
        Debug.LogWarning("is event valid");
        return (evEvent.nType != EngineConstants.EVENT_TYPE_INVALID) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Gets the current xEvent for the xEvent script.
    *
    * Gets the current xEvent for the xEvent script that was just fired.
    * This function should only be used in the xEvent script.
    *
    * @returns Returns the current event.
    * @remarks Using this function outside of the xEvent script might result
    * in odd behaviour as the xEvent returned might be invalid.
    * @sa HandlevEvent()
    * @author Brenon
    */
    public xEvent GetCurrentEvent()
    {
        xEvent ev = null;// = Event( EngineConstants.EVENT_TYPE_INVALID);
        if (gameObject.GetComponent<xGameObjectBase>().qEvent != null &&
            gameObject.GetComponent<xGameObjectBase>().qEvent.Count > 0)
        {
            ev = gameObject.GetComponent<xGameObjectBase>().qEvent[0];
            gameObject.GetComponent<xGameObjectBase>().qEvent.RemoveAt(0);
        }

        return ev;
    }

    /* @brief Gets xEvent creator.
    *
    * Returns the creator of the specified event.
    *
    * @param evEvent - The xEvent to check.
    * @returns Returns the creator GameObject of the event, returns an invalid GameObject on error.
    * @sa SetEventCreatorRef()
    * @author MarkB
    */
    public GameObject GetEventCreatorRef(ref xEvent evEvent)
    {
        //Debug.LogWarning("get event creator: double check");
        return evEvent.oCreator;
        //return GetEventObjectRef(ref evEvent, 0);
    }

    /* @brief Sets the xEvent creator.
    *
    * Sets the creator of the specified event.
    *
    * @param evEvent - The xEvent to set the creator on.
    * @param oCreator - The GameObject to set as the creator of the event.
    * @warning Overriding the creator GameObject on existing events can have adverse effects.
    * @sa GetEventCreatorRef()
    * @author Brenon
    */
    public void SetEventCreatorRef(ref xEvent evEvent, GameObject oCreator)
    {
        //Debug.LogWarning("set event creator: double check");
        //evEvent.oCreator = oCreator;
        //return GetEventObjectRef(ref evEvent, 0);
        //SetEventObjectRef(ref evEvent, 0, oCreator);
        evEvent.oCreator = oCreator;
    }

    /* @brief Gets the type of event.
    *
    * Returns the type of the specified event.
    *
    * @param evEvent - The xEvent to check.
    * @returns Returns the type of the specified event.
    * @sa SetEventTypeRef()
    * @author MarkB
    */
    public int GetEventTypeRef(ref xEvent evEvent)
    {
        //Debug.LogWarning("get event type");
        return (evEvent != null) ? evEvent.nType : Event(EngineConstants.EVENT_TYPE_INVALID).nType;
    }

    /* @brief Sets the type of event.
    *
    * Sets the type of the specified event.
    *
    * @param evEvent - The xEvent to set the type of.
    * @param nType - The type of the event.
    * @returns Returns the modified event, returns an invalid xEvent on error.
    * @warning Overriding xEvent types on existing events can have adverse effects.
    * @sa GetEventTypeRef()
    * @author MarkB
    */
    public void SetEventTypeRef(ref xEvent evEvent, int nType)
    {
        Debug.LogWarning("set event type");
        evEvent.nType = nType;
    }

    /* @brief Gets the specified integer on the event.
    *
    * Gets the specified integer on the event.
    *
    * @param evEvent - The xEvent to get the integer off of.
    * @param nIndex - The index of the integer to get.
    * @returns Returns the specified integer, returns -1 on error.
    * @sa SetEventIntegerRef()
    * @author MarkB
    */
    public int GetEventIntegerRef(ref xEvent evEvent, int nIndex)
    {
        //Debug.LogWarning("get event integer");
        return (evEvent.nList.Count > nIndex) ? evEvent.nList.ElementAt(nIndex) : -1;
    }

    /* @brief Sets the specified integer on the event.
    *
    * Sets the specified integer on the event.
    *
    * @param evEvent - The xEvent to set the integer on.
    * @param nIndex - The index of the integer to set.
    * @param nValue - The value of the integer to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @sa GetEventIntegerRef()
    * @author MarkB
    */
    public void SetEventIntegerRef(ref xEvent evEvent, int nIndex, int nValue)
    {
        //Debug.LogWarning("set event integer");
        evEvent.nList.Insert(nIndex, nValue);
    }

    /* @brief Gets the specified float on the event.
    *
    * Gets the specified floating point number on the event.
    *
    * @param evEvent - The xEvent to get the float off of.
    * @param nIndex - The index of the float to get.
    * @returns Returns the specified float, returns -1.0f on error.
    * @sa SetEventFloatRef()
    * @author MarkB
    */
    public float GetEventFloatRef(ref xEvent evEvent, int nIndex)
    {
        Debug.LogWarning("get event float");
        return (evEvent.fList.Count > nIndex) ? evEvent.fList.ElementAt(nIndex) : -1.0f;
    }

    /* @brief Sets the specified float on the event.
    *
    * Sets the specified floating point value on the event.
    *
    * @param evEvent - The xEvent to set the float on.
    * @param nIndex - The index of the float to set.
    * @param fValue - The value of the float to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @sa GetEventFloatRef()
    * @author MarkB
    */
    public void SetEventFloatRef(ref xEvent evEvent, int nIndex, float fValue)
    {
        Debug.LogWarning("set event float");
        evEvent.fList.Insert(nIndex, fValue);
    }

    /* @brief Gets the specified GameObject on the event.
    *
    * Gets the specified GameObject on the event.
    *
    * @param evEvent - The xEvent to get the GameObject off of.
    * @param nIndex - The index of the GameObject to get.
    * @returns Returns the specified object, returns an invalid GameObject on error.
    * @sa SetEventObjectRef()
    * @author MarkB
    */
    public GameObject GetEventObjectRef(ref xEvent evEvent, int nIndex)
    {
        //Debug.LogWarning("get event object");
        return (evEvent.oList.Count > nIndex) ? evEvent.oList.ElementAt(nIndex) : null;
    }

    /* @brief Sets the specified GameObject on the event.
    *
    * Sets the specified GameObject on the event.
    *
    * @param evEvent - The xEvent to set the GameObject on.
    * @param nIndex - The index of the GameObject to set.
    * @param oValue - The value of the GameObject to set.
    * @returns Returns the modified event, returns an invalid xEvent on error.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @sa GetEventObjectRef()
    * @author MarkB
    */
    public void SetEventObjectRef(ref xEvent evEvent, int nIndex, GameObject oValue)
    {
        //Debug.LogWarning("set event object");
        evEvent.oList.Insert(nIndex, oValue);
    }

    /* @brief Gets the specified string on the event.
    *
    * Gets the specified string on the event.
    *
    * @param evEvent - The xEvent to get the string from.
    * @param nIndex - The index of the string to get.
    * @returns Returns the specified string, returns an empty string on error.
    * @sa SetEventStringRef()
    * @author MarkB
    */
    public string GetEventStringRef(ref xEvent evEvent, int nIndex)
    {
        Debug.LogWarning("get event string");
        return (evEvent.sList.Count > nIndex) ? evEvent.sList.ElementAt(nIndex) : string.Empty;
    }

    /* @brief Sets the specified string on the event.
    *
    * Sets the specified string on the event.
    *
    * @param evEvent - The xEvent to set the string on.
    * @param nIndex - The index of the string to set.
    * @param sValue - The value of the string to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @author MarkB
    */
    public void SetEventStringRef(ref xEvent evEvent, int nIndex, string sValue)
    {
        Debug.LogWarning("set event string");
        evEvent.sList.Insert(nIndex, sValue);
    }

    /* @brief Gets the specified string on the event.
    *
    * Gets the specified string on the event.
    *
    * @param evEvent - The xEvent to get the string from.
    * @param nIndex - The index of the string to get.
    * @returns Returns the specified string, returns an empty string on error.
    * @sa SetEventResourceRef()
    * @author MarkB
    */
    public string GetEventResourceRef(ref xEvent evEvent, int nIndex)
    {
        Debug.LogWarning("get event string");
        return (evEvent.rList.Count > nIndex) ? evEvent.rList.ElementAt(nIndex) : String.Empty;
    }

    /* @brief Sets the specified string on the event.
    *
    * Sets the specified string on the event.
    *
    * @param evEvent - The xEvent to set the string on.
    * @param nIndex - The index of the string to set.
    * @param sValue - The value of the string to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @author MarkB
    */
    public void SetEventResourceRef(ref xEvent evEvent, int nIndex, string rValue)
    {
        Debug.LogWarning("set event string");
        evEvent.rList.Insert(nIndex, rValue);
    }

    /* @brief Gets the specified Vector3 on the event.
    *
    * Gets the specified Vector3 on the event.
    *
    * @param evEvent - The xEvent to get the Vector3 from.
    * @param nIndex - The index of the Vector3 to get.
    * @returns Returns the specified Vector3 - returns an empty Vector3 on error.
    * @sa SetEventVectorRef()
    * @author MarkB
    */
    public Vector3 GetEventVectorRef(ref xEvent evEvent, int nIndex)
    {
        Debug.LogWarning("get event vector/location");
        return (evEvent.lList.Count > nIndex) ? evEvent.lList.ElementAt(nIndex) : new Vector3();
    }

    /* @brief Sets the specified Vector3 on the event.
    *
    * Sets the specified Vector3 on the event.
    *
    * @param evEvent - The xEvent to set the Vector3 on.
    * @param nIndex - The index of the Vector3 to set.
    * @param vValue - The value of the Vector3 to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an event, as the array of values on the xEvent expands as needed.
    * @author MarkB
    */
    public void SetEventVectorRef(ref xEvent evEvent, int nIndex, Vector3 vValue)
    {
        throw new NotImplementedException();
    }

    /* @brief Handles the event.
    *
    * Handles the specified xEvent by passing a reference to it to the specified
    * script for processing. You can access the xEvent in the script
    * by calling GetCurrentEvent() and doing all of your processing
    * there. This function is executed inline.  This Ref version is faster
    * than the basic version, because no stack copy is required.
    *
    * @param evEvent - The xEvent to handle.
    * @param rScriptName - The script that will handle the xEvent (*.ncs). If no script is specified
    *  then the object's default script will be run
    * @sa GetCurrentEvent()
    * @author Noel
    */
    public void HandleEventRef(ref xEvent evEvent, string rScriptName)//= R""
    {
        Debug.LogWarning("handle event: TO DO");
        Type t = Type.GetType(rScriptName);
        //evEvent.oCreator.AddComponent(t);
        GameObject oObject = GetEventObjectRef(ref evEvent, 0);
        oObject.AddComponent(t);
        throw new NotImplementedException();
    }

    public void HandleEvent_String(xEvent evEvent, string sName = "")
    {
        throw new NotImplementedException();
    }

    /* @brief Sets the default script for handling events.
    *
    * Sets the default script for handling events. This is the
    * script that will be fire for all xEvent types, specifically
    * for engine level events. It should be noted that this can also
    * be called on Player Characters - though to get players firing events
    * properly, the specified events must first be enabled using EnablevEvent().
    *
    * @param oObject - The GameObject to set the xEvent script on.
    * @param rScriptName - The name of the script to use for default xEvent handling (*.ncs)
    * @sa EnablevEvent()
    * @author Brenon
    */
    public void SetEventScript(GameObject oObject, string rScriptName)//string
    {
        //TO DO: in Unity terms, add a script component on the object, check to see if addition versus replacement
        Debug.LogWarning("set event script: TO DO double check");
        Type t = Type.GetType(rScriptName);
        oObject.AddComponent(t);
    }

    /* @brief Enables or disables the specified xEvent for the target object.
    *
    * Enables or disables the specified xEvent for the target object. It should
    * be noted that this function can be used on player characters to enable
    * events that by default do not fire for players. By default, events fire
    * all the time for NPC's and not at all for PC's.
    *
    * @param oObject - The GameObject to enable/disable the xEvent type for.
    * @param bEnable - Enable or disable the xEvent type.
    * @param nEventType - The type of xEvent to enable/disable.
    * @sa SetEventScript()
    * @author Brenon
    */
    public void EnablevEvent(GameObject oObject, int bEnable, int nEventType)
    {
        throw new NotImplementedException();
    }

    /* @brief Registers an GameObject to listen to the specified xEvent type on the target object.
    *
    * Registers an GameObject to listen to the specified xEvent type on the target object.
    * When an GameObject is registered as a listener for an xEvent type, whenever the target
    * GameObject receives an xEvent of that type a listener xEvent will be fired to the
    * listening GameObject with the xEvent data that the listening GameObject can then process.
    *
    * @param oTarget - The GameObject to listen to.
    * @param oListener - The GameObject to register as a listener.
    * @param nEventType - The type of xEvent to listen for.
    * @remarks It is not possible to listen for other listener events, or for xEvent types
    * other than the ones defined in the scripting language.
    * @sa UnregisterEventLister(), GetEventTargetRef(ref)
    * @author Brenon
    */
    public void RegisterEventListener(GameObject oTarget, GameObject oListener, int nEventType)
    {
        throw new NotImplementedException();
    }

    /* @brief Unregisters an GameObject from the listener list.
    *
    * Unregisters an GameObject from the listener list. for the specific xEvent type on the
    * target object. It should be noted that objects will be removed automatically if the
    * listening GameObject does not exist.
    *
    * @param oTarget - The GameObject to unregister from.
    * @param oListener - The GameObject to unregister as a listener.
    * @param nEventType - The xEvent type to unregister for.
    * @sa RegisterEventListener(), GetEventTargetRef(ref)
    * @author Brenon
    */
    public void UnregisterEventListener(GameObject oTarget, GameObject oListener, int nEventType)
    {
        throw new NotImplementedException();
    }

    /* @brief Returns the target of a specific event.
    *
    * Returns the intended target GameObject of a specific event. It should be noted
    * that this function should only be used with listener events as in other
    * cases the target may not be specified correctly.
    *
    * @param evEvent - The xEvent to get the target from.
    * @returns Returns the target of the specified event, returns an invalid GameObject on error.
    * @author Noel
    */
    public GameObject GetEventTargetRef(ref xEvent evEvent)
    {
        //Debug.LogWarning("get event target: double check");
        //return evEvent.oTarget;
        return GetEventObjectRef(ref evEvent, 0);//Creator is separate, so target is zero?
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Objects
    /***************************************************************/
    /* @addtogroup objects Object Functions
    *
    * Functions to manage objects (create, destroy, copy and retreive data from them)
    */
    /* @{*/

    /* @brief Determines if an GameObject exists and is valid.
    *
    * Returns TRUE if the GameObject oObject exists and is a valid object, FALSE otherwise.
    *
    * @param oObject - The GameObject which may be valid.
    * @returns Returns TRUE if the GameObject exists and is valid, FALSE otherwise.
    * @remarks This function is very similiar to checking whether an GameObject is not equal to
    * the OBJECT_INVALID constant, but this is a more robust check. An GameObject that has been
    * passed as a paramater in a delayed function call or has been stored as a local object
    * will not equal the OBJECT_INVALID constant if the GameObject has been destroyed since it was
    * stored or passed. This function however will return FALSE in those circumstances. There
    * is no instance where an GameObject is equal to the OBJECT_INVALID constant and this function
    * will return TRUE.
    * @author Brenon
    */
    public int IsObjectValid(GameObject oObject)
    {
        //Debug.LogWarning("is object valid");
        //Type t = oObject.GetType();
        //return (GameObject.FindObjectOfType(t) != null) ? EngineConstants.TRUE : EngineConstants.FALSE;
        return (oObject.tag != "Invalid") ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Returns an object's type.
    *
    * Returns the GameObject type of the GameObject oObject. Object types are constants (EngineConstants.OBJECT_TYPE_*). Returns EngineConstants.OBJECT_TYPE_INVALID on failure.
    *
    * @param oObject - the GameObject to get the type of
    * @returns One of the GameObject type constants (EngineConstants.OBJECT_TYPE_*) or EngineConstants.OBJECT_TYPE_INVALID on failure.
    * @author Brenon
    */
    public int GetObjectType(GameObject oObject)
    {
        //Debug.LogWarning("get object type");
        return oObject.GetComponent<xGameObjectBase>().nObjectType;
    }

    /* @brief Returns the resref of the specified object
    *
    * Returns the resref of the specified object
    *
    * @param oObject - the GameObject to get the resref off of
    * @returns the resref of the GameObject or an empty string on failure.
    * @author Brenon
    */
    public string GetResRef(GameObject oObject)
    {
        //may be used to reset object from a template?
        /*Debug.LogWarning("get string reference string: TO DO");
        return GetLocal(oObject, "ResRef");*/
        throw new NotImplementedException();
    }

    /* @brief Returns the strref of the given object's name
    *
    * Returns the strref of the given object's name
    *
    * @param oObject - the GameObject whose name we want the strref of
    * @returns the strref of the given object's name
    * @author Pauls
    */
    public int GetNameStrref(GameObject oObject)
    {
        Debug.LogWarning("name string reference: TO DO");
        return -1;
    }

    /* @brief Returns the tag of the specified object
    *
    * Returns the tag of the specified object
    *
    * @param oObject - the GameObject to get the tag off of
    * @returns the tag of the GameObject or an empty string on failure.
    * @author Brenon
    */
    public string GetTag(GameObject oObject)
    {
        //Debug.LogWarning("get tag: " + oObject.name);//Name is unique where as tag is not
        //return oObject.name;
        return (oObject != null) ? oObject.name : GameObject.FindGameObjectWithTag("Invalid").tag;
    }

    /* @brief Sets the tag of the GameObject to the specified value.
    *
    * Sets the tag of the GameObject to the specified value.
    *
    * @param oObject - the GameObject to set the tag on
    * @param sTag - the tag to set the object
    * @author Brenon
    */
    public void SetTag(GameObject oObject, string sTag)
    {
        //Debug.LogWarning("set tag: " + sTag);
        //oObject.tag = sTag;
        throw new NotImplementedException();
    }

    /* @brief Returns the nth specified GameObject with the appropriate tag
    *
    * Returns the nth specified GameObject with the appropriate tag
    *
    * @param sTag - The tag reference to get the GameObject by
    * @param nNth - If there are multiple objects with the same tag, get the 'nth' specified object.
    * @returns the desired GameObject or OBJECT_INVALID on failure
    * @author Brenon
    */
    public GameObject GetObjectByTag(string sTag, int nNth = 0)
    {
        //Debug.LogWarning("get object by tag");//By name instead of by tag
        //return (GameObject.FindGameObjectsWithTag(sTag).Length > 0 && GameObject.FindGameObjectsWithTag(sTag).Length > nNth) ? GameObject.FindGameObjectsWithTag(sTag)[nNth] : null;
        return (GameObject.Find(sTag) != null) ? GameObject.Find(sTag) : GameObject.FindGameObjectWithTag("Invalid");
    }

    /* @brief Create a pool of creatures while loading an area. 
    * It should be used under EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT. 
    * When creatures die or are set inactive they will automatically return to the pool.
    * @param rTemplate - The template to use
    * @param nPoolSize - Number of creatures of this type to create
    * @returns TRUE if successful
    * @sa CreateObject, SetObjectActive
    * @author Jose
    */
    public int CreatePool(string rTemplate, int nPoolSize)
    {
        throw new NotImplementedException();
    }

    /* @brief Create an GameObject in the specified location. Will attempt to use a corresponding pool if one exists.
    *
    * @param nObjectType - The GameObject type (placeable, creature, etc)
    * @param rTemplate - The template to use
    * @param lLoc - Location of the object
    * @param sOverrideScript - Script assigned to the object. If empty, the engine will use the template script
    * @param bSpawnActive - Whether or not this GameObject is enabled at spawn time
    * @param bNoPermDeath - Set to TRUE to avoid destroying the GameObject permanently (only valid for pool creatures)
    * @param oAppearanceCopy - Set to the GameObject which you want to copy appearance data from (*** ONLY USE THIS WITH APPROVAL FROM PROGRAMMING FOR SPECIFIC HITCHING ISSUES ***)
    * @returns The new object, OBJECT_INVALID on failure
    * @sa CreatePool, DestroyObject
    * @author Jose
    */
    public GameObject CreateObject(int nObjectType, string rTemplate, Vector3 lLoc, string sOverrideScript = "", int bSpawnActive = EngineConstants.TRUE, int bNoPermDeath = EngineConstants.FALSE)
    {
        //Debug.LogWarning("create object: TO DO");
        if (sOverrideScript != "")
        {
            throw new NotImplementedException();
        }

        GameObject oObject = null;

        switch (nObjectType)
        {
            case EngineConstants.OBJECT_TYPE_AREA:
                {
                    //Create an empty object
                    oObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/areaPrefab"));
                    //oObject.tag = "Area";
                    oObject.name = rTemplate;
                    //Start adding stuff on the new object
                    oObject.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_AREA;
                    //oObject.AddComponent<xGameObjectARE>();
                    if (bSpawnActive != EngineConstants.TRUE)
                    {
                        oObject.SetActive(false);
                    }

                    //Get its template XML, Convert name to file ID
                    string id = GetResource("Name", rTemplate, "ID", "are");

                    //Unzip XML, Generate seed for unique filename
                    string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
                    increment++;

                    Unzip(id, seed);

                    string f = EngineConstants.SOURCE + id + seed + ".xml";

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(f);
                    XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
                    XElement root = xDoc.Root;
                    XElement agent = root.Element("Agent");

                    //update all the variables That are Not list
                    var e = agent.Elements();
                    foreach (var _x in e)
                    {
                        string _d = _x.Name.ToString();
                        if (_d.IndexOf("List") == -1)//If not list
                        {
                            string _v = _x.Value;
                            if (_v != "")
                            {
                                UpdateGameObjectProperty(oObject, _d, _v);
                            }
                        }
                    }

                    /*//Set basic details
                    oObject.gameObject.GetComponent<xGameObjectBase>().ResRefID = int.Parse(id);
                    oObject.gameObject.GetComponent<xGameObjectBase>().XResRefName = rTemplate;
                    oObject.gameObject.GetComponent<xGameObjectBase>().ResType = GetResourceType(f);

                    //Load the identified XML template for parsing
                    //XmlNode node = doc.SelectSingleNode("//Resource/Agent/ResRefName/text()");
                    //string rrn = xDoc.Descendants("ResRefName").First().Value;
                    //string rrn = agent.Element("ResRefName").Value;*/

                    break;
                }
            case EngineConstants.OBJECT_TYPE_WAYPOINT:
                {
                    oObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/waypointPrefab"));
                    oObject.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_WAYPOINT;
                    //oObject.gameObject.AddComponent<xGameObjectUTW>();
                    //oObject.tag = "Waypoint";
                    oObject.name = rTemplate;
                    if (bSpawnActive != EngineConstants.TRUE)
                    {
                        oObject.SetActive(false);
                    }

                    //For debug visual purposes add 3-D rectangle
                    GameObject oCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    oCube.GetComponent<Renderer>().material.color = Color.yellow;
                    oCube.transform.parent = oObject.transform;
                    break;
                }
            case EngineConstants.OBJECT_TYPE_CREATURE:
                {
                    oObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/creaturePrefab"));
                    oObject.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_CREATURE;
                    //oObject.gameObject.AddComponent<xGameObjectUTC>();
                    //oObject.tag = "Creature";
                    oObject.name = rTemplate;
                    if (bSpawnActive != EngineConstants.TRUE)
                    {
                        oObject.SetActive(false);
                    }
                    ParseCreature(oObject, rTemplate);
                    break;
                }
            case EngineConstants.OBJECT_TYPE_PLACEABLE:
                {
                    oObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/placeablePrefab"));
                    oObject.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_PLACEABLE;
                    //oObject.gameObject.AddComponent<xGameObjectUTP>();
                    //oObject.tag = "Placeable";
                    oObject.name = rTemplate;
                    if (bSpawnActive != EngineConstants.TRUE)
                    {
                        oObject.SetActive(false);
                    }
                    ParsePlaceable(oObject, rTemplate);
                    break;
                }
            default: throw new NotImplementedException();
        }

        return oObject;
    }

    /* @brief Destroy the specified object. !!GEORG SAYS: DO NOT USE. EVER. Use core_h.Safe_Destroy_Object instead!!
    *
    * @param oObject - Object to destroy
    * @param nDelay - Time in milliseconds to delay the destruction of the object. Immediate destruction by default
    * @sa CreateObject
    * @author Jose
    */
    public void DestroyObject(GameObject oObject, int nDelay = 0)
    {
        //do not call this function directly, instead use core_h.Safe_Destroy_Object
        //the reason is that the core function checks for safety, such as plot object, player, etc.
        //Debug.LogWarning("destroy object");
        GameObject.Destroy(oObject, Convert.ToSingle(nDelay));
    }

    /* @brief Creates an item at a specific location
    *
    * Creates an item using the specified file name at lLocation. If the GameObject is a creature
      then it will also attempt to use an appear animation if the flag is set. If an invalid GameObject type
      is specified, the file name does not exist or the Vector3 is bad an invalid GameObject will be returned.

    * NOTE: As with all commands that add items to a container, this xCommand will reset the container (if a placeable)
    *       to interactive not matter it's previous state.
    *
    * @param rItemFileName - The file name of the GameObject to create (*.uti)
    * @param oTarget - The item will be created inside the invetory of this object
    * @param nStackSize - Stack size of the item to be created
    * @param sTag - Optional tag for the new item
    * @param bSuppressNotification - if true, the "Item Acquired" notification will not be displayed
    * @param bDroppable - if true, the item can be looted when the target creature dies
    * @returns a valid new GameObject or OBJECT_INVALID on error
    * @author Brenon
    */
    public GameObject CreateItemOnObject(string rItemFileName, GameObject oTarget, int nStackSize = 1, string sTag = "", int bSuppressNotification = EngineConstants.FALSE, int bDroppable = EngineConstants.TRUE)
    {
        Debug.LogWarning("create item on object: TO DO");//instantiate item,tag item etc.
        Type t = Type.GetType(rItemFileName);
        oTarget.AddComponent(t);
        return oTarget;
    }

    /* @brief Enables or disables an GameObject in the engine. Creatures that belong to a pool will automatically return to it when set inactive.
    *
    * Each game engine GameObject has a boolean enabled or disabled flag in the engine.
      In a "disabled" state, the GameObject will not appear on the client visually, and
      will receive no AI updates. GameEffects will remain on the GameObject but all commands
      will be cleared upon disabling. 
    *
    * @param oObject - The GameObject to set status on 
    * @param nActive - Non-zero is active, zero is inactive
    * @param nAnimation - The ID of an appear/disappear animation to play
    * @param nVFX - The ID of a VFX to add while the animation is playing
    * @param nNextLine - queues up the xCommand to run as soon as the next conversation line begins
    * @author Derek Beland
    */
    public void SetObjectActive(GameObject oObject, int nActive, int nAnimation = 0, int nVFX = 0)
    {
        Debug.LogWarning("set object active: TO DO");
        oObject.SetActive(Convert.ToBoolean(nActive));
    }

    /* @brief Queries the active status of an object
    *
    * Each game engine GameObject has a boolean enabled or disabled flag in the engine. 
      In a "disabled" state, the GameObject will not appear on the client visually, and
      will receive no AI updates. Messages will continue to queue up so you are
      able to assign actions before enabling the object. 
    *
    * @param oObject - The GameObject query status on 
    * @returns non-zero if active, zero if inactive
    * @author Derek Beland
    */
    public int GetObjectActive(GameObject oObject)
    {
        Debug.LogWarning("get object active");
        return Convert.ToInt32(oObject.activeInHierarchy);
    }

    /* @brief Sets an GameObject as always being visible.
    *
    * The engine normally culls out objects that are too far or can't be seen,
      this can override this behavior to always show the GameObject as visible.

      WARNING: limit the use this function for performance reasons.
    *
    * @param oObject - The GameObject to set the visibilty for.
    * @param bAlwaysVisible - The state to set.
    * @author Jacques Lebrun
    */
    public void SetObjectAlwaysVisible(GameObject oObject, int bAlwaysVisible)
    {
        throw new NotImplementedException();
    }

    public int IsDestroyable(GameObject oObject)
    {
        Debug.LogWarning("is destroyed able");
        return oObject.GetComponent<xGameObjectBase>().bDestroyable;
    }

    public void SetDestroyable(GameObject oObject, int bDestroyable)
    {
        Debug.LogWarning("set destroy able");
        oObject.GetComponent<xGameObjectBase>().bDestroyable = bDestroyable;
    }

    public int IsPlot(GameObject oObject)
    {
        Debug.LogWarning("is plot");
        return oObject.GetComponent<xGameObjectBase>().bPlot;
    }

    public void SetPlot(GameObject oObject, int bPlot)
    {
        Debug.LogWarning("set plot");
        oObject.GetComponent<xGameObjectBase>().bPlot = bPlot;
    }

    public int IsImmortal(GameObject oObject)
    {
        Debug.LogWarning("is immortal");
        return oObject.GetComponent<xGameObjectBase>().bImmortal;
    }

    /* @brief Sets an GameObject to be unable to drop below 1 health
    *
    * NOTE: This is the engine function, you should not use it. Use core_h.SetImmortal intead
    *
    * @param oObject - The GameObject to set the visibilty for.
    * @param bImmortal - The state to set.
    * @author Georg
    */
    public void Engine_SetImmortal(GameObject oObject, int bImmortal)
    {
        Debug.LogWarning("engine_setimmortal");
        oObject.GetComponent<xGameObjectBase>().bImmortal = bImmortal;
    }

    public int GetAILevel(GameObject oObject)
    {
        Debug.LogWarning("get ai level");
        return FloatToInt(GetCreatureProperty(oObject, EngineConstants.PROPERTY_SIMPLE_LEVEL, EngineConstants.PROPERTY_VALUE_BASE));
    }

    public void SetAILevel(GameObject oObject, int nAILevel)
    {
        Debug.LogWarning("set AI level");
        SetCreatureProperty(oObject, EngineConstants.PROPERTY_SIMPLE_LEVEL, IntToFloat(nAILevel), EngineConstants.PROPERTY_VALUE_BASE);
    }

    /* @brief 
    *
    *Returns an GameObject reference for the area oObject is in.
    *
    * @param oObject - Target object
    * @author Brenon Holmes
    */
    public GameObject GetArea(GameObject oObject)
    {
        //Debug.LogWarning("get area");
        GameObject oArea;
        GameObject[] oAreaList = GameObject.FindGameObjectsWithTag("Area");
        if (oAreaList.Length != 1)//Either no area was found or more than one
        {
            throw new NotImplementedException();
        }
        //else oArea = GameObject.FindGameObjectWithTag("Area");
        else oArea = oAreaList[0];
        return oArea;
    }

    public GameObject GetModule()
    {
        return xGameObjectMOD.instance.gameObject;
    }

    // int IsItemIdentified(GameObject oItem, int nProperty) 

    // void SetItemIdentified(GameObject oItem, int nProperty, int bIdentified) 

    // int GetIdentifyLoreDifficulty() 

    // void SetIdentifyLoreDifficulty() 

    // void RestoreObject() 

    // int GetCreatureStealthMode() 

    // int IsCreatureSkillSuccessful() 

    /* @brief checks if the specified GameObject is currently conjuring a spell
    *
    * @param oObject The GameObject to check the conjure state
    * @returns TRUE if the GameObject is conjuring
    * @author Jose
    */
    public int IsConjuring(GameObject oObject)
    {
        Debug.LogWarning("is conjuring");
        return oObject.GetComponent<xGameObjectBase>().bConjuring;
    }

    /* @brief set how long to hold aim for the next projectile to be fired (control speed for archers & ranged combat in general)
    *
    * @param oObject The GameObject to set the duration for
    * @param fDuration Duration of the aim loop (in seconds)
    * @author Jose
    */
    public void SetAimLoopDuration(GameObject oObject, float fDuration)
    {
        //please see struct CombatAttackResultStruct in combat_h, this is for ranged weapons
        Debug.LogWarning("set aim loop duration: TO DO");
    }

    /* @brief Specifies the length that attacks will take
    *
    * @param oObject The GameObject to set the duration for
    * @param fDuration_s Duration of the attack (in seconds)
    * @author Gabo
    */
    public void SetAttackDuration(GameObject oObject, float fDuration_s)
    {
        //please see struct CombatAttackResultStruct in combat_h, this is for melee weapons
        Debug.LogWarning("set attack duration: TO DO");
    }

    /* @brief return an array of objects enclosed by the specified shape
    *
    *  Returns all objects enclosed by a specific shape. 
    * 
    *  Parameter Info for generic parameters fA, fB, fC:
    *
    *  SPHERE:    fA = radius (meters)
    *  CONE:      fA = radius (meters), fB = angle (degrees)
    *  RECTANGLE  fA = length, fB = width
    *
    * @param nObjectTypeMask Which objects to look for
    * @param nShapeId the shape to use
    * @param lLocation origin and direction for this shape
    * @param fA floating point generic parameter
    * @param fB optional floating point generic parameter
    * @param fC optional floating point generic parameter
    * @param bExcludeDead filter out dead objects if necessary
    * @author Jose
    */
    public List<GameObject> GetObjectsInShape(int nObjectTypeMask, int nShapeId, Vector3 lLocation, float fA, float fB = 0.0f, float fC = 0.0f, int bExcludeDead = EngineConstants.FALSE)
    {
        throw new NotImplementedException();
    }

    /* @brief filter an array of objects enclosed by the specified shape
    *
    *  Filter an array of objects enclosed by the specified shape
    *
    *  Parameter Info for generic parameters fA, fB, fC:
    *
    *  SPHERE:    fA = radius (meters)
    *  CONE:      fA = angle (degrees), fB = length (meters)
    *  CYLINDER:  fA = radius (meters), fB = length (meters)
    *
    * @param aObjects array of objects to filter based on the shape
    * @param nObjectTypeMask Which objects to look for
    * @param nShapeId the shape to use
    * @param lLocation origin and direction for this shape
    * @param fA floating point generic parameter
    * @param fB optional floating point generic parameter
    * @param fC optional floating point generic parameter
    * @param bExcludeDead filter out dead objects if necessary
    * @sa GetObjectsInShape
    * @author Jose
    */
    public List<GameObject> FilterObjectsInShape(List<GameObject> aObjects, int nObjectTypeMask, int nShapeId, Vector3 lLocation, float fA, float fB = 0.0f, float fC = 0.0f, int bExcludeDead = EngineConstants.FALSE)
    {
        throw new NotImplementedException();
    }

    /* @brief return an array of creatures contained in the melee ring for the given angles
    *
    * @param nRingOwnerId id of the creature for which to query the melee ring
    * @param fStartAngle the angle where the search should start (supported ranges 0->360 or -180->180)
    * @param fEndAngle angle where the search should stop (fEndAngle >= fStartAngle)
    * @param bOnlyHostiles set to TRUE if the array should only contain hostile creatures
    * @param nRingIndex the ring to query. By default the main ring will respond to the query (ring index 0)
    * @author Jose
    */
    public List<GameObject> GetCreaturesInMeleeRing(GameObject nRingOwnerId, float fStartAngle, float fEndAngle, int bOnlyHostiles = EngineConstants.FALSE, int nRingIndex = 0)
    {
        throw new NotImplementedException();
    }

    /* @brief return the position of a melee ring
    *
    * @param nRingOwnerId id of the creature for which to query the melee ring
    * @param nRingIndex the ring to query. By default the main ring will respond to the query (ring index 0)
    * @author Jose
    */
    public Vector3 GetMeleeRingPosition(GameObject nRingOwnerId, int nRingIndex = 0)
    {
        throw new NotImplementedException();
    }

    /* @brief play an animation on top of the current base animation
    *
    * @param nObjectId id of the GameObject in which to play the animation
    * @param nAnimation animation id (must be an additive or override animation)
    * @param bForceRestart play the animation again from the start even if it's already playing
    * @author Jose
    */
    public void PlayAdditiveAnimation(GameObject nObjectId, int nAnimation, int bForceRestart = EngineConstants.FALSE)
    {
        throw new NotImplementedException();
    }

    /* @brief stops an additive animation playing on top of the current base animation
    *
    * @param nObjectId id of the GameObject in which to stop the animation
    * @param nAnimation animation id (must be an additive or override animation)
    * @author Noel
    */
    public void StopAdditiveAnimation(GameObject nObjectId, int nAnimation)
    {
        throw new NotImplementedException();
    }

    /* @brief Fire a projectile from a specified source point to a target position
    *
    * @param nType the projectile type (see the PRJ 2da)
    * @param vSourcePosition start position for the projectile
    * @param vTargetPosition end position to hit
    * @param nCrustEffectId crust vfx to apply to the projectile
    * @param bWideAngle enable this to make the projectile follow a longer trajectory
    * @param oEventTarget GameObject to send the impact xEvent to
    * @returns projectile id
    * @sa FireHomingProjectile, SetProjectileImpactEvent
    * @author Jose
    */
    public GameObject FireProjectile(int nType, Vector3 vSourcePosition, Vector3 vTargetPosition, int nCrustEffectId = 0, int bWideAngle = EngineConstants.FALSE, GameObject oEventTarget = null)
    {
        throw new NotImplementedException();
    }

    /* @brief Fire a tracking projectile from a specified source point to a target object
    *
    * @param nType the projectile type (see the PRJ 2da)
    * @param vSourcePosition start position for the projectile
    * @param oTarget GameObject to follow and hit
    * @param nCrustEffectId crust vfx to apply to the projectile
    * @param oEventTarget GameObject to send the impact xEvent to
    * @returns projectile id
    * @sa FireProjectile, SetProjectileImpactEvent
    * @author Jose
    */
    public GameObject FireHomingProjectile(int nType, Vector3 vSourcePosition, GameObject oTarget, int nCrustEffectId = 0, GameObject oEventTarget = null)
    {
        throw new NotImplementedException();
    }

    /* @brief Replace the default xEvent with a custom xEvent to be reported on projectile impact
    *
    * @param oProjectile the projectile in which to replace the impact event
    * @param eCustom the xEvent to report on impact
    * @sa FireProjectile, FireHomingProjectile
    * @author Jose
    */
    public void SetProjectileImpactEvent(GameObject oProjectile, xEvent eCustom)
    {
        throw new NotImplementedException();
    }

    /* @brief Get the projectile associated with the given item
    *
    * @param oItem the item
    * @returns projectile type
    */
    public int GetProjectileFromItem(GameObject oItem)
    {
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Threat System
    /***************************************************************/
    /* @addtogroup threatsystem Threat System Functions
    *
    * Interface to the Threat System
    */
    /* @{*/

    /* @brief Change the threat value for a given enemy
    *
    * @param oCreature - owner of the threat table
    * @param oEnemy - enemy for which the value should be updated
    * @param fThreatChange - threat delta
    * @author Jose
    */
    public void UpdateThreatTable(GameObject oCreature, GameObject oEnemy, float fThreatChange)
    {
        Debug.Log("update threat table");
        xThreat _threat = oCreature.GetComponent<xGameObjectUTC>().oThreats
                  .Find(threat => threat.oTarget = oEnemy);
        if (_threat != null)
        {
            _threat.fThreat = fThreatChange;
        }
    }

    /* @brief Remove the threat entry for the given enemy
    *
    * @param oCreature - owner of the threat table
    * @param oEnemy - enemy for which the entry should be removed
    * @author Jose
    */
    public void ClearEnemyThreat(GameObject oCreature, GameObject oEnemy)
    {
        Debug.Log("clear enemy threat");
        oCreature.GetComponent<xGameObjectUTC>().oThreats
             .Remove(oCreature.GetComponent<xGameObjectUTC>().oThreats
                  .Find(threat => threat.oTarget = oEnemy));
    }

    /* @brief Clear all threat entries
    *
    * @param oCreature - owner of the threat table
    * @param bRemoveAllEntries - true = delete all entries from table; false = reset all entries to their minimum values but don't remove any of them
    * @author Jose
    */
    public void ClearThreatTable(GameObject oCreature, int bRemoveAllEntries = EngineConstants.TRUE)
    {
        Debug.Log("clear threat table");
        if (bRemoveAllEntries != EngineConstants.FALSE)
        {
            oCreature.GetComponent<xGameObjectUTC>().oThreats = new List<xThreat>();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    /* @brief Get the number of entries in the threat table
    *
    * @param oCreature - owner of the threat table
    * @returns table size
    * @author Jose
    */
    public int GetThreatTableSize(GameObject oCreature)
    {
        Debug.Log("get threat table size");
        return oCreature.GetComponent<xGameObjectUTC>().oThreats.Count;
    }

    /* @brief Get the enemy id on a specific index of the threat table
    *
    * @param oCreature - owner of the threat table
    * @param i - index
    * @returns enemy id
    * @author Jose
    */
    public GameObject GetThreatEnemy(GameObject oCreature, int i)
    {
        Debug.Log("get threat enemy, by index");
        return oCreature.GetComponent<xGameObjectUTC>().oThreats.ElementAt(i).oTarget;
    }

    /* @brief Get the threat value on a specific index of the table
    *
    * @param oCreature - owner of the threat table
    * @param i - index
    * @returns threat value
    * @author Jose
    */
    public float GetThreatValueByIndex(GameObject oCreature, int i)
    {
        Debug.Log("get threat value, by index");
        return oCreature.GetComponent<xGameObjectUTC>().oThreats.ElementAt(i).fThreat;
    }

    /* @brief Get the threat value for a specific enemy on the table
    *
    * @param oCreature - owner of the threat table
    * @param oEnemy - enemy id
    * @returns threat value
    * @author Jose
    */
    public float GetThreatValueByObjectID(GameObject oCreature, GameObject oEnemy)
    {
        Debug.Log("get threat the value by object ID");
        xThreat _threat = oCreature.GetComponent<xGameObjectUTC>().oThreats
                  .Find(threat => threat.oTarget = oEnemy);
        if (_threat != null)
        {
            return _threat.fThreat;
        }
        return 0.0f;
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Stats
    /***************************************************************/
    /* @addtogroup stats Stats Functions
    *
    * Functions to manage game stats (health, abilities etc')
    */
    /* @{*/

    /* @brief Get object's current health
    *
    * Get object's current health
    *
    * @param oObject - the GameObject that we are checking
    * @returns Returns the health of the object. Returns -1 if the GameObject isn't valid
    * @author Noel
    */
    public int GetHealth(GameObject oObject)
    {
        Debug.Log("get health");
        return FloatToInt(GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_CURRENT));
    }

    /* @brief Set a placeable's current health
    *
    * Set object's current health.
    * Bizarre things might happen if you set their health below zero without applying a death effect
    *
    * @param oPlc   - the placeable GameObject that we are modifying
    * @param nHealth - the new health value for the object
    * @author Noel
    */
    public void SetPlaceableHealth(GameObject oPlc, int nHealth)
    {
        Debug.Log("set placeable health");
        SetMaxHealth(oPlc, nHealth);
    }

    /* @brief Get object's maximum health
    *
    * Get object's maximum health
    *
    * @param oObject - the GameObject that we are checking
    * @returns Returns the maximum health of the object. Returns -1 if the GameObject isn't valid
    * @author Noel
    */
    public int Engine_GetMaxHealth(GameObject oObject)//renamed to Engine_ from Deprecated_
    {
        Debug.Log("get Max health: deprecated in the original script.ldf DA2?!?");
        return FloatToInt(GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_BASE));
    }

    /* @brief Set object's maximum health
    *
    * Set object's maximum health.
    *
    * @param oObject - the GameObject that we are modifying
    * @param nHealth - the new maximum health value for the object
    * @author Noel
    */
    public void SetMaxHealth(GameObject oObject, int nHealth)
    {
        Debug.Log("set max health");
        SetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, IntToFloat(nHealth), EngineConstants.PROPERTY_VALUE_BASE);
    }

    /* @brief Is this GameObject dead?
    *
    * Is this GameObject dead?
    *
    * @param oObject - the GameObject that we are checking
    * @returns Returns TRUE if the object's dead flag is set to true or its health is 0 or less, otherwise FALSE
    * @author Noel
    */
    public int IsDead(GameObject oObject = null)
    {
        if (oObject == null) oObject = gameObject;//gameObject
        return (oObject.GetComponent<xGameObjectBase>().bDead != EngineConstants.FALSE &&
             GetCreatureProperty(oObject, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, EngineConstants.PROPERTY_VALUE_CURRENT) <= 0)
             ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Is this GameObject dying or dead?
    *
    * If a creature has a death xEffect then it has already started dying or is already dead.
    * This can be used to know if a creature has already started a deathblow animation but its
    * hit points have not been updated.
    *
    * @param oObject - the creature that we are checking
    * @returns Returns TRUE if the GameObject has a death effect
    * @author Gabo
    */
    public int HasDeathEffect(GameObject oObject = null, int bCheckForDeathEvent = EngineConstants.FALSE)
    {
        if (oObject == null) oObject = gameObject;//gameObject
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the object's dead flag
    *
    *
    * @param oObject - the GameObject who's death we are defining
    * @author Gabo.
    */
    public void SetDead(GameObject oObject, int bDeadFlagValue)
    {
        Debug.Log("set dead");
        oObject.GetComponent<xGameObjectBase>().bDead = bDeadFlagValue;
    }

    /* @brief Get the specified attribute from oCreature
    *
    * Get oCreature's Attribute/Property value in an int form.
    * See GetCreatureProperty for more details
    *
    * @param oCreature - the creature whose stat is being requested
    * @param nAttribute - the property (stat) we want to know about
    * @param nValueType - the type of value of the property we want to know about (TOTAL, BASE, CURRENT, MODIFIER)
    * @returns Returns oCreature's Attribute Value
    * @author Gabo
    */
    public int GetCreatureAttribute(GameObject oCreature, int nAttribute, int nValueType = EngineConstants.PROPERTY_VALUE_TOTAL)
    {
        Debug.Log("get creature attribute");
        return FloatToInt(GetCreatureProperty(oCreature, nAttribute, nValueType));
    }

    /* @brief Get the specified attribute from oCreature
    *
    * Get oCreature's Property value.
    * Property types have slightly different definitions of each of the 4 value types.
    * A SIMPLE or DERIVED property has a BASE and TOTAL value as the same number and 
    * doesnt have CURRENT or MODIFIER.
    * An EngineConstants.ATTRIBUTE property has a BASE value and a MODIFIER value. Its TOTAL value 
    * is the clamped (between its min and max) sum of the BASE plues the MODIFIER.
    * A DEPLETABLE property is similar to an EngineConstants.ATTRIBUTE property but in addition has
    * a CURRENT value, which must be between the property min and the TOTAL value.
    * Properties are defined in Properties.xls.
    *
    * @param oCreature - the creature whose stat is being requested
    * @param nProperty - the property (stat) we want to know about
    * @param nValueType - the type of value of the property we want to know about (TOTAL, BASE, CURRENT, MODIFIER)
    * @returns Returns oCreature's Attribute Value
    * @author Gabo
    */
    public float GetCreatureProperty(GameObject oCreature, int nProperty, int nValueType = EngineConstants.PROPERTY_VALUE_TOTAL)
    {
        //Debug.LogWarning("get creature property: TO DO");
        xProperty _property = oCreature.GetComponent<xGameObjectUTC>().oProperties
             .Find(_p => _p.nID == nProperty);
        if (_property != null)
        {
            int nType = _property.nType;

            switch (nValueType)
            {
                case EngineConstants.PROPERTY_VALUE_BASE:
                    {
                        switch (nType)
                        {
                            case EngineConstants.PROPERTY_TYPE_SIMPLE:
                            case EngineConstants.PROPERTY_TYPE_DERIVED:
                            case EngineConstants.PROPERTY_TYPE_ATTRIBUTE:
                            case EngineConstants.PROPERTY_TYPE_DEPLETABLE:
                                {
                                    return _property.fValueTypeBase;
                                }
                            case EngineConstants.PROPERTY_TYPE_INVALID:
                            default:
                                {
                                    //Invalid or other type, WTF?
                                    throw new NotImplementedException();
                                }
                        }
                    }
                case EngineConstants.PROPERTY_VALUE_TOTAL:
                    {
                        switch (nType)
                        {
                            case EngineConstants.PROPERTY_TYPE_SIMPLE:
                            case EngineConstants.PROPERTY_TYPE_DERIVED:
                                {
                                    //Base and total should be equal
                                    if (_property.fValueTypeBase != _property.fValueTypeTotal)
                                    {
                                        throw new NotImplementedException();
                                    }
                                    return _property.fValueTypeTotal;//Assuming no need to clamp
                                }
                            case EngineConstants.PROPERTY_TYPE_ATTRIBUTE:
                            case EngineConstants.PROPERTY_TYPE_DEPLETABLE:
                                {
                                    //Total should be base plus modifier
                                    if (_property.fValueTypeTotal != _property.fValueTypeBase + _property.fValueTypeModifier)
                                    {
                                        throw new NotImplementedException();
                                    }
                                    //Check for clamping
                                    int max = GetM2DAInt(EngineConstants.TABLE_PROPERTIES, "Max", _property.nID);
                                    int min = GetM2DAInt(EngineConstants.TABLE_PROPERTIES, "Min", _property.nID);

                                    if (min <= _property.fValueTypeTotal && _property.fValueTypeTotal <= max) return _property.fValueTypeTotal;
                                    else if (_property.fValueTypeTotal < min) return min;
                                    else return max;
                                }
                            case EngineConstants.PROPERTY_TYPE_INVALID:
                            default:
                                {
                                    //Invalid or other type, WTF?
                                    throw new NotImplementedException();
                                }
                        }
                    }
                case EngineConstants.PROPERTY_VALUE_CURRENT:
                    {
                        switch (nType)
                        {
                            case EngineConstants.PROPERTY_TYPE_DEPLETABLE:
                                {
                                    return _property.fValueTypeCurrent;
                                }
                            default: throw new NotImplementedException();//Only depletable has current
                        }
                    }
                case EngineConstants.PROPERTY_VALUE_MODIFIER:
                    {
                        switch (nType)
                        {
                            case EngineConstants.PROPERTY_TYPE_DEPLETABLE:
                            case EngineConstants.PROPERTY_TYPE_ATTRIBUTE:
                                {
                                    return _property.fValueTypeModifier;
                                }
                            default: throw new NotImplementedException();//Depletable and attribute have modifier
                        }
                    }
                default:
                    {
                        //Property value type not found, WTF?
                        throw new NotImplementedException();
                    }
            }
        }
        else
        {
            return -1;//it is -1 as per related function comments
        }
    }

    /* @brief Get the specified attribute from oCreature
    *
    * Gets the type of property for a given property index.
    * The property types are: EngineConstants.ATTRIBUTE, SIMPLE, DEPLETABLE and DERIVED.
    * The property type definitions are found at the top of this file (script.ldf).
    *
    * @param oCreature - the creature whose property type is requested
    * @param nProperty - the property (stat) we want to know about
    * @returns Returns the selected property's type.
    * @author Gabo
    */
    public int GetCreaturePropertyType(GameObject oCreature, int nProperty)
    {
        string sType = GetM2DAString(EngineConstants.TABLE_PROPERTIES, "Type", nProperty);
        switch (sType)
        {
            case "DEPLETABLE": return EngineConstants.PROPERTY_TYPE_DEPLETABLE;
            case "SIMPLE": return EngineConstants.PROPERTY_TYPE_SIMPLE;
            case "ATTRIBUTE": return EngineConstants.PROPERTY_TYPE_ATTRIBUTE;
            case "DERIVED": return EngineConstants.PROPERTY_TYPE_DERIVED;
            default: return EngineConstants.PROPERTY_TYPE_INVALID;
        }
    }

    /* @brief Set the specified value of a given property on a oCreature
    *
    * Sets the specified value of the selected property on a creature. 
    * Doesnt work for TOTAL values. Use BASE instead. See GetCreatureProperty for more details
    *
    * @param oCreature - the creature whose property we want to set.
    * @param nProperty - the property (stat) we want to modify.
    * @param nValueType - the type of value of the property we want to modify (BASE, CURRENT, MODIFIER)
    * @returns Returns oCreature's Attribute Value
    * @author Gabo
    */
    public void SetCreatureProperty(GameObject oCreature, int nProperty, float fNewValue, int nValueType = EngineConstants.PROPERTY_VALUE_BASE)
    {
        List<xProperty> _properties = oCreature.GetComponent<xGameObjectUTC>().oProperties;
        xProperty _property = null;

        int nPropertyType = GetCreaturePropertyType(oCreature, nProperty);

        switch (nPropertyType)
        {
            //If depletable: base, total, modifier, current
            case EngineConstants.PROPERTY_TYPE_DEPLETABLE:
                {
                    switch (nValueType)
                    {
                        case EngineConstants.PROPERTY_VALUE_BASE:
                            {
                                //If exists, update, otherwise create a new one
                                _property = _properties.Find(_p => _p.nID == nProperty);
                                if (_property != null)
                                {
                                    _property.fValueTypeBase = fNewValue;
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                }
                                else
                                {
                                    _property = new xProperty(nProperty, fNewValue, nPropertyType, EngineConstants.PROPERTY_VALUE_BASE);
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                    _properties.Add(_property);
                                }
                                break;
                            }
                        case EngineConstants.PROPERTY_VALUE_CURRENT:
                            {
                                //If exists, update, otherwise create a new one
                                _property = _properties.Find(_p => _p.nID == nProperty);
                                if (_property != null)
                                {
                                    _property.fValueTypeCurrent = fNewValue;
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                    if (_property.fValueTypeTotal < fNewValue) _property.fValueTypeTotal = fNewValue;
                                }
                                else
                                {
                                    //Assuming code that requires current value already has a property Previously created as base
                                    /*_property = new xProperty(nProperty, fNewValue, nPropertyType, EngineConstants.PROPERTY_VALUE_CURRENT);
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                    _properties.Add(_property);*/
                                    throw new NotImplementedException();
                                }
                                break;
                            }
                        default: throw new NotImplementedException();
                    }
                    break;
                }
            //If Attribute: base, total, modifier
            case EngineConstants.PROPERTY_TYPE_ATTRIBUTE:
                {
                    switch (nValueType)
                    {
                        case EngineConstants.PROPERTY_VALUE_BASE:
                            {
                                //If exists, update, otherwise create a new one
                                _property = _properties.Find(_p => _p.nID == nProperty);
                                if (_property != null)
                                {
                                    _property.fValueTypeBase = fNewValue;
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                }
                                else
                                {
                                    _property = new xProperty(nProperty, fNewValue, nPropertyType, EngineConstants.PROPERTY_VALUE_BASE);
                                    _property.fValueTypeTotal = _property.fValueTypeBase + _property.fValueTypeModifier;
                                    _properties.Add(_property);
                                }
                                break;
                            }
                        default: throw new NotImplementedException();
                    }
                    break;
                }
            //If simple or derived, only base and total With identical values
            case EngineConstants.PROPERTY_TYPE_DERIVED:
            case EngineConstants.PROPERTY_TYPE_SIMPLE:
                {
                    switch (nValueType)
                    {
                        case EngineConstants.PROPERTY_VALUE_BASE:
                        case EngineConstants.PROPERTY_VALUE_TOTAL:
                            {
                                //If exists, update, otherwise create a new one
                                _property = _properties.Find(_p => _p.nID == nProperty);
                                if (_property != null)
                                {
                                    _property.fValueTypeBase = _property.fValueTypeTotal = fNewValue;
                                }
                                else
                                {
                                    _property = new xProperty(nProperty, fNewValue, nPropertyType, EngineConstants.PROPERTY_VALUE_BASE);
                                    _property.fValueTypeTotal = _property.fValueTypeBase;
                                    _properties.Add(_property);
                                }
                                break;
                            }
                        default: throw new NotImplementedException();//Should have been only base and total
                    }
                    break;
                }
        }
    }

    /* @brief Update the specified attribute base value on a oCreature
    *
    * Updates (adds the given value to the current value) the specified value of the 
    * selected property of a creature.
    * Only works for MODFIER and CURRENT values. See GetCreatureProperty for more details
    *
    * @param oCreature - the creature whose property will be updated
    * @param nProperty - the property (stat) whose value we want to update
    * @param nValueType - the type of value of the property we want to update (CURRENT, MODIFIER)
    * @returns Returns oCreature's Attribute Value
    * @author Gabo
    */
    public void UpdateCreatureProperty(GameObject oCreature, int nProperty, float fNewValue, int nValueType)
    {
        Debug.LogWarning("update creature property");
        SetCreatureProperty(oCreature, nProperty, fNewValue, nValueType);//Redirect to set creature property
    }

    /* @brief Clears all the properties on a creature
    *
    * Health will be set to 1. All other properties will be set to 0.
    * This function will NOT generate any events due to the properties changing.
    * It is a hard coded function meant to be used when creating a character.
    *
    * @param oCreature - the creature whose properties will be cleared.
    * @author Gabo
    */
    public void ClearCreatureProperties(GameObject oCreature)
    {
        Debug.Log("clear creature properties");
        oCreature.GetComponent<xGameObjectUTC>().oProperties = new List<xProperty>();
    }

    /* @brief Determine whether oCreature has nAbilityID in their list of abilities
    *
    * Determine whether oCreature has nAbilityID in their list of abilities
    *
    * @param oCreature - the creature 
    * @param nAbilityID - the ability identifier
    * @returns Returns TRUE if oCreature has the specified ability
    * @author Sophia, Jose
    */
    public int HasAbility(GameObject oCreature, int nAbility)
    {
        //Debug.Log("has ability");
        return (oCreature.GetComponent<xGameObjectUTC>().oAbilities.Contains(nAbility)) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Determine whether oCreature could spend a point on ability nAbilityID
    *
    * @param oCreature - the creature 
    * @param nAbilityID - the ability identifier
    * @returns Returns TRUE if oCreature meets the prerequisites for nAbility
    * @author Paul Schultz
    */
    public int IsAbilityAvailable(GameObject oCreature, int nAbility)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gives the number of abilities a creature has of a specified type and GUI type
    *
    * @param oCreature - the creature 
    * @param nType - The type of ability to count (if type is 0, all abilities are counted)
    * @param nGUIType - The GUI type to filter the count (if GUI type is 0, there is no GUI type filtering)
    * @returns Returns TRUE if oCreature meets the prerequisites for nAbility
    * @author Gabo
    */
    public int GetAbilityCount(GameObject oCreature, int nType = 0, int nGUIType = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns a list of ability IDs for non-item abilities active on the given creature.
    *
    * @param oCreature - the creature to query
    * @param nType - the type of ability to query for. EngineConstants.INVALID (0) returns all non-item abilities.
    * @param bOnlyCoolingDown - return only abilities that are currently cooling down
    * @returns Returns a list of integer ability IDs.
    * @author Sebastian Hanlon
    */
    public List<int> GetAbilityList(GameObject oCreature, int nType = EngineConstants.ABILITY_INVALID, int bOnlyCoolingDown = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds the specified ability to oCreature
    *
    * Give oCreature the specified ability
    *
    * @param oCreature - the creature
    * @param nAbilityID - the ability identifier
    * @author Sophia
    */
    public void AddAbility(GameObject oCreature, int nAbility, int bSendNotification = EngineConstants.FALSE)
    {
        //Debug.Log("add ability");
        if (bSendNotification != EngineConstants.FALSE)
        {
            throw new NotImplementedException();
        }
        if (oCreature.GetComponent<xGameObjectUTC>().oAbilities.Contains(nAbility))
        {
            Debug.LogWarning("add ability: already exists");
            return;
        }
        else
        {
            oCreature.GetComponent<xGameObjectUTC>().oAbilities.Add(nAbility);
        }
    }

    /* @brief Removes the specified ability from oCreature
    *
    * Takes from oCreature the specified ability
    *
    * @param oCreature - the creature
    * @param nAbilityID - the ability identifier
    * @author Sophia
    */
    public void RemoveAbility(GameObject oCreature, int nAbility)
    {
        Debug.Log("remove ability");
        oCreature.GetComponent<xGameObjectUTC>().oAbilities.Remove(nAbility);
    }

    /* @brief Returns the current stamina of a creature
    *
    * Returns the current stamina of a creature
    *
    * @param oCreature - the creature whose stamina we get 
    * @returns Returns the creature's current stamina.
    * @author Sam
    */
    public int GetCreatureStamina(GameObject oCreature)
    {
        Debug.Log("get creature stamina");
        return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT));
    }

    /* @brief Sets the current stamina of a creature
    *
    * Sets the current stamina of a creature
    *
    * @param oCreature - the creature whose stamina we set 
    * @param nStamina - the new value for the creature's current stamina
    * @author Sam
    */
    public void SetCreatureStamina(GameObject oCreature, int nStamina)
    {
        Debug.Log("set creature stamina");
        UpdateCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, IntToFloat(nStamina), EngineConstants.PROPERTY_VALUE_CURRENT);
    }

    /* @brief Returns the max stamina of a creature
    *
    * Returns the max stamina of a creature
    *
    * @param oCreature - the creature whose stamina we get 
    * @returns Returns the creature's current stamina.
    * @author Sam
    */
    public int GetCreatureMaxStamina(GameObject oCreature)
    {
        Debug.Log("get creature max stamina");
        return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL));
    }

    /* @brief Sets the max stamina of a creature
    *
    * Sets the max stamina of a creature
    *
    * @param oCreature - the creature whose stamina we set 
    * @param nStamina - the new value for the creature's max stamina
    * @author Sam
    */
    public void SetCreatureMaxStamina(GameObject oCreature, int nMaxStamina)
    {
        Debug.Log("set creature max stamina BASE?");
        SetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, IntToFloat(nMaxStamina), EngineConstants.PROPERTY_VALUE_BASE);
    }

    /* @brief Returns the current mana of a creature
    *
    * Returns the current mana of a creature
    *
    * @param oCreature - the creature whose mana we get 
    * @returns Returns the creature's current mana.
    * @author Sam
    */
    public int GetCreatureMana(GameObject oCreature)
    {
        Debug.Log("get creature mana");
        return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_CURRENT));
    }

    /* @brief Sets the current mana of a creature
    *
    * Sets the current mana of a creature
    *
    * @param oCreature - the creature whose mana we set
    * @param nMana - the new value for the creature's current mana
    * @author Sam
    */
    public void SetCreatureMana(GameObject oCreature, int nMana)
    {
        Debug.Log("set creature mana");
        UpdateCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, IntToFloat(nMana), EngineConstants.PROPERTY_VALUE_CURRENT);
    }

    /* @brief Returns the max mana of a creature
    *
    * Returns the max mana of a creature
    *
    * @param oCreature - the creature whose mana we get 
    * @returns Returns the creature's max mana.
    * @author Sam
    */
    public int GetCreatureMaxMana(GameObject oCreature)
    {
        Debug.Log("get creature max mana");
        return FloatToInt(GetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA, EngineConstants.PROPERTY_VALUE_TOTAL));
    }

    /* @brief Sets the max mana of a creature
    *
    * Sets the max mana of a creature
    *
    * @param oCreature - the creature whose mana we set 
    * @param nMana - the new value for the creature's max mana
    * @author Sam
    */
    public void SetCreatureMaxMana(GameObject oCreature, int nMaxMana)
    {
        Debug.Log("set creature max mana BASE?");
        SetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, IntToFloat(nMaxMana), EngineConstants.PROPERTY_VALUE_BASE);
    }

    // int HasSpell(GameObject oObject, int nSpell) 

    // void AddSpell(GameObject oObject, int nSpell) 

    // void RemoveSpell(GameObject oObject, int nSpell) 

    // int GetCreatureSkillRank(GameObject oCreature, int nSkill) 

    // int SetCreatureSkillRank(GameObject oCreature, int nSkill, int nSkillRank) 

    // void SetCreatureXP(GameObject oCreature, int nXP) 

    // int GetCreatureXP(GameObject oCreature) 

    // void AddCreatureXP(GameObject oCreature, int nXP) 

    // void RemoveCreatureXP(GameObject oCreature, int nXP) 

    // int GetCreatureAlignment(GameObject oCreature) 

    // void SetCreatureAlignment(GameObject oCreature, int nAlignment) 

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Bio / Appearance
    /***************************************************************/
    /* @addtogroup bio_appearanc Appearance Functions
    *
    * Functions that edit creature's stats (gender, size, name etc')
    */
    /* @{*/

    public int GetCreatureGender(GameObject oCreature)
    {
        Debug.Log("get creature gender");
        return oCreature.GetComponent<xGameObjectBase>().nGender;
    }

    /* @brief Set the gender of a creature
    *
    * Sets the gender of a creature to a EngineConstants.GENDER_* const. To be used only in
    * Character Creation
    *
    * @param oCreature - the creature whose gender is to be set.
    * @param nGender - EngineConstants.GENDER_ const.
    * @author Georg
    */
    public void SetCreatureGender(GameObject oCreature, int nGender)
    {
        Debug.Log("set creature gender");
        oCreature.GetComponent<xGameObjectBase>().nGender = nGender;
    }

    // int GetCreatureSize(GameObject oCreature) 

    // void SetCreatureSize(GameObject oCreature, int nSize) 

    // int GetCreatureSpeed(GameObject oCreature) 

    // void SetCreatureSpeed(GameObject oCreature, int nSpeed) 

    public int GetCreatureRacialType(GameObject oCreature)
    {
        if (oCreature.GetComponent<xGameObjectBase>().nRacialType == 0) //Was not yet set
        {
            oCreature.GetComponent<xGameObjectBase>().nRacialType = GetLocalInt(oCreature, "Race");
        }
        return oCreature.GetComponent<xGameObjectBase>().nRacialType;
    }

    /* @brief Sets a creature's racial type
    *
    * Sets a creature's racial type. This has far reaching implications and should
    * not be called outside of character creation

    * @param oCreature - the creature whose racial type we set 
    * @author Georg
    */
    public void SetCreatureRacialType(GameObject oCreature, int nRacialType)
    {
        Debug.Log("set creature racial type");
        oCreature.GetComponent<xGameObjectBase>().nRacialType = nRacialType;
    }

    // int GetCreatureAge(GameObject oCreature) 

    // void SetCreatureAge(GameObject oCreature, int nAge) 

    // int GetCreatureAppearance(GameObject oCreature) 

    // void SetCreatureAppearance(GameObject oCreature, int nAppearance) 

    /* @brief Gets an object's name.
    *
    * Gets an object's name. The non-localized name is returned if it is set, otherwise
    * the localized name will be returned.
    */
    public string GetName(GameObject oidObject)
    {
        Debug.Log("engine get name");
        return oidObject.name;
    }

    /* @brief Sets an object's non-localized name.
    *
    * Overrides an object's name with non-localized text. Non-localized names have
    * a higher priority than localized names.
    */
    public void SetName(GameObject oObject, string sName)
    {
        Debug.Log("engine set name");
        oObject.name = sName;
    }

    /* @brief Sets an object's localized name.
    *
    * Sets an object's localized name.
    */
    public void SetLocName(GameObject oObject, int nStringIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Stores the party dog's name
    *
    * This also updates the value of designer tag "<DogName/>".  It does not however update 
    * any specific creature's name.  This must be done using SetName().
    */
    public void StoreDogName(string sName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the party dog's name
    *
    * Returns the value previously stored using StoreDogName()
    */
    public string RecallDogName()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // string GetRandomName() 

    public float GetCreatureGoreLevel(GameObject oCreature)
    {
        //return oCreature.GetComponent<xGameObjectUTC>().GORE;
        return GetLocalFloat(oCreature, "GORE");
    }

    public void SetCreatureGoreLevel(GameObject oCreature, float fGoreLevel)
    {
        UpdateGameObjectProperty(oCreature, "GORE", fGoreLevel.ToString());
        //oCreature.GetComponent<xGameObjectUTC>().GORE = fGoreLevel;
    }

    /*
        @brief Gets the heraldic sign for item oItem 
        Will return zero for items that do not support heraldry
    */
    public int GetItemHeraldry(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /*
        @brief Sets the heraldic sign for item oItem to nHeraldry   . 
        Will autofail for items that do not support heraldry
    */
    public void SetItemHeraldry(GameObject oItem, int nHeraldry)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Update a creature's appearance and inventory from a template
    *
    * @author Jacques Lebrun
    */
    public void LoadItemsFromTemplate(GameObject oCreature, string sTemplate, int bReplaceInventory = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the AppearanceType of a creature
    *
    * Gets the AppearanceType of a creature
    *
    * @param oCreature - A creature
    * @author Georg Zoeller
    */
    public int GetAppearanceType(GameObject oCreature)
    {
        var nAppearance = oCreature.GetComponent<xGameObjectBase>().nAppearanceType;
        if (nAppearance == 0) nAppearance = GetLocalInt(oCreature, "Appearance");
        return nAppearance;
    }

    /* @brief Sets the AppearanceType of a creature. Can be used for character generation and shape shifting
    *
    * Sets the AppearanceType of a creature. Can be used character generation and shape shifting
    *
    * @param oCreature - A creature
    * @param nAppearanceId - Appearance 2da row index. Use -1 to return to original appearance
    * @param bSetAsOriginal - Specifying TRUE will set the given id as the original appearance type
    * @author Georg Zoeller, Jose
    */
    public void SetAppearanceType(GameObject oCreature, int nApperanceId, int bSetAsOriginal = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the head morph file to use for the creature
    *
    * @param oCreature - The creature to affect
    * @param headmorphname - New head morph to use
    * @author Nicolas Ng Man Sun
    */
    public void SetHeadMorphName(GameObject oCreature, string headmorphname)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the ambient activity pattern for the creature
    *
    * @param oCreature - The creature to evaluate
    * @author Gavin Burt
    */
    public int GetCreatureAmbientActivityPattern(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the ambient activity pattern for the creature
    *
    * @param oCreature - The creature to affect
    * @param nAmbientActivityPattern - The value to set
    * @author Gavin Burt
    */
    public void SetCreatureAmbientActivityPattern(GameObject oCreature, int nAmbientActivityPattern)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the ambient movement pattern for the creature
    *
    * @param oCreature - The creature to evaluate
    * @author Gavin Burt
    */
    public int GetCreatureAmbientMovementPattern(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the ambient movement pattern for the creature
    *
    * @param oCreature - The creature to affect
    * @param nAmbientMovementPattern - The value to set
    * @author Gavin Burt
    */
    public void SetCreatureAmbientMovementPattern(GameObject oCreature, int nAmbientMovementPattern)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Areas
    /***************************************************************/
    /* @addtogroup area Area Functions
    *
    * Functions that manage areas
    */
    /* @{*/

    // int GetAreaProperties(int nProperty) 

    // int SetAreaProperties(int nProperty, int nValue) 

    /* @brief Moves the entire party into an area
    *
    * This function will do an area transtion if the target area is within the same area 
    * list of the current area OR if the target area is in a different area list then
    * it will unload the current area list and load into memory (with a loading bar)
    * a new area list. The party will then be jumped to the specified waypoint (if it exists)
    *
    * @param sArea - tag of the target area
    * @param sWaypointTag - tag of the target waypoint
    * @returns 1 for success, 0 for failure (area list does not exist or area does not exist)
    * @remarks this function will jump ALL party members
    * @author Ross
    */
    /*int?*/
    public void DoAreaTransition(string sArea, string sWaypointTag)
    {
        //TO DO loading screen, again, I'm obnoxious :-)
        SceneCleanup();

        xGameObjectMOD.instance.tArea = sArea;
        xGameObjectMOD.instance.tWaypoint = sWaypointTag;

        xEvent ev = Event(EngineConstants.EVENT_TYPE_MODULE_AREA_TRANSITION);
        SetEventCreatorRef(ref ev, gameObject);
        SignalEvent(GetModule(), ev);
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Area Of Effects
    /***************************************************************/
    /* @addtogroup aoe Area of Effect Functions
    *
    * Functions that handle AOE effects
    */
    /* @{*/

    /* @brief Gets the bit flags of an AOE object
    *
    * @param oAOE - The AOE that has the flags
    * @returns The flags values
    * @author Gabo
    */
    public int GetAOEFlags(GameObject oAOE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the bit flags of an AOE object
    *
    * @param oAOE - The AOE that has the flags
    * @param nFlags - The flag values tha will be set on the AOE
    * @author Gabo
    */
    public void SetAOEFlags(GameObject oAOE, int nFlags)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Indicates if a creature is in an AOE
    *
    * @param oCreature - The creature being checked
    * @param oAOE - The AOE that the creature may be in
    * @returns True if the creature is in the AOE
    * @author Gabo
    */
    public int IsInAOE(GameObject oCreature, GameObject oAOE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns a list of creatures within an AOE
    *
    * @param oAOE - The AOE that is being queried
    * @returns an array of creatures that are in the AOE
    * @author Gabo
    */
    public List<GameObject> GetCreaturesInAOE(GameObject oAOE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // GameObject GetAreaOfEffectCreator(GameObject oAreaOfEffect) 

    /* @brief Returns a list of ability IDs for each AOE in which a creature is in.
    *
    * @param oCreature - The creature being queried
    * @returns an array of ability ids.
    * @author Gabo
    */
    public List<int> GetAbilitiesDueToAOEs(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Individual "impact" events for each GameObject within an Area of Effect.
    *
    * Assembles (and delays for 0-149 milliseconds) an individual impact event
    * associated with an area of xEffect to ensure that they don't all happen simultaneously.
    * This is called directly by the Area of Effect GameObject itself, thus we don't need to know
    * anything about its center Vector3 (except in the case of fireball, hence the fourth
    * parameter).
    *
    * @param oCaster - The person responsible for the area of effect.
    * @param oTarget - The person who will run the effect.
    * @param nAbility - The ability that should be cast.
    * @param lTarget - The Vector3 of the target of the xEvent (used for fireball and other
    *                  abilities that can be cast at the ground.
    * @param nBaseDelay - The base delay is 0 milliseconds; however, some cone effects
    *                     require more than a random 150 millisecond delay to make crusts
    *                     hit simultaneously with the cone.  Use BaseDelay for this purpose.
    *
    * @returns Nothing.  Creates the xEvent internally, and stores it on the queue.
    * @author MarkB
    */
    public void SetIndividualImpactAOEEvent(GameObject oCaster, GameObject oTarget, int nAbility, Vector3 lTarget, int nBaseDelay = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Placeables
    /***************************************************************/
    /* @addtogroup placeables Placeable Object Functions
    *
    * Functions to access, set and interact with placeable objects.
    *
    */
    /* @{*/

    // int GetPlaceableOpenState(GameObject oPlaceable) 

    // int IsPlaceableUseable(GameObject oPlaceable) 

    // void SetPlaceableUseable(GameObject oPlaceable, int bUseable) 

    // int IsPlaceableContainer(GameObject oPlaceable) 

    // void SetPlaceableContainer(GameObject oPlaceable, int bContainer) 

    /* @brief Gets the state of the specified placeable object.
    *
    * Returns the current state of a specified placeable object. The state
    * will be a valid state defined by the state controller for the
    * specified placeable. The placeable states are defined in the placeables.xls file in override.
    *
    * @param oPlaceable - The placeable to get the state of
    * @returns Returns the state of the placeable object
    * @sa SetPlaceableState()
    * @author Paul
    */
    public int GetPlaceableState(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the state controller ID of the specified placeable object.
    *
    * @author Nicolas Ng Man Sun
    */
    public int GetPlaceableStateController(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the state for a specified placeable object.
    *
    * Sets the state for this placeable, but only if the state is a valid one
    * for the placeable itself as defined by the state controller. The placeable states are defined in the placeables.xls file in override.
    *
    * @param oPlaceable - The placeable to set the state on
    * @param nState - The state to set on the placeable
    * @sa GetPlaceableState()
    * @author Paul
    */
    public void SetPlaceableState(GameObject oPlaceable, int nState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the result of a placeable action.
    *
    * @param oPlaceable - The placeable to report results from.
    * @param nAction - The use action to report on.
    * @param bSuccess - Whether the action succeeded or not.
    * @param bVariation - Whether to use the default transition or the variation. Only applies when the action succeded.
    * @author Jacques Lebrun
    */
    public void SetPlaceableActionResult(GameObject oPlaceable, int nAction, int bSuccess, int bVariation = EngineConstants.FALSE)
    {
        SetLocalInt(oPlaceable, "PLC_ACTION_RESULT", bSuccess);
    }

    /* @brief Gets the current use action on the placeable.
    *
    * @param oPlaceable - The placeable to get the action from
    * @author Jacques Lebrun
    */
    public int GetPlaceableAction(GameObject oPlaceable)
    {
        return GetLocalInt(oPlaceable, "PLC_ACTION");
    }

    /* @brief Gets the base type of the placeable.
    *
    * @param oPlaceable - The placeable to get the type from
    * @author Jacques Lebrun
    */
    public int GetPlaceableBaseType(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets whether the key should be removed automatically when used.
    *
    * @param oPlaceable - The placeable.
    * @author Jacques Lebrun
    */
    public int GetPlaceableAutoRemoveKey(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets whether a key is required to use this placeable.
    *
    * @param oPlaceable - The placeable.
    * @author Jacques Lebrun
    */
    public int GetPlaceableKeyRequired(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the GameObject tag of the key required to use this placeable
    *
    * @param oPlaceable - The placeable.
    * @author Jacques Lebrun
    */
    public string GetPlaceableKeyTag(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the pick lock level
*
* @param oPlaceable - The placeable.
* @author Jacques Lebrun
*/
    public int GetPlaceablePickLockLevel(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the treasure rank.
    *
    * @param oPlaceable - The placeable.
    * @author Jacques Lebrun
    */
    public int GetPlaceableTreasureRank(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the treasure category.
    *
    * @param oPlaceable - The placeable.
    * @author Jacques Lebrun
    */
    public int GetPlaceableTreasureCategory(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the Popup text
    *
    * @param oPlaceable - The placeable.
    * @author John  Fedrokiw 
    */
    public string GetPlaceablePopupText(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set a creature's ranks in a class
    *
    * @param oCreature - The creature
    * @param nClass - The class to set rank in
    * @param nRanks - The rank the creature should have
    * @author Paul Schultz
    */
    public void SetClassRank(GameObject oCreature, int nClass, int nRank)
    {
        Debug.Log("set class rank:TO DO class may be obsolete");
        oCreature.GetComponent<xGameObjectBase>().nRank = nRank;
    }

    /* @brief Get a creature's ranks in a class
    *
    * @param oCreature - The creature
    * @param nClass - The class you're querying
    * @author Paul Schultz
    */
    public int GetClassRank(GameObject oCreature, int nClass)
    {
        Debug.Log("get class rank:TO DO class may be obsolete");
        return oCreature.GetComponent<xGameObjectBase>().nRank;
    }

    /* @brief Get the treasure category.
    *
    * @param oCreature - The creature.
    * @author Adriana Lopez
    */
    public int GetCreatureTreasureCategory(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // Vector3 GetTransitionTarget(GameObject oPlaceable) 

    // void SetTransitionTarget(GameObject oPlaceable, Vector3 lLocation) 

    // int IsDoorCommandPossible(GameObject oPlaceable) 

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Items
    /***************************************************************/
    /* @addtogroup items Items Functions
    *
    * Functions to access, set and interact with items.
    *
    */
    /* @{*/

    // int GetItemType(GameObject oItem) 

    // int GetItemDefenseValue(GameObject oItem) 

    // int IsWeaponRanged(GameObject oItem) 

    // int IsItemDroppable(GameObject oItem) 

    // void SetItemDroppable(GameObject oItem, int bDroppable) 

    // int IsItemStolen(GameObject oItem) 

    // void SetItemStolen(GameObject oItem, int bStolen) 

    // void SetMaxItemStackSize(GameObject oItem, int nMaxStackSize) 

    // int HasItemProperty(GameObject oItem, int nItemProperty) 

    /* @brief Gets the maximum stack Size of an item
    *
    * Returns an integer representing the maximum stack size of an item. 
    *
    * @param oItem - An Item Object
    * @returns 0 if the item is invalid; defauts to 1 if the item is not stackable.
    * @author Georg
    */
    public int GetMaxItemStackSize(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the stack Size of an item
    *
    * Returns an integer representing the current stack size of an item. 
    *
    * @param oItem - An Item Object
    * @returns 0 if the item is invalid; defauts to 1 if the item is not stackable.
    * @author Georg
    */
    public int GetItemStackSize(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the stack Size of an item
    *
    * Sets the stack size of item oItem to nStackSize. Clamps it between 1 and MaxStackSize. 
    *
    * @param oItem An Item 
    * @param nStackSize The new StackSize.
    * @author Georg
    */
    public void SetItemStackSize(GameObject oItem, int nStackSize)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enumerates items attached to this item
    *
    * An empty array means there are no slots available to attach items
    * A non-empty array enumerates all slots. Empty slots will have
    * OBJECT_INVALID, filled slots will have the OBJECT_ID of the
    * attached items
    *
    * @param oItem An Item 
    * @author Georg
    */
    public List<GameObject> GetItemSubItems(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds sub-item (rune) to a given item
    *
    *
    * @param oMainItem An Item 
    * @param oSubItem A sub-item
    * @param nSlotNumber sub-item slot to put the sub-item into 
    * @author Georg
    */
    public void AddItemSubItem(GameObject oMainItem, GameObject oSubItem, int nSlotNumber)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief removes sub-item (rune) from a given item 
    *
    *
    * @param oMainItem An Item with sub-item slots
    * @param nSlotNumber sub-item slot to remove an item from
    * @param nPreserveSubItem Set to 1 if the rune should not be deleted.
    * @author Georg
    */
    public void RemoveItemSubItem(GameObject oMainItem, int nSlotNumber, int nPreserveSubItem = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the BaseItemType of an item
    *
    * returns the base item type of an item as EngineConstants.BASE_ITEM_*
    *
    * @param oItem - An Item Object
    * @author Georg
    */
    public int GetBaseItemType(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the material type of an item
    *
    * returns the material type of an item
    *
    * @param oItem - An Item Object
    * @author Georg
    */
    public int GetItemMaterialType(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the material type of an item
    *
    * @param oItem - An Item Object
    * @param nMaterialType - The MATERIAL_TYPE to set the item to
    * @author Georg
    */
    public void SetItemMaterialType(GameObject oItem, int nMaterialType)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets all items in an GameObject inventory
    *
    * Provides access to a creatures or placeables inventory and equipped items.
    *
    * @param oObject        - A creature or placeable with an inventory
    * @param nGetItemOptions  - A EngineConstants.GET_ITEMS_* constant. THIS IS NOT A BITFIELD!
    * @param nBaseItemType      - Return only items with base item type matching. 0 to disable this filter.
    * @param sFilterTag     - Return only items with a matching tag. "" to disable this filter.
    *
    * @author Georg
    */
    public List<GameObject> GetItemsInInventory(GameObject oObject, int nGetItemsOptions = EngineConstants.GET_ITEMS_OPTION_ALL, int nBaseItemType = 0, string sTagFilter = "", int bIgnorePlotItems = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the ability_id associated with an item
    *
    * An items ability id (if != EngineConstants.ABILITY_INVALID) is the one active ability an item will grant when equipped to the quickbar.
    *
    * @param   oItem        - An item
    * @returns an index into EngineConstants.ABI_BASE.xls
    *
    * @author Georg
    */
    public int GetItemAbilityId(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the ability_id associated with an item
    *
    * An items ability id (if != EngineConstants.ABILITY_INVALID) is the one active ability an item will grant when equipped to the quickbar.
    *
    * CORE_FUNCTION. Do not use unless you are Georg or Yaron. Do not use on equipped items. Ever. Use the Wrapper.
    *
    * @param   oItem        - An item
    * @param   nAbility     - an index into an item ability defined in EngineConstants.ABI_BASE.xls
    * @param   nPower       - An optional power value for the item. Default is 1
    *
    * @author Georg
    */
    public void SetItemAbilityId(GameObject oItem, int nAbilityId, int nPower = 1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Inventory / Equip Slots
    /***************************************************************/
    /* @addtogroup inventory Inventory & Equip Slot Functions
    *
    * Functions to access creature and player inventory/equip slots.
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETITEMINEQUIPSLOT = 401;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ADDITEMTOINVENTORY = 402;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ISLOOTABLE = 404;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETLOOTABLE = 405;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_REMOVEGOLD = 407;

    /* @brief Gets the item in a creature's equip slot
    *
    * Returns the item in the specified equip slot for a creature
    *
    * @param nSlot - the equip slot to be examined
    * @param oCreature - the creature to examine
    * @author Noel
    */
    public GameObject GetItemInEquipSlot(int nSlot, GameObject oCreature = null, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
    {
        if (oCreature == null) oCreature = gameObject;//gameObject
        GameObject oItem = null;
        try { oItem = oCreature.gameObject.GetComponent<xGameObjectUTC>().oGear[nSlot]; }
        catch { Warning("Item in slot " + nSlot + " not found"); }
        return oItem;

    }

    /* @brief Removes item(s) from its container
    *
    * Removes the given item(s) from its container/inventory.
    *
    * @param oItem - the item to remove
    * @param nNumItems - the number of items to be removed from the stack (if applicable).
    *     Use -1 to remove all items in stack.
    * @author Henry
    */
    public void RemoveItem(GameObject oItem, int nNumItems = -1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes item(s) from its container
    *
    * Removes the matching item(s) from its container/inventory.
    *
    * @param oPossessor - The placeable or creature to remove items from.
    * @param nNumItems - The number of items to be removed. Use -1 to remove all matching items.
    * @author Henry
    */
    public void RemoveItemsByTag(GameObject oPossessor, string sTag, int nNumItems = -1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Counts items with the given tag in the given container/inventory.
    *
    * Counts items with the given tag in the given container/inventory.
    *
    * @param oPossessor - The placeable or creature to count items in.
    * @param sTag - The tag of the items to count.
    * @author Henry
    */
    public int CountItemsByTag(GameObject oPossessor, string sTag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Add money to a creature
    *
    * Add money to a creature
    *
    * @param nCoppers  - The amount to give, in copper pieces
    * @param oCreature - The creature receiving the money
    * @param bNotify   - Notify the player his money amount changed
    * @author EricP
    */
    public void AddCreatureMoney(int nCoppers, GameObject oCreature = null, int bNotify = EngineConstants.TRUE)
    {
        if (oCreature == null) oCreature = gameObject;//gameObject
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get amount of money a creature posseses
    *
    * Gets amount of money a creature posseses
    *
    * @param oCreature - the creature receiving the money
    * @author EricP
    */
    public int GetCreatureMoney(GameObject oCreature = null)
    {
        if (oCreature == null) oCreature = gameObject;//gameObject
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gives money to a creature
    *
    * Gives money to a creature
    *
    * @param nCoppers  - The amount to give, in copper pieces
    * @param oCreature - The creature receiving the money
    * @param bNotify   - Notify the player his money amount changed
    * @author EricP
    */
    public void SetCreatureMoney(int nCoppers, GameObject oCreature = null, int bNotify = EngineConstants.TRUE)
    {
        if (oCreature == null) oCreature = gameObject;//gameObject
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Opens the inventory of an GameObject for a target player.
    *
    * A player can view their own inventory or that of one of their party, a
    * container, a store or a stash.
    *
    * @param oObject - the GameObject to be opened
    * @param oPlayer - the player doing the opening
    * @author Sophia
    */
    public void OpenInventory(GameObject oObject, GameObject oPlayer, int bCanAddItems = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns an item possessed by an GameObject (creature or placeable) with the given tag
    *
    * Returns an item possessed by an GameObject (creature or placeable) with the given tag
    *
    * @param oObject - the GameObject with an inventory
    * @param sTag - tag for the item to return
    * @author Noel
    */
    public GameObject GetItemPossessedBy(GameObject oObject, string sTag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Switches the current weapon set with the given weapon set.
    *
    * Makes the active weapon set become the one that is given in the input
    * and generates all the necessary equip events for the new weapons. If 
    * the current weapon set is the one requested, this does nothing. There
    * are only two weapon sets so the input value can only be 0 or 1. If no
    * weapon set is given, the xCommand will switch to the next set.
    *
    * @param oObject - The GameObject to have its weapon set switched.
    * @param mWeaponSet - The weapon set number, it can be 0 or 1.
    * @author Gabo.
    */
    public void SwitchWeaponSet(GameObject oObject, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the active weaponset on a creature
    *   
    *  Returns the weaponset currently active by oCreature. 
    *
    *  0 - WeaponSet 1 (1 in the Toolset)
    *  1 - Weaponset 2 (2 in the Toolset)
    *
    * Warning: Returns 0 also on failure (oObject not a creature..)
    *
    * @param oObject The creature the check the weapon set on
    * @author Georg Zoeller
    */
    public int GetActiveWeaponSet(GameObject oCreature)
    {
        throw new NotImplementedException();
        /*string sRet = GetLocal(oCreature, EngineConstants.ACTIVE_WEAPON_SET);
        int nRet;
        //If  the returned value can be parsed in to an integer, and it is equal to 1
        if (int.TryParse(sRet, out nRet) && nRet == EngineConstants.TRUE) 
        {
            //TO DO
            //xGameObject o = oObject.gameObject.GetComponent<xGameObjectBase>();
            return EngineConstants.TRUE;//1 = alternate Weapon set
        }
        else
        {
            return EngineConstants.FALSE;//it's either empty or the main weapon set
        }*/
    }

    /* @brief Equips an item on a creature
    *
    * Equips an item from the inventory into the equiped slots.
    * If no equipment slot is given, the item will be equipped where it
    * best fits. If the equip slot is in the weapon set it will be equipped
    * in the weapon set number that is given. If no weapon set number is given
    * the active weapon set will be used.
    * If an incorrect slot is given or an invalid GameObject is given
    * the function will return 0. 
    *
    * @param oObject - The GameObject to have its weapon set switched.
    * @param nEquipSlot - The optinal equip slot number. Use the INVENTORY_SLOT constants to specify a particular slot.
    * @param nWeaponSet - The optinal weapon set number, it can be 0 or 1.
    * @author Gabo.
    */
    public int EquipItem(GameObject oObject, GameObject oItem, int nEquipSlot = EngineConstants.INVENTORY_SLOT_INVALID, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Unequips an item from a creature
    *
    * Removes an item from the equip slots and puts it in the inventory.
    * If there is any kind of error, this will return 0.
    *
    * @param oObject - The GameObject to have its weapon set switched.
    * @param mWeaponSet - The weapon set number, it can be 0 or 1.
    * @author Gabo.
    */
    public int UnequipItem(GameObject oObject, GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enables/disables equipment access
    *
    * Enables or disables equipment access for a particular party member.
    * NOTE: This xCommand will not work in EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT.
    *
    * @param oCreature - The creature.
    * @param bEnabled - 1 to enable, 0 to disable.
    * @author Henry
    */
    public void SetCanChangeEquipment(GameObject oCreature, int bCanChange)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enables/disables equipment access for a particular slot
    *
    * Enables or disables equipment access for a particular party member
    * for a particular slot
    * NOTE: This xCommand will not work in EngineConstants.EVENT_TYPE_AREALOAD_PRELOADEXIT.
    *
    * @param oCreature - The creature.
    * @param nSlotID - the 0-indexed slot to enable or disable
    * @param bEnabled - 1 to enable, 0 to disable.
    * @author Paul Schultz
    */
    public void SetCanChangeEquipmentSlot(GameObject oCreature, int nSlotID, int bEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Tells whether or a creature can change equipment for a given slot
    *
    * Tells whether or a creature can change equipment for a given slot
    *
    * @param oCreature - The creature.
    * @param nSlotID - the 0-indexed slot we're interested in
    * @returns 1 if the slot can be changed, 0 if it cannot
    * @author Paul Schultz
    */
    public int GetCanChangeEquipmentSlot(GameObject oCreature, int nSlotID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the max size of the party's inventory (ie. number of slots)
    *
    * Gets the max size of the creature's inventory (ie. number of slots)
    *
    * @param oCreature - The creature ID (defaults to player)
    * @author Henry
    */
    public int GetMaxInventorySize(GameObject oCreature = null)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the max size of the party's inventory (ie. number of slots)
    *
    * Sets the max size of the creature's inventory (ie. number of slots)
    *
    * @param nMaxSize - The total number of slots
    * @param oCreature - The creature ID (defaults to player)
    * @author Henry
    */
    public void SetMaxInventorySize(int nMaxSize, GameObject oCreature = null)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Store the party inventory on a placeable
    *
    * Store the party inventory on a placeable
    * NOTE: As with all commands that add items to a container, this xCommand will reset the container (placeable)
    *       to interactive not matter it's previous state.
    *
    * @param oPlaceable - Target placeable
    * @author Gavin Burt
    */
    public void StorePartyInventory(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Store a follower inventory on a placeable
    *
    * Store a follower inventory on a placeable
    * NOTE: As with all commands that add items to a container, this xCommand will reset the container (placeable)
    *       to interactive not matter it's previous state.
    *
    * @param oFollower - Source creature
    * @param oPlaceable - Target placeable
    * @author Gavin Burt
    */
    public void StoreFollowerInventory(GameObject oFollower, GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Restore the party inventory from a placeable
    *
    * Restore the party inventory from a placeable (previously set by StorePartyInventory)
    *
    * @param oPlaceable - Source placeable
    * @author Gavin Burt
    */
    public void RestorePartyInventory(GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Restore a follower inventory from a placeable
    *
    * Restore a follower inventory from a placeable (previously set by StoreFollowerInventory)
    *
    * @param oFollower - Target creature
    * @param oPlaceable - Source placeable
    * @author Gavin Burt
    */
    public void RestoreFollowerInventory(GameObject oFollower, GameObject oPlaceable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Move an item from one GameObject to another.
    *
    * Move an item from one GameObject to another.
    *
    * @param oSource - Source object
    * @param oTarget - Target object
    * @param oItem - Item to be moved
    * @author Gavin Burt
    */
    public void MoveItem(GameObject oSource, GameObject oTarget, GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Move all items from one GameObject to another.
    *
    * Move all items from one GameObject to another.
    *
    * @warning This only moves items from the default Vector3 to another default
    * location.  This means that when moving items from a creature it is only
    * moving items from the back to the other backpack (equipped items are not considered).
    *
    * @param oSource - Source object
    * @param oTarget - Target object
    * @author Gavin Burt
    */
    public void MoveAllItems(GameObject oSource, GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Displays a text message over a Players portrait
    *
    * Displays a text message or number over a player portrait
    *
    * @param oPlayerCreature - The creature to display the floaty over.
    * @param sMessage - The text of the message.
    * @param nColour - The text colour, in hex (eg. 0xff0000 is red).
    * @author John Fedorkiw
    */
    public void DisplayPortraitMessage(GameObject oPlayerCreature, string sMessage, int nColour = 16777215)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Displays a floating message over a creature
    *
    * Displays an animated message or number floating over a creature
    * indicating damage taken, critical hits, etc.
    *
    * @param oCreature - The creature to display the floaty over.
    * @param sMessage - The text of the message.
    * @param nStyle - The visual style of the floaty.
    * @param nColour - The text colour, in hex (eg. 0xff0000 is red).
    * @param nDuration - If specified, the floaty will be displayed for the indicated seconds.
    * Note: If the time is zero, the floaty message is still displayed momentarily,
    * as there is a fade in and fade out animation. Also  If the floaty message sports 
    * a style of "FLOATY_HIT" or "FLOATY_CRITICAL_HIT" the duration is completely ignored, 
    * this is controlled through ActionScript!
    * @author Henry
    */
    public void DisplayFloatyMessage(GameObject oCreature, string sMessage, int nStyle = EngineConstants.FLOATY_MESSAGE, int nColour = 16777215, float nDuration = 0.5f)
    {
        GameObject oFloaty = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/floatyPrefab"));
        oFloaty.name = oCreature.name + "_floaty";
        var _floaty = oFloaty.GetComponent<GUIText>();

        byte R = (byte)((nColour >> 16) & 0xFF);
        byte G = (byte)((nColour >> 8) & 0xFF);
        byte B = (byte)((nColour) & 0xFF);

        _floaty.font.material.color = _floaty.GetComponent<Renderer>().material.color = new Color(R, G, B, 1);

        _floaty.fontSize = 12;
        _floaty.text = sMessage;

        _floaty.transform.position = Camera.main.WorldToViewportPoint(oCreature.transform.position);
        //_floaty.transform.position = new Vector3(0.5f, 0.5f, 0.0f);
        oFloaty.AddComponent<xFloaty>();
        oFloaty.GetComponent<xFloaty>().displayTime = nDuration;
    }

    /* @brief Plays a floaty xEffect on the floaty capsule of the target.
    *
    * @param nFloatyEffect - The type of xEffect to play.
    * @param oTarget	   - The owner of the floaty capsule to play the xEffect on.
    * @author Jacques
    */
    public void PlayFloatyEffect(int nFloatyEffect, GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Indicate to the engine that the TARGET creature has taken damage from the SOURCE creature. This is so the GUI can display indicators when PC's take damage from off screen enemies.
    *
    * @param oSource - The creature that inflicted the damage
    * @param oTarget - The creature that was damaged
    * @author John Fedorkiw
    */
    public void SignalDamage(GameObject oSource, GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Starts the character generation or level up process
    * Do not use, use the functions in ui_h instead!!
    * Starts the character generation or level up process.
    *
    * @param oCreature - The creature to generate.
    * @param nMode - 0 = char gen, 1 = level up
    * @param bImportEnabled - applicable to chargen only, allows importing a hero from an existing save file (in addition to
    *                         the option of creating a new character.
    * @author Henry
    */
    public void StartCharGen(GameObject oCreature, int nMode = 0, int nImportEnabled = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Preloads strings needed to run chargen smoothly.
    *
    * @author Jacques
    */
    public void PreloadCharGen()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Displays a status message in the middle of the screen
    *
    * Displays a status message in the middle of the screen
    *
    * @param sMessage - The text of the message.
    * @param nColour - The text colour, in hex (eg. 0xff0000 is red)
    * @author Henry
    */
    public void DisplayStatusMessage(string sMessage, int nColour = 16777215)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets or clears an ability in a creature's quickslot.
    *
    * Sets or clears an ability in a creature's quickslot.
    *
    * @param oCreature - The creature.
    * @param nSlot - The index of the slot to set, or -1 to use the first empty slot.
    * @param nAbilityID - The ability ID to put in the slot (or 0 to clear).
    * @param sItemTag - An item tag in case the ability is linked to a specific item.
    * @param nPlaySound - if TRUE plays the GUI sound associated with adding an ability ot the quickslot.
    * @author Henry
    */
    public void SetQuickslot(GameObject oCreature, int nSlot, int nAbilityID, string sItemTag = "", int nPlaySound = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the ability ID for the ability in the given quickslot
    *
    * Gets the ability in the given quickslot
    *
    * @param oCreature - The creature.
    * @param nSlot - The index of the slot to set.
    *
    * @returns The ID of the ability currently in the slot (or zero if it is empty)
    * @author Fedorkiw
    */
    public int GetQuickslot(GameObject oCreature, int nSlot)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Changes the quickbar currently used by the creature
    *
    * Changes the quickbar currently used by the creature
    *
    * @param oCreature - The creature.
    * @param nBarNumber - The index of the bar to use (0 to 4)
    * @author Gabo
    */
    public void SetQuickslotBar(GameObject oCreature, int nBarNumber)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Locks or unlocks the quickslot bar for the user interface of a creature
    *
    * Locks or unlocks the quickslot bar for the user interface of a creature
    *
    * @param oCreature - The creature.
    * @param bLock - Locks the quickbar if true, unlocks it if false
    * @author Gabo
    */
    public void LockQuickslotBar(GameObject oCreature, int bLock)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows or hides a floating icon above a creature.
    *
    * Shows or hides a floating icon above a creature.
    *
    * @param oCreature - The creature.
    * @param sIconName - The texture to use (or empty string to hide the icon)
    * @author Henry
    */
    public void ShowFloatyIcon(GameObject oCreature, string sIconName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Notifies the GUI that one of the party members can level up
    *
    * Notifies the GUI that one of the party members can level up.
    * Call again with 0 after the level up has finished.
    *
    * @param oPartyMember - The party member who can level up.
    * @param bCanLevelUp - 1 if the party member can level up, 0 if the
    *   party member can no longer level up
    * @author Henry
    */
    public void SetCanLevelUp(GameObject oPartyMember, int bCanLevelUp = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get whether a creature can level up or not
    *
    * @param oCreature - The creature
    * @author Paul Schultz
    */
    public int GetCanLevelUp(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get autolevelup flag of a creature
    *
    * @param oCreature - The creature
    *
    * 0 = off, 1 = autolevel, 2 = force autolevel
    *
    * @author Jacques Lebrun
    */
    public int GetAutoLevelUp(GameObject oPartyMember)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set autolevelup flag of a creature
    *
    * @param oCreature - The creature
    * @param nAutoLevelUp - The flag
    *
    * 0 = off, 1 = autolevel, 2 = force autolevel
    * 
    * @author Jacques Lebrun
    */
    public void SetAutoLevelUp(GameObject oPartyMember, int nAutoLevelUp)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the maximum attainable level.
    * 
    * @author Jacques Lebrun
    */
    public int GetMaxLevel()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the Chanters GUI (quest board)
    * @author Paul Schultz
    */
    public void ShowChantersGUI(int nBoardID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Indicates if a creature has its weapons unsheathed.
    *
    * Indicates if a creature has its weapons unsheathed.
    *
    * @param oObject - The creature that may have its weapons unsheathed.
    * @author Gabo.
    */
    public int GetWeaponsUnsheathedStatus(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the General Scoreboard GUI
    * @author Henry
    */
    public void ShowGeneralScoreboardGUI()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the Championship Scoreboard GUI
    * @author Henry
    */
    public void ShowChampionshipScoreboardGUI()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the Tournament Scoreboard GUI
    * @author Henry
    */
    public void ShowTournamentScoreboardGUI()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the Party Picker GUI
    * @author Paul Schultz
    */
    public void ShowPartyPickerGUI()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the status of the party picker GUI (button invisible, visible but party picker unusable, etc.)
    *
    * @param nStatus - PP_GUI_STATUS_NO_USE = 0 (not visible on main GUI)
    *                - PP_GUI_STATUS_READ_ONLY = 1 (visible on main GUI, unusable)
    *                - PP_GUI_STATUS_USE = 2 (visible on main GUI, usable)
    * @author Paul Schultz
    */
    public void SetPartyPickerGUIStatus(int nStatus)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the status of the party picker GUI (button invisible, visible but party picker unusable, etc.)
    *
    * @author Paul Schultz
    */
    public int GetPartyPickerGUIStatus()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the disabled state of the indicated GUI Element
    * 
    * @param nGuiElement See GUI_HIGHLIGHT_* defines.
    * @param bEnable Boolean indicating if it should be enabled or disabled
    * @param nHighlightArgs Some highlights require additional information
    */
    public void SetGUIElementEnabled(int nElementID, int bEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Highlight the indicated GUI Element
    * 
    * @param nGuiElement See GUI_HIGHLIGHT_* defines.
    * @param bHIghlighted Boolean indicating if the highlight should be turned on or off
    * @param nHighlightArgs Some highlights require additional information
    */
    public void SetGUIElementHighlighted(int nElementID, int bHighlighted, int nHighlightArgs = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    //If called with <blank, blank, 0> it hides the current tutorial!
    /* @brief Display a tutorial popup with the indicated text.
    * @author John Fedorkiw
    *
    * @param sTitle - The text to be displayed as the title
    * @param sText  - The body text
    * @param nPopupId - The type of tutorial poup to show.  Set to -1 if you wish to hide the currently displayed popup
    * @param fDisplayTime - Amount of time the tutorial should be displayed for. -1 if it shouldn't be hidden automatically
    * @param oObject - The GameObject to signal the callback xEvent to.
    * @param evEvent - The callback xEvent to signal. When the Tutorial displayed is ended (either because the fDisplayTime specified is up or it is interrupted by another call to DisplayTutorial) this xEvent is fired and Float[0] holds the remaining display time (which would only be non-zero if the tutorial was replaced with another tutorial message)
    * @param scriptname - If specified, overides the default script the callback is sent to
    *
    */
    public void DisplayTutorial(string sTitle, string sText, int nPopupID, float fDisplayTime, GameObject oObject, xEvent evEvent, string scriptname = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Display a note popup with the indicated text.
    * @author Michael Hamilton
    *
    * @param sTitle - The text to be displayed as the title
    * @param sText  - The body text
    *
    */
    public void DisplayNote(string sTitle, string sText)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Show a popup
    * @author Jacques Lebrun
    */
    public void ShowPopup(int nMessageStrRef, int nPopupType, GameObject oOwner = null, int bShowInputField = EngineConstants.FALSE, int nDefaultInputStrRef = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds an entry to the General Scoreboard GUI
    *
    * @param sName - Contestant's name
    * @param nVictories - Number of victories
    * @param nDefeats - Number of defeats
    * @param nFatalities - Number of fatalities
    * @param bQualified - 1 if the contestant has qualified for the Championship, 0 otherwise
    * @param bDead - 1 if the contestant is dead, 0 otherwise
    * @author Henry
    */
    public void AddGeneralScoreboardEntry(string sName, int nVictories, int nDefeats, int nFatalities, int bQualified, int bDead)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets an entry in the Championship Scoreboard GUI
    *
    * @param nEntryID - ID of the entry to set (0-15 first round, 16-23 quarterfinals, 24-27 semifinals, 28-29 final, 30 champion)
    * @param sName - Name of contestant
    * @param bWinner - 1 if this contestant won their game, 0 otherwise
    * @param bIsPlayer - 1 if this contestant is the player, 0 otherwise
    * @author Henry
    */
    public void SetChampionshipScoreboardEntry(int nEntryID, string sName, int bWinner, int bIsPlayer)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets an entry in the Tournament Scoreboard GUI
    *
    * @param nEntryID - ID of the entry to set (0-13 first seven rounds, 14 champion)
    * @param sName - Name of contestant
    * @param bDefeated - 1 if the contestant has been defeated, 0 otherwise
    * @param bIsPlayer - 1 if this contestant is the player, 0 otherwise
    * @author Henry
    */
    public void SetTournamentScoreboardEntry(int nEntryID, string sName, int bDefeated, int bIsPlayer)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the Crafting GUI for the given crafting skill
    *
    * @param nCraftingSkillID - ID of the crafting skill (eg. Herbalism, Poison, Traps)
    * @author Henry
    */
    public void ShowCraftingGUI(int nCraftingSkillID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for unlocking a specialization
    *
    * @param nSpecID : ID of the specialization as found in CLA_base.xls
    * @author Paul
    */
    public void ShowSpecUnlockedNotification(int nSpecID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for unlocking an area on the world map
    *
    * @param sAreaName : name of the unlocked area
    * @param sImage : optional image override to show with the notification
    * @author Paul
    */
    public void ShowAreaUnlockedNotification(string sAreaName, string sImage = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for recieving a crafting string
    *
    * @param sResourceName: name of the crafting string
    * @author Michael Webb
    */
    public void ShowCraftingResourceDiscoveredNotification(string sResourceName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes a recipe from the list of recipes the party knows
    *
    * @param nRecipeID	ID of the recipe to be removed
    * @author 		Paul Schultz
    */
    public void RemovePartyCraftingRecipe(int nRecipeID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for recieving a crafting recipe
    *
    * @param sRecipeName: name of the crafting recipe
    * @author Michael Webb
    */
    public void ShowCraftingRecipeAcquiredNotification(string sRecipeName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for a party member being acquired
    *
    * @param oidPartyMember: GameObject ID of the party member
    * @author Michael Webb
    */
    public void ShowPartyMemberNotification(GameObject oidPartyMember)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows a notification for having new mail
    *
    * @author Nicolas
    */
    public void ShowNewMailNotification()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Displays the sub area banner for a fixed amount of time.
    *
    * @author Jacques
    */
    public void ShowSubAreaNotification(string sText)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the movie to be played next time we enter GameModeLoading
     *
     * @author John Fedorkiw
     */
    public void SetNextLoadScreenMovie(string sMovie)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the game completion percentage to be shown by the character record.
    *
    * This currently being calculated as an aggregate of hidden achievements, so it spans
    * games other than the one currently in progress.
    *
    * @param fPercentage : Percentage complete. Clamped to [0, 100] game-side
    * @author Paul Schultz
    */
    public void SetGameCompletionPercentage(float fPercentage)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Update the HP/Pixel of the main hud Health Bar
    *
    * @param fHPPerPixel  Set the HP pixel to be displayed on the Main UI
    * @author John Fedorkiw  
    */
    public void SetHealthBarSize(float fHPPerPixel)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Update the MP/Pixel of the main hud Mana Bar
    *
    * @param fHPPerPixel  Set the MP Per pixel to be displayed on the Main UI
    * @author John Fedorkiw  
    */
    public void SetManaBarSize(float fMPPerPixel)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Toggle the display of the helmet in the portrait on the main HUD
    *
    * @param oCreature The Player Creature to set the helmet in portrait state on
    * @param bShowHelmetInPortrait A boolean indicating if the helmet should be shown in the portrait.
    *
    * @author John Fedorkiw  
    */
    public void ShowHelmetInPortrait(GameObject oCreature, int bShowHelmetInPortrait)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Display the health bar of the specified creature for a brief moment
    *
    * @param oCreature The creature to show the health bar for
    *
    * @author Nicolas Ng Man Sun
    */
    public void ShowHealthFloaty(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the boolean value of the indicated GUI Attribute. Note: In the PC Build of the game you can use the console xCommand "explore" to browse the attribute tree.
    *
    * @param sAttribute The Path to an attribute (e.g. ClientOptions.GameOptions.DemoMode)
    * @author John Fedorkiw  
    */
    public int GetAttributeBool(string sAttribute)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the Int value of the indicated GUI Attribute. Note: In the PC Build of the game you can use the console xCommand "explore" to browse the attribute tree.
    *
    * @param sAttribute The Path to an attribute (e.g. ClientOptions.GameOptions.DifficultyLevel)
    * @author John Fedorkiw  
    */
    public int GetAttributeInt(string sAttribute)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the Float value of the indicated GUI Attribute. Note: In the PC Build of the game you can use the console xCommand "explore" to browse the attribute tree.
    *
    * @param sAttribute The Path to an attribute (e.g. GameModeExplore.CameraPositionX)
    * @author John Fedorkiw  
    */
    public float GetAttributeFloat(string sAttribute)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the String value of the indicated GUI Attribute. Note: In the PC Build of the game you can use the console xCommand "explore" to browse the attribute tree.
    *
    * @param sAttribute The Path to an attribute (e.g. SelectedChar.Name)
    * @author John Fedorkiw  
    */
    public string GetAttributeString(string sAttribute)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /***************************************************************/
    /* @brief Put the game into target request mode. If the user selects a target (or if a valid target is already available) the specified xEvent will be fired back to scripting.
    *
    * @param nTargetType - The type of target to request  (Target types are defined inside targettype.xls). Hostile: 4, AoE: 16
    * @param fAOEParamater1 - This is used to specify the radius of a circular AoE. It should be zero if you only wish to target single objects
    * @param fAOEParamater2 - _NOT CURRENTLY USED_. This should be 0.0f
    * @param nEventID - ID of the xEvent fired to the specified object(or overriden script).  The xEvent returned has an Object[0] value of the creature targeted (if any creature) and a Vector[0] value indicating the position of the target. Note if a creature is targeted the position is the position of the targeted creature.
    * @param oObject - The GameObject to signal the xEvent to.
    * @param scriptname - If specified overides the default script
    *
    * @author John Fedorkiw 
    */
    public void RequestTarget(int nTargetType, float fAOEParamater1, float fAOEParamater2, int eventID, GameObject oObject, string scriptname = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /***************************************************************/

    /***************************************************************/
    // Traps & Locks
    /***************************************************************/

    /* @brief Gets the difficulty value of detecting a particular trap object
* @param   oTrap A trap placeable
* @returns the difficulty value of detecting the trap
*
* @author Gabo
*/
    public int GetTrapDetectionDifficulty(GameObject oTrap)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the difficulty value of detecting a particular trap object
    * @param   oTrap A trap placeable
    * @returns the difficulty value of disarming the trap
    *
    * @author Gabo
    */

    public int GetTrapDisarmDifficulty(GameObject oTrap)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief  Sets the detected state of the trap
    * @param   oTrap A trap placeable
    * @param   bDetected Is this trap detected by the player 
    *
    * @author John
    */
    public void SetTrapDetected(GameObject oTrap, int bDetected)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /***************************************************************/
    // Get Nearest
    /***************************************************************/
    /* @addtogroup get_nearest Get Nearest Functions
    *
    * Functions to find nearest objects in various ways
    */
    /* @{*/

    /* @brief Returns N nearest objects of a specific type
    *
    * Returns N nearest GameObject of a specific type
    *
    * @param * oObject - target Object
    * @param * nObjectType - type for the objects to query for their distance
    * @param * nNumberOfObjects (optional) - Number of objects to return
    * @param * nCheckLiving (optional) - only returns objects if they are alive
    * @param * nCheckPerceived (optional) - only returns objects if they are within the perception radius.
    * @author Adriana
    */
    public List<GameObject> GetNearestObject(GameObject oObject, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, int nNumberOfObjects = 1, int nCheckLiving = 0, int nCheckPerceived = 0, int nIncludeSelf = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief N nearest objects of a specific type to a Location
    *
    * Returns N nearest objects of a specific type to a Location
    *
    * @param * Location - target Location
    * @param * nObjectType - type for the objects to query for their distance
    * @param * nNumberOfObjects (optional) - Number of objects to return
    * @author Adriana
    */
    public List<GameObject> GetNearestObjectToLocation(Vector3 lLocation, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, int nNumberOfObjects = 1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns N nearest GameObject of a specific type, with a specifc tag
    *
    * Returns N nearest GameObject of a specific type, with a specifc tag
    *
    * @param * oObject - target Object
    * @param * nObjectType - type for the objects to query for their distance
    * @param * sTag - Tag for the objects to query
    * @param * nNumberOfObjects (optional) - Number of objects to return
    * @param * nCheckLiving (optional) - only returns objects if they are alive
    * @param * nCheckPerceived (optional) - only returns objects if they are within the perception radius.
    * @author Adriana
    */
    public List<GameObject> GetNearestObjectByTag(GameObject oObject, string sTag, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, int nNumberOfObjects = 1, int nCheckLiving = 0, int nCheckPerceived = 0, int nIncludeSelf = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the one nearest GameObject to the specified object
    *
    * @param * oObject - target Object
    * @param * nIncludeSelf - TRUE if the query should check the target object
    * @param * sTag - Tag for the objects to query
    */
    public GameObject UT_GetNearestObjectByTag(GameObject oObject, string sTag, int nIncludeSelf = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns N nearest GameObject of a specific type, with a specifc Group Id
    *
    * Returns N nearest GameObject of a specific type, with a specifc Group Id
    *
    * @param * oObject - target Object
    * @param * nObjectType - type for the objects to query for their distance
    * @param * nGroupId - Group Id for the objects to query
    * @param * nNumberOfObjects (optional) - Number of objects to return
    * @param * nCheckLiving (optional) - only returns objects if they are alive
    * @param * nCheckPerceived (optional) - only returns objects if they are within the perception radius.
    * @author Adriana
    */
    public List<GameObject> GetNearestObjectByGroup(GameObject oObject, int nGroupId, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, int nNumberOfObjects = 1, int nCheckLiving = 0, int nCheckPerceived = 0, int nIncludeSelf = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns N nearest GameObject of a specific type, with a specifc Hostility
    *
    * Returns N nearest GameObject of a specific type, with a specifc Hostility
    *
    * @param * oObject - target Object
    * @param * nObjectType - type for the objects to query for their distance
    * @param * nHostility - Hostility for the objects to query (true/false)
    * @param * nNumberOfObjects (optional) - Number of objects to return
    * @param * nCheckLiving (optional) - only returns objects if they are alive
    * @param * nCheckPerceived (optional) - only returns objects if they are within the perception radius.
    * @author Adriana
    */
    public List<GameObject> GetNearestObjectByHostility(GameObject oObject, int nHostility, int nObjectType = EngineConstants.OBJECT_TYPE_ALL, int nNumberOfObjects = 1, int nCheckLiving = 0, int nCheckPerceived = 0, int nIncludeSelf = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Indicates if a creature is perceiving any hostiles
    *
    * Indicates if a creature is perceiving any hostiles
    *
    * @param oObject - The creature that may be perceiving hostiles
    * @author Gabo.
    */
    public int IsPerceivingHostiles(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Indicates if the party of a creature is perceiving any hostiles
    *
    * Indicates if the party of a creature is perceiving any hostiles
    *
    * @param oObject - The creature whose party may be perceiving hostiles
    * @author Gabo.
    */
    public int IsPartyPerceivingHostiles(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Makes a creature be perceived as long as its within the outer perception radius
    *
    * This xCommand goes beyond the normal AI perception ring, but will return false
    * if the creature to be perceived is dead, the same as the perceiving creature or
    * beyond the visual radius (about 60m).
    *
    * @param oPerceivingCreature - The creature that will see another creature
    * @param oPerceivingCreature - The creature that will be seen.
    * @author Gabo.
    */
    public int TriggerPerception(GameObject oPerceivingCreature, GameObject oPerceivedCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns an array of hostile creatures
    *
    * Returns an array of hostile creatures
    *
    * @param oCreature - Creature to test against
    * @param obHostile - Filter to only retrieve hostile creatures.
    * @author Adriana
    */
    public List<GameObject> GetPerceivedCreatureList(GameObject oCreature, int bHostile = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns if CreatureA perceives CreatureB
    *
    * Returns true if CreatureB exists on CreatureA's perception list. This is a cheap check
    * against the perception list, it does not perform expensive line of sight checking.
    *
    * Note: Returns FALSE if A or B are not creatures or invalid.
    *
    * @param oidA - Creature who is perceiving
    * @param oidB - Creature who is being perceived (or not)
    * @author Georg Zoeller
    */
    public int IsPerceiving(GameObject oidA, GameObject oidB)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Delete all entries in the perception list of a creature
    *
    * @param oPerceiver - The creature for which the perception list will be reset
    * @author Jose
    */
    public void ClearPerceptionList(GameObject oPerceiver)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Conversation
    /***************************************************************/
    /* @addtogroup conversation Conversation Functions
    *
    * Functions to handle conversations
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SPEAKONELINERCONVERSATION = 437;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETLASTSPEAKER = 439;

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETCUSTOMTOKEN = 441;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_INCONVERSATION = 442;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETCONVERSATIONENTRYPARAMETER = 443;

    // this makes sure an already running ambient dialog triggers it's plot flag action
    public void ClearAmbientDialogs(GameObject oObject)
    {
        Warning("ambience conversations to be implemented!");
        //SetLocalInt(GetModule(), "AMB_SYSTEM_CONVERSATION", 0);
    }

    /* @brief Begins a conversation with the given object
    *
    * If rConversationFile is specified then that file will be used, otherwise
    * the conversation specified on the creature will be used
    *
    * @param * oTarget - The GameObject that will own the conversation
    * @param rConversationFile (optional) - The name of a dlg file to be used (*.con)
    * @author Jon Thompson
    */
    //zDA2 public int BeginConversation(GameObject oTarget, string rConversationFile)//= R""
    public void BeginConversation(GameObject oListener, GameObject oSpeaker, string rDialogFile = "")//zDA:O
    {
        if (rDialogFile == "")
        {
            int nConversation = GetLocalInt(oSpeaker, "ConversationURI");
            rDialogFile = GetResource("ID", nConversation.ToString(), "Name");
        }

        //Set the conversation active in the module and let the conversation engine pick it up when the game states changes
        SetLocalString(GetModule(), "CONVERSATION", rDialogFile);
        WR_SetGameMode(EngineConstants.GM_CONVERSATION);
    }

    /* @brief Starts a slideshow, as used in the DA epilogue
    *
    * @param * rConversation - The conversation with the slideshow information
    *
    * @author Jon Thompson
    */
    public void BeginSlideshow(string rConversation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns true if the GameObject has a conversation assigned to it
    *
    * Returns true if the GameObject has a conversation assigned to it
    *
    * @param * oObject - The GameObject to verify.
    * @author Jon Thompson
    */
    public int HasConversation(GameObject oObject)
    {
        return (GetLocalInt(oObject, "ConversationURI") != 0) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Returns true if the current conversation line is ambient
    *
    * Returns true if the current conversation line is ambient
    *
    * @author Jon Thompson
    */
    public int ConversationIsAmbient()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns true if the given conversation is ambient, false if it isn't or if it cannot be determined.
    *
    * Returns true if the given conversation is ambient, false if it isn't or if it cannot be determined.
    *
    * @author Bryan Derksen
    */
    public int IsConversationAmbient(string rConversation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the PC (if any) involved in the conversation that ran this script
    *
    * @author Jon Thompson
    */
    public GameObject GetPCSpeaker()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Plays a cutscene
    *
    * Plays a cutscene
    *
    * @param rCutscene - The file name of the cutscene.cut file to play.
    * @author Jon Thompson
    */
    public void PlayCutscene(string rCutscene)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Plays a bik movie.
    *
    * Call this to play a movie. If one's already playing, this will queue up after it.
    *
    * @param rMovie - The bik movie file.
    * @author Paul
    */
    public void PlayMovie(string rMovie)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Stores
    /***************************************************************/
    /* @addtogroup stores Stores Functions
    *
    * Functions to manage stores (open, close, set stats)
    */
    /* @{*/

    /* @brief Shows the Store GUI for a specific store
    * @author Henry Smith
    *
    * @param oStore - The store GameObject to display
    */
    public void OpenStore(GameObject oStore)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the money the store has available for buying items (-1 for infinite)
    * @author Henry Smith
    *
    * @param oStore - The store object
    */
    public int GetStoreMoney(GameObject oStore)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the money the store has available for buying items (-1 for infinite)
    * @author Henry Smith
    *
    * @param oStore - The store object
    * @param oStore - The total money (in coppers, -1 for infinite)
    */
    public void SetStoreMoney(GameObject oStore, int nMoney)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the maximum price the store will pay for an item
    * @author Henry Smith
    *
    * @param oStore - The store object
    */
    public int GetStoreMaxBuyPrice(GameObject oStore)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the maximum price the store will pay for an item
    * @author Henry Smith
    *
    * @param oStore - The store object
    * @param nMaxBuyPrice - The max buy price
    */
    public void SetStoreMaxBuyPrice(GameObject oStore, int nMaxBuyPrice)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the store's mark up percentage for selling items
    * @author Henry Smith
    *
    * @param oStore - The store object
    */
    public int GetStoreMarkUp(GameObject oStore)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the store's mark up percentage for selling items
    * @author Henry Smith
    *
    * @param oStore - The store object
    * @param oStore - The mark up percentage (eg. 100 = normal item price, 150 = 150% item price)
    */
    public void SetStoreMarkUp(GameObject oStore, int nMoney)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the store's mark down percentage for buying items
    * @author Henry Smith
    *
    * @param oStore - The store object
    */
    public int GetStoreMarkDown(GameObject oStore)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the store's mark down percentage for buying items
    * @author Henry Smith
    *
    * @param oStore - The store object
    * @param nMarkDown - The mark down percentage (eg. 100 = normal item price, 75 = 75% item price)
    */
    public void SetStoreMarkDown(GameObject oStore, int nMarkDown)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Item upgrading / runes
    /***************************************************************/
    /* @addtogroup stores Item upgrading / runes Functions
    *
    * Functions to manage item upgrading (opening the GUI, any rune-related functions)
    */
    /* @{*/

    /* @brief Shows the Item upgrade GUI
    * @author Paul Schultz
    */
    public void OpenItemUpgradeGUI()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Locations
    /***************************************************************/
    /* @addtogroup locations Locations Functions
    *
    * Functions to manage locations (setting, getting etc')
    */
    /* @{*/

    /* @brief Location GameObject constructor.
    *
    * This function creates a Location GameObject based on the specified parameters.
    *
    * @param oArea - The area of the location
    * @param vPosition - The Vector3 position of the location
    * @param fAngle - The angle degree orientation of the location
    * @author Brenon Holmes
    */
    public Vector3 Location(GameObject oArea, Vector3 vPosition, float fAngle)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Used to test if a specific Vector3 is safe.
    *
    * This function is used to test to see if a Vector3 is valid.
    * It will return TRUE if the specified Vector3 is valid. An invalid location
    * is defined by an invalid area object, an empty position Vector3 and an empty orientation vector.
    *
    * @param lLocation - The Vector3 tested to see if it's valid
    * @returns TRUE on success, FALSE on error
    * @sa IsLocationSafe, GetSafeLocation
    * @author Brenon
    */
    public int IsLocationValid(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Used to test if a specific Vector3 is safe.
    *
    * This function is used to test if a specific Vector3 is safe.
    * A safe Vector3 is a walkable section of terrain that is not occupied by a creature.
    * It should be noted that walkable also means that the specified Vector3 would not
    * be occupied by a placeable object, unless the placeable GameObject had no walkmesh.
    *
    * @param lLocation - The Vector3 tested to see if it's safe
    * @returns TRUE on success, FALSE on error
    * @sa IsLocationValid
    * @warning This function is expensive, and as such should not be called frequently if it can be helped.
    * @author Brenon
    */
    public int IsLocationSafe(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function will compute a new safe Vector3 based on the specified location.
    *
    * This function will compute a new safe Vector3 based on the specified location.
    * If the specified Vector3 is a safe location, then that Vector3 will be returned.
    * A safe Vector3 is a walkable section of terrain that is not occupied by a creature.
    * It should be noted that walkable also means that the specified Vector3 would not be occupied by a placeable object,
    * unless the placeable GameObject had no walkmesh.
    *
    * @param lLocation - The Vector3 to base a new safe Vector3 off of (if necessary)
    * @returns a valid safe location
    * @sa IsLocationSafe
    * @warning This function is expensive, and as such should not be called frequently if it can be helped.
    * @author Brenon
    */
    public Vector3 GetSafeLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the Vector3 of the specified object.
    *
    * This function returns the Vector3 of the specified object.
    * If an invalid GameObject is specified, then an invalid Vector3 will be returned.
    *
    * @param oObject - the GameObject to get the Vector3 of
    * @returns a valid Vector3 on success or invalid Vector3 on error
    * @sa IsLocationValid
    * @author Brenon
    */
    public Vector3 GetLocation(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the Vector3 of the specified object.
    *
    * This function sets the Vector3 of the specified object.
    * If another area is specified, the GameObject will be moved. However it should be
    * noted that moving an GameObject in this manner is inherently unsafe as the target
    * Vector3 might not be a "safe" location.
    *
    * @param oObject - the GameObject to set the Vector3 of
    * @param lLocation - the Vector3 to set the GameObject to
    * @returns a valid Vector3 on success or invalid Vector3 on error
    * @warning Moving an GameObject to another area via this function should probably be discouraged. Use of the JumpToPoint or JumpToObject commands would be much safer.
    * @author Brenon
    */
    public void SetLocation(GameObject oObject, Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the position component of the location..
    *
    * This function returns the position component of the location.
    *
    * @param lLocation - the Vector3 to retrieve the position from
    * @returns a non empty Vector3 on success or an empty Vector3 on failure
    * @author Brenon
    */
    public Vector3 GetPositionFromLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief sets the position of the specified location
    *
    * This function sets the position component of the specified location.
    *
    * @param lLocation - the Vector3 to set the position on
    * @param vPosition - the position to set on the location
    * @returns a valid Vector3 on success or an invalid Vector3 on failure
    * @author Brenon
    */
    public Vector3 SetLocationPosition(Vector3 lLocation, Vector3 vPosition)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief returns the area component of the location
    *
    * This function returns the area component of the location.
    *
    * @param lLocation - the Vector3 to retrieve the area from
    * @returns a valid GameObject on success or an invalid GameObject on failure
    * @author Brenon
    */
    public GameObject GetAreaFromLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief sets the area component of the specefied location.
    *
    * This function sets the area component of the specified location.
    *
    * @param lLocation - The Vector3 to set the area on
    * @param oArea - The area to set on the location
    * @returns a valid Vector3 on success or an invalid Vector3 on failure
    * @author Brenon
    */
    public Vector3 SetLocationArea(Vector3 lLocation, GameObject oArea)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief returns the orientation component of the Vector3 as an absolute degree value.
    *
    * This function returns the orientation component of the Vector3 as an absolute degree value.
    *
    * @param lLocation - the Vector3 to retrieve the facing from
    * @returns An absolute degree angle representing the orientation of the location
    * @author Brenon
    */
    public float GetFacingFromLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief sets the angle orientation component of the specified location.
    *
    * This function sets the angle orientation component of the specified location.
    *
    * @param lLocation - the Vector3 to set the facing to
    * @param fAngle - the angle to set on the location
    * @returns a valid Vector3 on success or an invalid Vector3 on failure
    * @author Brenon
    */
    public Vector3 SetLocationFacing(Vector3 lLocation, float fAngle)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief returns the orientation component of the Vector3 as an orientation vector.
    *
    * This function returns the orientation component of the Vector3 as an orientation vector.
    *
    * @param lLocation - the Vector3 to retrieve the orientation from
    * @returns a non empty Vector3 on success or an empty Vector3 on failure
    * @author Brenon
    */
    public Vector3 GetOrientationFromLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief sets the Vector3 orientation component of the specified location.
    *
    * This function sets the Vector3 orientation component of the specified location.
    *
    * @param lLocation - the Vector3 to set the orientation on
    * @param vOrientation - the Vector3 orientation to set on the location
    * @returns a valid Vector3 on success or an invalid Vector3 on failure
    * @author Brenon
    */
    public Vector3 SetLocationOrientation(Vector3 lLocation, Vector3 vOrientation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Reaction
    /***************************************************************/
    /* @addtogroup reaction Reaction Functions
    *
    * Functions to manage reaction between objects and groups and group management (hostility etc')
    */
    /* @{*/

    /* @brief Returns whether two groups are hostile to each other.
    *
    * This function accesses the hostility tables for the two specified groups and returns whether they are hostile
    * to each other or not. If either of the two group ID's are not valid, the function will return FALSE.
    *
    * Changing group hostility will result in a perception xEvent being refired to all creatures that
    * can perceive a member of the group.
    *
    * @param nGroupA - The source group to use to check the hostility tables
    * @param nGroupB - The target group to use to check the hostility tables
    * @returns Returns TRUE if the two groups are hostile. FALSE otherwise. Returns FALSE on error.
    * @remarks If invalid group ID's are specified, the function will return FALSE
    * @sa SetGroupHostility(), GetGroupId(), SetGroupId()
    * @author Brenon
    */
    public int GetGroupHostility(int nGroupA, int nGroupB)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether two groups are hostile to each other.
    *
    * This function sets the group hostility between the two specified groups. Creatures default to their groupÆs
    * hostility, but this can be overridden with SetReactionOverride(). If two creatures are not hostile to each
    * other they cannot engage in hostile actions with each other in any way, shape or form.
    *
    * @param nGroupA - The source group to use to check the hostility tables
    * @param nGroupB - The target group to use to check the hostility tables
    * @param bHostile - Specifies whether to set the two groups as hostile or not
    * @sa GetGroupHostility(), GetGroupId(), SetGroupId(),SetReactionOverride(), GetReactionOverride()
    * @author Brenon
    */
    public void SetGroupHostility(int nGroupA, int nGroupB, int bHostile)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether two objects are hostile to each other.
    *
    * This function checks the hostility of the two specified objects and returns whether they are hostile to each other or not.
    * If either of the two objects are not valid, the function will return FALSE
    *
    * @param oSource - The source GameObject to use to check the hostility tables
    * @param oTarget - The target GameObject to use to check the hostility tables
    * @returns Returns TRUE if the two objects are hostile to one another, FALSE otherwise. Returns FALSE on error.
    * @sa SetGroupHostility(), GetGroupHostility()
    * @author Brenon
    */
    public int IsObjectHostile(GameObject oSource, GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the group ID of the specified object.
    *
    * This function returns the group ID of the specified object. If the GameObject is invalid, the function will return FALSE.
    *
    * @param oObject - The GameObject to get the group ID of
    * @returns Returns the group ID of the specified object
    * @sa SetGroupId()
    * @author Brenon
    */
    public int GetGroupId(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the group ID of the specified object.
    *
    * This function sets the group ID of the supplied object. It should be noted that any integer value can be used as it is simply an ID.
    *
    * @param oObject - The GameObject to set the group ID of
    * @param nGroupId - The group ID to set the specified GameObject to
    * @sa GetGroupId()
    * @warning Setting negative group ID values will result in the function failing.
    * @author Brenon
    */
    public void SetGroupId(GameObject oObject, int nGroupId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the team ID of the specified object. -1 sets the GameObject to be independent of any team
    *
    * @param oObject - The GameObject to set the team ID of
    * @param nTeamId - The team ID to set the specified GameObject to
    * @sa GetTeamId(), GetTeam()
    * @author Jose
    */
    public void SetTeamId(GameObject oObject, int nTeamId)
    {
        Debug.Log("set teamID");
        oObject.GetComponent<xGameObjectBase>().nTeamId = nTeamId;
    }

    /* @brief Gets the team ID of the specified object. -1 means that the GameObject is independent of any team
    *
    * @param oObject - The GameObject to get the team ID of
    * @sa SetTeamId(), GetTeam()
    * @author Jose
    */
    public int GetTeamId(GameObject oObject)
    {
        Debug.Log("get team ID");
        return oObject.GetComponent<xGameObjectBase>().nTeamId;

    }

    /* @brief Gets the team members given a team ID
    *
    * @param nTeamId - The team ID to get the members of
    * @param nMembersType - The type of members (Creatures or Placeables. EngineConstants.OBJECT_TYPE_ALL is not supported)
    * @sa SetTeamId(), GetTeamId()
    * @author Jose
    */
    public List<GameObject> GetTeam(int nTeamId, int nMembersType = EngineConstants.OBJECT_TYPE_CREATURE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the encounter ID of the specified object. 0 sets the GameObject to be independent of any encounter
    *
    * @param oObject - The creature to set the encounter ID of
    * @param nEncounterId - The encounter ID to set the specified creature to
    * @sa GetEncounterId(), GetEncounter()
    * @author Nicolas
    */
    public void SetEncounterId(GameObject oObject, int nEncounterId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the encounter ID of the specified creature. 0 means that the GameObject is independent of any encounter
    *
    * @param oObject - The creature to get the encounter ID of
    * @sa SetEncounterId(), GetEncounter()
    * @author Nicolas
    */
    public int GetEncounterId(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the encounter members given a encounter ID
    *
    * @param nEncounterId - The encounter ID to get the members of
    * @param nMembersType - The type of members (Creatures or Placeables. EngineConstants.OBJECT_TYPE_ALL is not supported)
    * @sa SetEncounterId(), GetEncounterId()
    * @author Nicolas
    */
    public List<GameObject> GetEncounter(int nEncounterId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Overrides the reaction of the specified object.
    *
    * This function overrides the reaction of the specified object. That means that the GameObject will be hostile or non-hostile (depending
    * on what is specified) regardless of what their group hostility is. If an GameObject is overriden to non-hostile, it will no longer be possible
    * for it to engage or anyone to engage with it in hostile actions in any way, shape or form.
    *
    * Changing a creature to a different group will cause a perception xEvent to be refired.
    *
    * @param oObject - The GameObject to override the reaction of
    * @param bHostile - Specifies whether to override the reaction of the GameObject to hostile or non-hostile.
    * @sa ResetReaction()
    * @author Brenon
    */
    public void SetReactionOverride(GameObject oObject, int bHostile)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Resets the reaction of the specified object.
    *
    * This function resets the reaction of the specified object, returning it to whatever base group hostility it should be at.
    * This function is normally used in conjunction with SetReactionOverride() to return an GameObject to their group's hostility.
    *
    * @param oObject - The GameObject to reset the reaction on
    * @sa SetReactionOverride()
    * @author Brenon
    */
    public void ResetReaction(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Sounds
    /***************************************************************/
    /* @addtogroup sounds Sounds Functions
    *
    * Functions to handle game sounds and music
    */
    /* @{*/

    /* @brief SetSoundSet
    * (core, for use in character generation only)
    * This is intended for use on the player only and will generate undesireable results
    * for any soundset not actually exported from the toolset.
    *
    * @param oTarget 
    * @param rSoundSetConv - Conversation file
    * @author Georg
    */
    public void SetSoundSet(GameObject oTarget, string sSoundSet)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Play a sound on a specified object
    *
    *
    * @param oTarget 
    * @param sSoundEventName
    * @author Marek
    */
    public void PlaySound(GameObject oTarget, string sSoundEventName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Stop sounds
    *
    *
    * @param sSoundEventName
    * @author Marek
    */
    public void StopSound(string sSoundEventName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Play music
    *
    *
    * @param sMusicName
    * @author Marek
    */
    public void PlayMusic(string sMusicName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Stop specified or all music
    *
    *
    * @param sMusicName
    * @author Marek
    */
    public void StopMusic(string sMusicName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set music intesity (set cue in music theme)
    *
    *
    * @param nIntensity
    * @author Marek
    */
    public void SetMusicIntensity(int nIntensity)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enable/disable sound by tag
    *
    *
    * @param sSoundTag
    * @param nActivate
    * @author Marek
    */
    public void ActivateSoundByTag(string sSoundTag, int nActivate)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Overwrite music volume state
    *
    *
    * @param sMusicVolumeTag
    * @param nMusicState
    * @author Marek
    */
    public void SetMusicVolumeStateByTag(string sMusicVolumeTag, int nMusicState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Play a voiceset entry on the specified object
    *
    * @param oTarget 
    * @param nSoundSetId - entry type from ss_types.xls
    * @param fProbabilityOverride - force a probability.
    * @author Georg
    */
    public void PlaySoundSet(GameObject oTarget, int nSoundSetEntry, float fProbabilityOverride = 0.0f)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Play a sound object
    *
    * @param oSound
    * @author Yuri Leontiev
    */
    public void PlaySoundObject(GameObject oSound)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Stop playing a sound object
    *
    * @param oSound
    * @author Yuri Leontiev
    */
    public void StopSoundObject(GameObject oSound)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets an audio parameter on a specific game object
    *
    * @param oTarget The GameObject to apply the RTPC on
    * @param sParameterName The name of the parameter to set
    * @param fValue The value to set the parameter to
    * @author Andrew Butcher
    */
    public void SetAudioGameParameter(GameObject oTarget, string sParameterName, float fValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets a global audio parameter
    *
    * @param sParameterName The name of the parameter to set
    * @param fValue The value to set the parameter to
    * @author Andrew Butcher
    */
    public void SetAudioGlobalGameParameter(string sParameterName, float fValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Players
    /***************************************************************/
    /* @addtogroup players Players Functions
    *
    * Functions to handle player and party actions (autosaves, banning etc')
    */
    /* @{*/

    /* @brief Returns true if the creature is the main character
    *
    *
    * @param oObject - The GameObject to test if it is the main character
    * @author Sam
    */
    public int IsHero(GameObject oCreature)
    {
        //return (oCreature.GetComponent<xGameObjectUTC>().Tag == "Player") ? EngineConstants.TRUE : EngineConstants.FALSE;
        //return (GetLocalString(oCreature, "Tag") == "Player") ? EngineConstants.TRUE : EngineConstants.FALSE;
        return (oCreature == xGameObjectMOD.instance.oHero) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Returns the hero player
    *
    *
    * @author Jacques
    */
    public GameObject GetHero()
    {
        return xGameObjectMOD.instance.oHero;
    }

    /* @brief Returns the currently main controlled party member. 
    *
    * The main controlled party member is the one the player controls its movement directly, and the
    * one whose quickbar currently appears on the screen.
    *
    * @author Jacques 
    */
    public GameObject GetMainControlled()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the GameObject for the party
    *
    *
    * @param oObject - Returns the GameObject for the party
    * @author Adriana
    */
    public List<GameObject> GetParty(GameObject oCreature)
    {
        return GetPartyPoolList();
    }

    /* @brief Performs the AutoSave functionality
    *
    * Performs the AutoSave functionality
    *
    * @param nSaveType - 1=Triggered autosave, 2=Beginning-of-Act autosave, 3=Area transition autosave
    * @author Gavin, David Robinson
    */
    public void DoAutoSave(int nSaveType = 1, string sSaveName = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ISGM = 501;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ISPLAYERVALID = 503;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETPLAYERPUBLICCDKEY = 505;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETPLAYERIPADDRESS = 506;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETPLAYERNAME = 507;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETSTARTLOCATION = 508;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_KICKPLAYER = 509;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_BANPLAYER = 510;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_UNBANPLAYER = 511;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ENDGAME = 512;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SAVEGAME = 514;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_EXPORTCHARACTER = 515;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_EXPORTALLCHARACTERS = 516;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_EXPLOREAREA = 517;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_UNEXPLOREAREA = 518;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_DISABLEMINIMAP = 519;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_EXPLOREOBJECTRADIUS = 520;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_UNEXPLOREOBJECTRADIUS = 521;

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Spells / Combat
    /***************************************************************/
    /* @addtogroup spells_combat Spells and Combat Functions
    *
    * Functions to handle spell and combat status on objects
    */
    /* @{*/

    /* @brief This function gets the combat target of a creature 
    *
    * This function gets the combat target of a creature. This target is set when using an attack or 
    * ability xCommand and is cleared (to target invalid) when combat state is set to false.
    *
    * @param oCreature - the creature whose combat target we are querying
    * @returns the id of the creature's target. Will be invalid if the creature has no target.
    * @sa GetAttackTarget
    * @author Gabo
    */
    public GameObject GetAttackTarget(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the weapon style used by a creature
    *
    * @param oCreature - the creature whose weapon style we are querying
    * @returns the weapon style (0 - none, 1 - single (with or without shield), 2 - dual, 3 - two handed)
    * @sa GetWeaponStyle
    * @author Gabo
    */
    public int GetWeaponStyle(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETSPELLTARGETOBJECT = 523;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETSPELLTARGETLOCATION = 524;

    /* @brief This function retrieves a damage xEffect associated with an attack
    *
    * @param oAttacker - the creature whose attack damage xEffect we are retrieving
    * @param nDamageEffectId - the id of the damage xEffect to get (this id is passed in the EngineConstants.ATTACK_IMPACT event)
    * @author Jose
    */
    public xEffect GetAttackImpactDamageEffect(GameObject oAttacker, int nDamageEffectId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function sets the results of an attack 
    *
    * @param oAttacker - the creature whose attack result we are storing
    * @param nResult1 - the main attack result (HIT/MISS/ETC)
    * @param eDamageEffect1 - the main attack damage effect
    * @param nResult2 - the offhand attack result (HIT/MISS/ETC)
    * @param eDamageEffect2 - the offhand attack damage effect
    * @author Jose
    */
    public void SetAttackResult(GameObject oAttacker, int nResult1, xEffect eDamageEffect1, int nResult2, xEffect eDamageEffect2)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function sets the result for an ability
    *
    * @param oUser - the creature whose ability result we are reporting
    * @param nProjectileTarget - request a specific target node for projectile based abilities
    * @author Jose
    */
    public void SetAbilityResult(GameObject oUser, int nProjectileTarget = EngineConstants.PROJECTILE_TARGET_INVALID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function sets the Combat State on a creature 
    * (core function)
    * This function sets the Combat State on a creature. 
    * ** If you are not Georg and you are using this function, you're in trouble!**
    *
    * @param oCreature - the creature whose combat state we are setting
    * @param nCombatState - the combat state (TRUE or FALSE)
    * @param nInstantEquipWeapon - if TRUE don't play enter/exit animations, 
    * just pop weapons in or out of the creature's hands
    * @returns 0
    * @sa GetCombatState
    * @author Jose
    */
    public void SetCombatState(GameObject oCreature, int nCombatState, int nInstantEquipWeapon = EngineConstants.FALSE)
    {
        if (nInstantEquipWeapon == EngineConstants.TRUE) throw new NotImplementedException();
        SetLocalInt(oCreature, "COMBAT_STATE", nCombatState);
    }

    /* @brief This function gets the Combat State of a creature 
    *
    * This function gets the Combat State of a creature 
    *
    * @param oCreature - the creature whose combat state we are querying
    * @returns TRUE if the creature is in combat, FALSE otherwise
    * @sa SetCombatState
    * @author Jose
    */
    public int GetCombatState(GameObject oCreature)
    {
        return GetLocalInt(oCreature, "COMBAT_STATE");
    }

    /* @brief Returns the rank of a creature 
    *
    * Returns the CreatureRank of a creature, representing its relative combat difficulty.
    *
    * @param oCreature - The creature
    * @returns The EngineConstants.CREATURE_RANK_* constant associated with the creature.
    * @author Georg
    */
    public int GetCreatureRank(GameObject oCreature)
    {
        return GetLocalInt(oCreature, "Rank");
    }

    /* @brief Sets the rank of a creature.
    *
    * Sets a creature's rank, representing its relative combat difficulty.
    *
    * @param oCreature - The creature.
    * @param nRank - The new rank (EngineConstants.CREATURE_RANK_*).
    */
    public void SetCreatureRank(GameObject oCreature, int nRank)
    {
        SetLocalInt(oCreature, "Rank", nRank);
    }

    /* @brief Returns the Creature Type (aka Combatant Type) of a creature
    *
    * The function returns the creature type index from (creaturetypes.xls) as defined
    * in the toolset.
    *
    * @param oidCreature - The creature
    * @returns Index into creaturetypes.xls (EngineConstants.CREATURE_TYPE_* constant)
    * @author Georg
    */
    public int GetCombatantType(GameObject oidCreature)
    {
        return GetLocalInt(oidCreature, "Combatant");
    }

    /* @brief This function returns if a creature is allowed to die permanently 
    *
    * This function returns the value set for the 'NoPermDeath' field in the toolset.
    * This is usually used to prevent deathblows or other methods of permanent destruction
    * from affecting plot important creatures in cases where using the Plot flag is not
    * an option. It always returns FALSE for members of the player's party.
    *
    * @param oidCreature - The creature
    * @returns Whether or not it is allowed to kill the creature permanently.
    * @author Georg
    */
    public int GetCanDiePermanently(GameObject oidCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows the death screen.
    *
    * Shows the death screen, indicating if the player is captured on death (which gives a different GUI screen)
    *
    * @param bCaptured - TRUE if player should be captured on death, FALSE otherwise
    */
    public void ShowDeathScreen(int bCaptured)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the death hint for the death screen.
    *
    * The death hint from the deathhints 2DA will be shown the next time the death screen is shown. The death hint is
    * cleared after the death screen is hidden. If this is not called before showing the death screen, no hint will be
    * displayed.
    *
    * @param nLoadHint The ID of the hint in the 2DA file.
    * @param n2DAReference The ID of the 2DA (272 for the random hint, 205 for the scripted hint). Default is 205.
    *
    * @author Jacques
    */
    public void SetDeathHint(int nDeathHint, int n2DAReference = 205)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the load hint for the loadscreen.
    *
    * The load hint from the loadhints 2DA will be shown on the next area list transition. After the transition
    * the hint is cleared. If this is not called before an area list transition, no hint will be shown. By default
    * the "story so far" text is displayed when loading from a saved game.
    *
    * @param nLoadHint The ID of the hint in the 2DA file.
    * @param n2DAReference The ID of the 2DA (271 for the random hint, 206 for the scripted hint). Default is 206.
    *
    * @author Jacques
    */
    public void SetLoadHint(int nLoadHint, int n2DAReference = 206)
    {
        //int _strref = GetM2DAInt(n2DAReference, "STRREF", nLoadHint);
        SetLocalInt(GetModule(), EngineConstants.AREA_LOAD_HINT, nLoadHint);
    }

    /* @brief Overrides the image for the next area transition.
    *
    */
    public void SetLoadImage(string sLoadImage)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set if the creature can be controlled by the user when they are in
    *          the party.
    *          If the primary controlled creature is set to uncontrollable, the next
    *          follower in the party will be set as the primay controlled creature.
    * @author Jacques
    */
    public void SetControllable(GameObject oFollower, int nIsControllable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Changes the primary controlled creature if they are in the active party.
    *          Calling this implicitly makes that creature controllable if it was
    *          previously set as uncontrollable.
    * @author Jacuqes
    */
    public void SetPrimaryControlled(GameObject oFollower)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function gets the Weapon Armor Penetration from the Combat Interaction data on oAttacker 
    *
    * This function gets the Weapon Armor Penetration from the Combat Interaction data on oAttacker 
    *
    * @param oAttacker - the creature whose combat interaction data we are querying 
    * @param oTArget - the target of the attack
    * @param nRightHandWeapon - TRUE by default, set to FALSE to return the left hand weapon value
    * @returns Returns the Weapon Armor Penetration from the combat interaction data of oAttacker
    * @sa GetWeaponArmorPenetration
    * @author Sophia
    */
    public float GetWeaponArmorPenetration(GameObject oAttacker, GameObject oTarget, int nRightHandWeapon = EngineConstants.TRUE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Spawn the creature's body bag
    *
    * Creates and populates the creature's body bag (only if creature is dead and lootable).
    *
    * @param oCreature - the creature
    * @author JamesG
    */
    public void SpawnBodyBag(GameObject oCreature, int bForce = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the time after which all dead creatures decay into lightweight placeables
    *
    * @param nMilliSec - Max time in milliseconds
    * @author Nicolas Ng Man Sun
    */
    public void SetCreaturesGlobalMaxTimeBeforeDecay(int nMilliSec)
    {
        //xGameObjectMOD.instance.GetComponent<xGameObjectMOD>().GLOBAL_MAX_TIME_BEFORE_DECAY = nMilliSec;
        SetLocalInt(GetModule(), "GLOBAL_MAX_TIME_BEFORE_DECAY", nMilliSec);
    }

    /* @brief Will force the creature to start decay after the specified time.
    *
    * @param oBodybag - placeable bodybag GameObject id (NOT the dead creature's oid)
    * @param nMilliSec - delay time in milliseconds (zero for immediate decay)
    * @author Nicolas Ng Man Sun
    */
    public void SetBodybagDecayDelay(GameObject oBodybag, int nMilliSec)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @ (chargen) brief Clears all abilities of a creature. 
    *
    * *** CHARACTER CREATION ONLY ***
    * Do not ever call outside of character generation, it will destroy a player's character.
    *
    * NOTE: item abilities will not be cleared. To clear an item ability, the item that added it must be removed.
    *
    * @param oCreature - the creature to clear the ability list on
    * @param nAbilityType - The ability list to be cleared (EngineConstants.ABILITY_TYPE_INVALID will clear all lists)
    * @author Georg/Gabo
    */
    public void CharGen_ClearAbilityList(GameObject oCreature, int nAbilityType = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enables/Disables the weapon trail for a creature
    *
    * Enables/Disables the weapon trail for a creature.
    *
    * @param oCreature - the creature
    * @param bEnable - TRUE/FALSE
    * @param nTypeID - Weapon Trail ID from WT_** 2da
    * @param fFinishTime - If disabling, how long to fade out the trail
    * @author Adriana Lopez
    */
    public void EnableWeaponTrail(GameObject oCreature, int bEnable, int nTypeID = 0, float fFinishTime = 1.0f)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Game Modes
    /***************************************************************/
    /* @addtogroup game_modes Game Modes
    *
    * Functions to control the game mode
    */
    /* @{*/

    /* @brief This function gets the current game mode.
    *
    * This function returns the current game mode for a specific player. A game mode can be combat, explore, conversation etc'
    *
    * @returns a EngineConstants.GM_* var on success, EngineConstants.GM_INVALID on error.
    * @sa SetGameMode
    * @author Jose
    */
    public int GetGameMode()
    {
        return GetLocalInt(GetModule(), "GAME_MODE");
    }

    /* @brief This function sets the current game mode.
    *
    * This function sets the current game mode for a specific player. 
    * A game mode can be combat, explore, conversation etc'
    *
    * @param nMode - the mode to set the game to: EngineConstants.GM_*
    * @sa GetGameMode
    * @author Jose
    */
    public void SetGameMode(int nMode)
    {
        //UpdateGameObjectProperty(GetModule(), "GAME_MODE", nMode.ToString());
        xEvent ev = Event(EngineConstants.EVENT_TYPE_GAMEMODE_CHANGE);
        SetEventIntegerRef(ref ev, 0, nMode);//New desired game mode
        SetEventIntegerRef(ref ev, 1, GetLocalInt(GetModule(), "GAME_MODE"));//Current-old Game mode
        SignalEvent(GetModule(), ev);
    }

    /* @brief This function unloads the current module and puts the game in pregame mode.sets the current game mode.
    *
    * This function unloads the current module and puts the game in pregame mode.
    * 
    * @param bShowCredits TRUE to show credits, FALSE to show start menu.
    *
    * @author Gavin Burt
    */
    public void ShowStartMenu(int bShowCredits = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function toggles game pause on/off.
    *
    * This function toggles the game paused on or off
    *
    * @sa ToggleGamePause
    * @param bPause - TRUE to pause, FALSE to unpause. 
    * @author EricP
    */
    public void ToggleGamePause(int bPause = EngineConstants.TRUE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function retrieves the auto-pause game option.
    *
    * This function retrieves the auto-pause game option.
    *
    * @sa GetAutoPauseCombatStatus
    * @author EricP
    */
    public int GetAutoPauseCombatStatus()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Abilities
    /***************************************************************/
    /* @addtogroup abilities Abilities
    *
    * Functions to control abilities
    */
    /* @{*/

    /* @brief This function activates a modal ability in a creature
    *
    * This function tells the engine to activate or deactivate 
    * the GUI indicator for a modal ability on a creature. 
    * Designers: Use ability_h.AbilitY_SetModalAbility instead.
    * 
    * @param oCreature - the creature in which to set the modal ability 
    * @param nAbilityId - modal ability to set
    * @param nStatus - modal ability enabled status (TRUE/FALSE)
    * @sa IsModalAbilityActive
    * @author Jose
    */
    public void Engine_SetModalAbilityGUI(GameObject oCreature, int nAbilityId, int nStatus)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Check if a modal ability is active in a creature
    *
    * Check if a modal ability is active in a creature.
    *
    * @param oCreature - the creature in which to check the modal ability 
    * @param nAbilityId - modal ability to check if it's active
    * @returns 1 if active. 0 if inactive.
    * @sa SetModalAbility
    * @author Jose
    */
    public int IsModalAbilityActive(GameObject oCreature, int nAbilityId)
    {
        int nAbility = oCreature.GetComponent<xGameObjectUTC>().oAbilitiesActive.FirstOrDefault(x => x == nAbilityId);
        if (nAbility != 0)
        {
            return (GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "usetype", nAbility) == 2) ? EngineConstants.TRUE : EngineConstants.FALSE;
        }
        return EngineConstants.FALSE;
    }

    /* @brief Set the cooldown for an ability. This is the initial value. The engine timer will automaticaly decrease the cooldown until it reaches zero.
    *
    * @param oCreature - owner of the ability
    * @param nAbilityId - ability to set the cooldown
    * @param fCooldownTime - time that the ability should be inactive (in seconds)
    * @param sSourceItemTag - if an item ability, specify the specific item providing the ability.  If an empty string, the engine will grab the first item with this ability, this may not be the desired intention if the player has several items with the same ability.
    * @sa GetRemainingCooldown
    * @author Jose
    */
    public void SetCooldown(GameObject oCreature, int nAbilityId, float fCooldownTime, string sSourceItemTag = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the remaining time for an ability to be used
    *
    * @param oCreature - owner of the ability
    * @param nAbilityId - ability to check the cooldown
    * @param sSourceItemTag - if an item ability, specify the specific item providing the ability.  If an empty string, the engine will grab the first item with this ability, this may not be the desired intention if the player has several items with the same ability.
    * @returns 0.0f if the ability is ready to be used again
    * @sa SetCooldown
    * @author Jose
    */
    public float GetRemainingCooldown(GameObject oCreature, int nAbilityId, string sSourceItemTag = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get a list of abilities that need to be turned off due to a condition change
    *
    * @param oCreature - owner of the abilities
    * @param nConditions - Bitmap of conditions that have changed
    * @returns an array of abilities
    * @author Gabo
    */
    public List<int> GetConditionedAbilities(GameObject oCreature, int nConditions)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get a list of abilities that need to be turned off due to a condition change
    *
    * The conditions parameter is to optimize the process a little bit. If a specific condition mask is passed,
    * the engine will only check for that condition. If the default value is used, the engine will check all 
    * conditions that the abilities on the creature has to have. The conditions for an ability are specified in a column of the 
    * same name in the EngineConstants.ABI_base.
    *
    * @param oCreature - owner of the abilities
    * @param nAbility - The ability in question
    * @param nConditions - A mask to tell the engine which conditions are being checked for (default is 0xFFFFFF, all conditions)
    * @returns TRUE if the ability can be used
    * @author Gabo
    */
    public int CanUseConditionedAbility(GameObject oCreature, int nAbility, int nConditions = 2147483647)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether the creature can use an ability.  This is used only as a response
    *          to the EngineConstants.EVENT_TYPE_ABILITY_ONTEST_USABLE event. 
    *
    * @author Nicolas Ng Man Sun
    */
    public int SetCanUseAbility(GameObject oCreature, int nConditions)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Commands
    /***************************************************************/
    /* @addtogroup commands Commands Functions
    *
    * Functions to handle the commands of the action queue
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMAND = 531;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ISCOMMANDVALID = 532;

    /* @brief This function adds the specified xCommand to the object.
    *
    * This function adds the specified xCommand to the GameObject xCommand queue. 
    * The xCommand can be added to the back of the queue or to the front, and can also be
    * flagged as (must-finish command).
    *
    * @param oObject - the GameObject to add the specified command
    * @param cCommand - the xCommand to add to the object
    * @param bAddToFront - specifies if the xCommand should be added to the front of the queue or not
    * @param bStatic - whether or not the xCommand will be added as a command. 
    * Static commands are flagged in a special manner and cannot be removed via 
    * regular clearing functionality. An override must specifically be specified to remove commands. 
    * As such, commands should only be specified as if they absolutely must finish.
    * @param nOverrideAddBehavior - replace the default add behavior by specifying a new behavior here
    * @returns TRUE on success, FALSE on failure In DA2, in DAO is void
    * @remarks Any duplicate commands in the queue that are adjacent to one another are deleted.
    * @author Brenon
    */
    public void AddCommand(GameObject oObject, xCommand cCommand,
        int bAddToFront = EngineConstants.FALSE, int bStatic = EngineConstants.FALSE,
        int nOverrideAddBehavior = -1)
    {
        cCommand.bStatic = bStatic;

        //If current command is not invalid, then added to queue
        if (oObject.GetComponent<xGameObjectBase>().cCommand.nType != EngineConstants.COMMAND_TYPE_INVALID)
        {
            if (cCommand.nType != EngineConstants.COMMAND_TYPE_INVALID)
            {
                if (bAddToFront == EngineConstants.FALSE)
                {
                    oObject.GetComponent<xGameObjectBase>().qCommand.Add(cCommand);
                }
                else //insert in front of queue
                {
                    oObject.GetComponent<xGameObjectBase>().qCommand.Insert(0, cCommand);
                }
            }
        }
        else //There is no current command, skip the queue and set it directly as current command
        {
            if (cCommand.nType != EngineConstants.COMMAND_TYPE_INVALID)
            {
                oObject.GetComponent<xGameObjectBase>().cCommand = cCommand;
            }
        }
    }

    /* @brief Remove a xCommand from the xCommand queue by the specified index. 
    * Excludes the currently active command.
    *
    * @param oObject - the GameObject in which to remove the xCommand from
    * @param nIndex - the index in the xCommand queue from which to remove the command.
    * @author Jacques Lebrun
    */
    public void RemoveCommandByIndex(GameObject oObject, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the result of a command.
    *
    * @param oObject - the GameObject in which to set the result of the command
    * @param nResult - the result of the xCommand (success, failure)
    * @author Jose
    */
    public void SetCommandResult(GameObject oObject, int nResult)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes a specific xCommand from an objects xCommand queue
    *
    * @param oObject
    * @param cCommand
    * @author Sam
    */
    public void RemoveCommand(GameObject oObject, xCommand cCommand)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_INSERTCOMMAND = 537;

    /* @brief This function clears the xCommand list for a given object
    *
    * This function clears the xCommand list for a given object. Note that this also clear
    * the currently executed xCommand which is outside of the queue.
    * 
    * @param oObject - the GameObject on which to clear the xCommand list
    * @param nHardClear - specifies if the GameObject can finish the action in progress or not (Hard = don't wait)
    * @returns TRUE on success, FALSE on failure
    * @remarks Designers should NOT use this function, but use the WR_ClearAllCommands
    * wrapper function instead, defined in wrappers_h
    * @author Sam, Jose
    */
    public int ClearAllCommands(GameObject oObject, int nHardClear = EngineConstants.TRUE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function clears all ambient conversation for a given GameObject or for all objects
    *
    * This function clears all ambient conversations for a given GameObject or for all objects
    * if OBJECT_INVALID is passed in
    * 
    * @param oObject - the GameObject on which to clear ambient conversations or OBJECT_INVALID to clear all
    * @returns TRUE on success, FALSE on failure
    * @author Yuri Leontiev
    */
    public int ClearAmbientConversations(GameObject oObject = null)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function returns the size of an GameObject xCommand queue, 
    * note that the currently active xCommand doesn't belong in the queue.
    *
    * This function returns the size of an GameObject xCommand queue, note that the currently active xCommand doesn't belong in the queue.
    *
    *   @param oObject - returns the size of this objects xCommand queue
    *   @returns int
    *   @author Sam
    */
    public int GetCommandQueueSize(GameObject oObject)
    {
        return oObject.GetComponent<xGameObjectBase>().qCommand.Count;
    }

    /* @brief This function returns the previously processed xCommand for the specified object
    *
    *   @returns xCommand - the previous command, use GetCommandType to see if it is 
    * EngineConstants.COMMAND_INVALID
    *   @author Jose
    */
    public xCommand GetPreviousCommand(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function returns current xCommand for the specified object
    *
    * The 'current' xCommand is the xCommand that is currently being executed. It is considered
    * outside of the xCommand queue.
    *
    *   @returns xCommand - the current command, use GetCommandType to see if it is EngineConstants.COMMAND_INVALID
    *   @author Sam
    */
    public xCommand GetCurrentCommand(GameObject oObject)
    {
        return oObject.GetComponent<xGameObjectBase>().cCommand;
    }

    /* @brief Returns whether the specified AI xCommand was added by the player (as a result of a mouse click or keyboard action).
    *
    *   @returns true if player issued
    *   @author Nicolas Ng Man Sun
    */
    public int GetCommandIsPlayerIssued(xCommand oNode)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function clears the current xCommand for the specified object
    *
    * The 'current' xCommand is the xCommand that is currently being executed. It is considered
    * outside of the xCommand queue.
    * 
    *   @returns void
    *   @author Sam
    */
    public void ClearCurrentCommand(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xCommand at the specified index in the xCommand queue.
    *
    * Index '0' is the xCommand at the top of the queue, but not being executed yet.
    *It's
    *   @returns xCommand - the xCommand at the specified index
    *   @param oObject - the GameObject to get the xCommand from
    *   @param nIndex - the index to get the xCommand from
    *   @author Sam
    */
    public xCommand GetCommandByIndex(GameObject oObject, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xCommand type.
    *
    * Returns the xCommand type.
    *
    *   @returns int - the type of command.
    *   @param cCommand - The command.
    *   @author Jacques Lebrun
    */
    public int GetCommandType(xCommand cCommand)
    {
        return cCommand.nType;
    }

    /* @brief Returns the xCommand priority. (DEPRECATED)
    *
    * Returns the xCommand priority.
    *
    *   @returns int - the priority of the command.
    *   @param cCommand - The command.
    *   @author Jose
    */
    public int GetCommandPriority(xCommand cCommand)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETCOMMANDTYPE = 542;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETCOMMANDID = 543;

    /* @brief This returns an integer associated with the specified command
    *
    *   @returns int
    *   @param cCommand - The command
    *   @param nIndex - The nth integer requested. Defaults to 0
    *   @author Sam
    */
    public int GetCommandIntRef(ref xCommand cCommand, int nIndex = 0)
    {
        return cCommand.nList.ElementAt(nIndex);
    }

    /* @brief This sets an integer associated with the specified command
    *
    *   @returns VOID
    *   @param cCommand - The command
    *   @param nCommandInt - The integer being set on the command
    *   @param nIndex - The nth integer being set. Defaults to 0
    *   @author Sam
    */
    public void SetCommandIntRef(ref xCommand cCommand, int nCommandInt, int nIndex = 0)
    {
        cCommand.nList.Insert(nIndex, nCommandInt);
    }

    /* @brief This returns a float associated with the specified command
    *
    *   @returns Float
    *   @param cCommand - The command
    *   @param nIndex - The nth float requested. Defaults to 0
    *   @author Gabo
    */
    public float GetCommandFloatRef(ref xCommand cCommand, int nIndex = 0)
    {
        return cCommand.fList.ElementAt(nIndex);
    }

    /* @brief This sets a float associated with the specified command
    *
    *   @returns VOID
    *   @param cCommand - The command
    *   @param nCommandFloat - The float being set on the command
    *   @param nIndex - The nth float being set. Defaults to 0
    *   @author Gabo
    */
    public void SetCommandFloatRef(ref xCommand cCommand, float nCommandFloat, int nIndex = 0)
    {
        cCommand.fList.Insert(nIndex, nCommandFloat);
    }

    /* @brief This returns an GameObject associated with the specified command
    *
    *   @returns object
    *   @param cCommand - The command
    *   @param nIndex - The nth GameObject requested. Defaults to 0
    *   @author Jose
    */
    public GameObject GetCommandObjectRef(ref xCommand cCommand, int nIndex = 0)
    {
        return cCommand.oList.ElementAt(nIndex);
    }

    /* @brief This sets an GameObject associated with the specified command
    *
    *   @returns VOID
    *   @param cCommand - The command
    *   @param nCommandObject - The GameObject being set on the command
    *   @param nIndex - The nth GameObject being set. Defaults to 0
    *   @author Jose
    */
    public void SetCommandObjectRef(ref xCommand cCommand, GameObject nCommandObject, int nIndex = 0)
    {
        cCommand.oList.Insert(nIndex, nCommandObject);
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETCOMMANDBOOL = 548;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETCOMMANDBOOL = 549;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETCOMMANDSTRING = 552;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETCOMMANDSTRING = 553;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETCOMMANDVECTOR = 554;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETCOMMANDVECTOR = 555;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDRANDOMWALK = 558;

    /* @brief This function is a move to Vector3 xCommand constructor. The GameObject executing the xCommand will use both the position and orientation of the location.
    *
    * This function is a move to Vector3 xCommand constructor.
    * It creates a move to Vector3 xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt to
    * move the creature to the specified location.
    *
    * @param lLocation - the Vector3 the xCommand should move the creature to
    * @param bRunToLocation - specifies whether the GameObject should run or not
    * @param bDeactivateAtEnd - deactivate the GameObject at the end of the movement. Guaranteed to happen even if the movement doesn't complete
    * @returns a valid command
    * @author Brenon, Jose
    */
    public xCommand CommandMoveToLocation(Vector3 lLocation, int bRunToLocation = EngineConstants.TRUE, int bDeactivateAtEnd = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public xCommand CommandMoveToMultiLocations(List<Vector3> lLocations, int bRunToLocation = EngineConstants.TRUE, int nStartingWP = 0, int bLoop = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a move to GameObject xCommand constructor. The GameObject executing the xCommand will only use the position and disregard the target GameObject orientation.
    *
    * This function is a move to GameObject xCommand constructor.
    * It creates a move to GameObject xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt to
    * move the creature to target object.
    *
    * @param oTarget - the GameObject the xCommand should move the creature to
    * @param bRunToLocation - specifies whether the GameObject should run or not
    * @param fMinRange - The closest to the GameObject we can be
    * @param bUseOriginalPosition - Even if the target is moving, use the original position
    * @returns a valid command
    * @remarks Only creatures can move, non-creature objects that have a move xCommand assigned to them will fail the xCommand when attempting to process it.
    * @author Noel, Jose
    */
    public xCommand CommandMoveToObject(GameObject oTarget, int bRunToLocation = EngineConstants.TRUE, float fMinRange = 0.0f, int bUseOriginalPosition = EngineConstants.FALSE, float fMaxRange = 0.0f)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Moves creature away from specified target GameObject at the specified distance
    *
    * If no clear line-of-sight exists between our current position and any position around the target
    * at the specified distance, the AI xCommand will return failure.
    * If we're currently further away from the target than the specified distance, the command
    * will actually bring us closer up to the specified distance.
    *
    * @param oTarget - the GameObject from whom to move away
    * @param fAwayDistance - The distance away from the target we want to be
    * @param bRunToLocation - specifies whether the GameObject should run or not
    * @returns a valid command
    * @author Nicolas Ng Man Sun
    */
    public xCommand CommandMoveAwayFromObject(GameObject oTarget, float fAwayDistance, int bRunToLocation = EngineConstants.TRUE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a fly xCommand constructor. The flying creature will use both the position and orientation of the target location.
    *
    * @param lLocation - the Vector3 the xCommand should fly the creature to
    * @param bIgnorePathing - (optiona) set to true if being able to path to the lLocation is not a requirement
    * @returns a valid command
    * @author Jose
    */
    public xCommand CommandFly(Vector3 lLocation, int bIgnorePathing = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDMOVETOOBJECT = 560;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDMOVEAWAYFROMOBJECT = 561;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDEQUIPITEM = 562;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDUNEQUIPITEM = 563;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDPICKUPITEM = 564;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDPUTDOWNITEM = 565;

    /* @brief This function is an attack xCommand consBased ontructor.
    *
    * This function is an attack xCommand constructor.
    * It creates an attack xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt to
    * make the creature attack the target object.
    * This xCommand will also move the attacker towards the target, if the creature
    * has a melee weapon equipped.
    * 
    * @param oTarget - the GameObject the xCommand make the creature attack.
    * @param nForcedResult - the xCommand will be executed without processing scripts
    * @returns a valid command, or invalid if the target is dead.
    * @remarks This xCommand should not be used to initiate combat.
    * @author Adriana/Brenon
    */
    public xCommand CommandAttack(GameObject oTarget, int nForcedResult = EngineConstants.COMBAT_RESULT_INVALID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Deathblow xCommand constructor.
    *
    * This creates a deathblow xCommand which will play a deathblow animation on the 
    * GameObject that executes it. If the attacker is the same as the GameObject executing
    * a deathblow attack animation will be played. If its not, then a deathblow 
    * damage animation will be played.
    * 
    * @param oAttacker - The GameObject that represents the attacker.
    * @param nDeathType - Indicates what deathblow will be used use (e.i. unsync, sword and shield, dog, etc.)
    * @returns a valid command
    * @author Gabo
    */
    public xCommand CommandDeathBlow(GameObject oTarget, int nDeathType = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Turn xCommand constructor.
    *
    * This creates a turn xCommand which will turn the GameObject towards a specific angle.
    * 
    * @param fFacingDirection - Angle to turn towards.
    * @returns a valid command
    * @author Adriana, Jose
    */
    public xCommand CommandTurn(float fFacingDirection)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDSPEAKSTRING = 567;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDSPEAKSTRINGBYSTRREF = 568;

    /* @brief This function is an equip item xCommand constructor.
    *
    * This function is an equip item xCommand constructor.
    * It creates an equip item xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt
    * to equip the specified item on the object.
    *
    * @param oItem - the item that the GameObject should equip
    * @param nEquipSlot - The optinal equip slot number. Use the INVENTORY_SLOT constants to specify a particular slot.
    * @param nWeaponSet - The optinal weapon set number, it can be 0 or 1.
    * @returns a valid command
    * @author Noel/Gabo
    */
    public xCommand CommandEquipItem(GameObject oItem, int nEquipSlot = EngineConstants.INVENTORY_SLOT_INVALID, int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is an unequip item xCommand constructor.
    *
    * This function is an unequip item xCommand constructor.
    * It creates an unequip item xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt
    * to unequip the specified item on the GameObject to the specified repository position.
    *
    * @param oItem - the item that the GameObject should equip
    * @returns a valid command
    * @author Noel/Gabo
    */
    public xCommand CommandUnequipItem(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a sheathe weapons xCommand constructor.
    *
    * Makes the creature executing this xCommand put away its weapons.
    *
    * @returns a valid command
    * @author Gabo
    */
    public xCommand CommandSheatheWeapons()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is an unsheathe weapons xCommand constructor.
    *
    * Makes the creature executing this xCommand draw its weapons.
    *
    * @returns a valid command
    * @author Gabo
    */
    public xCommand CommandUnsheatheWeapons()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a switch weapon set xCommand constructor.
    *
    * Makes the creature executing this xCommand switch weapon sets
    *
    * if the WeaponSet used is EngineConstants.INVALID_WEAPON_SET, then the next available
    * weapon set is made active.
    *
    * @param nWeaponSet - The weapon set to make active. 
    * @returns a valid command
    * @author Gabo
    */
    public xCommand CommandSwitchWeaponSet(int nWeaponSet = EngineConstants.INVALID_WEAPON_SET)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a play animation xCommand constructor.
    *
    * This function is a play animation xCommand constructor.
    * It creates a play animation xCommand which can then be added to any
    * object's xCommand queue. This command, when processed will attempt
    * to play the specified animation on the object.
    *
    * @param nAnimation - the animation that the GameObject should play
    * @param nLoops - The number of loops to play if its a looping animation or the next looping animation, if its a transition animation.
    * @param bPlayNext - Indicates if the engine will automatically play the next animation after the initial animation and its looping animation.
    * @param bBlendIn - 1 = fast blend (default), 2 = immediate (no blending)
    * @param bRandomizeOffset - Start playing the animation at a random position
    * @returns a valid command
    * @author Brenon
    */
    public xCommand CommandPlayAnimation(int nAnimation, int nLoops = 0, int bPlayNext = 0, int bBlendIn = 1, int bRandomizeOffset = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDOPENDOOR = 570;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDCLOSEDOOR = 571;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDUNLOCK = 572;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDLOCK = 573;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDCASTSPELLATOBJECT = 574;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDCASTSPELLATLOCATION = 575;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDGIVEITEM = 576;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDTAKEITEM = 577;

    public xCommand Command(int nCommandType)
    {
        return new xCommand(nCommandType);
    }

    /* @brief This function simply waits for the specified amount of time to pass.
    *
    * This function simply waits for the specified amount of time to pass.
    * If a negative value is specified, the xCommand will be given a wait time of zero.
    *
    * @param fDelay - The amount of time in seconds the xCommand should delay
    * @returns a valid command
    * @author Brenon
    */
    public xCommand CommandWait(float fSeconds)
    {
        xCommand cCommand = Command(EngineConstants.COMMAND_TYPE_WAIT);
        SetCommandFloatRef(ref cCommand, fSeconds);
        return cCommand;
    }

    /* @brief Adds a xCommand to move towards a character and initiate a conversation event
    *
    * Generates a xCommand structure for a creature to approach a target and initiate a 
    * conversation event.  If rConversationFile is specified then that file will be 
    * used, otherwise the conversation specified on the creature will be used
    *
    * @param oTarget - The GameObject which will be approached to initiate a conversation event.
    * @param rConversationFile (optional) - The name of a conversation file to be used (*.con)
    * @returns a valid command
    * @author Gabo
    */
    public xCommand CommandStartConversation(GameObject oTarget, string rConversationFile)//= R""
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds a xCommand to move to the Vector3 of a given object
    *
    * Adds a xCommand to move to the Vector3 of a given object
    *
    * @param oTarget - The GameObject we are moving to
    * @returns a valid command
    * @author EricP
    */
    public xCommand CommandJumpToObject(GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds a xCommand to move to a location
    *
    * Adds a xCommand to move to a location
    *
    * @param lLocation - The Vector3 the GameObject will move to
    * @returns a valid command
    * @author EricP
    */
    public xCommand CommandJumpToLocation(Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDUSESKILL = 583;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDUSEITEM = 584;

    /* @brief This function is a "use ability" xCommand constructor.
    *
    * This function is a "use ability" xCommand constructor.
    * It creates a "use ability" xCommand which can then be added to any object's
    * xCommand queue. This command, when processed will cause the GameObject to
    * use the ability if they're able
    *
    * @param nAbilityId - The ability to perform
    * @param oTarget - Object to perform the ability on (optional)
    * @param vTarget - ground target Vector3 to perform the ability on (optional)
    * @param nConjureTime - sets the conjure time of the ability (optional)
    * @param AbilitySourceItem - sets the item that's granting the ability.  If none is specified but the ability does come from an item, the the one in the creature's inventory with that ability is used.
    * @returns a valid command
    * @author Noel, Jose
    */
    public xCommand CommandUseAbility(int nAbilityId, GameObject oTarget, Vector3 vTarget, float nConjureTime = -1.0f, string sAbilitySourceItemTag = "")
    {
        //check for oTarget if null in line, cannot nullify the Vector3 in constructor
        //so it ended up being a requirement to pass the zero Vector3 when not needed…
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function is a do function xCommand constructor.
    *
    * This function is a do function xCommand constructor.
    * It creates a do function xCommand which can then be added to any object's
    * xCommand queue. This command, when processed will call the specified void
    * returning function on the object.
    *
    * @param fFunction - the void returning function to call
    * @returns a valid command
    * @warning: Specifying a non-void returning function can cause unstable behaviour.
    * @author Brenon
    */
    //xCommand CommandDoFunction(function fFunction) 

    /* @brief Create a xCommand to use an object.
    *
    * This will create a xCommand to use an object. Sub commands include:
    * (1) move to use point
    * (2) face object
    * (3) use object
    *
    * @param oTarget - the GameObject to use
    * @param nAction - the action to use on the object
    * @returns a valid command
    * @remarks See EngineConstants.PLACEABLE_ACTION_*
    * @author Jacques Lebrun
    */
    public xCommand CommandUseObject(GameObject oTarget, int nAction)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDINTERACTOBJECT = 587;
    /* @brief Creates and applies a xCommand to play an animation to interact with an object.
    *
    * This function is for playing ambient animations. It should not be used for 
    * player or combat interactions.
    *
    * This will create a xCommand to use an object. Sub commands include:
    * (1) move to use point
    * (2) face object
    * (3) interact with object
    *
    * If the target GameObject is a creature, this xCommand will clear all
    * commands on the creature and apply an interaction xCommand on it aswell
    * so they bothe play the sync animations properly.
    *
    * Once a creature has an interaction xCommand on it, additional calls to this
    * function will switch the animation being played immediately, without creating
    * another command.
    *
    * @param oCreature - The creature that will interact
    * @param oTarget - the placeable or creature to interact with
    * @param nInteractionId - The type of interaction (see SyncPlaceableAnims and SyncCreatureAnims)
    * @param nPose - The pose loop animation to use
    * @param nLoops - The number of times to play a pose loop (-1 will loop infinitely)
    * @param nPlayExit - Will play an exit animation after the loops end or if the xCommand is cancelled
    * @author Gabo
    */
    public void InteractWithObject(GameObject oCreature, GameObject oTarget, int nInteractionId, int nPose = 1, int nLoops = 0, int bPlayExit = 1, int bSkipReposition = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDMOVEAWAYFROMLOCATION = 588;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDFORCEMOVETOLOCATION = 589;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDFORCEMOVETOOBJECT = 590;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDFORCEFOLLOWOBJECT = 591;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_COMMANDEXAMINE = 592;

    /* @brief This function is a do xEvent xCommand constructor.
    *
    * This function is a do xEvent xCommand constructor.
    * It creates a do xEvent xCommand which can then be added to any object's
    * xCommand queue. This command, when processed will take the xEvent that is
    * set within it.
    *
    * @param evToQueue - the xEvent to queue on your xCommand queue
    * @warning: Specifying a non-void returning function can cause unstable behaviour.
    * @author MarkB
    */
    //xCommand CommandDoEvent(xEvent evToQueue) 

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Effect Access
    /***************************************************************/
    /* @addtogroup effect_access Effect Access Functions
    *
    * Functions to handle effects (setting, removing etc')
    */
    /* @{*/

    /* @brief This function return the center of a cluster of creatures
    *
    * Returns the center of the best cluster of creatures based on input params
    * 
    * @param oCreator - xEffect creator
    * @param nAbilityId - ability id
    * @param nClusterSize - min number of enemies needed
    * @param nAllyFailChance - A percentage chance to fail each possible cluster if any ally is inside the cluster. The chance is comulative per ally
    * @param nReturnFirstMatch - Return the first group matching the criterias instead of best match. Quicker.
    * @author EricP
    */
    public Vector3 GetClusterCenter(GameObject oCreator, int nAbilityId, int nClusterSize, int nAllyFailChance, int bReturnFirstMatch)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function applies an xEffect at a location.
    *
    * Applies eEffect to vLocation.  If nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, then fDuration is the duration of the effect. 
    * 
    * @param nDurationType - can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
    * @param eEffect - the xEffect to be applied
    * @param Vector3 - the Vector3 of the effect
    * @param fDuration - this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
    * @param oCreator - xEffect creator
    * @param nAbilityId - ability id
    * @author EricP
    */
    public void Engine_ApplyEffectAtLocation(int nDurationType, xEffect eEffect, Vector3 lLocation, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0)
    {
        if (oCreator == null) oCreator = gameObject;//gameObject
                                                    //check for oCreator if null in line
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function applies an xEffect on an object.
    *
    * Applies eEffect to oTarget.  If nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, then fDuration is the duration of the effect. Use core_h.ApplyEffectToObject instead of calling this directly!
    * 
    * @param nDurationType - can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
    * @param eEffect - the xEffect to be applied
    * @param oTarget - the target of the effect
    * @param fDuration - this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
    * @param oCreator - xEffect creator
    * @param nAbilityId - ability id
    * @author Sophia
    */
    public void Engine_ApplyEffectOnObject(int nDurationType, xEffect eEffect, GameObject oTarget, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0)
    {
        if (oCreator == null) oCreator = gameObject;//gameObject
                                                    //check for oCreator if null in line
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief This function applies an xEffect on an object.
    *
    * Applies eEffect to every member of the player's party!
    * 
    * @param nDurationType - can be EngineConstants.EFFECT_DURATION_TYPE_PERMANENT EngineConstants.EFFECT_DURATION_TYPE_INSTANTANEOUS or EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY.
    * @param eEffect - the xEffect to be applied
    * * @param fDuration - this value needs to be set only when nDurationType is EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY
    * @param oCreator - xEffect creator
    * @param nAbilityId - ability id
    * @param bExcludeCreator - Exclude the creator of the effect.
    * @author Georg Zoeller
    */
    public void Engine_ApplyEffectOnParty(int nDurationType, xEffect eEffect, float fDuration = 0.0f, GameObject oCreator = null, int nAbilityId = 0, int bExcludeCreator = EngineConstants.FALSE)
    {
        if (oCreator == null) oCreator = gameObject;//gameObject
                                                    //check for oCreator if null in line
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Creates a blank effect
    *
    * Creates an empty effect
    * 
    * @author Noel
    */
    public xEffect Effect(int nEffectType = EngineConstants.EFFECT_TYPE_INVALID)
    {
        //Debug.LogWarning("new Effect of type: " + nEffectType);
        return new xEffect(nEffectType);
    }

    /* @brief Create an AoEObject
    *
    * Creates an Area of Effect object, optionally with an embedded vfx that is synced
    * to the lifetime of the AoE Object.
    *
    * Please refer to the documentation or talk to georg about how to use these, they work
    * slightly different than in previous games!
    * 
    * @author Georg Zoeller
    */
    public xEffect EffectAreaOfEffect(int nId, string rScript, int nAoEVfx = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Is this xEffect valid?
    *
    * Tests to see if an xEffect is valid
    * 
    * @param eEffect - the xEffect to be tested
    * @author Noel
    */
    public int IsEffectValid(xEffect eEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Remove an effect
    *
    * Removes an xEffect from the GameObject it's applied to 
    * 
    * @param oTarget - The GameObject to remove the xEffect from
    * @param eEffect - the xEffect to be removed
    * @author Noel
    */
    public void RemoveEffect(GameObject oTarget, xEffect eEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Remove effects based on certain parameters
    *
    * Removes a group of effects that have the specified values from the GameObject it's applied to
    * 
    * @param oTarget - The GameObject to remove the xEffect from
    * @param nType - Only remove effects of this type (setting EngineConstants.EFFECT_TYPE_INVALID will remove all types of effects)
    * @param nAbilityId - Only remove effects of with this ability id (setting EngineConstants.ABILITY_INVALID will remove effects due to any ability)
    * @param oCreator - Only remove effects created by this GameObject (setting OBJECT_INVALID will remove effects created by anything)
    * @param bIncludeInnate - TRUE = remove innate abilities too if they match the other criteria
    * @author Gabo
    */
    public void RemoveEffectsByParameters(GameObject oTarget, int nType = EngineConstants.EFFECT_TYPE_INVALID, int nAbilityId = EngineConstants.ABILITY_INVALID, GameObject oCreator = null, int bIncludeInnate = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Remove all effects on an GameObject with certain limitations
    *
    * Removes all the effects on an object. You can exclude effects due to injuries and effects that ignore death.
    * 
    * @param oTarget - The GameObject to remove the xEffect from
    * @param bIgnoreInjuries - Don't remove effects that are due to injuries (that are within a certain ability id range)
    * @param bDeath - Don't remove effects that ignore death (use this when removing effects on a creature due to it dying)
    * @author Gabo
    */
    public void RemoveAllEffects(GameObject oTarget, int bIgnoreInjuries = EngineConstants.TRUE, int bDeath = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Remove Effects By Creator
    *
    * Removes all effects in the area created by a specific object. Can be limited to all effects with a specific ability Id.
    * 
    * @param oCreator   - The creator of the xEffect to be removed
    * @param nAbilitID  - The ability ID of the xEffect to be removed (EngineConstants.ABILITY_INVALID = all effects from oCreator)
    * @author Georg Zoeller
    */
    public void RemoveEffectsByCreator(GameObject oCreator, int nAbilityID = EngineConstants.ABILITY_INVALID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the specified integer on the effect
    *
    * Gets the specified integer on the effect
    *
    * @param efEffect- The xEffect to get the integer off of.
    * @param nIndex - The index of the integer to get.
    * @returns Returns the specified integer, returns -1 on error.
    * @sa SetEffectIntegerRef()
    * @author MarkB
    */
    public int GetEffectIntegerRef(ref xEffect efEffect, int nIndex)
    {
        return (efEffect.nList.Count > nIndex) ? efEffect.nList.ElementAt(nIndex) : -1;
    }

    /* @brief Sets the specified integer on the effect
    *
    * Sets the specified integer on the effect
    *
    * @param efEffect - The xEffect to set the value on.
    * @param nIndex - The index of the value to set.
    * @param nValue - The value of the value to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an effect, as the array of values on the xEffect expands as needed.
    * @sa GetEffectIntegerRef()
    * @author MarkB
    */
    public void SetEffectIntegerRef(ref xEffect efEffect, int nIndex, int nValue)
    {
        efEffect.nList.Insert(nIndex, nValue);
    }

    /* @brief Gets the specified float on the effect
    *
    * Gets the specified float on the effect
    *
    * @param efEffect- The xEffect to get the value off of.
    * @param nIndex - The index of the value to get.
    * @returns Returns the specified value, returns -1.0f on error.
    * @sa SetEffectFloatRef()
    * @author MarkB
    */
    public float GetEffectFloatRef(ref xEffect efEffect, int nIndex)
    {
        return (efEffect.fList.Count > nIndex) ? efEffect.fList.ElementAt(nIndex) : -1.0f;
    }

    /* @brief Sets the specified float on the effect
    *
    * Sets the specified float on the effect
    *
    * @param efEffect - The xEffect to set the value on.
    * @param nIndex - The index of the value to set.
    * @param fValue - The value of the value to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an effect, as the array of values on the xEffect expands as needed.
    * @sa GetEffectFloatRef()
    * @author MarkB
    */
    public void SetEffectFloatRef(ref xEffect efEffect, int nIndex, float fValue)
    {
        efEffect.fList.Insert(nIndex, fValue);
    }

    /* @brief Gets the specified GameObject on the effect
    *
    * Gets the specified GameObject on the effect
    *
    * @param efEffect- The xEffect to get the value off of.
    * @param nIndex - The index of the value to get.
    * @returns Returns the specified value, returns OBJECT_INVALID on error.
    * @sa SetEffectObjectRef()
    * @author MarkB
    */
    public GameObject GetEffectObjectRef(ref xEffect efEffect, int nIndex)
    {
        return (efEffect.oList.Count > nIndex) ? efEffect.oList.ElementAt(nIndex) : null;
    }

    /* @brief Sets the specified float on the effect
    *
    * Sets the specified GameObject on the effect
    *
    * @param efEffect - The xEffect to set the value on.
    * @param nIndex - The index of the value to set.
    * @param oValue - The value of the value to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an effect, as the array of values on the xEffect expands as needed.
    * @sa GetEffectObjectRef()
    * @author MarkB
    */
    public void SetEffectObjectRef(ref xEffect efEffect, int nIndex, GameObject oValue)
    {
        Debug.LogWarning("set effect object");
        efEffect.oList.Insert(nIndex, oValue);
    }

    /* @brief Gets the specified string on the effect
    *
    * Gets the specified string on the effect
    *
    * @param efEffect- The xEffect to get the value off of.
    * @param nIndex - The index of the value to get.
    * @returns Returns the specified value, returns empty string on error.
    * @sa SetEffectStringRef()
    * @author MarkB
    */
    public string GetEffectStringRef(ref xEffect efEffect, int nIndex)
    {
        return (efEffect.sList.Count > nIndex) ? efEffect.sList.ElementAt(nIndex) : string.Empty;
    }

    /* @brief Sets the specified string on the effect
    *
    * Sets the specified string on the effect
    *
    * @param efEffect - The xEffect to set the value on.
    * @param nIndex - The index of the value to set.
    * @param sValue - The value of the value to set.
    * @remarks It should be noted that there is no maximum number of values
    * on an effect, as the array of values on the xEffect expands as needed.
    * @sa GetEffectStringRef()
    * @author MarkB
    */
    public void SetEffectStringRef(ref xEffect efEffect, int nIndex, string sValue)
    {
        efEffect.sList.Insert(nIndex, sValue);
    }

    /* @brief Get the type of an effect
    *
    * Gets the xEffect type of an effect
    * 
    * @param eEffect - the xEffect to be examined
    * @author MarkB
    */
    public int GetEffectTypeRef(ref xEffect efEffect)
    {
        return (efEffect != null) ? efEffect.nType : Effect(EngineConstants.EFFECT_TYPE_INVALID).nType;
    }

    /* @brief Set the creator of an effect
    *
    * Gets the DurationType of an effect
    * 
    * @param eEffect - the xEffect 
    * @returns Returns the EngineConstants.EFFECT_DURATION_TYPE_* of an effect
    * @author MarkB
    */
    public int GetEffectDurationTypeRef(ref xEffect eEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the creator of an effect
    *
    * Gets the creator of an effect
    * 
    * @param eEffect - the xEffect to be examined
    * @returns Returns the GameObject that created the effect.  Returns OBJECT_INVALID if the xEffect isn't valid
    * @author MarkB
    */
    public GameObject GetEffectCreatorRef(ref xEffect efEffect)
    {
        return efEffect.oCreator;
    }

    /* @brief Set the creator of an effect
    *
    * Sets the creator of an effect
    * 
    * @param eEffect - the xEffect to be changed
    * @param oCreator - the GameObject that should be set as creator
    * @author MarkB
    */
    public void SetEffectCreatorRef(ref xEffect efEffect, GameObject oCreator)
    {
        efEffect.oCreator = oCreator;
    }

    /* @brief Returns the ID of an effect. 
    *
    *    Returns the internal unique ID of an effect. 
    *
    * @param eEffect    - The effect
    * @author MarkB
    */
    public int GetEffectIDRef(ref xEffect eEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the flags for an effect
    *
    * Returns the flags for an effect
    *
    * @param efEffect- The xEffect of which to get the flags.
    * @returns Returns the flags for the effect
    * @author MarkB
    */
    public int GetEffectFlagsRef(ref xEffect efEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the flags for an effect
    *
    * Sets the flags for an effect
    *
    * @param efEffect - The xEffect to set the flags on.
    * @param nFlags - The flags that will be set
    * @author MarkB
    */
    public void SetEffectFlagsRef(ref xEffect efEffect, int nFlags)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the animation for an effect
    *
    * Returns the animation for an effect
    *
    * @param efEffect- The xEffect of which to get the animation.
    * @returns Returns the animation for the effect
    * @author MarkB
    */
    public int GetEffectAnimationRef(ref xEffect efEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the animation for an effect
    *
    * Sets the animation for an effect
    *
    * @param efEffect - The xEffect to set the animation on.
    * @param nAnimation - The animation that will be set
    * @author MarkB
    */
    public void SetEffectAnimationRef(ref xEffect efEffect, int nAnimation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the ability id for the effect
    *
    * Returns the ability id for the effect
    *
    * @param efEffect- The xEffect to get the ability off of.
    * @returns Returns the ability id for the effect
    * @sa SetEffectAbilityIDRef()
    * @author MarkB
    */
    public int GetEffectAbilityIDRef(ref xEffect efEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the ability id for the effect
    *
    * Sets the ability id for the effect
    *
    * @param efEffect - The xEffect to get the ability off of.
    * @param nAbilityId - ability id
    * @sa GetEffectAbilityIDRef()
    * @author MarkB
    */
    public void SetEffectAbilityIDRef(ref xEffect efEffect, int nAbilityId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the specified integer on the xEffect Engine Data structure
    *
    * Sets the specified integer on the effect
    *
    * @param efEffect - The xEffect to set the value on.
    * @param nIndex - The index of the value to set.
    * @param nValue - The value of the value to set.
    * @remarks The Engine data structure on effects is a separate list of data structures
    *          for use within the engine; giving design the flexibility of maintaining their
    *          own list in scripting while giving programming certainty as to where to find
    *          information (in some cases) derived from scripting.
    * @author MarkB
    */

    public void SetEffectEngineIntegerRef(ref xEffect efEffect, int nIndex, int nValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void SetEffectEngineFloatRef(ref xEffect efEffect, int nIndex, float fValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void SetEffectEngineObjectRef(ref xEffect efEffect, int nIndex, GameObject oValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void SetEffectEngineVectorRef(ref xEffect efEffect, int nIndex, Vector3 vVector)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief  Gets the xEffect associated with the current event
    *
    *   Gets the xEffect associated with the current event.
    *   This xCommand should only be used in scripts handling xEffect applied/removed events.
    *
    * @returns an effect
    * @author Noel
    */
    public xEffect GetCurrentEffect()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief  Tells the game if the xEffect associated with the current xEvent is valid or not.
    *
    *   Tells the game if the xEffect associated with the current xEvent is valid or not.
    *   This determines if the xEffect is stored and if linked effects are also applied.
    *   This xCommand should only be used in scripts handling xEffect applied/removed events.
    *
    * @param nValid - The xEffect is valid and was properly applied.
    * @author Noel
    */
    public void SetIsCurrentEffectValid(int nValid = EngineConstants.TRUE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the list of effects that are currently applied to an object.
    *
    *    Returns the list of effects that are currently applied to an object. This includes both
    *    temporary and permanent effects. The order of the events inside the list is meaningless.
    *
    * @param oObject - The GameObject from which we try to get the effects list.
    * @param nEffectType - Optionally only return an array of a specified EffectType. Default setting returns all applied effects.
    * @param nAbilityId - Optionally filter the returned array to include only effects with a matching ability id (0 means no filter).
    * @param nEffectId - Optionally filter the array by EffectId (-1 means no filter).
    * @param nDurationType - Optionally filter the array by DurationType (EngineConstants.EFFECT_DURATION_TYPE_INVALID means no filter).
    * @author Sam, Georg, Gabo
    */
    public List<xEffect> GetEffects(GameObject oObject, int nEffectType = EngineConstants.EFFECT_TYPE_INVALID, int nAbilityId = 0, GameObject oCreator = null, int nDurationType = EngineConstants.EFFECT_DURATION_TYPE_INVALID, int nEffectId = -1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether or not a creature has effects matching the filter criteria
    *
    *   Returns whether or not a creature has effects matching the filter criteria
    *
    * @param oObject - The GameObject from which we try to get the effects list.
    * @param nEffectType - Optionally only return an array of a specified EffectType. 
    * Default setting returns all applied effects.
    * @param nAbilityId - Optionally filter the returned array to include 
    * only effects with a matching ability id (-1 means no filter).
    * @author Georg
    */
    public int GetHasEffects(GameObject oObject, int nEffectType = EngineConstants.EFFECT_TYPE_INVALID, int nAbilityId = -1)
    {
        if (nAbilityId != -1) throw new NotImplementedException();//Filter required
        return (oObject.GetComponent<xGameObjectUTC>().oEffects.Find(x => x.nType == nEffectType) != null) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Returns the list of effects that created by the object.
    *
    *    Returns the list of effects that created by the object
    *
    * @param oObject    - The GameObject from which we try to get the effects list.
    * @param nAbilityID - if specified (not ABILITY_INVALID), returns only effects with the specified ability id.
    * @param nType - if specified (not EFFECT_INVALIDEFFECT), returns only effects of the specified type.
    * @author Adriana, Georg
    */
    public xEffect[] GetEffectsByCreator(GameObject oCreator, int nAbilityID = EngineConstants.ABILITY_INVALID, int nType = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the Vector3 of a visual effect
    *
    *    Returns the Vector3 of a visual effect. If it was applied to a location, it will be
    *    Vector3 it was applied at. if it was applied to an object, it will be the objects location
    *
    * @param eVFX    - The visual effect
    * @author Georg
    */
    public Vector3 GetVisualEffectLocation(xEffect eVFX)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xEffect flags on a given object.
    *
    *    Effects can set xEffect flags. This function will return
    *  and integer that contains all the xEffect flags on an object
    *  due to the effects it currently holds.
    *
    * @param oOwner    - The GameObject that contains effects
    * @author Gabo
    */
    public int GetEffectsFlags(GameObject oOwner)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/

    /* @brief Applies a visual xEffect to a target object.
    *
    *    Applies a visual xEffect to a target object.
    *
    * @param oCreator   - The GameObject creating the visual effect
    * @param oTarget    - The GameObject receiving the visual effect
    * @param nVFXId     - The Id of the visual effect
    * @param nDurationType  - Temporary, instant, permanent, etc.
    * @param fDuration  - Duration of the effect, depending on the type
    * @param nAbilityId - The ability ID this xEffect is linked to (if applied by an ability)
    * @author Eric Paquette, Georg Zoeller
    */
    public void ApplyEffectVisualEffect(GameObject oCreator, GameObject oTarget, int nVFXId, int nDurationType, float fDuration, int nAbilityId = EngineConstants.ABILITY_INVALID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief add/remove visual xEffect for items
     */
    public void AddItemVisualEffect(GameObject oTarget, int nVFXId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public List<int> GetItemVisualEffectsIDs(GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void RemoveItemVisualEffect(GameObject oTarget, int nId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void RemoveAllItemVisualEffects(GameObject oTarget)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief EffectDamage Constructor.
    *
    *    EffectDamage Constructor
    *
    * @param fValue      - Amount of damage to be applied.
    * @param nDamageType - Damage Type to be applied (Physical, Fire):  Default of 1 is physical.
    * @param nFlags      - Special behavior bitfield:  Default is no flags (0).
    * @param nImpactVFX  - Impact VFX to play.  Default of 0 is no impact VFX.
    * @author Georg Zoeller, Ported Into Engine by Mark Brockington
    */
    public xEffect EffectDamage(float fValue, int nDamageType = 1, int nFlags = 0, int nImpactVFX = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief EffectImpact Constructor.
    *
    *    EffectImpact Constructor.
    *
    * @param fDamage     - Amount of damage to be applied.
    * @param oWeapon     - Weapon that applies the damage.
    * @param nVfx        - Impact VFX to play.  Default of 0 is no impact VFX.
    * @param nAbi        - Ability (default is invalid ability).
    * @param nDamageType - Damage Type to be applied (Physical, Fire):  Default of 1 is physical.
    * @author Georg Zoeller, Ported Into Engine by Mark Brockington
    */
    public xEffect EffectImpact(float fDamage, GameObject oWeapon, int nVfx = 0, int nAbi = 0, int nDamageType = 1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief EffectModifyProperty Constructor.
    *
    *    EffectModifyProperty Constructor.
    *
    * @author Georg Zoeller, Ported Into Engine by Mark Brockington
    */
    public xEffect EffectModifyProperty(int nProperty0, float fChange0, int nProperty1 = 0, float fChange1 = 0.0f, int nProperty2 = 0, float fChange2 = 0.0f)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief EffectModifyPropertyHostile Constructor.
    *
    *    EffectModifyPropertyHostile Constructor.
    *
    * @author Georg Zoeller, Ported Into Engine by Mark Brockington
    */
    public xEffect EffectModifyPropertyHostile(int nProperty0, float fChange0, int nProperty1 = 0, float fChange1 = 0.0f, int nProperty2 = 0, float fChange2 = 0.0f)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief ApplyEffectModifyProperty
    *
    *    ApplyEffectModifyProperty will go through the effort of applying the Modify
    *    Property without actually parsing the xEffect in the scripting language.
    *
    *    If the xEffect hasn't been created by EffectModifyProperty or EffectModifyPropertyHostile
    *    it will be ignored by the function and nothing will occur in the game engine.
    *
    * @author Mark Brockington
    */
    public void ApplyEffectModifyProperty(xEffect eModifyPropertyEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief RemoveEffectModifyProperty
    *
    *    RemoveEffectModifyProperty will go through the effort of unapplying the Modify
    *    Property without actually parsing the xEffect in the scripting language.
    *
    *    If the xEffect was not created by EffectModifyProperty or EffectModifyPropertyHostile
    *    it will be ignored by the function and nothing will occur in the game engine.
    *
    * @author Mark Brockington
    */
    public void RemoveEffectModifyProperty(xEffect eModifyPropertyEffect)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enables or disables the physics on a creature.
    * @author Gabo
    * @param oCreature   - Creature to be affected
    * @param bEnable     - If true, physics will be enabled, if false, they will be disabled.
    */
    public void SetPhysicsController(GameObject oCreature, int bEnable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Waypoints & Map Patches
    /***************************************************************/
    /* @addtogroup waypatches Waypoint & Map Patch Functions
    *
    * Functions to control the state of waypoints and map patches
    */
    /* @{*/

    /* @brief Controls the state of a map patch
    *
    * Map patches with a state of -1 are "invisible" to the client. Any
    * other setting 0-N will make the patch appear on the client's map.
    * If the action ID for the state is not -1, the patch is clickable,
    * and the action ID will be sent to server whenever client clicks
    * on the patch.
    *
    * @param oMapPatch - Object ID of patch to control
    * @param nState - State number to set, -1 = invisible
    * @sa GetMapPatchState(), GetPlayerMapPatch()
    * @author Derek Beland
    */
    public void SetMapPatchState(GameObject oMapPatch, int nState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the current state of a map patch
    *
    * Map patches with a state of -1 are "invisible" to the client. Any
    * other setting 0-N will make the patch appear on the client's map.
    * If the action ID for the state is not -1, the patch is clickable,
    * and the action ID will be sent to server whenever client clicks
    * on the patch.
    *
    * @param oMapPatch - Object ID of patch to control
    * @sa SetMapPatchState(), GetPlayerMapPatch()
    * @author Derek Beland
    */
    public int GetMapPatchState(GameObject oMapPatch)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the GameObject ID of the Nth player map patch
    *
    * Each player has a list of map patches and states for each area.
    * This function looks up the Nth GameObject ID of a map patch tag for a
    * particular player. Note that the patch could be from any of the
    * currently loaded areas.
    *
    * @param pPlayer - Object ID of player
    * @param sTag - Tag string to search for
    * @param nNth - Integer ordinal of object
    * @sa GetMapPatchState(), SetMapPatchState()
    * @author Derek Beland
    */
    public GameObject GetPlayerMapPatch(GameObject pPlayer, string sTag, int nNth = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Controls the state of a map pin
    *
    * Turns on or off a map pin.
    *
    * @param oMapPin - Object ID of Pin to control
    * @param nEnable - TRUE or FALSE
    * @sa GetMapPinState(), GetPlayerMapPin()
    * @author Derek Beland
    */
    public void SetMapPinState(GameObject oMapPin, int nEnable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the current state of a map Pin
    *
    * Returns TRUE or FALSE indicating whether the pin is enabled
    * or disabled
    *
    * @param oMapPin - Object ID of Pin to control
    * @sa SetMapPinState(), GetPlayerMapPin()
    * @author Derek Beland
    */
    public int GetMapPinState(GameObject oMapPin)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the GameObject ID of the Nth player map Pin
    *
    * Each player has a list of map pins and states for each area.
    * This function looks up the Nth GameObject ID of a map pin tag for a
    * particular player. Note that the pin could be from any of the
    * currently loaded areas.
    *
    * @param pPlayer - Object ID of player
    * @param sTag - Tag string to search for
    * @param nNth - Integer ordinal of object
    * @sa GetMapPinState(), SetMapPinState()
    * @author Derek Beland
    */
    public GameObject GetPlayerMapPin(GameObject pPlayer, string sTag, int nNth = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Plot Manager
    /***************************************************************/
    /* @addtogroup plotman Plot Manager Functions
    *
    * Functions to interface with the Plot Manager
    */
    /* @{*/

    /* @brief Returns the value of a plot flag
    *
    * Queries the state of a plot flag from a party's plot table.
    * In order to query DEFINED flags, this function needs to query the plot script associated with that flag.
    *
    * @param oParty - Party Object ID
    * @param strPlot - Plot name to query
    * @param nFlag - Plot flag # to query (32-127)
    * @param nCallScript - Whether or not to call the plot script. Note: this should not be set to TRUE when this function is used inside a plot script. If nBit is a defined flag then the script will start calling itself recursivly.
    * @sa SetPartyPlotFlag(), GetPartyPlotVar()
    * @returns value of the flag - TRUE or FALSE
    * @author Derek Beland
    */
    public int GetPartyPlotFlag(GameObject oParty, string strPlot, int nFlag, int nCallScript = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the value of a plot flag
    *
    * Sets the state of a plot flag in a party's plot table.
    *
    * @param oParty - Party Object ID
    * @param strPlot - Plot name to query
    * @param nFlag - Plot flag # to query (32-127)
    * @param nValue - Value to set (TRUE or FALSE)
    * @param nCallScript - Whether or not to call the plot script.
    * @sa GetPartyPlotFlag(), GetPartyPlotVar(), SetPartyPlotVar()
    * @author Derek Beland
    */
    public void SetPartyPlotFlag(GameObject oParty, string strPlot, int nFlag, int nValue, int nCallScript = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the name of a plot entry
    *
    * Returns a strref containing the localized name of the plot entry
    *
    * @param strPlot - Plot # to query
    * @sa GetPlotEntry2DA(), GetPlotPriority()
    * @author Derek Beland
    */
    public int GetPlotEntryName(string strPlot)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the resref of the plot (without extension) as a string
    *
    * @param strPlot a string containing the guid or resref of the plot
    * @author Hesky Fisher
    */
    public string GetPlotResRef(string strPlot)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the GUID of the plot as a string (you should always translate RESREFS to GUIDs before using them)
    *
    * @param strPlot a string containing the guid or resref of the plot
    * @return GUID for the associated PlotResRef, will return a blank string if the PlotResRef is unknown.
    * @author Mark Brockington
    */
    public string GetPlotGUID(string strPlotResRef)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the name of the plot flag as a string
    *
    * @param strPlot - a string containing the guid or resref of the plot
    * @param nFlag - plot flag number to query
    * @author Hesky Fisher
    */
    public string GetPlotFlagName(string strPlot, int nFlag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the reward ID related to a specific plot flag.
    *
    * @param strPlot - a string containing the guid or resref of the plot
    * @param nFlag - plot flag number to query
    * @author Georg Zoeller
    */
    public int GetPlotFlagRewardId(string strPlot, int nFlag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the party picker area name
    *
    * Sets the party picker area name. Each module can have its own area
    *
    * @param sAreaName - Name of the partypicker stage
    * @param s2DAName - Name of the 2DA to use with the stage
    * @author Eric Paquette
    */
    public void SetPartyPickerStage(string sAreaName, string s2DAName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the given plot as a Story plot.
    *
    * Sets the given plot as a Story plot. Story plots are displayed on the loading screen and will not appear in the Journal.
    *
    * @param sPlot - the plot resref
    * @author Henry Smith
    */
    public void SetStoryPlot(string sPlot)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the plot giving flag of the object
    *
    *
    * @param oid - the object.
    * @param oid - whether the GameObject is a plot giver.
    * @author Henry Smith
    */
    public void SetPlotGiver(GameObject oid, int bIsPlotGiver)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the plot giving flag of the object
    *
    *
    * @param oid - the object.
    * @param sPlotGUID - the plot GUID
    * @param bIsPlotDestination - whether the GameObject is a plot destination.
    * @author Christopher Kerr
    */
    public void SetPlotDestination(GameObject oid, string sPlotGUID, int bIsPlotDestination)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the summary for a given plot flag.
    *
    * Gets the summary for a given plot flag. The summary is whatever text
    * is between the <summary> tags. 
    *
    * @param sPlot - the plot resref
    * @param nFlag - the flag id
    * @author Bogdan Corciova
    */
    public string GetPlotSummary(string sPlot, int nFlag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the plot text id for a given plot flag.
    *
    * Gets the plot text id for a given plot flag.  This is
    * the full plot description.
    *
    * @param sPlot - the plot resref
    * @param nFlag - the flag id
    * @author Michael Hamilton
    */
    public int GetPlotEntryDescription(string sPlot, int nFlag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the value of the specified custom tag
    *
    * @param nKey - the hash value of the custom tag
    * @param nValue - the value to assign to it
    *
    * @author Nicolas Ng Man Sun
    */
    public void SetCustomTag(int nKey, int nValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Party and Group
    /***************************************************************/

    /* @addtogroup Party Party and Group control
    *
    * Functions for party and group functions
    */
    /* @{*/

    /* @brief Get the GameObject array of the party pool
    *
    * @returns the array of followers belonging to the party pool
    * @author Jacques Lebrun
    */
    public List<GameObject> GetPartyPoolList()
    {
        return xGameObjectMOD.instance.oPartyPool;
    }

    /* @brief Adds a creature into the player's active party
    *
    * This function will add the creature into the player's active party. The active
    * party are the creatures that are currently following the player and appear
    * in the main GUI. This number is limited to 3 additional followers in addition to
    * the player character.
    * NOTE: This function needs to have a return value when failure means that the player
    * Tried to add to many followers.
    *
    * DEPRECATED, use SetFollowerState()
    *
    * @param oCreatureToAdd - The creature to add to the party.
    * @param oPlayer - The player GameObject leading the party.
    * @remarks This function does not handle yet the party pool.
    * @sa RemoveFromParty(), GetPartySize()
    * @author Sophia
    */
    //void AddToParty(GameObject oCreatureToAdd, GameObject oPlayer) 

    /* @brief Removes a creature from the player's active party
    *
    * This function will remove the creature from the player's active party. The active
    * party are the creatures that are currently following the player and appear
    * in the main GUI. This number is limited to 3 additional followers in addition to
    * the player character.
    *
    * DEPRECATED, use SetFollowerState()
    *
    * @param oCreature - The creature to remove from the party.
    * @remarks This function does not handle yet the party pool.
    * @sa AddToParty(), 
    * @author Sophia
    */
    //void RemoveFromParty(GameObject oCreature) 

    /* @brief Gets the size of a player's active party
    *
    * This function returns the number of followers in the player's active party. The active
    * party are the creatures that are currently following the player and appear
    * in the main GUI. This number is limited to 3 additional followers in addition to
    * the player character.
    *
    * DEPRECATED - use GetPartyList()
    *
    * @remarks This function does not handle yet the party pool.
    * @author Sophia
    */
    //int GetPartySize(GameObject oPlayer) 

    /* @brief Returns the party list for the creature
    *
    *
    * @param oObject - The GameObject to test for returning the party
    * @author Adriana
    */
    public List<GameObject> GetPartyList(GameObject oCreature = null)
    {
        //Should return party with FOLLOWER_STATE_ACTIVE + Player
        List<GameObject> _party = new List<GameObject>();
        _party.Add(xGameObjectMOD.instance.oHero);

        _party.Concat(xGameObjectMOD.instance.GetComponent<xGameObjectMOD>().oPartyPool.
            FindAll(x => x.GetComponent<xGameObjectUTC>().FOLLOWER_STATE
            == EngineConstants.FOLLOWER_STATE_ACTIVE));

        return _party;
    }

    /* @brief Sets whether or not the party has mail waiting for them.
    *
    *
    * @param nHasMail - 0 if the party has no mail, nonzero otherwise.
    * @author PaulS
    */
    public void SetPartyHasMail(int nHasMail)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether or not the party has mail waiting for them and marks the Vector3 on the world map if they do.
    *
    *
    * @param sMailAreaTag - area tag of the map pin where the party has mail, or an empty string if they have no mail.
    * @author Bryan Derksen
    */
    public void SetPartyMailArea(string sMailAreaTag)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get follower state.
    *
    * @param oCreature - Party follower
    * @returns the state of the follower.
    * @author Jacques Lebrun
    */
    public int GetFollowerState(GameObject oCreature)
    {
        return GetLocalInt(oCreature, "FOLLOWER_STATE");
    }

    /* @brief Set the state of the follower.
    *
    * Follower State can be:
    * FOLLOWER_STATE_ACTIVE - Add follower to party pool + active party
    * FOLLOWER_STATE_AVAILABLE - Add follower to party pool and remove from active party
    * FOLLOWER_STATE_INVALID - Remove follower from party pool and active party
    * FOLLOWER_STATE_UNAVAILABLE - Remove follower from active party and do not allow adding it to active party again
    * FOLLOWER_STATE_SUSPENDED - Remove follower from active party and store it for putting it back later into the party
    * 
    * @param oCreature - Party follower
    * @param nState - the state
    * @author Jacques Lebrun
    */
    public void SetFollowerState(GameObject oCreature, int nState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the follower sub state.
    *
    * Returns any of the EngineConstants.FOLLOWER_STATE_X constants.
    *
    * @param oCreature - Party follower
    * @returns the sub state of the follower.
    * @author Jacques Lebrun
    */
    public int GetFollowerSubState(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the sub state of the follower.
    *
    * @param oCreature - Party follower
    * @param nState - the sub state
    * @author Jacques Lebrun
    */
    public void SetFollowerSubState(GameObject oCreature, int nSubState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Checks if the follower is locked in the current state
    *
    * @param oCreature - Party follower
    * @returns TRUE if the follower is locked, FALSE otherwise
    * @author Jacques Lebrun
    */
    public int IsFollowerLocked(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Locks the follower in the current state he is
    *
    * This can be used while the follower is in any state. It will not allow the player, using the GUI,
    * to change the state of a follower. For example: locking the follower while he is in the ACTIVE state
    * will not allow the player remove the follower from the active party.
    *
    * @param oCreature - Party follower
    * @param bLocked - TRUE to lock, FALSE to unlock
    * @author Jacques Lebrun
    */
    public void SetFollowerLocked(GameObject oCreature, int bLocked)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Checks if the follower is set to follow the party leader
    *
    * @param oCreature - Party follower
    * @returns TRUE if the follower is following the party, FALSE otherwise
    * @author Adriana Lopez
    */
    public int GetFollowPartyLeader(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the following state of the henchman to true or false
    *
    * Change the following state to false if you want the henchman to separate 
    * from the group
    *
    * @param oCreature - Party follower
    * @param bFollow - TRUE to follow, FALSE to separate from the party
    * @author Adriana Lopez
    */
    public void SetFollowPartyLeader(GameObject oCreature, int bFollow)
    {
        SetLocalInt(oCreature, EngineConstants.FOLLOWER_SCALED, bFollow);
    }

    /* @brief Returns TRUE if a creature is a member of a player's party
    *
    * This function returns TRUE if oPlayer and oCreature are in the same party
    *
    * @param oPlayer 
    * @param oCreature
    * @author Sam
    */
    public int IsFollower(GameObject oCreature)
    {
        //Debug.Log("is follower");
        return (GetLocalInt(oCreature, "FOLLOWER_STATE") != EngineConstants.FOLLOWER_STATE_INVALID) ? EngineConstants.TRUE : EngineConstants.FALSE;
    }

    /* @brief Adjust a party follower's approval rating
    *
    * @param oFollower follower whose approval of the hero has changed
    * @param nAmount
    * @author Paul Schultz
    */
    public void AdjustFollowerApproval(GameObject oFollower, int nAmount, int bNotify = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get a party follower's approval rating
    *
    * @param oFollower follower whose approval of the hero you're interested in
    * @returns oFollower's approval of the hero
    * @author Paul Schultz
    */
    public int GetFollowerApproval(GameObject oFollower)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Enable the approval rating for a follower
    *
    * Enabling approval allows the follower's approval rating to be modified
    * and will show the approval widgets in the GUIs.
    * Disabling approval will cause the approval rating to be lost.
    * 
    * @param oFollower follower whose approval of the hero has changed
    * @param bEnabled enabled state
    * @author Jacques Lebrun
    */
    public void SetFollowerApprovalEnabled(GameObject oFollower, int bEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the approval rating description text for a follower.
    *
    * Does nothing if approval is disabled.
    * 
    * @param oFollower follower whose approval of the hero has changed
    * @param nStrRef Description string ref
    * @author Jacques Lebrun
    */
    public void SetFollowerApprovalDescription(GameObject oFollower, int nDescStrRef)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the party leader.
    *
    * @returns Party leader creature GameObject (not necessarily the hero)
    * @author Gavin Burt
    */
    public GameObject GetPartyLeader()
    {
        return GetLocalObject(GetModule(), "PARTY_LEADER_STORE");
    }

    /* @brief Sets the party leader.
    *
    * @param oLeader Leader of the party (not necessarily the hero)
    * @author Gavin Burt
    */
    public void SetPartyLeader(GameObject oLeader)
    {
        UpdateGameObjectProperty(xGameObjectMOD.instance.gameObject, EngineConstants.PARTY_LEADER_STORE, oLeader.name);
    }

    /* @brief Adds a creature that follows you around but it is not part of the 
    * players party. The creature will not cross an area transition.
    *
    * @param oFollower Creature to add as a follower
    * @return 1 if the creature is added, 0 if it fails
    * @author Adriana Lopez
    */
    public int AddNonPartyFollower(GameObject oFollower)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes a non party follower
    *
    * @param oFollower Creature to add as a follower
    * @author Adriana Lopez
    */
    public void RemoveNonPartyFollower(GameObject oFollower)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns TRUE if the GameObject is currently being controlled by the player
    *   Note - this can be TRUE for the main player character, and also for any
    *   party members selected by the user (which can be more than one).
    *
    *
    * @param oCreature - Check this creature to determine if it is controlled by the player
    * @author Sam
    */
    public int IsControlled(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the package of a creature.
    *
    * @author Jacques
    */
    public int GetPackage(GameObject oCreature)
    {
        return GetLocalInt(oCreature, "Package");
    }

    /* @brief Returns the AI package of a creature.
    *
    * @author Jacques
    */
    public int GetPackageAI(GameObject oCreature)
    {
        return GetLocalInt(oCreature, "PackageAI");
    }

    /* @brief Returns whether AI is enabled by the user for this creature.
    *
    * The user can choose to selectively disable AI on a per-creature basis.
    *
    * @param oCreature - the creature to query.
    * @return - TRUE if AI is enabled for this creature.
    * @author Jacques
    */
    public int IsPartyAIEnabled(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the number of tactics for the creature.
    *   Zero can imply that either the tactics are disabled by the user, or they don't exist.
    *   for this creature (a non-party creature).
    *
    * @param oCreature - the creature to query.
    * @return - the number of available tactics.
    * @author Jacques
    */
    public int GetNumTactics(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the number of tactics available.
    *
    * @param oCreature - the creature to query.
    * @param nNumTactics - the number of tactics that can be set by the user.
    * @author Jacques
    */
    public void SetNumTactics(GameObject oCreature, int nNumTactics, int bSendNotification = EngineConstants.FALSE)
    {
        oCreature.GetComponent<xGameObjectBase>().nNumTactics = nNumTactics;
    }

    /* @brief Returns whether the specified tactic is enabled.
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return TRUE if the tactic is enabled.
    * @author Jacques
    */
    public int IsTacticEnabled(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the target type for the specified tactic.
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The target type.
    * @author Jacques
    */
    public int GetTacticTargetType(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the ID number of the targeted party member for those tactic
    *          targets which are generated on a per-follower basis.
    *
    * @param oCreature - The creature to query.
    * @param nIndex - The tactic index.
    * @return The ID number of the party member.
    * @author Cody Watts
    */
    public GameObject GetTacticTargetObject(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the condition id of the specified tactic
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The condition id.
    * @author Jacques
    */
    public int GetTacticCondition(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the ID number of the party member being referenced in
    *          those conditions which are generated on a per-follower basis.
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The ID number of the party member.
    * @author Cody Watts
    */
    public GameObject GetTacticConditionObject(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xCommand type for the specified tactic.
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The xCommand type.
    * @author Jacques
    */
    public int GetTacticCommand(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xCommand parameter for the specified tactic.
    * Can be the ability ID or item ID. Zero for none or not applicable.
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The target type.
    * @author Jacques
    */
    public int GetTacticCommandParam(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the xCommand item tag for the specified tactic.
    *
    * @param oCreature - the creature to query.
    * @param nIndex - the tactic index.
    * @return The item tag, or empty string if not applicable.
    * @author Jacques
    */
    public string GetTacticCommandItemTag(GameObject oCreature, int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets a tactic entry for a party member
    *
    * @author Jacques
    */
    public void SetTacticEntry(GameObject oCreature, int nIndex, int nEnabled, int nTargetType, int nCondition, int nCommandType, int nCommandParam = 0)
    {
        xTactic t = new xTactic();
        t.nEnabled = nEnabled;
        t.nTargetType = nTargetType;
        t.nCondition = nCondition;
        t.nCommandType = nCommandType;
        t.nCommandParam = nCommandParam;
        oCreature.GetComponent<xGameObjectUTC>().oTactics.Insert(nIndex - 1, t);//insert or replace?
    }

    /* @brief Get the current tactic preset of the creature,
    * 0 indicating none is selected or it has been customized.
    *
    * @author Jacques
    */
    public int GetTacticPresetID(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the current tactic preset of the creature.
    *
    * @author Jacques
    */
    public void SetTacticPresetID(GameObject oCreature, int nPresetID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Add to the list of selectable presets for the creature.
    *
    * @author Jacques
    */
    public void AddTacticPresetID(GameObject oCreature, int nPresetID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Modifies the look of the atmosphere
    *
    * @param nParamId - atmosphere parameter to modify
    * @param fParamValue - new value to set it to
    */
    public void SetAtmosphere(int nParamId, float fParamValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void SetAtmosphereRGB(int nParamId, float fRedValue, float fGreenValue, float fBlueValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void ResetAtmosphere()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Provides access to frame buffer effects
    *
    * @param sID - string ID of xEffect we want to modify
    **/
    public void FB_SetEffectResource(string sEID, string sResID, float fResValue)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void FB_SetEffectEnabled(string sEID, int nEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Modify creature look-at behaviour
    *
    * @param oCreature - oid of creature we want to modify
    * @param nEnabled - true or false
    **/
    public void SetLookAtEnabled(GameObject oCreature, int nEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Modify creature's Stealth state
    *
    * @param oCreature - oid of creature we want to modify
    * @param nEnabled - true or false
    **/
    public void SetStealthEnabled(GameObject oCreature, int nEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns creature's stealth state
    *
    * @param oCreature - oid of creature of interest
    **/
    public int GetStealthEnabled(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Modify whether the creature can be tracked via the Survival Skill
    *
    * @param oCreature - oid of creature we want to modify
    * @param nEnabled - true or false
    **/
    public void SetCreatureCanBeTracked(GameObject oCreature, int nEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether creature can be tracked via the Survival Skill
    *
    * @param oCreature - oid of creature of interest
    **/
    public int GetCreatureCanBeTracked(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Arrays
    /***************************************************************/

    /* @addtogroup Arrays array control functions
    *
    * Functions to control and read arrays
    */
    /* @{*/

    /* @brief Gets the size of an array
    *
    * Returns the size of an array, 0 if the array is empty.
    *
    * @param array - array whose size we check.
    * @author Sam Johnson
    */
    public int GetArraySize<T>(List<T> array)//any
    {
        //Debug.Log("get array size");
        return array.Count;
    }

    /* @Returns an array with all objects in an area.
    *
    * Returns an array with all objects in an area. You can also specify
    * a tag so the array only has all objects with that tag in that area.
    * Check the size afterwards with GetArraySize.
    *
    * @param oArea - an area object.
    * @param sTag - If specified, only objects will this tag will be returned. 
    * @author Georg, Gabo
    */
    public List<GameObject> GetObjectsInArea(GameObject oArea, string sTag = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    //const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETARRAYSIZE = 709;
    //const INT CVirtualMachineCommands::EngineConstants.COMMAND_DELETEARRAYINDEX = 710;
    //const INT CVirtualMachineCommands::EngineConstants.COMMAND_INSERTARRAYINDEX = 711;
    /* @}*/
    /***************************************************************/

    /* @brief Enable / disable access to the world map.
    *
    * @param nStatus - WM_GUI_STATUS_NO_USE = 0 (unavailable from area map)
    *                - WM_GUI_STATUS_READ_ONLY = 1 (available from area map, cannot travel)
    *                - WM_GUI_STATUS_USE = 2 (available from area map, click pin to travel)
    * @author Jacques
    */
    public void SetWorldMapGuiStatus(int nStatus)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the status of a world map icon.
    *
    * @param oLocation - the location.
    * @param nStatusId - the status
    * @param bSuppressActiveFlash - If nStatusId == WM_LOCATION_ACTIVE, set this flag to TRUE if you do not want the Vector3 to "flash". This flag has no xEffect if nStatusId != WM_LOCATION_ACTIVE.
    * @author Jacques
    */
    public void SetWorldMapLocationStatus(GameObject oLocation, int nStatusId, int bSuppressActiveFlash = EngineConstants.FALSE)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the player's Vector3 on the map.
    *
    * @param oMap - the map object.
    * @param oLocation - the Vector3 to place the player at.
    * @author Jacques
    */
    public void SetWorldMapPlayerLocation(GameObject oMap, GameObject oLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the primary map object.
    *
    * @param oMapId - the map to set as primary.
    * @author Jacques
    */
    public void SetWorldMapPrimary(GameObject oMapId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Force the world map to be shown.
    *
    * @author Jacques
    */
    public void OpenPrimaryWorldMap()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Force the world map to be closed.
    *
    * @author Curtis Onuczko
    */
    public void ClosePrimaryWorldMap()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether swapping to the primary/secondary world map swap options is enabled/disabled
    *
    * @author Paul Schultz
    */
    public void SetMapSwapEnabled(int nSwapID, int bEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Force the fade map to be shown.
    *
    * @author Paul Schultz
    */
    public void OpenFadeMap()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes the fog of war from the current area map.
    *
    * @author Jacques Lebrun
    */
    public void RevealCurrentMap()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Start traveling on the world map to the Vector3 clicked. If a random encounter area tag and waypoint tag were specified then travel on the world map and have a random encounter at the specified location. Otherwise, sends the EngineConstants.EVENT_TYPE_FINISHED_TRAVEL xEvent when completed.
    *
    * @param  sRandomEncounterArea        - tag of the random encounter target area
    * @param  sRandomEncounterWaypointTag - tag of the random encounter target waypoint
    * @param  oSourceLocationOverride     - map pin override GameObject to start travelling from on the world map
    * @author Curtis Onuczko
    */
    public void WorldMapStartTravelling(string sRandomEncounterArea = "", string sRandomEncounterWaypointTag = "", GameObject oSourceLocationOverride = null)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Continue traveling on the world map from the Vector3 that the random encounter occurred to the Vector3 that was previously clicked. Sends the EngineConstants.EVENT_TYPE_FINISHED_TRAVEL xEvent when completed.
    *
    * @author Curtis Onuczko
    */
    public void WorldMapCompleteRandomEncounter()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the loc name of a world map icon.
    *
    * @param oLocation - the location.
    * @param nStringId - the string ID of the name of the new location
    * @author Curtis O.
    */
    public void SetWorldMapLocationLocName(GameObject oLocation, int nStringID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Retrive the base value of an item.
    *
    * @author Jacques Lebrun
    */
    public int GetItemValue(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Item Properties
    /***************************************************************/
    /* @addtogroup item_prop Item Properties Functions
    *
    * Functions to get or set specific item properties on items.
    *
    */
    /* @{*/

    /* @brief Return an array with item properties present on an object
    *
    * @param   oitem            - the item
    * @param   bIncludeSubItems - include runes properties
    * @param   nDesiredType - if -1 returns all types, otherwise, only properties of the specified type
    * @returns Array with item properties
    * @author Georg Zoeller
    */
    public List<int> GetItemProperties(GameObject oItem, int bIncludeSubItems, int nDesiredType = -1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Return the power level of an item properties present on an object
    *
    * @param   oitem            - the item
    * @param   nPropertyId      - the property
    * @param   bIncludeSubItems - include runes properties
    * @param   bScalePower      - return the scaled power instead of the base value
    * @returns the propertie's power level
    * @author Georg Zoeller
    */
    public float GetItemPropertyPower(GameObject oItem, int nPropertyId, int bIncludeSubItems, int bScalePower = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Adds an item property
    *
    * @param   oitem    - the Item
    * @param   nProperty    - the Property
    * @param   nPower   - the POWER_LEVEL of the item property
    * @param   bRefreshItem - choose to recalculate parts of the item (cost, icon, etc...)
    * @author Georg Zoeller
    */
    public void AddItemProperty(GameObject oItem, int nProperty, float nPower, int bRefreshItem = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes an item property
    *
    * Removes the item property of type nProperty from oItem
    *
    * @param   oitem    - the Item
    * @param   nProperty    - the Property
    * @author Georg Zoeller
    */
    public void RemoveItemProperty(GameObject oItem, int nProperty)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Query the 'droppable' property of an item
    *
    * Returns whether or not an item is droppable
    *
    * @param   oitem    - the Item
    * @author Georg Zoeller
    */
    public int IsItemDroppable(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the 'droppable' property of an item
    *
    * Sets whether or not an item is droppable
    *
    * @param   oitem    - the Item
    * @param   bDroppable   - whether or not it is droppable
    * @author Georg Zoeller
    */
    public void SetItemDroppable(GameObject oItem, int bDroppable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the level of an item
    *
    * Sets the level of the item, used for scaling purposes
    *
    * @param   oitem    - the Item
    * @param   nLevel   - new item level
    * @author Nicolas Ng Man Sun
    */
    public void SetItemLevel(GameObject oItem, int nLevel)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the level of an item
    *
    * Returns the level of the item
    *
    * @param   oitem    - the Item of interest
    * @author Nicolas Ng Man Sun
    */
    public int GetItemLevel(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the 2da-row id of the item variation
    *
    * Get the 2da-row id of the item variation
    *
    * @param   oitem    - the Item of interest
    * @author Nicolas Ng Man Sun
    */
    public int GetItemVariation(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Query the 'Stealable' property of an item
    *
    * Returns whether or not an item is stealable
    *
    * @param   oitem    - the Item of interest
    * @author Nicolas Ng Man Sun
    */
    public int IsItemStealable(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* zDA:O @brief Get a stat modifier of the item (including material modifier)
    *
    * Queries the items and material 2DAs to get the stat modifier
    * that a certain item has when equipped.
    *
    * @param   oitem    - the Item
    * @param   nStatType - The stat that is being queried
    * @author Gabo
*/

    public float GetItemStat(GameObject oItem, int nStatType)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /* @brief Returns a specified value from DragonAge.ini
    *
    * Returns a specified value from DragonAge.ini as stored in the Options
    *
    * @param  sHeadingLabel - the Ini Section of the value (Leave blank for all sections)
    * @param  svalueLabel - The label for the ini value
    * @author Owen Borstad
    */
    public string ReadIniEntry(string sHeadingLabel, string sValueLabel)
    {

        TextAsset iniTextAsset;
        iniTextAsset = Resources.Load(System.IO.Path.GetFileNameWithoutExtension("DragonAge")) as TextAsset;
        File.WriteAllBytes(Application.persistentDataPath + "/DragonAge.ini", iniTextAsset.bytes);

        string f = Application.persistentDataPath + "/DragonAge.ini";
        using (var input = File.OpenText(f))
        {
            string line;
            while (null != (line = input.ReadLine()))
            {
                if (line.IndexOf(sValueLabel) != -1)
                {
                    throw new NotImplementedException();
                }
            }
        }
        File.Delete(f);

        return string.Empty;
    }

    /* @brief Returns the platform that the game is running under.
    *
    * Return the ID of the platform that the game is running under.
    *
    * @author Sydney Tang
    */
    public int GetPlatform()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Determines if a package is currently loaded.
    *
    * Returns TRUE if the package identified is currently loaded.
    *
    * @param sPackageUID - The package UID.
    * @returns Returns TRUE if the package identified is currently loaded, FALSE otherwise.
    * @author Gavin Burt
    */
    public int IsPackageLoaded(string sPackageUID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the EffectID played when the item is Hit
    *
    * @author Nicolas Ng Man Sun
    */
    //int GetItemOnHitEffectID(GameObject oItem) 

    /* @brief Returns the Power of the xEffect played when the item is Hit
    *
    * @author Nicolas Ng Man Sun
    */
    //int GetItemOnHitPower(GameObject oItem) 

    /* @brief Sets the xEffect played when the item is hit and its power 
    *
    * @author Nicolas Ng Man Sun
    */
    //void SetItemOnHitProperties(GameObject oItem, int nEffectId, int nPower) 

    /* @brief Sets the active plot action set, or zero to hide the control.
    *
    * See plotactions 2DA.
    *
    * @author Jacques Lebrun
    */
    public void SetPlotActionSet(int nPlotActionSet)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Turns off user interactions with the plot actions control.
    *
    * @author Jacques Lebrun
    */
    public void SetPlotActionsEnabled(int nEnabled)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the state of a plot action.
    *
    * 0 - Invalid  - action will not appear in the control.
    * 1 - Enabled  - action appears in the control and can be activated by the user
    *                unless the control is disabled
    * 2 - Disabled - action has been already used by the user and cannot be used again.
    * 3 - Active   - action is currently active.
    *
    * @author Jacques Lebrun
    */
    public void SetPlotActionState(int nPlotActionId, int nPlotActionState)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the current state of the plot action.
    *
    * @author Jacques Lebrun
    */
    public int GetPlotActionState(int nPlotActionId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the count value of the plot action to be displayed on the GUI.
    *
    * @author Jacques Lebrun
    */
    public void SetPlotActionCount(int nPlotActionId, int nPlotActionCount)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the current count value of the plot action.
    *
    * @author Jacques Lebrun
    */
    public int GetPlotActionCount(int nPlotActionId)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set a cooldown for the indicated plot action. When the cooldown is finished an Event sporting the indicated xEvent ID & the PlotActionID (in Integer argument 0) will be fired to the specified location)
    * @param nPlotActionID: Id of the plot action
    * @param fCooldownTime: amount of cooldown
    * @param nEventID - The ID of the Even fired back to script when the cooldown has completed. This xEvent will have an Integer[0] value identifying the PlotActionID that has finished cooling down
    * @param oObject - The GameObject to signal the xEvent to.
    * @param scriptname - If specified overides the default script
    * @author John Fedorkiw
    */
    public void SetPlotActionCooldown(int nPlotActionId, float fCooldownTime, int nEventID, GameObject oObject, string scriptname = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether the creature satifies *all* the requirements for using the item
    *
    * @author Nicolas Ng Man Sun
    */
    public int CanUseItem(GameObject oCreature, GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether the creature satifies a retriction property.  This is used only as a response
    *          to the EngineConstants.EVENT_TYPE_ITEM_ONTEST_USABLE event. 
    *
    * @author Nicolas Ng Man Sun
    */
    public void SetCanUseItem(GameObject oCreature, int value)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets whether the creature or placeable can be interacted with.
    *          Non-interactive objects act like geometries in that mousing
    *          over them doesn't not trigger any selection and you can click through
    *          them
    * @author Nicolas Ng Man Sun
    */
    public void SetObjectInteractive(GameObject oObject, int value)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether the creature or placeable can be interacted with.
    *
    * @author Nicolas Ng Man Sun
    */
    public int GetObjectInteractive(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* zDA:O @brief Returns the item's material progression.
*
* @author Curtis Onuczko
*/
    public int GetItemMaterialProgression(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether the item is unique.
    *
    * @author Curtis Onuczko
    */
    public int GetItemUnique(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief In N frames, send the indicated raw input event. This xEvent is queued and handled with the rest of the input(I.e not immediatly) Currently Available Events: (Available Events: '[Left|Right|Middle]MouseButton_[Pressed|Released|DobleClick]', eg: RightMouseButton_Released)
    *
    * @author John Fedorkiw
    */
    public void DEBUG_SendRawInputEvent(int nFrameNumber, string sRawEventName)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Set the position of the cursor -- THIS SHOULD ONLY BE USED FOR DEBUGGING PURPOSES!
    *
    * @author John Fedorkiw
    */
    public void DEBUG_SetCursorPosition(int x, int y)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the value of the game setting: Auto level party members
    *
    * @author Cody Watts
    */
    public int GetAutoLevelFollowers()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief !DEPRECATED!
    *
    *  DEPRECATED. Use GetAttributeBool("ClientOptions.GameOptions.ShowHostileDamageNumbers");
    *
    * @returns  RETURNS FALSE -- THIS IS A DEPRECATED FUNCTION!
    * @author John Fedorkiw
    */
    public int GetShowSpecialMoveFloaties()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the value of the "Show Tutorials" option
    *
    * Returns whether or not we should be displaying Special Move floaties
    *
    * @returns  Returns the value of the "Show Tutorials" option
    * @author John Fedorkiw
    */
    public int GetShowTutorials()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief GetPotionOption
    *   Return the player's selected quick heal potion selection. 0 is "Use smallest potion", 1 is "use most appropriate potion".
    *
    * @author Sebastian Hanlon
    */
    public int GetPotionOption()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the creature as a ghost.  Ghost creatures can go through regular creatures but will pathfind around other ghosts.
    *
    * @author Nicolas Ng Man Sun
    */
    public void SetCreatureIsGhost(GameObject oObject, int value)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Turns creature into a statue.  Statues have their animations frozen and cannot move.
    *
    * @author Nicolas Ng Man Sun
    */
    public void SetCreatureIsStatue(GameObject oObject, int value)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Starts the specified creature's heart beating
    *
    * @author Cody Watts
    */
    public void InitHeartbeat(GameObject oCreature, float fInterval)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Stops the creature's heartbeat
    *
    * @author Cody Watts
    */
    public void EndHeartbeat(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Links the minimaps of two areas - what is explored in one will appear as explored in the other.
    *
    * @param   sFirstArea - The string reference for the first area
    * @param   sSecondArea - The string reference for the second area
    * @returns TRUE on success, FALSE otherwise.
    * @author  Cody Watts
    */
    public int LinkAreaMiniMaps(string sFirstArea, string sSecondArea)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the inventory slot this item is equipped in, or EngineConstants.INVENTORY_SLOT_INVALID, if not equipped.
    *
    * @param oItem - The item in question.
    * @returns The inventory slot, or EngineConstants.INVENTORY_SLOT_INVALID, if not equipped.
    * @author  Cody Watts
    */
    public int GetItemEquipSlot(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets an item to be irremovable. Once equipped, an irremovable item cannot be unequipped by the player, only by scripting.
    *
    * @param oItem - The item to be made irremovable.
    * @param bIrremovable - The irremovable flag; TRUE makes the GameObject irremovable, FALSE makes it removable once again.
    * @author  Cody Watts
    */
    public void SetItemIrremovable(GameObject oItem, int bIrremovable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether a given item is irremovable.
    *
    * @returns TRUE if the item is irremovable, FALSE otherwise.
    * @author  Cody Watts
    */
    public int IsItemIrremovable(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets an item to be indestructible. Indestructible items cannot be destroyed by the player.
    *
    * @param oItem - The item to be made indestructible.
    * @param bIndestructible - The indestructible flag; TRUE makes the GameObject indestructible, FALSE makes it destructible once again.
    * @author  Cody Watts
    */
    public void SetItemIndestructible(GameObject oItem, int bIndestructible)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether a given item is indestructible.
    *
    * @returns TRUE if the item is indestructible, FALSE otherwise.
    * @author  Cody Watts
    */
    public int IsItemIndestructible(GameObject oItem)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Determines if an GameObject is humanoid.
    *
    * Returns TRUE if the GameObject oObject uses the humanoid combat blend tree animations.
    * Returns FALSE if oObject is not a creature or does not use the humanoid animations.
    *
    * @param oObject - The GameObject that is to be checked for humamoind status.
    * @author Craig Welburn
    */
    public int IsHumanoid(GameObject oObject)
    {
        int nAppearance = GetLocalInt(oObject, "Appearance");
        if (GetM2DAString(EngineConstants.TABLE_APPEARANCE, "MODELTREE", nAppearance).IndexOf("humanoid") != -1)
        {
            return EngineConstants.TRUE;
        }
        return EngineConstants.FALSE;

    }

    /* @brief Returns stored character generation slider value
    *
    * This is used primarily to determine a specific visual trait of the player creature.
    * @param nIndex  0 = head shape, 1 = skin colour
    *
    * @author Nicolas Ng Man Sun
    */
    public float GetCharGenSliderValue(int nIndex)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Forces a creature to appear on the player's minimap as a blue dot. This can
    * be used to highlight non-hostile creatures of interest, such as NPCs that are
    * fighting alongside your party.
    *
    * @param oCreature - The creature to show on the map
    * @param bEnable - TRUE to make the creature appear on the map, and FALSE to hide it.
    * @author Cody Watts
    */
    public void ShowAsAllyOnMap(GameObject oCreature, int bEnable)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the 2DA row ID to use for getting a given creature's background defaults.
    *          The 2DA referred to is background_defaults in rules\background.xls
    *
    * @param oCreature - The creature whose background defaults you're interested in
    * @author Paul Schultz
    */
    public int GetBackgroundDefaultsIndex(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the formation Vector3 of the given party member as if it was following the leader
    *
    * @author Nicolas Ng Man Sun
    */
    public Vector3 GetFollowerWouldBeLocation(GameObject oObject)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the user's zoom level
    *
    * @param fZoomLevel - desired zoom level where 0 is fully zoomed out, 1 is fully zoomed in.
    * @author Jacques Lebrun
    */
    public void SetZoomLevel(float fZoomLevel)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Reset the camera facing direction to align with the player creature.
    *
    * @author Jacques Lebrun
    */
    public void ResetCameraFacing()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Unlocks the achievement for the current player. This is slow and should no long be used. Use UnlockAchievementByID(int nID) instead.
    *
    * @author Craig Welburn
    */
    public void UnlockAchievement(string sAchievementID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Unlocks the achievement for the current player.
    *
    * @author Craig Welburn
    */
    public void UnlockAchievementByID(int nID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether or not the player has unlocked the specified achievement. This is slow and should no longer be used. Use GetHasAchievementByID(int nID) instead.
    *
    * @author Craig Welburn
    */
    public int GetHasAchievement(string sAchievementID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns whether or not the player has unlocked the specified achievement.
    *
    * @author Craig Welburn
    */
    public int GetHasAchievementByID(int nID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the count for the specified achievement (i.e. the count towards the unlocked at value).
    *
    * @author Craig Welburn
    */
    public int GetAchievementCountByID(int nID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Increments the count for the specified achievement by the amount specified. 
    * Returns true if the achievement was unlocked due to the increment, false otherwise.
    *
    * @author Craig Welburn
    */
    public int IncrementAchievementCountByID(int nID, int nIncrement = 1)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Updates the specified online statistic for the player.
    *
    * @author Craig Welburn
    */
    public void UpdateOnlineStatistic(string sStatName, int nUpdateType, int nUpdatePeriod, float fStatValue, string sStatText)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Gets the specified online statistic for the player.
    *
    * @author Craig Welburn
    */
    public float GetOnlineStatistic(string sStatName, int nPeriodType)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sends the specified online telemetry for the player.
    *
    * @author Craig Welburn
    */
    public void SendOnlineTelemetry(int nModuleID, int nGroupID, string sTelemetryText)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sends the screen shot online for the player.
    *
    * @author Craig Welburn
    */
    public void SendOnlineScreenShot(string sFileName, string sShortDescription, string sLongDescription)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Exposes ECString.Split to the scripting language using L"," as hardcoded delimiter.
    *          This is reserved for optimization, do not use, it is not fully implemented
    *
    * @author Georg Zoeller
    */
    public string[] ECSplitString(string sString)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Scales all items equipped on the creature based on their material progression table
    *  
    *   For this to work, a number of conditions need to be met
    *   - area must have a valid area_id entry in ability_data
    *   - creature must not be a party member
    *
    *   @param nTargetLevel - Target Level
    *
    *   @author Georg Zoeller
    *
    */
    public void ScaleEquippedItems(GameObject oCreature, int nTargetLevel)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns TRUE if a creature is currently moving
    *   @param oCreature - the creature to check movement for
    *   @author Jose
    */
    public int IsMoving(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets a flag int the build marking it dirty.
    *  
    *   This function should be called in all the cheat scripts that can mess with the build.
    *
    *   @author Bogdan Corciova
    *
    */
    public void SetCheatUsedFlag()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Shows all the codex entries in the journal.
    *  
    *   This is a DEBUG function. It will show all the codex entries in the journal.
    *
    *   @author Bogdan Corciova
    *
    */
    public void ShowAllCodexEntries()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the roam Vector3 for the creature.
    *  
    *
    *   @author Pat LaBine
    *
    */
    public void SetRoamLocation(GameObject oCreature, Vector3 lLocation)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Sets the roam radius for the creature.
    *  
    *
    *   @author Pat LaBine
    *
    */
    public void SetRoamRadius(GameObject oCreature, float fRadius)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the roam Location for the creature.
    *  
    *
    *   @author Pat LaBine
    *
    */
    public Vector3 GetRoamLocation(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Returns the current plot assist option setting of the game.

    * @returns Returns a plot assist constant EngineConstants.GAME_PLOT_ASSSIST_*
    * @author Craig Welburn
    */
    public int GetGamePlotAssist()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the body bag placeable associated with this creature..
    *  
    *
    *   @author Pat LaBine
    *
    */
    public GameObject GetCreatureBodyBag(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Determine whether or not a piece of Post Release Content (PRC) is installed, enabled and authorized.
    *
    *  @param sPRCName  - Must correspond to the PRC's AddInItem UID from the AddIns.xml file.
    *  
    *   @returns Returns true if the PRC specified is installed, currently enabled and authorized.
    *   @author Craig Welburn
    *
    */
    public int GetPRCEnabled(string sPRCName = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Get the time that has elapsed for this particular playthrough of the game.
    *
    *  
    *   @returns Returns the elapsed playthrough time in seconds.
    *   @author Craig Welburn
    *
    */
    public int GetPlayTime()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Buy/Download downloadable content.
    *
    *  @param sOfferId  - Must be a valid offer.
    *  
    *   @returns Returns true if the BuyDownload msg was successfully dispatched.
    *   @author Chris Smith
    *
    */
    public int BuyDownload(string sOfferId = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Special Post Campaign save for PRC.
    *
    *  @param none
    *
    *   @author Chris Smith
    *
    */
    public void SaveGamePostCampaign()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Count the areas in the active savegame for a given group
    *
    *  @param nGroup - Group number, or -1 for 'all areas'
    *  @returns number of areas for which data would be cleared
    *  @author David Robinson
    *
    */
    public int CountAreasInSavegameByGroup(int nGroupID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Removes area data from active savegame for a given group
    *
    *  @param nGroup - Group number, or -1 for 'all areas'
    *  @returns number of areas for which data was cleared
    *
    *  @author David Robinson
    *
    */
    public int PurgeAreaGroupFromSavegame(int nGroupID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Check if any offers or addins exist.
    *  @param none
    *
    *   @author Chris Smith
    *
    */
    public int IsPRCAvailable()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Logs an xEvent for the hero's "Story So Far" in the save game.
    *  @param nEventID - The ID of the Event being logged.
    *
    *   @author Craig Welburn
    *
    */
    public void LogStoryEvent(int nEventID)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Moves a quest or a whole quest group into the completed section.
    *  @param sGroup - The plot or group string reference to move to completed section.
    *
    *   @author Jonathan Ferland
    *
    */
    public void CloseQuestGroup(string sGroupOrPlotResRef)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Remove a quest.
    *  @param sGroup - The plot string reference to remove.
    *
    *   @author Jonathan Ferland
    *
    */
    public void RemoveQuest(string sPlotResRef)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Opens the Journal PRC Tab.
    *  @param sOfferId - The offer id to select when the PRC tab opens.
    *
    *   @author Eric Paquette
    *
    */
    public void OpenJournalPRCTab(string sOfferId = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // EOR Change
    // Mark L
    // Adding script xCommand for rumble
    /* @brief This starts a rumble event
    *
    *   @returns VOID
    *   @param fLife - Lifetime of the rumble
    *   @param fIntensity - A scalar to the shape for determining the strength of the rumble
    *   @param fBaseIntensity - Added to the shape*scalar for the strength of the rumble
    *   @param iType - Shape of the rumble (0 constant, 1 ramp up, 2 ramp down, 3 sine wave)
    *   @param fIterations - Number of times to repeat the shape during the lifetime of the rumble
    *   @author Gabo
    */
    public void StartRumbleEvent(float fLife, float fIntensity, float fBaseIntensity, int iType, float fIterations)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    // End EOR

    /* @brief Plays a hit xEffect package (screenshake + rumble)
    *  @param nHitEffect - index into hiteffects 2da
    *  @param nStepUp - steps up the intensity of the xEffect (if possible)
    *
    *   @author Noel Borstad
    *
    */
    public void DoPlayerHitEffect(int nHitEffect, int nStepUp = 0)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Determines whether specific content appears in the market place.
    *  @param sOfferId - The offer id to check.
    *
    *   @author Jonathan Ferland
    *
    */
    public int IsContentAvailableInMarketPlace(string sOfferId = "")
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public void SpawnProjectiveDecal(Vector3 vPosition, Vector3 vDir, int nType)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief 
    *
    * Spawns projective decals randomly placed around an object
    *
    * @author Noel
    */
    public void SpawnRandomDecals(GameObject oTarget, int nCount, float fRange, int nType)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public float CombatGetOptimalValue(int nLevel, int nProperty)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    public float CombatGetPercentageFromScaled(int nLevel, float fScaled, int nProperty)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief 
    *
    *Get the current animation of a given object.
    *
    * @param o - An object
    * @author Jon Cooper
    */
    public int GetCurrentAnimation(GameObject o)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Preloads a creature for a conversation
    *
    * @param oCreature The creature to preload
    *
    * @author Jon Thompson
    */
    public void PreloadCreatureForConversation(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Unloads a preloaded a creature for a conversation
    *
    * @param oCreature The creature to unload
    *
    * @author Jon Thompson
    */
    public void UnloadCreatureForConversation(GameObject oCreature)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /***************************************************************/
    // Public Demo Flag
    /***************************************************************/
    /* @brief Checks if the game is in Public Demo Mode
    *
    * @returns 1 in Public Demo Mode, 0 otherwise
    * @author James Redford
    */
    public int PublicDemo()
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /***************************************************************/
    // Camera
    /***************************************************************/
    /* @addtogroup camera Camera Functions
    *
    * Functions to control the camera
    */
    /* @{*/

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Journal
    /***************************************************************/
    /* @addtogroup journal Journal Functions
    *
    * Functions to manage the journal
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_ADDJOURNALENTRY = 444;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_REMOVEJOURNALENTRY = 445;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETJOURNALENTRYXP = 446;

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Messaging
    /***************************************************************/
    /* @addtogroup messaging Messaging Functions
    *
    * Functions to send messages inside the game
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SENDMESSAGETOPLAYER = 472;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SENDMESSAGETOPLAYERBYSTRREF = 473;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SENDMESSAGETOGM = 474;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SENDFEEDBACKTOPLAYER = 475;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SENDFEEDBACKTOPLAYERBYSTRREF = 476;

    /* @}*/
    /***************************************************************/

    /***************************************************************/
    // Time Functions
    /***************************************************************/
    /* @addtogroup time Time Functions
    *
    * Functions to handle time (get and set)
    */
    /* @{*/

    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_SETTIME = 495;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETTIMEHOUR = 496;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETTIMEMINUTE = 497;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETTIMESECOND = 498;
    // const INT CVirtualMachineCommands::EngineConstants.COMMAND_GETTIMEMILLISECOND = 499;

    /* @}*/
    /***************************************************************/

    //#endregion

    //MATH
    public float RandomFloat()
    {
        return Random.Range(0f, 1f);
    }

    public int Engine_Random(int max)
    {
        return Random.Range(0, max);
    }

    public float fabs(float fValue)
    {
        return Mathf.Abs(fValue);
    }

    public int abs(int nValue)
    {
        return Mathf.Abs(nValue);
    }

    /* @brief Returns a base value to an exponent.
*
* Returns the float fValue raised to the power of float fExponent.
*
* @param fValue - Base value.
* @param fExponent - Exponent value.
* @returns The value of fValue raised to the power of fExponent.
* @sa sqrt()
* @author Brenon
*/

    public float pow(float fValue, float fExponent)
    {
        return Mathf.Pow(fValue, fExponent);
    }

    public float sin(float fValue)
    {
        return Mathf.Sin(fValue);
    }

    public float cos(float fValue)
    {
        return Mathf.Cos(fValue);
    }

    #region DHK Custom Functions

    //this function returns the name of the XML node, for Convenience
    public string GetNodeName(int n2DA)
    {
        //TO DO integer to table name
        Debug.Log("Table request: " + n2DA);
        switch (n2DA)
        {
            case EngineConstants.TABLE_AREA_LOAD_HINT:
            case EngineConstants.TABLE_AREA_LOAD_HINT_VLOW:
            case EngineConstants.TABLE_AREA_LOAD_HINT_LOW:
            case EngineConstants.TABLE_AREA_LOAD_HINT_MID:
            case EngineConstants.TABLE_AREA_LOAD_HINT_HIGH: return "loadhint";
            case EngineConstants.TABLE_ABILITIES: return "ability";
            case EngineConstants.TABLE_EVENTS: return "event";
            case EngineConstants.TABLE_APPEARANCE: return "appearance";
            case EngineConstants.TABLE_AREA_DATA: return "area";
            case EngineConstants.TABLE_RESOURCES: return "resource";
            case EngineConstants.TABLE_PROPERTIES: return "property";
            case EngineConstants.TABLE_ITEMS: return "item";
            case EngineConstants.TABLE_EXPERIENCE: return "experience";
            case EngineConstants.TABLE_RULES_CLASSES: return "class";
            case EngineConstants.TABLE_RULES_RACES: return "race";
            case EngineConstants.TABLE_PACKAGES: return "package";
            case EngineConstants.TABLE_AUTOSCALE: return "autoscale";
            case EngineConstants.TABLE_AUTOSCALE_DATA: return "autoscaledata";
            case EngineConstants.TABLE_DIFFICULTY: return "difficulty";
            case EngineConstants.TABLE_UI_MESSAGES: return "uimessage";
            case EngineConstants.TABLE_COMMANDS: return "command";
            default: throw new NotImplementedException();
                //default: Warning("table not found: " + n2DA); return "";
        }
    }

    //this function returns the XML resource
    public XmlDocument GetXML(int n2DA)
    {
        string _resource = "";

        switch (n2DA)
        {
            case EngineConstants.TABLE_AREA_LOAD_HINT: _resource = "LoadHints"; break;
            case EngineConstants.TABLE_AREA_LOAD_HINT_VLOW: _resource = "LoadHintsVLowLevel"; break;
            case EngineConstants.TABLE_UI_MESSAGES: _resource = "UIMessages"; break;
            case EngineConstants.TABLE_PROPERTIES: _resource = "Properties"; break;
            case EngineConstants.TABLE_PACKAGES: _resource = "Packages"; break;
            case EngineConstants.TABLE_RESOURCES: _resource = "Resources"; break;
            case EngineConstants.TABLE_TALK: _resource = "TalkTable"; break;
            case EngineConstants.TABLE_GAME_SETTINGS: _resource = "GameSettings"; break;
            case EngineConstants.TABLE_EVENTS: _resource = "EngineEvents"; break;
            case EngineConstants.TABLE_APPEARANCE: _resource = "AppearanceBase"; break;
            case EngineConstants.TABLE_ABILITIES: _resource = "AbilitiesBase"; break;
            case EngineConstants.TABLE_AREA_DATA: _resource = "AreaData"; break;
            case EngineConstants.TABLE_EXPERIENCE: _resource = "Experience"; break;
            case EngineConstants.TABLE_ITEMS: _resource = "BaseItem"; break;
            case EngineConstants.TABLE_RULES_CLASSES: _resource = "BaseClass"; break;
            case EngineConstants.TABLE_RULES_RACES: _resource = "BaseRace"; break;
            case EngineConstants.TABLE_AUTOSCALE: _resource = "AutoScale"; break;
            case EngineConstants.TABLE_AUTOSCALE_DATA: _resource = "AutoscaleData"; break;
            case EngineConstants.TABLE_DIFFICULTY: _resource = "Difficulty"; break;
            case EngineConstants.TABLE_COMMANDS: _resource = "Commands"; break;
            default: _resource = ""; break;
        }

        if (_resource != "")
        {
            TextAsset textAsset = (TextAsset)Resources.Load(_resource, typeof(TextAsset));
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(textAsset.text);
            return xmldoc;
        }
        else
        {
            Debug.Log("update me");
            throw new NotImplementedException();
        }
    }

    public int GetGameSettings(string setting)
    {
        XmlDocument xmldoc = GetXML(EngineConstants.TABLE_GAME_SETTINGS);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        int gameSetting = Convert.ToInt32((string)
            (from el in root.Elements("GameSetting")
             where (string)el.Attribute("Type") == setting
             select el).First().Attribute("Value").Value);

        return gameSetting;
    }

    public string GetResourceType(string fullpath)
    {
        XmlDocument xmldoc = new XmlDocument();
        FileStream fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read);
        xmldoc.Load(fs);
        XmlElement root = xmldoc.DocumentElement;
        String resType = string.Empty;
        if (root.HasAttribute("ResType"))
        {
            resType = root.GetAttribute("ResType");
            //Console.WriteLine(resType);
        }
        else resType = "TYPE NOT FOUND";
        return resType;
    }

    public string GetResource(string nodeIn, string value, string nodeOutValue, string nodeType = "")
    {
        XmlDocument xmldoc = GetXML(EngineConstants.TABLE_RESOURCES);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        string _nodeOutValue;
        if (nodeType != "")
        {
            _nodeOutValue = (string)
            (from el in root.Elements("Resource")
             where (string)el.Element(nodeIn) == value &&
             (string)el.Element("Type") == nodeType
             select el).First().Element(nodeOutValue).Value;
            return _nodeOutValue;
        }
        _nodeOutValue = (string)
            (from el in root.Elements("Resource")
             where (string)el.Element(nodeIn) == value
             select el).First().Element(nodeOutValue).Value;
        return _nodeOutValue;
    }

    public string _GetLocal(GameObject oObject, string sKey)
    {
        //using dictionary instead of reflection, instead of something like
        //var value = targetObject.GetType().GetProperty(vari).GetValue(targetObject, null);

        /*Debug.Log("locals: may need to check if it exists xGameObject on object…");
        string sRet = "";
        if (oObject.GetComponent<xGameObjectBase>().sLocals.TryGetValue(sKey, out sRet))
        {
            //if found, return value
            return sRet;
        }
        else
        {
            //if not found, create Fake key 4*=****
            //oObject.GetComponent<xGameObjectBase>().sLocals[sKey] = sRet;
            sRet = EngineConstants.NA_STRING;
            SetLocal(oObject, sKey, sRet);
        }
        return sRet;*/
        throw new NotImplementedException();
    }

    public void _SetLocal(GameObject oObject, string sKey, string sValue)
    {
        //Debug.Log("locals: may need to check if it exists xGameObject on object…");
        //oObject.GetComponent<xGameObjectBase>().sLocals[sKey] = sValue;
        throw new NotImplementedException();
    }

    public string GetScriptName(string sName)
    {
        string[] a = sName.Split(null);
        string sRet = String.Empty;
        /*foreach (var s in a[a.Length - 1])
        {
             sRet += s;
        }*/
        sRet = SubString(a[a.Length - 1], 1, a[a.Length - 1].Length - 2);//skip ( and ) characters
        return sRet;
    }

    //Double check performance
    public void CheckZip()
    {
        //Zip file workaround
        //Check to see if the zip file is already present
        string fullZipPathName = Application.persistentDataPath + "/Source.zip";
        if (!File.Exists(fullZipPathName))
        {
            string assetName = "Source";
            TextAsset textAsset = Resources.Load(assetName, typeof(TextAsset)) as TextAsset;
            System.IO.File.WriteAllBytes(fullZipPathName, textAsset.bytes);
        }
        else Console.WriteLine("already exists");
    }

    public void Unzip(string resource, string seed)
    {
        CheckZip();

        //Append XML to name
        string file = Application.persistentDataPath + "/Source/" + resource + seed + ".xml";
        resource += ".xml";

        string fullZipPathName = Application.persistentDataPath + "/Source.zip";
        //Extract resource from zip file
        using (ZipInputStream s = new ZipInputStream(File.OpenRead(fullZipPathName)))
        {
            ZipEntry zipResource;
            while ((zipResource = s.GetNextEntry()) != null)
            {
                if (zipResource.Name == resource)
                {
                    using (FileStream streamWriter = File.Create(file))
                    {
                        int size = 2048;
                        byte[] fdata = new byte[2048];
                        while (true)
                        {
                            size = s.Read(fdata, 0, fdata.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(fdata, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public string GetParameterName<T>(T item) where T : class
    {
        //var t = GetParameterName(new { EngineConstants.AI_WEAPON_SET_RANGED });
        if (item == null)
            return string.Empty;

        return typeof(T).GetProperties()[0].Name;
    }

    public void UpdateGameObjectProperty(GameObject oObject, string key, string value)
    {
        /* example 
        xGameObject t = gameObject.GetComponent<xGameObjectBase>();
                    string k = "test";
                    int v = 50;
                    UpdateGameObjectProperty(t, k, v);*/
        //string[] t = oObject.GetType().ToString().Split();
        char c = (char)46;
        var obt = GetObjectType(oObject.gameObject);
        object x;
        switch (obt)
        {
            case EngineConstants.OBJECT_TYPE_AREA:
                {
                    x = oObject.GetComponent<xGameObjectARE>();
                    break;
                }
            case EngineConstants.OBJECT_TYPE_CREATURE:
                {
                    x = oObject.GetComponent<xGameObjectUTC>();
                    break;
                }
            case EngineConstants.OBJECT_TYPE_MODULE:
                {
                    x = oObject.GetComponent<xGameObjectMOD>();
                    break;
                }
            case EngineConstants.OBJECT_TYPE_PLACEABLE:
                {
                    x = oObject.GetComponent<xGameObjectUTP>();
                    break;
                }
            case EngineConstants.OBJECT_TYPE_WAYPOINT:
                {
                    x = oObject.GetComponent<xGameObjectUTW>();
                    break;
                }
            default: throw new NotImplementedException();
        }

        if (x == null)
        {
            throw new NotImplementedException();
        }
        string o = x.GetType().GetProperty(key).ToString().Split()[0].Split(c)[1];
        switch (o)
        {
            case "Int32":
                {
                    int v = int.Parse(value);
                    x.GetType().GetProperty(key).SetValue(x, v, null);
                    break;
                }
            case "GameObject":
                {
                    GameObject go = GameObject.Find(value);
                    x.GetType().GetProperty(key).SetValue(x, go, null);
                    break;
                }
            case "Vector3":
                {
                    char cm = (char)44;
                    string[] va = value.Split(cm);
                    //For now we put all objects that same height coordinate
                    //Warning("debug mode: 3-D coordinates in complete!");
                    Vector3 v = new Vector3(Convert.ToSingle(va[0]), 0, Convert.ToSingle(va[2]));
                    //Vector3 v = new Vector3(Convert.ToSingle(va[0]), Convert.ToSingle(va[1]), Convert.ToSingle(va[2]));
                    x.GetType().GetProperty(key).SetValue(x, v, null);
                    break;
                }
            case "String":
                {
                    x.GetType().GetProperty(key).SetValue(x, value, null);
                    break;
                }
            case "Boolean":
                {
                    x.GetType().GetProperty(key).SetValue(x, Boolean.Parse(value), null);
                    break;
                }
            case "Single":
                {
                    x.GetType().GetProperty(key).SetValue(x, float.Parse(value), null);
                    break;
                }
            default: throw new NotImplementedException();
        }
    }

    public void ParseArea(GameObject oArea, string rTemplate)
    {
        //Get its template XML, Convert name to file ID
        string id = GetResource("Name", rTemplate, "ID", "are");
        string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
        increment++;

        Unzip(id, seed);

        string f = EngineConstants.SOURCE + id + seed + ".xml";

        //GameObject oArea = GameObject.FindGameObjectWithTag("Area");
        //Load the identified XML template for parsing
        //XmlNode node = doc.SelectSingleNode("//Resource/Agent/ResRefName/text()");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(f);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        XElement agent = root.Element("Agent");

        //Get variables list
        IEnumerable<XElement> vl = agent.Element("VariableList").Elements("Agent");
        foreach (XElement ve in vl)
        {
            //Find property using reflection and set the value accordingly
            UpdateGameObjectProperty(oArea, ve.Element("xname").Value, ve.Element("Data").Value);
            //a.VariableList.Add(ve.Element("xname").Value, ve.Element("Data").Value);
        }

        //Get the script for the area, And add it to object
        string sUriName = GetResource("ID", agent.Element("ScriptURI").Value, "Name");
        //Check if custom script, And add it, otherwise ignore
        if (sUriName != "area_core")
        {
            oArea.GetComponent<xGameObjectBase>().bCustom = EngineConstants.TRUE;
            oArea.gameObject.AddComponent(Type.GetType(sUriName));
        }

        //update all the variables That are Not list
        var e = agent.Elements();
        foreach (var _x in e)
        {
            string _d = _x.Name.ToString();
            if (_d.IndexOf("List") == -1)//If not list
            {
                string _v = _x.Value;
                if (_v != "")
                {
                    UpdateGameObjectProperty(oArea, _d, _v);
                }
            }
        }

        //Get object list to be populated on the area
        IEnumerable<XElement> o = agent.Element("ObjectList").Elements("Agent");
        //List<string> sl = new List<string>();

        string n;
        GameObject on;
        foreach (XElement xe in o)
        {
            string oid = xe.Element("ObjectURI").Value;
            Unzip(oid, seed);
            string fn = EngineConstants.SOURCE + oid + seed + ".xml";
            string tn = GetResourceType(fn);
            switch (tn)
            {
                case "utp":
                    {
                        n = GetResource("ID", oid, "Name");
                        on = CreateObject(EngineConstants.OBJECT_TYPE_PLACEABLE, n, Vector3.zero);
                        //Add placeable properties From area
                        on = ParseAreaPlaceable(on, xe);
                        oArea.gameObject.GetComponent<xGameObjectARE>().ObjectList.Add(on);
                        break;
                    }
                case "utc":
                    {
                        n = GetResource("ID", oid, "Name");
                        on = CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, n, Vector3.zero);
                        //Add creature properties from area
                        on = ParseAreaCreature(on, xe);
                        oArea.gameObject.GetComponent<xGameObjectARE>().ObjectList.Add(on);
                        break;
                    }
                default: break;//resource type not explicitly set, ignoring
            }
            //string tn = GetResourceString("ID", ob, "Type");
            //string hn = GetResourceString("ID", ob);
            /*if (tn == "utc" || tn == "chr")  
            {
                //string fn = GetResourceString("ID", ob, "FullName");
                i++;
                //Console.WriteLine("something");
            }*/
            //sl.Add(oid + "." + tn);
        }

        //Get Waypoint list to be populated on the area
        IEnumerable<XElement> w = agent.Element("WaypointList").Elements("Agent");

        string wn;
        GameObject ow;
        foreach (XElement xe in w)
        {
            wn = xe.Element("Tag").Value;
            ow = CreateObject(EngineConstants.OBJECT_TYPE_WAYPOINT, wn, Vector3.zero);

            //Add waypoint properties on UTW
            //TO DO: handle all elements in a loop
            /*var e = xe.Elements();
            foreach (var _x in e)
            {
                var _d = _x.Name;
                var _v = _x.Value;
                Console.WriteLine("");
            }*/

            //For now, manually, Few of them, relevant
            UpdateGameObjectProperty(ow, xe.Element("Tag").Name.ToString(), xe.Element("Tag").Value);
            UpdateGameObjectProperty(ow, xe.Element("WaypointName").Name.ToString(), xe.Element("WaypointName").Value);
            UpdateGameObjectProperty(ow, xe.Element("position").Name.ToString(), xe.Element("position").Value);
            UpdateGameObjectProperty(ow, xe.Element("orientation").Name.ToString(), xe.Element("orientation").Value);

            //Update object position And orientation
            ow.gameObject.transform.position = ow.gameObject.GetComponent<xGameObjectUTW>().position;
            var rot = ow.gameObject.GetComponent<xGameObjectUTW>().orientation;
            ow.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
        }

        /*string rrn = (string)
            (from el in root.Elements("Resource")
             where (int)el.Element("Agent").Element( "ResRefName") == nStrRef
             select el).First().Element("Agent").Value;*/
        //Create XML from stream

    }

    public void ParsePlaceable(GameObject oPlaceable, string rTemplate)
    {
        //Add the Cylinder as visual aid
        GameObject oC = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        oC.GetComponent<Renderer>().material.color = Color.magenta;
        oC.transform.parent = oPlaceable.transform;

        //Get its template XML, Convert name to file ID
        string id = GetResource("Name", rTemplate, "ID", "utp");
        string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
        increment++;

        Unzip(id, seed);

        string f = EngineConstants.SOURCE + id + seed + ".xml";

        //Load the identified XML template for parsing
        //XmlNode node = doc.SelectSingleNode("//Resource/Agent/ResRefName/text()");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(f);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        XElement agent = root.Element("Agent");

        //Get variables list
        IEnumerable<XElement> vl = agent.Element("VariableList").Elements("Agent");
        foreach (XElement ve in vl)
        {
            //Find property using reflection and set the value accordingly
            UpdateGameObjectProperty(oPlaceable, ve.Element("xname").Value, ve.Element("Data").Value);
            //Check for world map
            if (ve.Element("Data").Value == "world_map")
            {
                Color color = new Color(0.2F, 0.4F, 0.8F, 1F);
                oC.GetComponent<Renderer>().material.color = color;
                oPlaceable.tag = "WorldMap";
            }
        }

        //Add script URI
        string sUriName = GetResource("ID", agent.Element("ScriptURI").Value, "Name");
        //Check if custom script, And add it, otherwise ignore
        if (sUriName != "placeable_core")
        {
            oPlaceable.GetComponent<xGameObjectBase>().bCustom = EngineConstants.TRUE;
            oPlaceable.gameObject.AddComponent(Type.GetType(sUriName));
        }

        //update all the variables That are Not list
        var e = agent.Elements();
        foreach (var _x in e)
        {
            string _d = _x.Name.ToString();
            if (_d.IndexOf("List") == -1)//If not list
            {
                string _v = _x.Value;
                if (_v != "")
                {
                    UpdateGameObjectProperty(oPlaceable, _d, _v);
                }
            }
        }
        /*//Manually update some relevant variables during debugging tests
        UpdateGameObjectProperty(oPlaceable, agent.Element("xname").Name.ToString(), agent.Element("xname").Value);
        UpdateGameObjectProperty(oPlaceable, agent.Element("Tag").Name.ToString(), agent.Element("Tag").Value);
        UpdateGameObjectProperty(oPlaceable, agent.Element("Appearance").Name.ToString(), agent.Element("Appearance").Value);
        UpdateGameObjectProperty(oPlaceable, agent.Element("Useable").Name.ToString(), agent.Element("Useable").Value);*/

    }

    public void ParseCreature(GameObject oCreature, string rTemplate)
    {
        //Add the Sphere as visual aid
        GameObject oS = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //oS.GetComponent<Renderer>().material.color = Color.grey;//Neutral
        oS.transform.parent = oCreature.transform;

        //Get its template XML, Convert name to file ID
        string id = GetResource("Name", rTemplate, "ID", "utc");
        string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
        increment++;

        Unzip(id, seed);

        string f = EngineConstants.SOURCE + id + seed + ".xml";

        //Load the identified XML template for parsing
        //XmlNode node = doc.SelectSingleNode("//Resource/Agent/ResRefName/text()");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(f);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        XElement agent = root.Element("Agent");

        //Get variables list
        IEnumerable<XElement> vl = agent.Element("VariableList").Elements("Agent");
        foreach (XElement ve in vl)
        {
            //Find property using reflection and set the value accordingly
            UpdateGameObjectProperty(oCreature, ve.Element("xname").Value, ve.Element("Data").Value);
        }

        //Add script URI
        string sUriName = GetResource("ID", agent.Element("ScriptURI").Value, "Name");
        //Check if custom script, And add it, otherwise ignore
        if (sUriName != "creature_core")
        {
            //Check to see if maybe player_core
            if (sUriName == "player_core")
            {
                oCreature.gameObject.AddComponent(Type.GetType(sUriName));
                //Remove the mutually exclusive creature core
                Destroy(oCreature.gameObject.GetComponent<creature_core>());
            }
            else//Custom script
            {
                oCreature.GetComponent<xGameObjectBase>().bCustom = EngineConstants.TRUE;
                oCreature.gameObject.AddComponent(Type.GetType(sUriName));
            }
        }

        //update all the variables That are Not list
        var e = agent.Elements();
        foreach (var _x in e)
        {
            string _d = _x.Name.ToString();
            if (_d.IndexOf("List") == -1)//If not list
            {
                string _v = _x.Value;
                if (_v != "")
                {
                    UpdateGameObjectProperty(oCreature, _d, _v);
                }
            }
        }

        /*//Manually update some relevant variables during debugging tests
        UpdateGameObjectProperty(c, agent.Element("xname").Name.ToString(), agent.Element("xname").Value);
        UpdateGameObjectProperty(c, agent.Element("Tag").Name.ToString(), agent.Element("Tag").Value);
        UpdateGameObjectProperty(c, agent.Element("Race").Name.ToString(), agent.Element("Race").Value);
        UpdateGameObjectProperty(c, agent.Element("Gender").Name.ToString(), agent.Element("Gender").Value);
        UpdateGameObjectProperty(c, agent.Element("Group").Name.ToString(), agent.Element("Group").Value);
        UpdateGameObjectProperty(c, agent.Element("Team").Name.ToString(), agent.Element("Team").Value);
        UpdateGameObjectProperty(c, agent.Element("Selectable").Name.ToString(), agent.Element("Selectable").Value);
        UpdateGameObjectProperty(c, agent.Element("PlotGiver").Name.ToString(), agent.Element("PlotGiver").Value);
        UpdateGameObjectProperty(c, agent.Element("Class").Name.ToString(), agent.Element("Class").Value);*/

        switch (oCreature.GetComponent<xGameObjectUTC>().Group)
        {
            case EngineConstants.GROUP_PC:
                {
                    oS.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                }
            case EngineConstants.GROUP_HOSTILE:
            case EngineConstants.GROUP_HOSTILE_ON_GROUND:
                {
                    oS.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case EngineConstants.GROUP_FRIENDLY:
                {
                    oS.GetComponent<Renderer>().material.color = Color.green;
                    break;
                }
            case EngineConstants.GROUP_NEUTRAL:
            default:
                {
                    oS.GetComponent<Renderer>().material.color = Color.grey;
                    break;
                }
        }

        int propertiesSet = SetCreatureBaseProperties(oCreature);

        //Signal events  Spawn
        xEvent ev = Event(EngineConstants.EVENT_TYPE_SPAWN);
        SetEventCreatorRef(ref ev, gameObject);
        SetEventObjectRef(ref ev, 0, oCreature.gameObject);
        SignalEvent(oCreature.gameObject, ev);
    }

    public GameObject ParseAreaPlaceable(GameObject oPlaceable, XElement xe)
    {
        IEnumerable<XElement> vl = xe.Element("VariableList").Elements("Agent");
        foreach (XElement ve in vl)
        {
            //Check to see if the Placeable is actually an area transition, If yes then re-tag
            if (ve.Element("xname").Value == "PLC_AT_DEST_AREA_TAG")
            {
                oPlaceable.tag = "AreaTransition";
                GameObject oMesh = FindChild(oPlaceable, "Cylinder");
                if (oMesh != null) oMesh.GetComponent<Renderer>().material.color = Color.cyan;
            }

            //Find property using reflection and set the value accordingly
            UpdateGameObjectProperty(oPlaceable, ve.Element("xname").Value, ve.Element("Data").Value);
        }

        //Manually update some relevant variables during debugging tests
        UpdateGameObjectProperty(oPlaceable, xe.Element("position").Name.ToString(), xe.Element("position").Value);
        UpdateGameObjectProperty(oPlaceable, xe.Element("orientation").Name.ToString(), xe.Element("orientation").Value);

        //Update object position And orientation
        oPlaceable.gameObject.transform.position = oPlaceable.gameObject.GetComponent<xGameObjectUTP>().position;
        var rot = oPlaceable.gameObject.GetComponent<xGameObjectUTP>().orientation;
        oPlaceable.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        return oPlaceable;
    }

    public GameObject ParseAreaCreature(GameObject oCreature, XElement xe)
    {
        IEnumerable<XElement> vl = xe.Element("VariableList").Elements("Agent");
        foreach (XElement ve in vl)//Double check if creature in area ever has variable list
        {
            //Find property using reflection and set the value accordingly
            UpdateGameObjectProperty(oCreature, ve.Element("xname").Value, ve.Element("Data").Value);
        }

        //Manually update some relevant variables during debugging tests
        UpdateGameObjectProperty(oCreature, xe.Element("position").Name.ToString(), xe.Element("position").Value);
        UpdateGameObjectProperty(oCreature, xe.Element("orientation").Name.ToString(), xe.Element("orientation").Value);

        //Update object position And orientation
        oCreature.gameObject.transform.position = oCreature.gameObject.GetComponent<xGameObjectUTC>().position;
        var rot = oCreature.gameObject.GetComponent<xGameObjectUTC>().orientation;
        oCreature.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        //Trigger enter event
        GameObject oArea = GameObject.FindGameObjectWithTag("Area");
        xEvent ev = Event(EngineConstants.EVENT_TYPE_ENTER);
        SetEventCreatorRef(ref ev, oCreature.gameObject);
        SetEventObjectRef(ref ev, 0, oCreature.gameObject);
        SignalEvent(oArea.gameObject, ev);

        return oCreature;
    }

    public void ParsePlayer(string rTemplate)
    {
        //This is a temp function, certain things may be set manually for testing purposes

        GameObject oPlayer = CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, rTemplate, Vector3.zero);
        xGameObjectMOD.instance.oHero = oPlayer;
        //xGameObjectMOD.instance.oPlayerPool.Add(oPlayer);//Add party member to party Pool

        //Set party leader in module
        SetPartyLeader(oPlayer);

        GameObject w = GameObject.Find(xGameObjectMOD.instance.tWaypoint);
        oPlayer.gameObject.transform.position = new Vector3(w.transform.position.x + 0.50f, w.transform.position.y + 0.10f, w.transform.position.z - 0.50f);
        var rot = oPlayer.gameObject.gameObject.GetComponent<xGameObjectUTC>().orientation;
        oPlayer.gameObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        //Set the player followers state as something different than invalid :-)
        SetLocalInt(oPlayer, "FOLLOWER_STATE", EngineConstants.FOLLOWER_STATE_LOADING);

        //Trigger enter event
        GameObject oArea = GameObject.FindGameObjectWithTag("Area");
        xEvent ev = Event(EngineConstants.EVENT_TYPE_ENTER);
        SetEventCreatorRef(ref ev, oPlayer.gameObject);
        SetEventObjectRef(ref ev, 0, oPlayer.gameObject);
        SignalEvent(oArea.gameObject, ev);
    }

    public int SetCreatureBaseProperties(GameObject oCreature)
    {
        //float fMax = GetCreatureProperty(OBJECT_SELF, PROPERTY_DEPLETABLE_MANA_STAMINA, PROPERTY_VALUE_BASE);
        int nClass = GetLocalInt(oCreature, "Class");
        oCreature.GetComponent<xGameObjectBase>().nCoreClass = nClass;
        SetCreatureProperty(oCreature, EngineConstants.PROPERTY_SIMPLE_CURRENT_CLASS, nClass);

        int nRace = GetCreatureRacialType(oCreature);

        //Warriorx
        /*    Chargen_SelectGender(oChar,GetCreatureGender(oChar));
    Chargen_SelectRace(oChar,nRace);
    Chargen_SelectCoreClass(oChar,nClass);
    Chargen_SelectBackground(oChar, nBackground,FALSE);

    int nEquipIdx = Chargen_GetEquipIndex(nRace, nClass, nBackground);*/

        float fBase = GetM2DAFloat(EngineConstants.TABLE_RULES_CLASSES, "BaseHealth", nClass);
        if (fBase <= 0)//Health below zero
        {
            SetLocalInt(oCreature, EngineConstants.CREATURE_SPAWN_DEAD, EngineConstants.TRUE);
            oCreature.GetComponent<xGameObjectBase>().bDead = EngineConstants.TRUE;
        }
        SetCreatureProperty(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH, fBase, EngineConstants.PROPERTY_VALUE_BASE);//Should create a new one
        return EngineConstants.TRUE;
    }

    public void Engine_Conversation()
    {
        ParseConversation();

        xConversation cnv = GetConversation();

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
                    plot = ParsePlot(GetResource("ID", plotID.ToString(), "Name"));
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
            node = cnv.NPCLineList.ElementAt(lIndex);
        }
        else //nothing found?
        {
            throw new NotImplementedException();
        }

        //Time to display the first branch, and if null, the player choices directly
        GameObject oConversation = GameObject.Find("Canvas").transform.Find("convPanel").gameObject;
        oConversation.SetActive(true);
        GameObject npcLine = oConversation.transform.Find("NPCLine").gameObject;

        //Prepare an array of text lines, to be visible or not based on need
        List<GameObject> pLines = new List<GameObject>();
        GameObject line;
        line = oConversation.transform.Find("0").gameObject;
        pLines.Add(line);
        line = oConversation.transform.Find("1").gameObject;
        pLines.Add(line);
        line = oConversation.transform.Find("2").gameObject;
        pLines.Add(line);
        line = oConversation.transform.Find("3").gameObject;
        pLines.Add(line);
        line = oConversation.transform.Find("4").gameObject;
        pLines.Add(line);
        line = oConversation.transform.Find("5").gameObject;
        pLines.Add(line);

        Text ct = (Text)npcLine.GetComponent(typeof(Text));
        ct.text = node.text;

        //Get the list of player replies
        List<xConvNode> pReplies = new List<xConvNode>();
        foreach(Transition t in cnv.NPCLineList.ElementAt(lIndex).TransitionList)
        {
            pReplies.Add(cnv.PlayerLineList.ElementAt(t.LineIndex));
        }

        //activate text lines
        foreach (xConvNode pNode in pReplies)
        {
            char c = (char)124;
            string[] split = pNode.text.Split(c);
            pNode.text = split[1];
            string lineLocation = split[0][0].ToString();
            string iconID = split[0][1].ToString();
            GameObject lReply = pLines.ElementAt(int.Parse(lineLocation));
            lReply.GetComponent<Text>().text = pNode.text;
            lReply.GetComponent<xConvTouch>().index = int.Parse(lineLocation);
            lReply.GetComponent<xConvTouch>().iconID = iconID;
            lReply.SetActive(true);
        }

        Console.WriteLine();
    }

    public xPlot ParsePlot(string rTemplate)
    {
        xPlot plot = new xPlot();

        //Get its template XML, Convert name to file ID
        string id = GetResource("Name", rTemplate, "ID", "plo");
        plot.ResRefID = int.Parse(id);//Double check
        string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
        increment++;

        Unzip(id, seed);

        string f = EngineConstants.SOURCE + id + seed + ".xml";

        //Load the identified XML template for parsing
        //XmlNode node = doc.SelectSingleNode("//Resource/Agent/ResRefName/text()");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(f);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        XElement agent = root.Element("Agent");

        //update all the variables That are Not list
        var e = agent.Elements();
        foreach (var _x in e)
        {
            string _d = _x.Name.ToString();
            if (_d.IndexOf("List") == -1)//If not list
            {
                string _v = _x.Value;
                if (_v != "")
                {
                    UpdateObjectProperty(plot, _d, _v);
                }
            }
        }

        //Parse the plot status list
        IEnumerable<XElement> statuslist = agent.Element("StatusList").Elements("Agent");
        foreach (XElement _ll in statuslist)
        {
            var _node = new xPlotNode();

            foreach (XElement _l in _ll.Elements())
            {
                string _d = _l.Name.ToString();
                if (_d.IndexOf("List") == -1)//If not list
                {
                    string _v = _l.Value;
                    if (_v != "")
                    {
                        UpdateObjectProperty(_node, _d, _v);
                    }
                }
            }

            //Create the new plot element with the default value and added to the list
            xPlotElement _element = new xPlotElement(_node, _node.DefaultValue);
            plot.StatusList.Add(_element);
        }

        //Add the newly parsed plot into the main plot list for future use
        xGameObjectMOD.instance.oPlots.Add(plot);

        return plot;
    }

    public void ParseConversation()
    {
        xConversation cnv = new xConversation();

        string sConversation = GetLocalString(GetModule(), "CONVERSATION");

        //Get its template XML, Convert name to file ID
        string id = GetResource("Name", sConversation, "ID", "dlg");

        //Unzip XML, Generate seed for unique filename
        string seed = String.Format("{0:x}", DateTime.Now.ToString("hh:mm:ss tt").GetHashCode() + increment);
        increment++;

        Unzip(id, seed);

        string f = EngineConstants.SOURCE + id + seed + ".xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(f);
        XDocument xDoc = XDocument.Load(new XmlNodeReader(xmldoc));
        XElement root = xDoc.Root;
        XElement agent = root.Element("Agent");

        //update all the variables That are Not list
        var e = agent.Elements();
        foreach (var _x in e)
        {
            string _d = _x.Name.ToString();
            if (_d.IndexOf("List") == -1)//If not list
            {
                string _v = _x.Value;
                if (_v != "")
                {
                    UpdateObjectProperty(cnv, _d, _v);
                }
            }
        }

        //Parse the start list
        IEnumerable<XElement> sl = agent.Element("StartList").Elements("Agent");
        foreach (XElement _s in sl)
        {
            cnv.StartList.Add(int.Parse(_s.Element("LineIndex").Value));
        }

        //Parse the NPC and player lines
        IEnumerable<XElement> npc = agent.Element("NPCLineList").Elements("Agent");
        foreach (XElement _ll in npc)
        {
            var _node = new xConvNode();

            foreach (XElement _l in _ll.Elements())
            {
                _node.TransitionList = new List<Transition>();
                string _d = _l.Name.ToString();
                if (_d.IndexOf("List") == -1)//If not list
                {
                    string _v = _l.Value;
                    if (_v != "")
                    {
                        UpdateObjectProperty(_node, _d, _v);
                    }
                }
            }

            //Handle lists
            IEnumerable<XElement> tl = _ll.Element("TransitionList").Elements("Agent");
            foreach (XElement _t in tl)
            {
                var _transition = new Transition();
                _transition.IsLink = bool.Parse(_t.Element("IsLink").Value);
                _transition.LineIndex = int.Parse(_t.Element("LineIndex").Value);
                _node.TransitionList.Add(_transition);
            }

            cnv.NPCLineList.Add(_node);
        }

        IEnumerable<XElement> player = agent.Element("PlayerLineList").Elements("Agent");
        foreach (XElement _ll in player)
        {
            var _node = new xConvNode();

            foreach (XElement _l in _ll.Elements())
            {
                _node.TransitionList = new List<Transition>();
                string _d = _l.Name.ToString();
                if (_d.IndexOf("List") == -1)//If not list
                {
                    string _v = _l.Value;
                    if (_v != "")
                    {
                        UpdateObjectProperty(_node, _d, _v);
                    }
                }
            }

            //Handle lists
            IEnumerable<XElement> tl = _ll.Element("TransitionList").Elements("Agent");
            foreach (XElement _t in tl)
            {
                var _transition = new Transition();
                _transition.IsLink = bool.Parse(_t.Element("IsLink").Value);
                _transition.LineIndex = int.Parse(_t.Element("LineIndex").Value);
                _node.TransitionList.Add(_transition);
            }

            cnv.PlayerLineList.Add(_node);
        }

        //Temporary during the bug, the goal is to pre-parse conversations 
        //and other resources during area load and store them
        xGameObjectMOD.instance.oConversation = cnv;
    }

    public object GetGameObjectType(GameObject oObject)
    {
        if (oObject == null)
        {
            throw new NotImplementedException();
        }
        switch (oObject.GetComponent<xGameObjectBase>().nObjectType)
        {
            case EngineConstants.OBJECT_TYPE_CREATURE:
                {
                    return oObject.GetComponent<xGameObjectUTC>();
                }
            case EngineConstants.OBJECT_TYPE_PLACEABLE:
                {
                    return oObject.GetComponent<xGameObjectUTP>();
                }
            case EngineConstants.OBJECT_TYPE_AREA:
                {
                    return oObject.GetComponent<xGameObjectARE>();
                }
            case EngineConstants.OBJECT_TYPE_MODULE:
                {
                    return oObject.GetComponent<xGameObjectMOD>();
                }
            case EngineConstants.OBJECT_TYPE_WAYPOINT:
                {
                    return oObject.GetComponent<xGameObjectUTW>();
                }
            default: throw new NotImplementedException();
        }
    }

    //public GameObject FindChild(string pRoot, string pName)
    public GameObject FindChild(GameObject pRoot, string pName)
    {
        //Transform pTransform = GameObject.Find(pRoot).GetComponent<Transform>();
        Transform pTransform = pRoot.GetComponent<Transform>();
        foreach (Transform trs in pTransform)
        {
            if (trs.gameObject.name == pName)
                return trs.gameObject;
        }
        return null;
    }

    public void SceneCleanup()
    {
        //Safe_destroy_object and this function should work similarly
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            //Skip if
            //explicitly named
            string s = go.ToString().Split()[0];
            if (s == "GameModule" || s == "MainCamera" || s == "Floaty" ||
                s == "DirectionalLight" || s == "Invalid" ||
                s == "Floor" || s == "floorPlane")
            {

            }
            //Mesh attached to a creature that is party
            else if (go.transform.parent != null &&
                go.transform.parent.gameObject.GetComponent<xGameObjectBase>().nObjectType == EngineConstants.OBJECT_TYPE_CREATURE &&
                IsPartyMember(go.transform.parent.gameObject) == EngineConstants.TRUE)
            {

            }
            //Doesn't have a parent and Is creature and is party member
            else if (go.transform.parent == null &&
                go.GetComponent<xGameObjectBase>().nObjectType == EngineConstants.OBJECT_TYPE_CREATURE &&
                IsPartyMember(go) == EngineConstants.TRUE)
            {

            }
            //Otherwise destroy
            else
            {
                DestroyObject(go);
            }
        }
    }

    public void DeleteTemp()
    {
        var d = Application.persistentDataPath + "/Source/";
        var files = new DirectoryInfo(d).GetFiles("*.xml");
        foreach (var file in files)
        {
            if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromMinutes(5))
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch
                {
                    //Still in use, ignoring now, try again later
                }
            }
        }
    }

    public void UpdateObjectProperty(object oObject, string key, string value)
    {
        char c = (char)46;

        string o = oObject.GetType().GetProperty(key).ToString().Split()[0].Split(c)[1];
        switch (o)
        {
            case "Int32":
                {
                    int v = int.Parse(value);
                    oObject.GetType().GetProperty(key).SetValue(oObject, v, null);
                    break;
                }
            case "UInt32":
                {
                    uint v = uint.Parse(value);
                    oObject.GetType().GetProperty(key).SetValue(oObject, v, null);
                    break;
                }
            case "Guid":
                {
                    Guid guid = new Guid(value);
                    oObject.GetType().GetProperty(key).SetValue(oObject, guid, null);
                    break;
                }
            case "GameObject":
                {
                    GameObject go = GameObject.Find(value);
                    oObject.GetType().GetProperty(key).SetValue(oObject, go, null);
                    break;
                }
            case "Vector3":
                {
                    char cm = (char)44;
                    string[] va = value.Split(cm);
                    //For now we put all objects that same height coordinate
                    //Warning("debug mode: 3-D coordinates in complete!");
                    Vector3 v = new Vector3(Convert.ToSingle(va[0]), 0, Convert.ToSingle(va[2]));
                    //Vector3 v = new Vector3(Convert.ToSingle(va[0]), Convert.ToSingle(va[1]), Convert.ToSingle(va[2]));
                    oObject.GetType().GetProperty(key).SetValue(oObject, v, null);
                    break;
                }
            case "String":
                {
                    oObject.GetType().GetProperty(key).SetValue(oObject, value, null);
                    break;
                }
            case "Boolean":
                {
                    oObject.GetType().GetProperty(key).SetValue(oObject, Boolean.Parse(value), null);
                    break;
                }
            case "Single":
                {
                    oObject.GetType().GetProperty(key).SetValue(oObject, float.Parse(value), null);
                    break;
                }
            default: throw new NotImplementedException();
        }
    }

    public xConversation GetConversation()
    {
        return xGameObjectMOD.instance.oConversation;
    }
    #endregion

    #region moved from constants files
    //item_constants_h
    public float ScaleItemPower(float fInitialValue, GameObject oCreator)
    {
        float fValue = fInitialValue;
        if (IsObjectValid(oCreator) != EngineConstants.FALSE)
        {
            float fAttribute = GetCreatureProperty(oCreator, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC); // GetAttributeModifier(oCreator, EngineConstants.PROPERTY_ATTRIBUTE_MAGIC);
            LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Item fAttribute = " + ToString(fAttribute));
            fValue *= 0.75f + (EngineConstants.ITEM_POWER_SCALING_FACTOR * fAttribute);
        }
        LogTrace(EngineConstants.LOG_CHANNEL_COMBAT_ABILITY, "Item fValue = " + ToString(fValue));

        return fValue;
    }
    #endregion

    #region Unknown

    #endregion

    #region from zDA:O , needs update to zDA2
    /***************************************************************/
    // Cutscene
    /***************************************************************/
    /* @addtogroup cutscene Cutscene Functions
    *
    * Functions to load and play cutscenes
*/
    /* @{*/

    /* @brief Loads a cutscene
    *
    * Starts off the cutscene process by loading up the cutscene in the background
    * while the game plays (loads a little bit each update). Will not take over control
    * of the input or interfere with the user's play experience, except for the slight
    * slow-down while the system loads up the scene.
    * If parameter actors are specified, the objects will be used to take the appearance only
    * of the creatures and bring them into the scene.
    *
    * @param rCutscene - The file name of the cutscene.cut file to load.
    * @param oPlayer - The specific player that this cutscene is for. If none is specified, all players get it.
    * @param bPlayImmediately - Takes over control on the client and forces the load in one big hit, then plays it automatically.
    * @param aTargetTagsToReplace - The tags of the actors that should be replaced (tag aTargetTagsToReplace[0] will be replaced by object aReplacementObjects[0])
    * @param aReplacementObjects - The object ids of the objects that will override the appearances of the actors specified in aTargetTagsToReplace
    * @param bActorsHaveWeapons - If the actors have been overridden, do the creatures bring their weapons into the scene with them.
    * @sa PlayCutscene()
    * @author Paul
*/
    public void LoadCutscene(string rCutscene, GameObject oPlayer, int bPlayImmediately, string[] aTargetTagsToReplace, List<GameObject> aReplacementObjects, int bActorsHaveWeapons)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @brief Plays a cutscene.
    *
    * A call to LoadCutscene MUST precede this call if it is to do anything.
    * If the scene has been loaded, or at least partially loaded,
    * this will force the rest of the load and then play the scene.
    *
    * @param oPlayer - The specific player to activate their scene. If none is specified, all players get it.
    * @sa LoadCutscene()
    * @author Paul
    */
    public void PlayCutscene(GameObject oPlayer = null)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    /* @}*/
    /***************************************************************/

    /* @brief SetTrainingMode
*   When the game is in training mode the game will be paused and the following interactions will be disabled: Unpausing, Saving, Item Destruction. This also fires a: 'EVENT_TYPE_TRAINING_BEGIN' event
*
* @author John Fedorkiw
* @param bEnable - 0 disables, otherwise set to the proper training mode.
*/

    public void SetTrainingMode(int nTrainingMode)
    {
        Debug.Log("update me");
        throw new NotImplementedException();
    }

    #endregion
}