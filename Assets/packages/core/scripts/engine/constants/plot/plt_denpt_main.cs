//ready
#pragma warning disable 0162
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414
#define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class EngineConstants
{
     // Plot defines

     // The Landsmeet
     public const string PLT_DENPT_MAIN = "841A4E6E0CDD43D3BA3BA484D9A2771F";
     public const int LANDSMEET_QUEST_DONE = 0;
     public const int LANDSMEET_ALISTAIR_IS_KING = 1;
     public const int LANDSMEET_PLOT_OPENED = 2;
     public const int LANDSMEET_ALISTAIR_ENGAGED_TO_ANORA = 3;
     public const int LANDSMEET_ANORA_IS_QUEEN = 4;
     public const int LANDSMEET_PLAYER_IS_KING = 5;
     public const int LANDSMEET_LOGHAIN_KILLED = 6;
     public const int LANDSMEET_OPENING_DONE = 7;
     public const int LANDSMEET_LOGHAIN_LIVES = 8;
     public const int LANDSMEET_EAMON_GOES_WITH_OR_WITHOUT_ALISTAIR = 9;
     public const int LANDSMEET_PLAYER_GOES = 11;
     public const int LANDSMEET_LOGHAIN_DEFEATED = 12;
     public const int LANDSMEET_PC_FIGHTS_LOGHAIN = 13;
     public const int LANDSMEET_ALISTAIR_SUGGESTS_EXECUTION = 14;
     public const int LANDSMEET_ANORA_QUEEN_LOGHAIN_LIVES = 15;
     public const int LANDSMEET_ALISTAIR_LEAVES_FOREVER = 16;
     public const int LANDSMEET_ALISTAIR_KILLED = 17;
     public const int LANDSMEET_ALISTAIR_AND_ANORA_ENGAGED_LOGHAIN_LIVES = 18;
     public const int LANDSMEET_PC_SUGGESTS_EXECUTION = 19;
     public const int LANDSMEET_PC_EXECUTES_LOGHAIN = 20;
     public const int LANDSMEET_LOST = 21;
     public const int LANDSMEET_ALISTAIR_EXECUTES_LOGHAIN = 22;
     public const int LANDSMEET_PC_DOES_NOT_CHALLENGE = 23;
     public const int LANDSMEET_ALISTAIR_FIGHTS_LOGHAIN = 24;
     public const int LANDSMEET_SHALE_FIGHTS_LOGHAIN = 25;
     public const int LANDSMEET_STEN_FIGHTS_LOGHAIN = 26;
     public const int LANDSMEET_OGHREN_FIGHTS_LOGHAIN = 27;
     public const int LANDSMEET_MORRIGAN_FIGHTS_LOGHAIN = 28;
     public const int LANDSMEET_WYNNE_FIGHTS_LOGHAIN = 29;
     public const int LANDSMEET_LELIANA_FIGHTS_LOGHAIN = 30;
     public const int LANDSMEET_ZEVRAN_FIGHTS_LOGHAIN = 31;
     public const int LANDSMEET_PC_CHALLENGES_ANORA = 32;
     public const int LANDSMEET_BIG_FIGHT = 33;
     public const int LANDSMEET_BEGINS = 34;
     public const int LANDSMEET_PLAYER_ARGUMENT_PLUS_1 = 35;
     public const int LANDSMEET_PLAYER_ARGUMENT_PLUS_2 = 36;
     public const int LANDSMEET_PLAYER_ARGUMENT_MINUS_1 = 37;
     public const int LANDSMEET_PLAYER_ARGUMENT_MINUS_2 = 38;
     public const int LANDSMEET_PLAYER_ARGUMENT_MINUS_3 = 39;
     public const int LANDSMEET_WON = 40;
     public const int LANDSMEET_PLAYER_ARGUMENT_PLUS_3 = 41;
     public const int LANDSMEET_PC_CHALLENGES_LOGHAIN = 42;
     public const int LANDSMEET_ALISTAIR_KING_NOT_ANORA_QUEEN_NOT_LOGHAIN_LIVES = 43;
     public const int LANDSMEET_ANORA_IMPRISONED = 44;
     public const int LANDSMEET_LEAVING_WITH_LOGHAIN = 45;
     public const int LANDSMEET_JUMP_TO_EAMONS_ESTATE = 46;
     public const int LANDSMEET_CAUTHRIEN_SPOKE_ABOUT_MARICS_SHIELD = 47;
     public const int LANDSMEET_CAUTHRIEN_ATTACKS = 48;
     public const int LANDSMEET_CAUTHRIEN_CONVINCED_PLUS_3 = 49;
     public const int LANDSMEET_CAUTHRIEN_CONVINCED_PLUS_1 = 50;
     public const int LANDSMEET_CAUTHRIEN_BETRAYS = 51;
     public const int LANDSMEET_CAUTHRIEN_CONVINCED_PLUS_2 = 52;
     public const int LANDSMEET_BEGINS_CUTSCENE_END = 53;
     public const int LANDSMEET_DUEL = 54;
     public const int LANDSMEET_DUEL_CUTSCENE_END = 55;
     public const int LANDSMEET_ALISTAIR_ENGAGED_TO_PLAYER = 56;
     public const int LANDSMEET_PLAYER_AMBUSHED_BY_CROWS = 58;
     public const int LANDSMEET_CAUTHRIEN_KILLED = 59;
     public const int LANDSMEET_WRAP_UP = 61;
     public const int LANDSMEET_BIG_FIGHT_OVER = 62;
     public const int LANDSMEET_PC_MENTIONED_HOWE = 64;
     public const int LANDSMEET_PC_MENTIONED_BLIGHT = 65;
     public const int LANDSMEET_PC_MENTIONED_KILLING_CAILAN = 66;
     public const int LANDSMEET_PC_MENTIONED_SLAVE_TRADE = 67;
     public const int LANDSMEET_PERSUADED_WULFF = 68;
     public const int LANDSMEET_PERSUADED_ALFSTANNA = 69;
     public const int LANDSMEET_PERSUADED_SIGHARD = 70;
     public const int LANDSMEET_ALISTAIR_LEAVES_PARTY_AFTER_LANDSMEET_ALISTAIR_KING = 71;
     public const int LANDSMEET_EVIDENCE_FOUND = 256;
     public const int LANDSMEET_QUESTS_OPEN = 257;
     public const int LANDSMEET_FIND_QUEEN_DONE_NO_EVIDENCE = 258;
     public const int LANDSMEET_LOST_WITHOUT_ANORA_SUPPORT = 259;
     public const int LANDSMEET_PLAYER_ARGUMENT_GREATER_THAN_5 = 260;
     public const int LANDSMEET_ANORA_ON_THRONE = 261;
     public const int LANDSMEET_READY_PLAYER_HAS_NOT_GONE = 262;
     public const int LANDSMEET_CAUTHRIEN_CONVINCED_5_PLUS = 263;
     public const int LANDSMEET_DONE_GRAND_CLERIC_COMPLETED = 264;
     public const int LANDSMEET_ALISTAIR_ON_THRONE = 265;
     public const int LANDSMEET_SIGHARD_SIDES_WITH_PLAYER = 266;
     public const int LANDSMEET_ALFSTANNA_SIDES_WITH_PLAYER = 267;
     public const int LANDSMEET__ALISTAIR_ANORA_ENGAGED_LOGHAIN_ALIVE = 268;
     public const int LANDSMEET_ALISTAIR_SPEAKS_AFTER_LANDSMEET = 269;
     public const int LANDSMEET_ANORA_QUEEN_OR_PLAYER_KING = 270;
}