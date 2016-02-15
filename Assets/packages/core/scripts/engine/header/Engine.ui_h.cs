//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Engine
{
    //------------------------------------------------------------------------------
    // ui_h
    //------------------------------------------------------------------------------
    /*
        Any function interacting with the UI is wrapped here.

    */
    //------------------------------------------------------------------------------
    // @author georg zoeller
    //------------------------------------------------------------------------------

    //#include"2da_constants_h"
    //#include"log_h"
    //#include"core_h"

    //moved public const int UI_CFG_SHOW_ABILITY_MESSAGES = 1;

    //moved public const int EngineConstants.UI_MESSAGE_OUT_OF_AMMO        = 1;
    //moved public const int EngineConstants.UI_MESSAGE_DEATHBLOW          = 2;
    //moved public const int EngineConstants.UI_MESSAGE_ABILITY_CONDITION_NOT_MET  = 3;
    //moved public const int EngineConstants.UI_MESSAGE_MISSED             = 4;
    //moved public const int EngineConstants.UI_MESSAGE_BLOCKED            = 5000;
    //moved public const int EngineConstants.UI_MESSAGE_BACKSTAB           = 5;
    //moved public const int EngineConstants.UI_MESSAGE_OUT_OF_MANA        = 6;
    //moved public const int EngineConstants.UI_MESSAGE_OUT_OF_STAMINA     = 7;
    //moved public const int EngineConstants.UI_MESSAGE_IMMUNE             = 8;
    //moved public const int EngineConstants.UI_MESSAGE_RESISTED           = 9;
    //moved public const int EngineConstants.UI_MESSAGE_LEVELUP            = 10;
    //moved public const int EngineConstants.UI_MESSAGE_STUNNED            = 11;
    //moved public const int EngineConstants.UI_MESSAGE_GRAB_BROKEN        = 12;
    //moved public const int EngineConstants.UI_MESSAGE_TRAP_TRIGGERED     = 13;
    //moved public const int EngineConstants.UI_MESSAGE_TRAP_PLACED        = 14;
    //moved public const int EngineConstants.UI_MESSAGE_TRAP_DETECTED      = 15;
    //moved public const int EngineConstants.UI_MESSAGE_CANT_DO_IN_COMBAT  = 16;
    //moved public const int EngineConstants.UI_MESSAGE_LOCKED             = 17;
    //moved public const int EngineConstants.UI_MESSAGE_UNLOCKED           = 18;
    //moved public const int EngineConstants.UI_MESSAGE_CAN_NOT_USE_OBJECT = 19;
    //moved public const int EngineConstants.UI_MESSAGE_APPROVAL_INCREASED = 20;
    //moved public const int EngineConstants.UI_MESSAGE_APPROVAL_DECREASED = 21;
    //moved public const int EngineConstants.UI_MESSAGE_INTERRUPTED        = 22;
    //moved public const int EngineConstants.TRAP_DISARM_SUCCEEDED         = 23;
    //moved public const int EngineConstants.TRAP_DISARM_FAILED            = 24;
    //moved public const int EngineConstants.UI_MESSAGE_SHATTERED          = 25;
    //moved public const int EngineConstants.UI_MESSAGE_EVASION            = 27;
    //moved public const int EngineConstants.UI_MESSAGE_NO_EFFECT          = 28;
    //moved public const int EngineConstants.UI_MESSAGE_UNLOCK_SKILL_LOW   = 29;
    //moved public const int EngineConstants.UI_MESSAGE_KEY_REQUIRED       = 30;
    //moved public const int EngineConstants.UI_MESSAGE_SPELL_IMMUNITY     = 31;
    //moved public const int EngineConstants.UI_MESSAGE_UNLOCKED_BY_KEY    = 32;
    //moved public const int EngineConstants.UI_MESSAGE_NOT_AT_THIS_LOCATION    = 33;

    //moved public const int EngineConstants.UI_MESSAGE_LOCKPICK_NOT_POSSIBLE = 3516;
    //moved public const int EngineConstants.UI_MESSAGE_DISARM_NOT_POSSIBLE = 3517;
    //moved public const int EngineConstants.UI_MESSAGE_LEVER_PULLED = 3520;
    //moved public const int EngineConstants.UI_MESSAGE_VALVE_OPEN = 3521;
    //moved public const int EngineConstants.UI_MESSAGE_VALVE_CLOSE = 3522;

    //moved public const int STRING_ID_XP_FLOATY = 379101;

    //moved public const int UI_DEBUG_LAST_MOVEMENT_FAILED = 1001;
    //moved public const int UI_DEBUG_LAST_ATTACK_FAILED = 1002;
    //moved public const int UI_DEBUG_LAST_WEAPON_SWITCH_FAILED = 1003;
    //moved public const int UI_DEBUG_LAST_WAIT_FAILED = 1004;
    //moved public const int UI_DEBUG_CREATURE_WAITING_NOW = 1005;
    //moved public const int UI_DEBUG_NO_LOS = 1006;
    //moved public const int UI_DEBUG_INVALID_PATH = 1007;
    //moved public const int UI_DEBUG_INVALID_DATA = 1008;
    //moved public const int UI_DEBUG_COMMAND_CLEARED = 1009;
    //moved public const int UI_DEBUG_COMMAND_FAILED = 1010;
    //moved public const int UI_DEBUG_NO_SPACE_IN_MELEE_RING = 1011;
    //moved public const int UI_DEBUG_TARGET_DESTROYED = 1012;
    //moved public const int UI_DEBUG_MOVEMENT_DISABLED = 1013;
    //moved public const int UI_DEBUG_CREATURE_IMMORTAL = 1014;
    //moved public const int UI_DEBUG_EVENT_IMPACT_ATTACK = 1015;
    //moved public const int UI_DEBUG_EVENT_IMPACT_CAST = 1016;
    //moved public const int UI_DEBUG_COMMAND_TIMED_OUT = 1017;

    //moved public const int EngineConstants.UI_MESSAGE_TYPE_SPECIAL = 10000;
    //moved public const int EngineConstants.UI_SPECIAL_MSG_DAMAGE =  10001;
    //moved public const int EngineConstants.UI_SPECIAL_MSG_DAMAGE_CRIT = 10002;
    //moved public const int EngineConstants.UI_SPECIAL_MESSAGE_HEAL = 10003;

    //moved public const int EngineConstants.UI_MESSAGE_KEY_ACQUIRED = 4001;

    //moved public const int EngineConstants.UI_MESSAGE_TYPE_PORTRAIT_FOLLOWER = 4;
    //moved public const int EngineConstants.UI_MESSAGE_TYPE_PORTRAIT = 3;

    //moved public const int EngineConstants.UI_DISPLAY_MASK_PLAYER   = 0x00000001;
    //moved public const int EngineConstants.UI_DISPLAY_MASK_PARTY    = 0x00000002;
    //moved public const int EngineConstants.UI_DISPLAY_MASK_HOSTILE  = 0x00000004;
    //moved public const int EngineConstants.UI_DISPLAY_MASK_ALL      = 0x000000ff;

    //moved public const float EngineConstants.UI_DISPLAY_DURATION_DEFAULT  = 3.0f;
    //moved public const float EngineConstants.UI_DISPLAY_DURATION_CODEX    = 3.0f;
    //moved public const float EngineConstants.UI_DISPLAY_DURATION_POPUP    = 3.0f;
    //moved public const float EngineConstants.UI_DISPLAY_DURATION_ABILITY  = 1.5f;
    //moved public const float EngineConstants.UI_DISPLAY_DURATION_DAMAGE   = 1.5f;
    //moved public const float EngineConstants.UI_DISPLAY_DURATION_XP       = 1.5f;

    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_DEFAULT  = 0xFFFFFF;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_CODEX    = 0xFFFFFF;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_POPUP    = 0xFFFFFF;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_ABILITY  = 0xffff00;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_DAMAGE   = 0xFFFFFF;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_XP       = 0xFFFFFF;
    //moved public const int   EngineConstants.UI_DISPLAY_COLOR_ERROR    = 0xFF0000;

    public int UI_CheckDisplayMask(int nMessage, GameObject oTarget)
    {
        int nDispMask = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "DisplayMask", nMessage);

        if (nDispMask != 0)
        {
            int bShow = ((nDispMask & EngineConstants.UI_DISPLAY_MASK_ALL) == EngineConstants.UI_DISPLAY_MASK_ALL) ? EngineConstants.TRUE : EngineConstants.FALSE;

            if (bShow == EngineConstants.FALSE)
            {
                int bPlayer = IsControlled(oTarget);
                int bParty = IsFollower(oTarget);
                int bHostile = IsObjectHostile(GetHero(), oTarget);

                bShow = (((nDispMask & EngineConstants.UI_DISPLAY_MASK_PLAYER) == EngineConstants.UI_DISPLAY_MASK_PLAYER) && bPlayer != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;
                bShow = (bShow != EngineConstants.FALSE || (((nDispMask & EngineConstants.UI_DISPLAY_MASK_PARTY) == EngineConstants.UI_DISPLAY_MASK_PARTY) && bParty != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;
                bShow = (bShow != EngineConstants.FALSE || (((nDispMask & EngineConstants.UI_DISPLAY_MASK_HOSTILE) == EngineConstants.UI_DISPLAY_MASK_HOSTILE) && bParty != EngineConstants.FALSE)) ? EngineConstants.TRUE : EngineConstants.FALSE;
            }

            return bShow;
        }
        return EngineConstants.FALSE;
    }

    // -----------------------------------------------------------------------------
    // @author: georg
    //
    // Check the GameObject mask setting for the message to determine whether or not
    // it can be shown on the selected GameObject type
    // -----------------------------------------------------------------------------
    public int UI_CheckObjectMask(int nMessage, GameObject oTarget)
    {
        int nDispMask = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "ObjectMask", nMessage);

        if (nDispMask != 0)
        {
            int bShow = ((nDispMask & 0xffffff) == 0xffffff) ? EngineConstants.TRUE : EngineConstants.FALSE;

            if (bShow == EngineConstants.FALSE)
            {
                bShow = ((nDispMask & GetObjectType(oTarget)) == GetObjectType(oTarget)) ? EngineConstants.TRUE : EngineConstants.FALSE;
            }

            return bShow;
        }
        return EngineConstants.FALSE;
    }

    public void UI_DisplayDamageFloaty(GameObject oTarget, GameObject oDamager, int nDamage, int nType, int nAbilityId = 0, int bBonusDamage = 0, int bManaDamage = 0, int bBackstab = 0, int nDamageType = EngineConstants.DAMAGE_TYPE_PHYSICAL)
    {

        int nMessage = EngineConstants.UI_SPECIAL_MSG_DAMAGE;
        int bCritical = EngineConstants.FALSE;
        if (nType == 1 || bBackstab != EngineConstants.FALSE)
        {
            nMessage = EngineConstants.UI_SPECIAL_MSG_DAMAGE_CRIT;
            bCritical = EngineConstants.TRUE;
        }
        else if (nType == 2)
        {
            nMessage = EngineConstants.UI_SPECIAL_MESSAGE_HEAL;

            // heal messages do not show for 0 hp
            if (nDamage == 0)
            {
                return;
            }
        }

        if (GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "bDisabled", nMessage) != EngineConstants.FALSE)
        {
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing message " + ToString(nMessage) + " - bDisabled set.", oTarget);
#endif
            return;
        }

        if (IsPlot(oTarget) != EngineConstants.FALSE)
        {
            UI_DisplayMessage(oTarget, 1014);
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing message " + ToString(nMessage) + " - GameObject is PLOT.", oTarget);
#endif
            return;
        }

        int nColor = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "Color", nMessage);

        // color override;
        if (IsPartyMember(oTarget) != EngineConstants.FALSE)
        {
            nColor = 0xcc0000;
        }
        else if (IsPartyMember(oDamager) != EngineConstants.FALSE)
        {
            nColor = 0xffffff;

            // -----------------------------------------------------------------
            // Special Attacks: Yellow
            // -----------------------------------------------------------------
            /* if (nAbilityId)
             {
                  nColor = 0xffff00;
             }*/

            int nClr = GetColorByDamageType(nDamageType);
            if (nClr != 0)
            {
                nColor = nClr;
            }

        }

        if (bManaDamage != EngineConstants.FALSE)
        {
            nColor = 0x6699FF;
        }

        if (nType == 2)
        {

            // -----------------------------------------------------------------
            // Healing: Green
            // -----------------------------------------------------------------
            nColor = 0x00cc00;
        }

        string sPrefix = "";
        string sPostfix = "";
        if (nMessage == EngineConstants.UI_SPECIAL_MESSAGE_HEAL || bBonusDamage != EngineConstants.FALSE)
        {
            sPrefix = GetStringByStringId(397775); /* + */
        }
        else if (bManaDamage == 1)
        {
            sPrefix = GetStringByStringId(397776); /* - */
        }
        else if (bManaDamage == 2)
        {
            sPrefix = GetStringByStringId(397776); /* - */
        }

        if (UI_CheckDisplayMask(nMessage, oTarget) != EngineConstants.FALSE)
        {
            int nStyle = (bCritical != EngineConstants.FALSE) ? EngineConstants.FLOATY_CRITICAL_HIT : EngineConstants.FLOATY_HIT;
            DisplayFloatyMessage(oTarget, sPrefix + IntToString(abs(nDamage)) + sPostfix, nStyle, nColor, EngineConstants.UI_DISPLAY_DURATION_DAMAGE);
        }
    }

    public void UI_DisplayHealFloaty(GameObject oTarget, GameObject oHealer, int nHealed)
    {
        UI_DisplayDamageFloaty(oTarget, oHealer, nHealed, 2);
    }

    /*-----------------------------------------------------------------------------
    * @brief Displays placeable popup text. Returns EngineConstants.TRUE if popup was displayed.
    *-----------------------------------------------------------------------------*/
    public int UI_DisplayPopupText(GameObject oPlaceable, GameObject oTarget)
    {
        string sText = GetPlaceablePopupText(oPlaceable);
        if (GetStringLength(sText) != EngineConstants.FALSE)
        {
            DisplayFloatyMessage(oTarget, sText, EngineConstants.FLOATY_MESSAGE, EngineConstants.UI_DISPLAY_COLOR_POPUP, EngineConstants.UI_DISPLAY_DURATION_POPUP);
            return EngineConstants.TRUE;
        }
        return EngineConstants.FALSE;
    }

    public void UI_DisplayMessage(GameObject oTarget, int nMessage, string sParam0 = "", int nColor = 0)
    {
        /*
            EngineConstants.FLOATY_HIT - Rising white number
            EngineConstants.FLOATY_CRITICAL_HIT - Rising white number that grows and turns red
            EngineConstants.FLOATY_MESSAGE - Yellow text (for debugging)
        */

        if (GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "bDisabled", nMessage) != EngineConstants.FALSE)
        {
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing message " + ToString(nMessage) + " - bDisabled set.", oTarget);
#endif
            return;
        }

        int bShow = UI_CheckObjectMask(nMessage, oTarget);
        if (bShow == EngineConstants.FALSE)
        {
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing message " + ToString(nMessage) + " - Object Mask Failed", oTarget);
#endif
        }

        bShow = (bShow != EngineConstants.FALSE && UI_CheckDisplayMask(nMessage, oTarget) != EngineConstants.FALSE && UI_CheckObjectMask(nMessage, oTarget) != EngineConstants.FALSE) ? EngineConstants.TRUE : EngineConstants.FALSE;

        Warning("UI messages always show");
        bShow = EngineConstants.TRUE;

        if (bShow != EngineConstants.FALSE)
        {
            int nId = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "StringId", nMessage);
            string sMessage;

            int nType = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "Type", nMessage);

            float fDuration = GetM2DAFloat(EngineConstants.TABLE_UI_MESSAGES, "Duration", nMessage);
            if (fDuration <= 0.2f)
            {
                fDuration = EngineConstants.UI_DISPLAY_DURATION_DEFAULT;
            }

            if (nType == 2 /*debug*/ && GetDebugHelpersEnabled() == EngineConstants.FALSE)
            {
#if DEBUG
                Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing type 2 (debug) message " + ToString(nMessage) + " - Debug Helpers disabled", oTarget);
#endif
                return;
            }

            // -----------------------------------------------------------------
            // special messages are defined in the scripted param to this function
            // -----------------------------------------------------------------
            if (nMessage >= EngineConstants.UI_MESSAGE_TYPE_SPECIAL)
            {
                sMessage = sParam0;
            }
            else
            {
                if (nType == 2 && EngineConstants.LOG_ENABLED == EngineConstants.FALSE)
                {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage), "Not showing message " + ToString(nMessage) + " - Debug Type 2 but not log enabled.", oTarget);
#endif
                    return;
                }

                if (nId == 0)
                {
                    sMessage = "?" + GetM2DAString(EngineConstants.TABLE_UI_MESSAGES, "StringText", nMessage);
                }
                else
                {
                    sMessage = GetStringByStringId(nId);

                    if (sMessage == "")
                    {
#if DEBUG
                        Warning("String table lookup on UI message failed. Blocking bug to georg. Details:" + ToString(nMessage) + " strid:" + ToString(nId));
#endif
                    }
                }
            }

            if (GetObjectType(oTarget) == EngineConstants.OBJECT_TYPE_CREATURE)
            {
                if (GetStealthEnabled(oTarget) != EngineConstants.FALSE && IsPartyMember(oTarget) == EngineConstants.FALSE)
                {
#if DEBUG
                    Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage", "Not showing message: creature stealthy", oTarget);
#endif
                    return;
                }

            }

            if (nColor == 0)
            {
                nColor = GetM2DAInt(EngineConstants.TABLE_UI_MESSAGES, "Color", nMessage);
            }
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage " + ToString(nMessage) + " " + IntToHexString(nColor), sMessage, oTarget);
#endif
            if (nType == EngineConstants.UI_MESSAGE_TYPE_PORTRAIT)
            {
                DisplayPortraitMessage(oTarget, sMessage, nColor);
            }
            else if (nType == EngineConstants.UI_MESSAGE_TYPE_PORTRAIT_FOLLOWER)
            {
                if (IsFollower(oTarget) != EngineConstants.FALSE)
                {
                    DisplayPortraitMessage(oTarget, sMessage, nColor);
                }
                else
                {
                    DisplayFloatyMessage(oTarget, sMessage, EngineConstants.FLOATY_MESSAGE, nColor, fDuration);
                }
            }
            else
            {
                if (nMessage == EngineConstants.UI_MESSAGE_MISSED || nMessage == EngineConstants.UI_MESSAGE_IMMUNE)
                {
                    DisplayFloatyMessage(oTarget, sMessage, EngineConstants.FLOATY_HIT, nColor, fDuration);
                }
                else
                {

                    DisplayFloatyMessage(oTarget, sMessage, EngineConstants.FLOATY_MESSAGE, nColor, fDuration);
                }
            }
        }
        else
        {
#if DEBUG
            Log_Trace(EngineConstants.LOG_CHANNEL_UIMESSAGES, "UI_DisplayMessage", "not showing " + ToString(nMessage), oTarget);
#endif
        }

    }

    public void UI_DisplayApprovalChangeMessage(GameObject oFollower, int nApprovalChange)
    {

        // TEMP - until we decide what to do with these message
        int nMes;
        if (nApprovalChange > 0)
            nMes = EngineConstants.UI_MESSAGE_APPROVAL_INCREASED;
        else
            nMes = EngineConstants.UI_MESSAGE_APPROVAL_DECREASED;

        UI_DisplayMessage(oFollower, nMes, IntToString(nApprovalChange));
    }

    public void UI_DisplayAbilityMessage(GameObject oTarget, int nAbility, int nDeactivate = EngineConstants.FALSE)
    {

        if (IsPartyMember(oTarget) == EngineConstants.FALSE)
        {
            if (nAbility == EngineConstants.ABILITY_TALENT_STEALTH)
            {
                return;
            }

            if (GetCombatState(oTarget) == EngineConstants.FALSE)
            {
                return;
            }
        }

        if (nDeactivate == EngineConstants.FALSE && (GetStealthEnabled(oTarget) == EngineConstants.FALSE || IsPartyMember(oTarget) != EngineConstants.FALSE))
        {
            if (GetShowSpecialMoveFloaties() != EngineConstants.FALSE)
            {
                int nStringId = GetM2DAInt(EngineConstants.TABLE_ABILITIES_SPELLS, "Namestrref", nAbility);
                string sDisplay = "";
                if (nStringId == 0)
                {
#if DEBUG
                    if (GetDebugHelpersEnabled() != EngineConstants.FALSE)
                    {
                        sDisplay = "?- NO STRINGREF - " + GetM2DAString(EngineConstants.TABLE_ABILITIES_SPELLS, "Label", nAbility);
                    }
#endif
                    return;
                }
                else
                {
                    sDisplay = GetStringByStringId(nStringId);
                }

                /*  if(nDeactivate && GetDebugHelpersEnabled())
                  {
                      //    s = "?DEACTIVATING: " + s;
                  }*/

                DisplayFloatyMessage(oTarget, sDisplay, EngineConstants.FLOATY_MESSAGE, EngineConstants.UI_DISPLAY_COLOR_ABILITY, EngineConstants.UI_DISPLAY_DURATION_ABILITY);
            }
        }
    }

    public void UI_DisplayAreaEnterMessage()
    {
        string sName = GetName(GetArea(GetHero()));

        if (IsStringEmpty(sName) != EngineConstants.FALSE && EngineConstants.LOG_ENABLED != EngineConstants.FALSE)
        {
#if DEBUG
            DisplayStatusMessage("Missing Area Name!!", EngineConstants.UI_DISPLAY_COLOR_ERROR);
#endif
        }
        else
        {
            DisplayStatusMessage(sName, EngineConstants.UI_DISPLAY_COLOR_DEFAULT);
        }

    }

    public void UI_DisplayCodexMessage(GameObject oPlaceable, string sText)
    {
        string sPrefix = GetStringByStringId(377259);
        sText = sPrefix + " " + sText;
        DisplayFloatyMessage(oPlaceable, sText, EngineConstants.FLOATY_MESSAGE, EngineConstants.UI_DISPLAY_COLOR_CODEX, EngineConstants.UI_DISPLAY_DURATION_CODEX);
    }

    public void UI_DisplayRoomEnterMessage(GameObject oTrigger)
    {

        int nId = GetLocalInt(oTrigger, EngineConstants.TRIGGER_ROOM_TEXT);

        if (nId > 0)
        {
            string sStr = GetStringByStringId(nId);
            DisplayStatusMessage(sStr, EngineConstants.UI_DISPLAY_COLOR_DEFAULT);
            SetLocalInt(oTrigger, EngineConstants.TRIGGER_ROOM_TEXT, 0);
        }

    }

    public void UI_DisplayXPFloaty(GameObject oTarget, int nXP)
    {
        string s = IntToString(nXP);

        DisplayFloatyMessage(oTarget, s + " " + GetStringByStringId(EngineConstants.STRING_ID_XP_FLOATY), EngineConstants.FLOATY_MESSAGE, EngineConstants.UI_DISPLAY_COLOR_XP, EngineConstants.UI_DISPLAY_DURATION_XP);
    }
}