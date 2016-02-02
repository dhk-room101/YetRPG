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

         Prelude
          -> Barricade Fire

         This is a self contained event, controlling the fire on the barricades
         in the first floor of the signal tower

         Calling PRE_BarricadeFire_Setup() does everything

     */
     //------------------------------------------------------------------------------
     // Created By: Joshua Stiksma
     // Created On: September 18, 2007
     //==============================================================================

     //#include"utility_h"
     //#include"events_h"
     //#include"pre_objects_h"

     //------------------------------------------------------------------------------

     //moved public const int       PRE_EVENT_BARRICADE_FIRE_START      = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_01;
     //moved public const int       PRE_EVENT_BARRICADE_FIRE_COMPLETE   = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_02;
     //moved public const int       PRE_EVENT_BARRICADE_FIRE_ATTACK     = EngineConstants.EVENT_TYPE_CUSTOM_EVENT_03;

     //moved public const int       PRE_BARRICADE_FIRE_EFFECT           = PRE_VFX_BARRICADE;
     //moved public const int       PRE_BARRICADE_FIRE_MAX_NUM_PER_LOOP = 3;
     //moved public const int       PRE_BARRICADE_FIRE_MAX_LOOPS        = 30;

     //moved public const float     PRE_BARRICADE_FIRE_START_DELAY      = 0.3f; // Time between flame bursts

     //------------------------------------------------------------------------------

     //moved public const string     PRE_WP_BARRICADE_FIRE_PREFIX       = PRE_WP_TOWER_1_BARRICADE_FIRE;
     //moved public const string     PRE_WP_BARRICADE_FIRE_SOUND_PREFIX = PRE_WP_TOWER_1_FIRE_SOUND;
     //moved public const string     PRE_WP_FIREBALL_TARGET             = "pre410wp_fireball";
     //moved public const string     PRE_WP_EMISSARY_RETREAT            = "pre410wp_emissary_retreat";
     //moved public const string     PRE_CR_OWNER_CREATURE              = "pre410cr_genlock_emissary";

     //------------------------------------------------------------------------------

     public void PRE_BarricadeFire_Setup()
     {

          xEvent evFire;
          xEvent evAttack;
          GameObject oCreature;

          //------------------------------------------------------------------

          Log_Trace(EngineConstants.LOG_CHANNEL_TEMP, "pre410_barricade_fire_h.nss:PRE_BarricadeFire_Setup()", "", gameObject);

          //------------------------------------------------------------------

          evFire = Event(EngineConstants.PRE_EVENT_BARRICADE_FIRE_START);
          SetEventIntegerRef(ref evFire, 0, 0);
          evAttack = Event(EngineConstants.PRE_EVENT_BARRICADE_FIRE_ATTACK);
          oCreature = UT_GetNearestCreatureByTag(gameObject, EngineConstants.PRE_CR_OWNER_CREATURE);

          //DelayEvent(0.0f, oCreature, evAttack);
          //DelayEvent(3.0f, oCreature, evFire);
          SignalEvent(oCreature, evAttack);
          SignalEvent(oCreature, evFire);

     }
}