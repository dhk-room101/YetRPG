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
     //==============================================================================
     /*

         A Paragon of Her Kind
          -> Caridin's Spirit Apparatus

         yes, this was done in scripting...

         *** Each face uses EngineConstants.CREATURE_COUNTER_1 as its Face Number (1-4)
         *** Each face uses EngineConstants.CREATURE_COUNTER_2 to keep track of its Health
         // 0 = phase 1
         // 1 = phase 2
         // 2 = phase 3
         // 3 = phase 4 (defeated)

     */
     //------------------------------------------------------------------------------
     // Created By: joshua
     // Created On: December 18, 2007
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"orz_constants_h"
     //#include"plt_orz540pt_anvil_ot_public void"

     //------------------------------------------------------------------------------
     // Constants
     //------------------------------------------------------------------------------

     //moved public const int       EngineConstants.ORZ_APPARATUS_PHASE_1                       = 0;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PHASE_2                       = 1;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PHASE_3                       = 2;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PHASE_4                       = 3;

     //moved public const int       EngineConstants.ORZ_APPARATUS_NUM_FACES                     = 4;

     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_INIT                    = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_UPDATE                  = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_SPECIAL                 = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;
     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_DEFEATED                = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_04;
     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_SPIRIT_PROJECTILE       = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_05;
     //moved public const int       EngineConstants.ORZ_APPARATUS_EVENT_SPIRIT_POST_DEATH       = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_06;

     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_1              = 1004;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_2              = 1028;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_3              = 1037;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_SPIRIT_DISSIPATE          = 93045;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_SPIRIT_EXPLODE            = 90183;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_ANVIL_IMPACT_1            = 5025;
     //moved public const int       EngineConstants.ORZ_APPARATUS_VFX_ANVIL_IMPACT_2            = 90049;

     //moved public const int       EngineConstants.ORZ_APPARATUS_PRJ_PHASE_1                   = 208;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PRJ_PHASE_2                   = 209;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PRJ_PHASE_3                   = 210;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PRJ_SUMMON                    = 211;
     //moved public const int       EngineConstants.ORZ_APPARATUS_PRJ_ATTACK                    = 212;

     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ROTATE                  = 1000;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ROTATE_LOOP             = 1001;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ROTATE_COOLDOWN         = 1002;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_SUMMON                  = 2000;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_SUMMON_COOLDOWN         = 2001;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ATTACK                  = 3000;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP             = 3001;
     //moved public const int       EngineConstants.ORZ_APPARATUS_STATE_ATTACK_COOLDOWN         = 3002;

     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE            = 0.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_LOOP       = 0.01f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_LOOP_2     = 2.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_COOLDOWN   = 2.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_SUMMON            = 1.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_SUMMON_COOLDOWN   = 1.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK            = 2.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_LOOP       = 4.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_LOOP_2     = 5.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_COOLDOWN   = 2.0f;

     //moved public const float     EngineConstants.ORZ_APPARATUS_ROTATION_STEP_SIZE            = 2.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_FACING_3                      = -172.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_FACING_4                      = -82.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_FACING_1                      = 8.0f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_FACING_2                      = 98.0f;

     //moved public const float     EngineConstants.ORZ_APPARATUS_POS_X                         = 288.00f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_POS_Y                         = 108.00f;
     //moved public const float     EngineConstants.ORZ_APPARATUS_POS_Z                         = -13.75f;

     //moved public const string    EngineConstants.ORZ_APPARATUS_BODY                          = "orz540ip_apparatus";
     //moved public const string    EngineConstants.ORZ_APPARATUS_FACE_PREFIX                   = "orz540cr_spirit_app_head_";
     //moved public const string    EngineConstants.ORZ_APPARATUS_SPIRIT_WP_PREFIX              = "orz540wp_spirit_";
     //moved public const string    EngineConstants.ORZ_APPARATUS_CR_SPIRIT_1                   = "orz540cr_spirit_1";
     //moved public const string    EngineConstants.ORZ_APPARATUS_CR_SPIRIT_2                   = "orz540cr_spirit_2";
     //moved public const string    EngineConstants.ORZ_APPARATUS_CR_SPIRIT_3                   = "orz540cr_spirit_3";
     //moved public const string    EngineConstants.ORZ_APPARATUS_IP_ANVIL                      = "orz540ip_apparatus_control";
     //moved public const string    EngineConstants.ORZ_APPARATUS_SOUND_SPIN_START              = "vfx_spells/vfx_new/creation/fxa_burrow_imp_p_imp_n";
     //moved public const string    EngineConstants.ORZ_APPARATUS_SOUND_SPIN_END                = "glo_fly_plc/placeables/statue/statue_destroy";

     //moved public const string  rORZ_APPARATUS_SPIRIT_1                     = "orz540cr_spirit_1.utc";
     //moved public const string  rORZ_APPARATUS_SPIRIT_2                     = "orz540cr_spirit_2.utc";
     //moved public const string  rORZ_APPARATUS_SPIRIT_3                     = "orz540cr_spirit_3.utc";

     //------------------------------------------------------------------------------
     // Function Declaration
     //------------------------------------------------------------------------------

     // Apparatus xEvent functions
     public void ORZ_Apparatus_EncounterInit()
     {
          List<GameObject> arFaceArray = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);
          int i;
          xEvent evEvent;

          SetLocalInt(gameObject, EngineConstants.PLC_COUNTER_1, 0);
          evEvent = Event(EngineConstants.ORZ_APPARATUS_EVENT_UPDATE);
          SetEventIntegerRef(ref evEvent, 0, EngineConstants.ORZ_APPARATUS_STATE_ROTATE_COOLDOWN);        // Encounter State
          SetEventIntegerRef(ref evEvent, 1, 0);                                 // Encounter State Counter
          SetEventIntegerRef(ref evEvent, 2, 0);                                 // Encounter State Lock (CUT)
          SetEventIntegerRef(ref evEvent, 3, 0);                                 // Current Rotation Steps Left
          SetEventIntegerRef(ref evEvent, 4, 0);                                 // Current Rotation State
          SetEventIntegerRef(ref evEvent, 5, 0);                                 // Current Attack State

          for (i = 0; i < EngineConstants.ORZ_APPARATUS_NUM_FACES; i++)
          {
               // store face as an xEvent GameObject to save some time during rotation loop
               SetEventObjectRef(ref evEvent, i, arFaceArray[i]);

               // position got fudged by the cutscene
               SetPosition(arFaceArray[i], Vector(EngineConstants.ORZ_APPARATUS_POS_X, EngineConstants.ORZ_APPARATUS_POS_Y, EngineConstants.ORZ_APPARATUS_POS_Z), EngineConstants.FALSE);
               _Apparatus_Phase_Init(arFaceArray[i], 0);
          }

          UT_TeamGoesHostile(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);
          PlaySoundSet(arFaceArray[0], EngineConstants.SS_COMBAT_TAUNT, 1.0f);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, EffectVisualEffect(90016), gameObject);
          DelayEvent(1.0f, gameObject, evEvent);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventEncounterUpdate(xEvent evEvent)
     {
          int bEncounterDefeated = WR_GetPlotFlag(EngineConstants.PLT_ORZ540PT_ANVIL_OT_VOID, EngineConstants.ORZ_AOTV__EVENT_TRAP_3_SOLVED);
          int nEncounterState = GetEventIntegerRef(ref evEvent, 0);
          int nEncounterCounter = GetEventIntegerRef(ref evEvent, 1);
          int nNextEncounterState = 0;
          float fDelay = 0.0f;

          // Kill switch
          if (bEncounterDefeated != EngineConstants.FALSE)
               return;

          switch (nEncounterState)
          {
               case EngineConstants.ORZ_APPARATUS_STATE_ROTATE:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ROTATE
                         //------------------------------------------------------------------

                         // screenshake, play the rotation sound
                         ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(2), 2.0f);
                         PlaySound(gameObject, EngineConstants.ORZ_APPARATUS_SOUND_SPIN_START);

                         // Calculate Rotation Steps Left
                         SetEventIntegerRef(ref evEvent, 3, FloatToInt(90.0f / EngineConstants.ORZ_APPARATUS_ROTATION_STEP_SIZE));

                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ROTATE_LOOP;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE;
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_ROTATE_LOOP:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ROTATE
                         //------------------------------------------------------------------
                         int nRotationStepsLeft = GetEventIntegerRef(ref evEvent, 3);
                         int i;
                         float fFacing;
                         GameObject oCurrentFace;

                         if (nRotationStepsLeft > 0)
                         {
                              for (i = 0; i < EngineConstants.ORZ_APPARATUS_NUM_FACES; i++)
                              {
                                   oCurrentFace = GetEventObjectRef(ref evEvent, i);
                                   fFacing = (GetFacing(oCurrentFace) + EngineConstants.ORZ_APPARATUS_ROTATION_STEP_SIZE);
                                   // Do smooth rotation here.
                                   SetFacing(oCurrentFace, fFacing);
                              }
                              // decrement steps left in rotation
                              SetEventIntegerRef(ref evEvent, 3, (nRotationStepsLeft - 1));

                              nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ROTATE_LOOP;
                              fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_LOOP;
                         }
                         else
                         {
                              int nRotationState = GetEventIntegerRef(ref evEvent, 4);

                              PlaySound(gameObject, EngineConstants.ORZ_APPARATUS_SOUND_SPIN_END);

                              // Update Rotation State
                              nRotationState = ((nRotationState + 1) % 4);
                              SetEventIntegerRef(ref evEvent, 4, nRotationState);
                              SetLocalInt(gameObject, EngineConstants.PLC_COUNTER_2, nRotationState);

                              // Last Ditch Effort: Force faces to proper position
                              for (i = 0; i < EngineConstants.ORZ_APPARATUS_NUM_FACES; i++)
                              {
                                   oCurrentFace = GetEventObjectRef(ref evEvent, i);
                                   int nFacing = ((GetLocalInt(oCurrentFace, EngineConstants.CREATURE_COUNTER_1) - 1) + nRotationState) % 4;
                                   switch (nFacing)
                                   {
                                        case 0: SetFacing(oCurrentFace, EngineConstants.ORZ_APPARATUS_FACING_1); break;
                                        case 1: SetFacing(oCurrentFace, EngineConstants.ORZ_APPARATUS_FACING_2); break;
                                        case 2: SetFacing(oCurrentFace, EngineConstants.ORZ_APPARATUS_FACING_3); break;
                                        case 3: SetFacing(oCurrentFace, EngineConstants.ORZ_APPARATUS_FACING_4); break;
                                   }
                              }

                              nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ROTATE_COOLDOWN;
                              fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_LOOP_2;
                         }
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_ROTATE_COOLDOWN:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ROTATE_COOLDOWN
                         //------------------------------------------------------------------
                         int nRotationState = GetEventIntegerRef(ref evEvent, 4);
                         List<GameObject> arPartyList = GetPartyList(GetHero());
                         int i, size;

                         PlaySoundSet(GetEventObjectRef(ref evEvent, 0), EngineConstants.SS_COMBAT_TAUNT, 1.0f);

                         // Knockdown nearby party members
                         Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectVisualEffect(3015), GetLocation(gameObject));
                         size = GetArraySize(arPartyList);
                         for (i = 0; i < size; i++)
                         {
                              if (IsDead(arPartyList[i]) == EngineConstants.FALSE && GetDistanceBetween(arPartyList[i], gameObject) < 5.0f)
                              {
                                   if (ResistanceCheck(gameObject, arPartyList[i], EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH, EngineConstants.RESISTANCE_PHYSICAL) == EngineConstants.FALSE)
                                        ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectKnockdown(gameObject, 0), arPartyList[i], 1.0f, gameObject);
                              }
                         }

                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_SUMMON;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ROTATE_COOLDOWN;
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_SUMMON:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_SUMMON
                         //------------------------------------------------------------------
                         int nRotationState = GetEventIntegerRef(ref evEvent, 4);
                         int i;

                         // joshua(04/21/09): no sucking in spirits unless one is killed                         
                         SetLocalInt(GetObjectByTag(EngineConstants.ORZ_APPARATUS_BODY), EngineConstants.PLC_COUNTER_3, EngineConstants.FALSE);

                         for (i = 0; i < EngineConstants.ORZ_APPARATUS_NUM_FACES; i++)
                         {
                              _Apparatus_Phase_Summon(GetEventObjectRef(ref evEvent, i), nRotationState);
                         }

                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_SUMMON_COOLDOWN;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_SUMMON;
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_SUMMON_COOLDOWN:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_SUMMON_COOLDOWN
                         //------------------------------------------------------------------
                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ATTACK;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_SUMMON_COOLDOWN;
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_ATTACK:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ATTACK
                         //------------------------------------------------------------------
                         SetEventIntegerRef(ref evEvent, 5, 0);  // Reset Attack Counter
                         PlaySoundSet(GetEventObjectRef(ref evEvent, 0), EngineConstants.SS_COMBAT_ATTACK, 1.0f);

                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK;
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP
                         //------------------------------------------------------------------
                         int nRotationState = GetEventIntegerRef(ref evEvent, 4);
                         int nAttackCounter = GetEventIntegerRef(ref evEvent, 5);
                         GameObject oApparatus = GetObjectByTag(EngineConstants.ORZ_APPARATUS_BODY);
                         int i;

                         // joshua(04/21/09): no sucking in spirits unless one is killed  
                         if (nAttackCounter < 6 || GetLocalInt(oApparatus, EngineConstants.PLC_COUNTER_3) == EngineConstants.FALSE)
                         {
                              // use attacks
                              for (i = 0; i < EngineConstants.ORZ_APPARATUS_NUM_FACES; i++)
                                   _Apparatus_Phase_Attack(GetEventObjectRef(ref evEvent, i), nRotationState);

                              // Increment Attack Counter
                              SetEventIntegerRef(ref evEvent, 5, (nAttackCounter + 1));

                              nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP;
                              fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_LOOP;
                         }
                         else
                         {
                              // soundset, apply cool vfx vortex
                              PlaySoundSet(GetEventObjectRef(ref evEvent, 0), EngineConstants.SS_COMBAT_TAUNT, 1.0f);
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(90121), gameObject, EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_LOOP_2, gameObject);

                              nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ATTACK_COOLDOWN;
                              fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_LOOP_2;
                         }

                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_STATE_ATTACK_COOLDOWN:
                    {
                         //------------------------------------------------------------------
                         // EngineConstants.ORZ_APPARATUS_STATE_ATTACK_LOOP
                         //------------------------------------------------------------------       
                         // destroy the remaining spirits, play soundset
                         _Apparatus_DestroyActiveSpirits();
                         PlaySoundSet(GetEventObjectRef(ref evEvent, 0), EngineConstants.SS_COMBAT_TAUNT, 1.0f);

                         nNextEncounterState = EngineConstants.ORZ_APPARATUS_STATE_ROTATE;
                         fDelay = EngineConstants.ORZ_APPARATUS_DELAY_STATE_ATTACK_COOLDOWN;
                         break;
                    }
          }
          //--------------------------------------------------------------------------
          if (nNextEncounterState > 0)
          {
               nEncounterCounter++;
               SetEventIntegerRef(ref evEvent, 0, nNextEncounterState);
               SetEventIntegerRef(ref evEvent, 1, nEncounterCounter);
               SetLocalInt(gameObject, EngineConstants.PLC_COUNTER_1, nEncounterCounter);
               DelayEvent(fDelay, gameObject, evEvent);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventEncounterDefeated(xEvent evEvent)
     {
          List<GameObject> arAnvils = UT_GetAllObjectsInAreaByTag(EngineConstants.ORZ_APPARATUS_IP_ANVIL, EngineConstants.OBJECT_TYPE_PLACEABLE);
          int i, size = GetArraySize(arAnvils);

          // reset all the anvils
          for (i = 0; i < size; i++)
               _Apparatus_DeactivateAnvil(arAnvils[i]);

          // clean up everything else
          RemoveVisualEffect(gameObject, 90016);
          _Apparatus_ClearAllFaceEffects();
          _Apparatus_DestroyActiveSpirits();

          // set the plot flag that this trap is solved
          WR_SetPlotFlag(EngineConstants.PLT_ORZ540PT_ANVIL_OT_VOID, EngineConstants.ORZ_AOTV__EVENT_TRAP_3_SOLVED, EngineConstants.TRUE, EngineConstants.TRUE);

          // Send special xEvent to the apparatus
          DelayEvent(2.0f, gameObject, Event(EngineConstants.ORZ_APPARATUS_EVENT_SPECIAL));
          PlaySoundSet(GetObjectByTag(EngineConstants.ORZ_APPARATUS_FACE_PREFIX + "1"), EngineConstants.SS_COMBAT_DEATH, 1.0f);

     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventCoolDefeatedEffects(xEvent evEvent)
     {
          int nCount = GetEventIntegerRef(ref evEvent, 0);
          Vector3 loc = GetLocation(gameObject);
          Vector3 vPos = GetPositionFromLocation(loc);
          xEffect eff = EffectVisualEffect(90183);

          if (nCount == 0)
          {
               vPos.z -= 1.0f;
               loc = SetLocationPosition(loc, vPos);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectVisualEffect(1052), GetLocation(gameObject), 5.0f, gameObject);

               // remove all effects placed on the party by the apparatus
               List<GameObject> arParty = GetPartyList();
               int i, size = GetArraySize(arParty);
               for (i = 0; i < size; i++)
                    RemoveEffectsByParameters(arParty[i], EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, gameObject);

               ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(4), 5.0f);
               vPos.z += 7.0f;
               loc = SetLocationPosition(loc, vPos);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eff, loc, 3.0f, gameObject);
               SetEventIntegerRef(ref evEvent, 0, (nCount + 1));
               DelayEvent(0.5f, gameObject, evEvent);
          }
          else if (nCount == 1)
          {
               vPos.z += 3.0f;
               loc = SetLocationPosition(loc, vPos);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eff, loc, 3.0f, gameObject);
               SetEventIntegerRef(ref evEvent, 0, (nCount + 1));
               DelayEvent(0.5f, gameObject, evEvent);
          }
          else if (nCount == 2)
          {
               vPos.z += 0.0f;
               loc = SetLocationPosition(loc, vPos);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, eff, loc, 3.0f, gameObject);
               UT_TeamGoesHostile(EngineConstants.ORZ_TEAM_AOTV_TRAP_3, EngineConstants.FALSE);
               SetEventIntegerRef(ref evEvent, 0, (nCount + 1));
               DelayEvent(1.5f, gameObject, evEvent);
          }
          else if (nCount == 3)
          {
               List<GameObject> arParty = GetPartyList();
               int nPartySize = GetArraySize(arParty);
               int nRand = Engine_Random(nPartySize);
               PlaySoundSet(arParty[nRand], EngineConstants.SS_TASK_COMPLETE, 1.0f);
               SetEventIntegerRef(ref evEvent, 0, (nCount + 1));
               SetEventIntegerRef(ref evEvent, 1, nRand);
               DelayEvent(1.0f, gameObject, evEvent);
          }
          else if (nCount == 4)
          {
               List<GameObject> arParty = GetPartyList();
               int nPartySize = GetArraySize(arParty);
               int nRand = GetEventIntegerRef(ref evEvent, 1);
               PlaySoundSet(arParty[(nRand + 1) % nPartySize], EngineConstants.SS_CHEER, 1.0f);
               List<GameObject> arFaces = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);

          }
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_DestroyActiveSpirits()
     {
          List<GameObject> arSpirits = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3_SPIRITS);
          int i, size = GetArraySize(arSpirits);

          for (i = 0; i < size; i++)
               if (IsDead(arSpirits[i]) == EngineConstants.FALSE && GetObjectActive(arSpirits[i]) != EngineConstants.FALSE)
                    _Apparatus_SpiritDissipate(arSpirits[i], gameObject);
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_ClearAllFaceEffects()
     {
          List<GameObject> arFaceArray = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);
          int i, size = GetArraySize(arFaceArray);

          for (i = 0; i < size; i++)
               RemoveEffectsByParameters(arFaceArray[i], EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, arFaceArray[i]);
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_SpiritDissipate(GameObject oSpirit, GameObject oTarget)
     {
          Vector3 vSource = GetPosition(oSpirit);
          Vector3 vTarget;

          // figure out targets and sources
          if (GetTeamId(oTarget) == EngineConstants.ORZ_TEAM_AOTV_TRAP_3)
               vTarget = _Apparatus_GetFacePosition(oTarget, EngineConstants.TRUE);
          else
          {
               vTarget = GetPosition(oTarget);
               vTarget.z += 0.75f;
          }

          vSource.z += 0.75f;

          // Fire projectile at oTarget
          FireProjectile(EngineConstants.ORZ_APPARATUS_PRJ_SUMMON, vSource, vTarget, 0, EngineConstants.TRUE);

          // Apply vfx
          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY,
              EffectVisualEffect(EngineConstants.ORZ_APPARATUS_VFX_SPIRIT_DISSIPATE), GetLocation(oSpirit), 3.0f);

          // deactivate spirit to place back in creature pool, remove all hostile effects
          RemoveEffectsDueToPlotEvent(gameObject);
          RemoveEffectsByParameters(gameObject, EngineConstants.EFFECT_TYPE_DOT, EngineConstants.ABILITY_INVALID);
          WR_SetObjectActive(oSpirit, EngineConstants.FALSE);
          SetTeamId(oSpirit, -1);
          UT_CombatStop(oSpirit, GetHero());
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_Phase_Init(GameObject oFace, int nRotationState = 0)
     {
          int nPhase = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_2);
          int nPhaseFaceVFX = 0;

          // Remove all self-created effects and apply new one
          RemoveEffectsByParameters(oFace, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, oFace);
          switch (nPhase)
          {
               case EngineConstants.ORZ_APPARATUS_PHASE_1: nPhaseFaceVFX = EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_1; break;
               case EngineConstants.ORZ_APPARATUS_PHASE_2: nPhaseFaceVFX = EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_2; break;
               case EngineConstants.ORZ_APPARATUS_PHASE_3: nPhaseFaceVFX = EngineConstants.ORZ_APPARATUS_VFX_FACE_PHASE_3; break;
          }
          if (nPhaseFaceVFX > 0)
               ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, EffectVisualEffect(nPhaseFaceVFX), oFace, 0.0f, oFace);
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_Phase_Summon(GameObject oFace, int nRotationState)
     {
          int nFaceNumber = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_1);
          int nPhase = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_2);
          int nWaypointNumber = (((nFaceNumber - 1) + nRotationState) % 4) + 1;
          float fFacing = (GetFacing(oFace) * EngineConstants.PI) / 180.0f;
          GameObject oWaypoint = UT_GetNearestObjectByTag(oFace, EngineConstants.ORZ_APPARATUS_SPIRIT_WP_PREFIX + ToString(nWaypointNumber));
          Vector3 vSource, vTarget;
          string rSummon;
          xEvent evPrj;
          GameObject oPrj;

          switch (nPhase)
          {
               case EngineConstants.ORZ_APPARATUS_PHASE_1: rSummon = EngineConstants.rORZ_APPARATUS_SPIRIT_1; break;
               case EngineConstants.ORZ_APPARATUS_PHASE_2: rSummon = EngineConstants.rORZ_APPARATUS_SPIRIT_2; break;
               case EngineConstants.ORZ_APPARATUS_PHASE_3: rSummon = EngineConstants.rORZ_APPARATUS_SPIRIT_3; break;
               default: rSummon = EngineConstants.INVALID_RESOURCE; break;
          }
          if (rSummon != EngineConstants.INVALID_RESOURCE)
          {
               vSource = _Apparatus_GetFacePosition(oFace, EngineConstants.TRUE);
               vTarget = GetPosition(oWaypoint);
               // fire a projectile to the summon Vector3 to cause the summon
               oPrj = FireProjectile(EngineConstants.ORZ_APPARATUS_PRJ_SUMMON, vSource, vTarget, 0, EngineConstants.FALSE, gameObject);
               evPrj = Event(EngineConstants.EVENT_TYPE_ATTACK_IMPACT);
               SetEventCreatorRef(ref evPrj, oFace);
               SetEventIntegerRef(ref evPrj, 0, EngineConstants.ORZ_APPARATUS_PRJ_SUMMON);
               SetEventIntegerRef(ref evPrj, 1, nWaypointNumber);
               SetEventObjectRef(ref evPrj, 0, oWaypoint);
               SetEventResourceRef(ref evPrj, 0, rSummon);
               SetProjectileImpactEvent(oPrj, evPrj);
          }
     }

     //------------------------------------------------------------------------------

     public void _Apparatus_Phase_Attack(GameObject oFace, int nRotationState)
     {
          int nFaceNumber = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_1);
          int nPhase = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_2);
          int nWaypointNumber = (((nFaceNumber - 1) + nRotationState) % 4) + 1;
          Vector3 vSource = _Apparatus_GetFacePosition(oFace);
          GameObject oWaypoint = UT_GetNearestObjectByTag(oFace, EngineConstants.ORZ_APPARATUS_SPIRIT_WP_PREFIX + ToString(nWaypointNumber));
          List<GameObject> arObjects = GetObjectsInShape(EngineConstants.OBJECT_TYPE_CREATURE, EngineConstants.SHAPE_CONE, GetLocation(oFace), 80.0f, 30.0f, 0.0f, EngineConstants.TRUE);
          int i, size = GetArraySize(arObjects);
          int nNumTargets = 0;
          int nProjectile = 0;
          int nTargetFilter = 0;
          xEvent evPrj;
          GameObject oPrj;
          List<GameObject> arTargets = new List<GameObject>();

          switch (nPhase)
          {
               case EngineConstants.ORZ_APPARATUS_PHASE_1:
                    //nProjectile = EngineConstants.ORZ_APPARATUS_PRJ_PHASE_1; CUT -- too confusing for start of fight
                    nTargetFilter = 3;
                    break;

               case EngineConstants.ORZ_APPARATUS_PHASE_2:
                    nProjectile = EngineConstants.ORZ_APPARATUS_PRJ_PHASE_2;
                    nTargetFilter = 2;
                    break;

               case EngineConstants.ORZ_APPARATUS_PHASE_3:
                    nProjectile = EngineConstants.ORZ_APPARATUS_PRJ_PHASE_3;
                    nTargetFilter = 1;
                    break;
          }

          if (nProjectile > 0)
          {
               int nObjectHostility;
               for (i = 0; i < size; i++)
               {
                    nObjectHostility = IsObjectHostile(oFace, arObjects[i]) != EngineConstants.FALSE ? 1 : 2;
                    if (nObjectHostility == nTargetFilter)
                         arTargets[nNumTargets++] = arObjects[i];
               }

               size = GetArraySize(arTargets);
               for (i = 0; i < size; i++)
               {
                    oPrj = FireHomingProjectile(nProjectile, vSource, arTargets[i], 0, gameObject);
                    evPrj = Event(EngineConstants.EVENT_TYPE_ATTACK_IMPACT);
                    SetEventCreatorRef(ref evPrj, oFace);
                    SetEventObjectRef(ref evPrj, 0, arTargets[i]);
                    SetEventIntegerRef(ref evPrj, 0, nProjectile);
                    SetProjectileImpactEvent(oPrj, evPrj);
               }
          }

     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventEncounterAttackImpact(xEvent evEvent)
     {
          int nPrj = GetEventIntegerRef(ref evEvent, 0);
          GameObject oTarget = GetEventObjectRef(ref evEvent, 0);
          GameObject oSource = GetEventCreatorRef(ref evEvent);
          string evResource = GetEventResourceRef(ref evEvent, 0);
          float fDiffMod = GetGameDifficulty() * 0.5f + 0.5f;

          switch (nPrj)
          {
               case EngineConstants.ORZ_APPARATUS_PRJ_PHASE_1:
                    {
                         if (CheckSpellResistance(oSource, oTarget, EngineConstants.ABILITY_SPELL_LIGHTNING) == EngineConstants.FALSE)
                         {
                              DamageCreature(oTarget, oSource, (fDiffMod * 0.02f * GetMaxHealth(oTarget)), EngineConstants.DAMAGE_TYPE_ELECTRICITY);
                              ApplyEffectVisualEffect(oSource, oTarget, 1005, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         }
                         else
                              UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_RESISTED);
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_PRJ_PHASE_2:
                    {
                         HealCreature(oTarget, EngineConstants.FALSE, (fDiffMod * 0.075f * GetMaxHealth(oTarget)), EngineConstants.FALSE);
                         ApplyEffectVisualEffect(oSource, oTarget, 1021, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_PRJ_PHASE_3:
                    {
                         if (CheckSpellResistance(oSource, oTarget, EngineConstants.ABILITY_SPELL_BLOOD_WOUND) == EngineConstants.FALSE)
                         {
                              DamageCreature(oTarget, oSource, (fDiffMod * 0.05f * GetMaxHealth(oTarget)), EngineConstants.DAMAGE_TYPE_SPIRIT);
                              ApplyEffectVisualEffect(oSource, oTarget, 6, EngineConstants.EFFECT_DURATION_TYPE_INSTANT, 0.0f);
                         }
                         else
                              UI_DisplayMessage(oTarget, EngineConstants.UI_MESSAGE_RESISTED);
                         break;
                    }
               case EngineConstants.ORZ_APPARATUS_PRJ_SUMMON:
                    {
                         if (evResource != EngineConstants.INVALID_RESOURCE)
                         {
                              // grab spirit from creature pool
                              GameObject oSpirit = CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, evResource, GetLocation(oTarget));

                              // Initialize the summon, there is a lot of work to do here:
                              RemoveEffectsDueToPlotEvent(oSpirit);
                              HealCreature(oSpirit, EngineConstants.FALSE, 9999.99f, EngineConstants.TRUE);

                              // Activate Creature, get him ready for combat
                              WR_SetObjectActive(oSpirit, EngineConstants.TRUE);
                              WR_ClearAllCommands(oSpirit, EngineConstants.TRUE);
                              UT_CombatStart(oSpirit, GetHero());

                              // These need to be re-set every time the creature shows up
                              SetTeamId(oSpirit, EngineConstants.ORZ_TEAM_AOTV_TRAP_3_SPIRITS);
                              SetLocalInt(oSpirit, EngineConstants.CREATURE_COUNTER_2, GetEventIntegerRef(ref evEvent, 1)); // anvil #
                              SetLocalInt(oSpirit, EngineConstants.AI_FLAG_STATIONARY, EngineConstants.AI_STATIONARY_STATE_SOFT);
                              SetImmortal(oSpirit, EngineConstants.TRUE);
                              ClearThreatTable(oSpirit);

                              // reduce the mana/stamina of the creature by a good portion
                              float fManaStamRemoved = GetCurrentManaStamina(oSpirit) * (0.85f - (0.20f * fDiffMod));
                              ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_INSTANT, EffectModifyManaStamina(-fManaStamRemoved), oSpirit);

                              // summon glow VFX, play animation
                              ApplyEffectVisualEffect(oSource, oSpirit, 90132, EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, 2.0f);
                              WR_AddCommand(oSpirit, CommandPlayAnimation(EngineConstants.COMBAT_ANIMATION_ENTER_BERSERK));
                         }
                         break;
                    }
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventSpiritDeath(xEvent evEvent)
     {
          GameObject oSpirit = gameObject;
          int nWaypointNumber = GetLocalInt(oSpirit, EngineConstants.CREATURE_COUNTER_2);
          GameObject oHome = GetObjectByTag(EngineConstants.ORZ_APPARATUS_SPIRIT_WP_PREFIX + ToString(nWaypointNumber));
          GameObject oAnvil = UT_GetNearestObjectByTag(oHome, EngineConstants.ORZ_APPARATUS_IP_ANVIL);

          // joshua(04/21/09): no sucking in spirits unless one is killed  
          SetLocalInt(GetObjectByTag(EngineConstants.ORZ_APPARATUS_BODY), EngineConstants.PLC_COUNTER_3, EngineConstants.TRUE);

          // dissipate, streaming vfx to the proper anvil
          _Apparatus_SpiritDissipate(oSpirit, oAnvil);

          // big giant VFX because the spirit died
          Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY,
              EffectVisualEffect(EngineConstants.ORZ_APPARATUS_VFX_SPIRIT_EXPLODE), GetLocation(oSpirit), 3.0f, oSpirit);

          // The appropriate anvil is now active : Spawn effects on anvil
          if (WR_GetPlotFlag(EngineConstants.PLT_ORZ540PT_ANVIL_OT_VOID, EngineConstants.ORZ_AOTV__EVENT_TRAP_3_SOLVED) == EngineConstants.FALSE)
          {
               // activate the anvil
               _Apparatus_ActivateAnvil(oAnvil);

               // add soundset trigger for if player has not used an anvil yet
               List<GameObject> arFaces = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);
               int i, size = GetArraySize(arFaces);
               int nAnvilUsed = 0;
               for (i = 0; i < size; i++)
                    nAnvilUsed += GetLocalInt(arFaces[i], EngineConstants.CREATURE_COUNTER_2);
               if (nAnvilUsed <= 2)
                    PlaySoundSet(GetEventCreatorRef(ref evEvent), EngineConstants.SS_EXPLORE_LOOK_HERE, 1.0f);
               else
                    PlaySoundSet(GetEventCreatorRef(ref evEvent), EngineConstants.SS_COMBAT_ENEMY_KILLED, 0.5f);
          }
     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventAnvilUse(xEvent evEvent)
     {
          GameObject oAnvil = gameObject;
          GameObject oApparatus = GetObjectByTag(EngineConstants.ORZ_APPARATUS_BODY);
          int nControllerNum = GetLocalInt(oAnvil, EngineConstants.PLC_COUNTER_1);
          int nRotationState = GetLocalInt(oApparatus, EngineConstants.PLC_COUNTER_2);
          int nFaceToAttack = (((nControllerNum + 3) - nRotationState) % 4) + 1;
          GameObject oFace = GetObjectByTag(EngineConstants.ORZ_APPARATUS_FACE_PREFIX + ToString(nFaceToAttack));
          int nFacePhase = GetLocalInt(oFace, EngineConstants.CREATURE_COUNTER_2);
          Vector3 vTarget = _Apparatus_GetFacePosition(oFace, EngineConstants.TRUE);
          xEvent evPrj;
          GameObject oPrj;

          // fire a projectile into the proper face
          oPrj = FireProjectile(EngineConstants.ORZ_APPARATUS_PRJ_SUMMON, GetPosition(oAnvil), vTarget, 0, EngineConstants.TRUE, oAnvil);
          evPrj = Event(EngineConstants.EVENT_TYPE_ATTACK_IMPACT);
          SetEventCreatorRef(ref evPrj, GetEventCreatorRef(ref evEvent));
          SetEventObjectRef(ref evPrj, 0, oFace);
          SetEventVectorRef(ref evPrj, 0, Location(GetArea(oAnvil), vTarget, 0.0f));
          SetEventIntegerRef(ref evPrj, 0, nRotationState);
          SetProjectileImpactEvent(oPrj, evPrj);

          // play soundset, deactivate anvil
          PlaySoundSet(GetEventCreatorRef(ref evEvent), EngineConstants.SS_TASK_COMPLETE, 1.0f);
          _Apparatus_DeactivateAnvil(oAnvil);
     }

     //------------------------------------------------------------------------------

     public void ORZ_Apparatus_HandleEventAnvilImpact(xEvent evEvent)
     {
          int nRotationState = GetEventIntegerRef(ref evEvent, 0);
          Vector3 evLocation = GetEventVectorRef(ref evEvent, 0);
          GameObject oTarget = GetEventObjectRef(ref evEvent, 0);
          GameObject oApparatus = GetObjectByTag(EngineConstants.ORZ_APPARATUS_BODY);
          List<GameObject> arFaces = UT_GetTeam(EngineConstants.ORZ_TEAM_AOTV_TRAP_3);
          int nFacePhase = GetLocalInt(oTarget, EngineConstants.CREATURE_COUNTER_2);
          int nLastPhase, count, size, i;

          if (nFacePhase < EngineConstants.ORZ_APPARATUS_PHASE_4 && IsObjectValid(oTarget) != EngineConstants.FALSE)
          {
               // show damage feedback
               _Apparatus_DamageFloaty(oTarget, 500);

               // Nightmare mode gets to see phase 2
               if (GetGameDifficulty() <= EngineConstants.GAME_DIFFICULTY_HARD && nFacePhase == EngineConstants.ORZ_APPARATUS_PHASE_1)
                    nFacePhase++;

               // go to next state
               SetLocalInt(oTarget, EngineConstants.CREATURE_COUNTER_2, (++nFacePhase));
               _Apparatus_Phase_Init(oTarget, nRotationState);

               // apply vfx
               Vector3 vPos = _Apparatus_GetFacePosition(oTarget);
               evLocation = SetLocationPosition(evLocation, vPos);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT,
                   EffectVisualEffect(EngineConstants.ORZ_APPARATUS_VFX_ANVIL_IMPACT_1), evLocation);
               Engine_ApplyEffectAtLocation(EngineConstants.EFFECT_DURATION_TYPE_INSTANT,
                   EffectVisualEffect(EngineConstants.ORZ_APPARATUS_VFX_ANVIL_IMPACT_2), evLocation);
               ApplyEffectOnParty(EngineConstants.EFFECT_DURATION_TYPE_TEMPORARY, EffectScreenShake(4), 2.0f);

               // play supporting soundsets
               PlaySoundSet(oTarget, EngineConstants.SS_COMBAT_TAUNT, 1.0f);
               PlaySoundSet(GetEventCreatorRef(ref evEvent), EngineConstants.SS_COMBAT_ENEMY_KILLED, 0.5f);

               // check for encounter defeated
               count = 0;
               size = GetArraySize(arFaces);
               for (i = 0; i < size; i++)
                    if (GetLocalInt(arFaces[i], EngineConstants.CREATURE_COUNTER_2) >= EngineConstants.ORZ_APPARATUS_PHASE_4)
                         count++;
               if (count >= EngineConstants.ORZ_APPARATUS_NUM_FACES)
                    SignalEvent(oApparatus, Event(EngineConstants.ORZ_APPARATUS_EVENT_DEFEATED));
          }
          else
          {
               _Apparatus_DamageFloaty(oTarget, 0);
          }
     }

     //------------------------------------------------------------------------------

     public Vector3 _Apparatus_GetFacePosition(GameObject oFace, int bHigh = EngineConstants.FALSE)
     {
          float fFacing = (GetFacing(oFace) * EngineConstants.PI) / 180.0f;
          Vector3 vPos = GetPosition(oFace);

          vPos.x += -sin(fFacing) * 1.25f;
          vPos.y += -cos(fFacing) * 1.25f;
          vPos.z += 1.45f - (bHigh != EngineConstants.FALSE ? 0.95f : 0.0f);

          return vPos;
     }

     public void _Apparatus_ActivateAnvil(GameObject oAnvil)
     {
          _Apparatus_DeactivateAnvil(oAnvil);
          ApplyEffectVisualEffect(oAnvil, oAnvil, 90132, EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, 0.0f);
          ApplyEffectOnObject(EngineConstants.EFFECT_DURATION_TYPE_PERMANENT, EffectVisualEffect(5033), oAnvil, 0.0f, oAnvil);
          SetObjectInteractive(oAnvil, EngineConstants.TRUE);
     }

     public void _Apparatus_DeactivateAnvil(GameObject oAnvil)
     {
          RemoveEffectsByParameters(oAnvil, EngineConstants.EFFECT_TYPE_INVALID, EngineConstants.ABILITY_INVALID, oAnvil);
          SetObjectInteractive(oAnvil, EngineConstants.FALSE);
     }

     public void _Apparatus_DamageFloaty(GameObject oTarget, int nDamage)
     {
          // hack for displaying damage properly
          UI_DisplayDamageFloaty(oTarget, GetHero(), nDamage, EngineConstants.DAMAGE_TYPE_SPIRIT, 1);
     }
}