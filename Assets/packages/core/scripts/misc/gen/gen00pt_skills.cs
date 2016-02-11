#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System;

public class gen00pt_skills : xPlotConditional
{
    /*//:://////////////////////////////////////////////
/*
    skill checks
*/
    //:://////////////////////////////////////////////
    //:: Created By: Yaron
    //:: Created On: July 12th, 2006
    //:://////////////////////////////////////////////
    /*# include "plt_gen00pt_skills"
# include "utility_h"
    // included in achievement_core_h; #include "wrappers_h"
# include "global_GameObjects_h"
# include "plot_h"
# include "achievement_core_h"*/

    Engine engine { get; set; }

    void Awake()
    {
        if (engine == null) engine = gameObject.GetComponent<Engine>();
    }

    public override int StartingConditional(xEvent eParms)
    {


        //xEvent eParms = engine.GetCurrentEvent();                // Contains all input parameters
        int nType = engine.GetEventTypeRef(ref eParms);               // GET or SET call
        string strPlot = engine.GetEventStringRef(ref eParms, 0);         // Plot GUID
        int nFlag = engine.GetEventIntegerRef(ref eParms, 1);          // The bit flag # being affected
        int nValue = engine.GetEventIntegerRef(ref eParms, 2);        // On SET call, the value about to be written
        int nOldValue = engine.GetEventIntegerRef(ref eParms, 3);     // On SET call, the current flag value
        GameObject oParty = engine.GetEventCreatorRef(ref eParms);      // The owner of the plot table for this script
        int nResult = EngineConstants.FALSE;

        engine.plot_GlobalPlotHandler(eParms); // any global plot operations, including debug info

        GameObject oPC = engine.GetHero();

        if (nType == EngineConstants.EVENT_TYPE_SET_PLOT) // actions -&gt; normal flags only
        {
            switch (nFlag)
            {
                default:    break;
            }
        }
        else // EVENT_TYPE_GET_PLOT -&gt; defined conditions only
        {
            int nSkill = 0;
            int nLevel = 0;

            switch (nFlag)
            {
                case EngineConstants.GEN_PERSUADE_LOW: nSkill = EngineConstants.SKILL_PERSUADE; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_PERSUADE_MED: nSkill = EngineConstants.SKILL_PERSUADE; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_PERSUADE_HIGH: nSkill = EngineConstants.SKILL_PERSUADE; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_PERSUADE_VERY_HIGH: nSkill = EngineConstants.SKILL_PERSUADE; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_HERBALISM_LOW: nSkill = EngineConstants.SKILL_HERBALISM; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_HERBALISM_MED: nSkill = EngineConstants.SKILL_HERBALISM; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_HERBALISM_HIGH: nSkill = EngineConstants.SKILL_HERBALISM; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_HERBALISM_VERY_HIGH: nSkill = EngineConstants.SKILL_HERBALISM; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_POISON_LOW: nSkill = EngineConstants.SKILL_POSION; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_POISON_MED: nSkill = EngineConstants.SKILL_POSION; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_POISON_HIGH: nSkill = EngineConstants.SKILL_POSION; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_POISON_VERY_HIGH: nSkill = EngineConstants.SKILL_POSION; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_TRAPS_LOW: nSkill = EngineConstants.SKILL_TRAPS; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_TRAPS_MED: nSkill = EngineConstants.SKILL_TRAPS; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_TRAPS_HIGH: nSkill = EngineConstants.SKILL_TRAPS; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_TRAPS_VERY_HIGH: nSkill = EngineConstants.SKILL_TRAPS; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_STEALTH_LOW: nSkill = EngineConstants.SKILL_STEALTH; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_STEALTH_MED: nSkill = EngineConstants.SKILL_STEALTH; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_STEALTH_HIGH: nSkill = EngineConstants.SKILL_STEALTH; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_STEALTH_VERY_HIGH: nSkill = EngineConstants.SKILL_STEALTH; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_STEALING_LOW: nSkill = EngineConstants.SKILL_STEALING; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_STEALING_MED: nSkill = EngineConstants.SKILL_STEALING; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_STEALING_HIGH: nSkill = EngineConstants.SKILL_STEALING; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_STEALING_VERY_HIGH: nSkill = EngineConstants.SKILL_STEALING; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_SURVIVAL_LOW: nSkill = EngineConstants.SKILL_SURVIVAL; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_SURVIVAL_MED: nSkill = EngineConstants.SKILL_SURVIVAL; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_SURVIVAL_HIGH: nSkill = EngineConstants.SKILL_SURVIVAL; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_SURVIVAL_VERY_HIGH: nSkill = EngineConstants.SKILL_SURVIVAL; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_LOCKPICKING_LOW: nSkill = EngineConstants.SKILL_LOCKPICKING; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_LOCKPICKING_MED: nSkill = EngineConstants.SKILL_LOCKPICKING; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_LOCKPICKING_HIGH: nSkill = EngineConstants.SKILL_LOCKPICKING; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_LOCKPICKING_VERY_HIGH: nSkill = EngineConstants.SKILL_LOCKPICKING; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

                case EngineConstants.GEN_INTIMIDATE_LOW: nSkill = EngineConstants.SKILL_INTIMIDATE; nLevel = EngineConstants.UT_SKILL_CHECK_LOW; break;
                case EngineConstants.GEN_INTIMIDATE_MED: nSkill = EngineConstants.SKILL_INTIMIDATE; nLevel = EngineConstants.UT_SKILL_CHECK_MED; break;
                case EngineConstants.GEN_INTIMIDATE_HIGH: nSkill = EngineConstants.SKILL_INTIMIDATE; nLevel = EngineConstants.UT_SKILL_CHECK_HIGH; break;
                case EngineConstants.GEN_INTIMIDATE_VERY_HIGH: nSkill = EngineConstants.SKILL_INTIMIDATE; nLevel = EngineConstants.UT_SKILL_CHECK_VERY_HIGH; break;

            }
            nResult = engine.UT_SkillCheck(nSkill, nLevel, oPC);

            engine.ACH_CheckPersuadeAchievement(oPC, nResult, nSkill, EngineConstants.SKILL_PERSUADE);
            engine.ACH_CheckIntimidateAchievement(oPC, nResult, nSkill, EngineConstants.SKILL_INTIMIDATE);


        }
        engine.plot_OutputDefinedFlag(eParms, nResult);

        return nResult;
    }
}