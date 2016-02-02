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
     // -----------------------------------------------------------------------------
     //  Sound set system.
     //
     //  A rough implementation for E3, but it seems we're now permanently stuck with it.
     //
     // -----------------------------------------------------------------------------

     //#include"2da_constants_h"
     //#include"sys_stealth_h"
     //#include"core_h"

     //moved public const float EngineConstants.SOUND_THRESH_MANA_STAMINA_LOW       = 0.15;
     //moved public const float EngineConstants.SOUND_THRESH_HEALTH_LOW             = 0.25f;
     //moved public const float EngineConstants.SOUND_THRESH_HEALTH_NEAR_DEATH      = 0.15;
     //moved public const float EngineConstants.SOUND_THRESH_DAMAGE_AMOUNT          = 5.0f; // minimum damage for pain grunt, see damaged xEvent in rules core

     //moved public const int EngineConstants.SOUND_SITUATION_COMMAND_COMPLETE      = 1;
     //moved public const int EngineConstants.SOUND_SITUATION_ENEMY_SIGHTED         = 2;
     //moved public const int EngineConstants.SOUND_SITUATION_GOT_DAMAGED           = 3;
     //moved public const int EngineConstants.SOUND_SITUATION_ENEMY_KILLED          = 4;
     //moved public const int EngineConstants.SOUND_SITUATION_COMBAT_CHARGE         = 5;
     //moved public const int EngineConstants.SOUND_SITUATION_COMBAT_BATTLECRY      = 6;
     //moved public const int EngineConstants.SOUND_SITUATION_ATTACK_IMPACT         = 7;
     //moved public const int EngineConstants.SOUND_SITUATION_PARTY_MEMBER_SLAIN    = 8;
     //moved public const int EngineConstants.SOUND_SITUATION_DYING                 = 9;
     //moved public const int EngineConstants.SOUND_SITUATION_END_OF_COMBAT         = 10;
     //moved public const int EngineConstants.SOUND_SITUATION_SELECTED              = 11;
     //moved public const int EngineConstants.SOUND_SITUATION_ORDER_RECEIVED        = 12;
     //moved public const int EngineConstants.SOUND_SITUATION_SPELL_INTERRUPTED     = 13;
     //moved public const int EngineConstants.SOUND_SITUATION_SKILL_FAILURE         = 14;

     public float _GetRelativeResourceLevel(GameObject oCreature, int nResource)
     {
          float fCur = GetCreatureProperty(oCreature, nResource, EngineConstants.PROPERTY_VALUE_CURRENT);
          float fMax = GetCreatureProperty(oCreature, nResource, EngineConstants.PROPERTY_VALUE_TOTAL);

          return (fMax > 0.0 ? fCur / fMax : 0.0f);

     }

     /*
     * @brief Returns the state of a soundset flag
     *
     * A creature flag (EngineConstants.SOUNDSET_FLAG_*) is a persistent boolean variable
     *
     * @param oCreature The creature to check
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE state of the flag.
     */
     public int SSGetSoundSetFlag(GameObject oCreature, int nSSEntry)
     {
          // -------------------------------------------------------------------------
          // We got more than 32 SS entries, so we need find out in which variable
          // this particular ssentry is stored. We do this by dividing the entry
          // by 32 and appending the result to the base flag variable name.
          // -------------------------------------------------------------------------
          int nVar = nSSEntry / 32;
          string sVar = EngineConstants.SOUND_SET_FLAGS + ToString(nVar);

          int nFlag = (0x00000001 << (nSSEntry % 32));

          int nVal = GetLocalInt(oCreature, sVar);

          Log_Trace(EngineConstants.LOG_CHANNEL_SOUNDSETS, "sys_soundsets_h.GetFlag." + sVar, "Flag: " + IntToString(nSSEntry) + "(" + IntToHexString(nFlag) + ")" + " Value: " + IntToHexString(nVal) + " Result: " + IntToString(((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE)), oCreature);

          return ((nVal & nFlag) == nFlag ? EngineConstants.TRUE : EngineConstants.FALSE);
     }

     /*
     * @brief Sets a EngineConstants.SOUNDSET_FLAG_* flag (boolean persistent variable) on a creature
     **
     * @param oCreature The creature to set the flag on
     * @param nFlag     EngineConstants.SOUNDSET_FLAG_* to set.
     * @param bSet      whether to set or to clear the flag.
     *
     * @returns  EngineConstants.TRUE or EngineConstants.FALSE
     **/
     public void SSSetSoundSetFlag(GameObject oCreature, int nSSEntry, int bSet = EngineConstants.TRUE)
     {

          // -------------------------------------------------------------------------
          // We got more than 32 SS entries, so we need find out in which variable
          // this particular ssentry is stored. We do this by dividing the entry
          // by 32 and appending the result to the base flag variable name.
          // -------------------------------------------------------------------------
          int nVar = nSSEntry / 32;
          string sVar = EngineConstants.SOUND_SET_FLAGS + ToString(nVar);

          int nFlag = (0x00000001 << (nSSEntry % 32));

          int nVal = GetLocalInt(oCreature, sVar);
          int nOld = nVal;

          if (bSet == EngineConstants.TRUE)
          {
               nVal |= nFlag;
          }
          else
          {
               nVal &= ~nFlag;
          }

          Log_Trace(EngineConstants.LOG_CHANNEL_SOUNDSETS, "sys_soundset_h.SetFlag." + sVar + "." + ToString(bSet == EngineConstants.TRUE), "Flag: " + IntToString(nSSEntry) + "(" + IntToHexString(nFlag) + ")" + " Was: " + IntToHexString(nOld) + " Is: " + IntToHexString(nVal), oCreature);

          SetLocalInt(oCreature, sVar, nVal);
     }

     public int _GetSituationalCombatSound(GameObject oCreature, int nSituation, GameObject oTarget = null, int nCommandType = EngineConstants.COMMAND_TYPE_INVALID)
     {
          int nSound = 0;

          switch (nSituation)
          {
               case EngineConstants.SOUND_SITUATION_COMBAT_BATTLECRY:
                    {
                         int nRand = Engine_Random(12);
                         if (nRand < 1)
                              nSound = EngineConstants.SS_COMBAT_TAUNT;
                         else if (nRand < 7)
                              nSound = EngineConstants.SS_COMBAT_ATTACK;
                         else
                              nSound = EngineConstants.SS_COMBAT_BATTLE_CRY;

                         break;
                    }
               case EngineConstants.SOUND_SITUATION_GOT_DAMAGED:
                    nSound = EngineConstants.SS_COMBAT_PAIN_GRUNT;
                    break;
               /*case EngineConstants.SOUND_SITUATION_COMBAT_CHARGE:
                   nSound = EngineConstants.SS_COMBAT_ATTACK;
                   break;*/
               case EngineConstants.SOUND_SITUATION_ENEMY_KILLED:
                    nSound = EngineConstants.SS_COMBAT_ENEMY_KILLED;
                    break;
               case EngineConstants.SOUND_SITUATION_ATTACK_IMPACT:
                    nSound = EngineConstants.SS_COMBAT_ATTACK_GRUNT;
                    break;
               case EngineConstants.SOUND_SITUATION_END_OF_COMBAT:
                    nSound = EngineConstants.SS_COMBAT_CHEER_PARTY;
                    break;
               case EngineConstants.SOUND_SITUATION_SPELL_INTERRUPTED:
                    nSound = EngineConstants.SS_SPELL_FAILED;
                    break;
               case EngineConstants.SOUND_SITUATION_PARTY_MEMBER_SLAIN:
                    if (IsObjectValid(oTarget) != EngineConstants.FALSE && IsObjectHostile(oTarget, oCreature) != EngineConstants.FALSE)
                         nSound = EngineConstants.SS_COMBAT_MONSTER_SLEW_PARTY_MEMBER;
                    break;
               case EngineConstants.SOUND_SITUATION_COMMAND_COMPLETE:
                    {
                         if (_GetRelativeResourceLevel(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH) < EngineConstants.SOUND_THRESH_HEALTH_NEAR_DEATH)
                              nSound = EngineConstants.SS_COMBAT_NEAR_DEATH;
                         else if (_GetRelativeResourceLevel(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH) < EngineConstants.SOUND_THRESH_HEALTH_LOW)
                              nSound = EngineConstants.SS_COMBAT_HEAL_ME;
                         else if (_GetRelativeResourceLevel(oCreature, EngineConstants.PROPERTY_DEPLETABLE_MANA_STAMINA) < EngineConstants.SOUND_THRESH_MANA_STAMINA_LOW
                                && IsControlled(oCreature) != EngineConstants.FALSE)
                         {
                              nSound = (GetCreatureCoreClass(oCreature) == EngineConstants.CLASS_WIZARD) ? EngineConstants.SS_MANA_LOW : EngineConstants.SS_COMBAT_STAMINA_LOW;
                         }
                         break;
                    }
               case EngineConstants.SOUND_SITUATION_DYING:
                    nSound = EngineConstants.SS_COMBAT_DEATH;
                    break;
               case EngineConstants.SOUND_SITUATION_SELECTED:
                    nSound = EngineConstants.SS_COMBAT_SELECT_NEUTRAL;
                    break;
               case EngineConstants.SOUND_SITUATION_ORDER_RECEIVED:
                    nSound = EngineConstants.SS_EXPLORE_START_TASK;
                    break;
          }
          return nSound;
     }

     public int _GetSituationalExploreSound(GameObject oCreature, int nSituation, GameObject oTarget, int nCommandType)
     {
          int nSound = 0;

          switch (nSituation)
          {
               case EngineConstants.SOUND_SITUATION_COMMAND_COMPLETE:
                    if (_GetRelativeResourceLevel(oCreature, EngineConstants.PROPERTY_DEPLETABLE_HEALTH) < EngineConstants.SOUND_THRESH_HEALTH_LOW)
                         nSound = EngineConstants.SS_EXPLORE_HEAL_ME;
                    break;
               case EngineConstants.SOUND_SITUATION_ENEMY_SIGHTED:
                    {
                         if (IsPartyMember(oCreature) != EngineConstants.FALSE)
                         {
                              switch (GetCreatureTypeClassification(GetAppearanceType(oTarget)))
                              {
                                   case EngineConstants.CREATURE_TYPE_DARKSPAWN:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_DARKSPAWN;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_ANIMAL:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_ANIMAL;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_BEAST:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_BEAST;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_UNDEAD:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_UNDEAD;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_DEMON:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_DEMON;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_DRAGON:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_DRAGON;
                                        break;
                                   case EngineConstants.CREATURE_TYPE_INVALID:
                                        nSound = 0;
                                        break;
                                   default:
                                        nSound = EngineConstants.SS_EXPLORE_ENEMIES_SIGHTED_OTHER;
                                        break;
                              }
                              break;
                         }
                         else
                         {
                              nSound = RandomFloat() > 0.5 ? EngineConstants.SS_WARCRY : EngineConstants.SS_COMBAT_BATTLE_CRY;
                         }
                         break;
                    }
               case EngineConstants.SOUND_SITUATION_SELECTED:
                    {
                         nSound = EngineConstants.SS_EXPLORE_SELECT_NEUTRAL;
                         if (IsFollower(oCreature) != EngineConstants.FALSE)
                         {
                              nSound = EngineConstants.SS_EXPLORE_SELECT_HATE;
                              int nApproval = GetFollowerApproval(oCreature);

                              if (nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_NEUTRAL))
                                   nSound = EngineConstants.SS_EXPLORE_SELECT_NEUTRAL;
                              if (nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_WARM))
                                   nSound = EngineConstants.SS_EXPLORE_SELECT_FRIENDLY;
                              if (nApproval >= GetLocalInt(GetModule(), EngineConstants.APP_RANGE_VALUE_FRIENDLY))
                                   nSound = EngineConstants.SS_EXPLORE_SELECT_LOVE;
                         }
                         break;
                    }
               case EngineConstants.SOUND_SITUATION_DYING:
                    nSound = EngineConstants.SS_COMBAT_DEATH;
                    break;
               case EngineConstants.SOUND_SITUATION_ORDER_RECEIVED:
                    {
                         nSound = EngineConstants.SS_EXPLORE_START_TASK;
                         break;
                    }
               case EngineConstants.SOUND_SITUATION_SKILL_FAILURE:
                    {
                         nSound = EngineConstants.SS_SKILL_FAILURE;
                         break;
                    }
          }
          return nSound;
     }

     public void SSPlaySituationalSound(GameObject oCreature, int nSituation, GameObject oTarget = null, int nCommandType = EngineConstants.COMMAND_TYPE_INVALID)
     {

          if (IsObjectValid(oCreature) != EngineConstants.FALSE && (IsDead(oCreature) == EngineConstants.FALSE || nSituation == EngineConstants.SOUND_SITUATION_DYING) && GetObjectActive(oCreature) != EngineConstants.FALSE && GetObjectType(oCreature) == EngineConstants.OBJECT_TYPE_CREATURE)
          {
               int nMode = GetGameMode();
               int nSound = 0;

               // -------------------------------------------------------------------------
               // Only play sound in combat or explore mode so cinematics don't get unintentional VO.
               // -------------------------------------------------------------------------
               if (nMode == EngineConstants.GM_COMBAT)
               {
                    nSound = _GetSituationalCombatSound(oCreature, nSituation, oTarget, nCommandType);
               }
               else if (nMode == EngineConstants.GM_EXPLORE)
               {
                    nSound = _GetSituationalExploreSound(oCreature, nSituation, oTarget, nCommandType);
               }

               // Immortal creatures never ask for healing.
               if (nSound == EngineConstants.SS_COMBAT_NEAR_DEATH || nSound == EngineConstants.SS_COMBAT_HEAL_ME ||
                   nSound == EngineConstants.SS_EXPLORE_HEAL_ME)
               {
                    if (IsImmortal(oCreature) != EngineConstants.FALSE || IsPlot(oCreature) != EngineConstants.FALSE)
                    {
                         nSound = 0;
                    }
               }

               if (nSound > 0)
               {
                    if (nSound == EngineConstants.SS_COMBAT_DEATH)
                    {
                         Log_Trace(EngineConstants.LOG_CHANNEL_COMBAT_DEATH, "sys_soundsets.play", "nSituation: " + ToString(nSituation) + ", Sound: " + ToString(nSound));

                         int nRank = GetCreatureRank(oCreature);
                         if (nRank == EngineConstants.CREATURE_RANK_BOSS || nRank == EngineConstants.CREATURE_RANK_ELITE_BOSS)
                         {
                              // boss+ creature always complain about dying.
                              PlaySoundSet(oCreature, nSound, 1.0f);
                         }
                         else
                         {
                              PlaySoundSet(oCreature, nSound);
                         }
                    }
                    else if (SSGetSoundSetFlag(oCreature, nSound) == EngineConstants.FALSE)
                    {
                         if (nSituation == EngineConstants.SOUND_SITUATION_ORDER_RECEIVED)
                         {
                              if (IsStealthy(oCreature) == EngineConstants.FALSE)
                              {
                                   float fProb = GetM2DAFloat(EngineConstants.TABLE_COMMANDS, "SoundsetProbability", nCommandType);

                                   // No voice chat when moving in explore mode.
                                   switch (nCommandType)
                                   {
                                        case EngineConstants.COMMAND_TYPE_DRIVE:
                                        case EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION:
                                        case EngineConstants.COMMAND_TYPE_MOVE_TO_OBJECT:
                                             if (nMode == EngineConstants.GM_EXPLORE) fProb = 0.0f;
                                             break;
                                   }

                                   if (fProb > 0.0f)
                                   {
#if DEBUG
                                        Log_Trace(EngineConstants.LOG_CHANNEL_SOUNDSETS, "sys_soundsets.play", "SOUND_SITUATION_ORDER_RECEIVED: Command = " + Log_GetCommandNameById(nCommandType) + ", Probability = " + ToString(fProb));
#endif
                                        PlaySoundSet(oCreature, nSound, fProb);
                                   }
                              }
                         }
                         else
                         {
#if DEBUG
                              Log_Trace(EngineConstants.LOG_CHANNEL_SOUNDSETS, "sys_soundsets.play", "oCreature: " + ToString(oCreature) + ", nSituation: " + ToString(nSituation) + ", Sound: " + ToString(nSound));
#endif
                              PlaySoundSet(oCreature, nSound);
                         }

                         if (GetIsSoundSetEntryTypeRestricted(nSound) != EngineConstants.FALSE)
                         {
                              SSSetSoundSetFlag(oCreature, nSound);
                         }
                    }
                    else
                    {
#if DEBUG
                         Log_Trace(EngineConstants.LOG_CHANNEL_SOUNDSETS, "sys_soundsets.play", "Not playing sound " + ToString(nSound) + " as flag was set");
#endif
                    }
               }
          }
     }

     public void SSResetSoundsetRestrictions(GameObject oCreature)
     {
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_NEAR_DEATH, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_HEAL_ME, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_EXPLORE_HEAL_ME, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_MANA_LOW, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_STAMINA_LOW, EngineConstants.FALSE);
     }

     public void SSPartyResetSoundsetRestrictions()
     {
          List<GameObject> aParty = GetPartyList();
          int nSize = GetArraySize(aParty);
          int i;

          for (i = 0; i < nSize; i++)
          {
               SSResetSoundsetRestrictions(aParty[i]);
          }

     }

     public void SSResetSoundsetRestrictionsOnHeal(GameObject oCreature)
     {
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_NEAR_DEATH, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_HEAL_ME, EngineConstants.FALSE);
          SSSetSoundSetFlag(oCreature, EngineConstants.SS_EXPLORE_HEAL_ME, EngineConstants.FALSE);
     }

     public void SSResetSoundsetRestrictionsOnDamage(GameObject oCreature, float fOldHealth)
     {
          if (fOldHealth > EngineConstants.SOUND_THRESH_HEALTH_LOW)
          {
               SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_HEAL_ME, EngineConstants.FALSE);
               SSSetSoundSetFlag(oCreature, EngineConstants.SS_EXPLORE_HEAL_ME, EngineConstants.FALSE);
          }
          else if (fOldHealth > EngineConstants.SOUND_THRESH_HEALTH_NEAR_DEATH)
          {
               SSSetSoundSetFlag(oCreature, EngineConstants.SS_COMBAT_NEAR_DEATH, EngineConstants.FALSE);
          }
     }

     /*
     * @brief Causes a party member of a given class (other than oPartyMember) to play a sound for a given situation.
     **/
     public void SSPartyMemberComment(int nClass, int nSituation, GameObject oPartyMember)
     {
          List<GameObject> aParty = GetPartyList(oPartyMember);
          int i;
          for (i = 0; i < GetArraySize(aParty); i++)
          {
               if (GetCreatureCoreClass(aParty[i]) == nClass
                  && aParty[i] != oPartyMember)
               {
                    SSPlaySituationalSound(aParty[i], nSituation);
                    break;
               }
          }
     }
}