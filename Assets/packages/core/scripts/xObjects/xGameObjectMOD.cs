#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class xGameObjectMOD : MonoBehaviour
{
    #region Variables initialized from MOD a.k.a. Module
    public int LOG_ACTIVE { get; set; }
    public int LOG_RULES_CURRENT_LEVEL { get; set; }
    public int LOG_AI_CURRENT_LEVEL { get; set; }
    public int LOG_PLOT_CURRENT_LEVEL { get; set; }
    public int LOG_SYSTEMS_CURRENT_LEVEL { get; set; }
    public int APP_RANGE_VALUE_CRISIS { get; set; }
    public int APP_RANGE_VALUE_HOSTILE { get; set; }
    public int APP_RANGE_VALUE_NEUTRAL { get; set; }
    public int APP_RANGE_VALUE_WARM { get; set; }
    public int APP_RANGE_VALUE_FRIENDLY { get; set; }
    public int APP_ROMANCE_RANGE_VALUE_INTERESTED { get; set; }
    public int APP_ROMANCE_RANGE_VALUE_CARE { get; set; }
    public int APP_ROMANCE_RANGE_VALUE_ADORE { get; set; }
    public int APP_ROMANCE_RANGE_VALUE_LOVE { get; set; }
    public int MODULE_COUNTER_1 { get; set; }
    public int MODULE_COUNTER_2 { get; set; }
    public int MODULE_COUNTER_3 { get; set; }
    public int HANDLE_EVENT_RETURN { get; set; }
    public int COUNTER_MAIN_PLOTS_DONE { get; set; }
    public int DEMO_ACTIVE { get; set; }
    public int NTB_CLAN_ATTITUDE_COUNTER { get; set; }
    public int NTB_PC_CAMP_RESISTANCE { get; set; }
    public int ALISTAIR_FRIEND_TRACK { get; set; }
    public int PARTY_BANTER_ROTATION_COUNTER { get; set; }
    public GameObject PARTY_BANTER_ARRAY_SLOT_1 { get; set; }
    public GameObject PARTY_BANTER_ARRAY_SLOT_2 { get; set; }
    public GameObject PARTY_BANTER_ARRAY_SLOT_3 { get; set; }
    public string PARTY_BANTER_CONVERSATION_FILE { get; set; }
    public int AMB_SYSTEM_CONVERSATION { get; set; }
    public string PARTY_TRIGGER_CONVERSATION_FILE { get; set; }
    public string PARTY_TRIGGER_PLOT { get; set; }
    public int NRD_DIVINE_COIN_COUNT { get; set; }
    public int WRD_MERCHANT_LOSS_CRATES_FOUND { get; set; }
    public int WRD_MERCHANT_LOSS_CRATES_DELIVERED { get; set; }
    public int WRD_DESERTERS_SENT_TO_ROSSLEIGH { get; set; }
    public int WRD_JACOBSON_FIRST_BATTLE_LOSSES { get; set; }
    public int WRD_MISSING_MINERS_FOUND { get; set; }
    public int WRD_MISSING_MINERS_REWARDED { get; set; }
    public int ORZ_SUPPORT_BHELEN { get; set; }
    public int ORZ_SUPPORT_HARROWMONT { get; set; }
    public string ORZ_OGHREN_DEEPROADS_TRACKER { get; set; }
    public string RUNSCRIPT_VAR { get; set; }
    public int PROVING_FIGHT_ID { get; set; }
    public int APP_APPROVAL_RATE_ALISTAIR { get; set; }
    public int APP_APPROVAL_RATE_DOG { get; set; }
    public int APP_APPROVAL_RATE_MORRIGAN { get; set; }
    public int APP_APPROVAL_RATE_WYNNE { get; set; }
    public int APP_APPROVAL_RATE_SHALE { get; set; }
    public int APP_APPROVAL_RATE_STEN { get; set; }
    public int APP_APPROVAL_RATE_ZEVRAN { get; set; }
    public int APP_APPROVAL_RATE_OGHREN { get; set; }
    public int APP_APPROVAL_RATE_LELIANA { get; set; }
    public int APP_APPROVAL_RATE_LOGHAIN { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_ALISTAIR { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_DOG { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_MORRIGAN { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_WYNNE { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_SHALE { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_STEN { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_ZEVRAN { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_OGHREN { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_LELIANA { get; set; }
    public int APP_APPROVAL_GIFT_COUNT_LOGHAIN { get; set; }
    public int ORZ_ARENA_CURRENT_SOLO_MATCHES { get; set; }
    public int ORZ_ARENA_CURRENT_GROUP_MATCHES { get; set; }
    public int ORZ_ARENA_CURRENT_GRAND_MELEE_MATCHES { get; set; }
    public int ORZ_ARENA_CURRENT_CHALLENGE_MATCHES { get; set; }
    public int ORZ_ARENA_CURRENT_BOAST_MATCHES { get; set; }
    public int ORZ_ARENA_CURRENT_DEEP_WALK_MATCHES { get; set; }
    public int ORZ_ARENA_TOTAL_VICTORIES { get; set; }
    public int ORZ_ARENA_TOTAL_DEFEATS { get; set; }
    public int ORZ_ARENA_TOTAL_KILLS { get; set; }
    public int ORZ_CURRENT_TRIAL_EVENT { get; set; }
    public int TRACKING_GAME_ID { get; set; }
    public int TRACKING_SEQ_NO { get; set; }
    public int RAND_DENERIM_ENCOUNTERS_SET { get; set; }
    public string PARTY_OVERRIDE_CONVERSATION { get; set; }
    public int AI_DISABLE_TABLES { get; set; }
    public int NRD_BANDIT_BELT_COUNT { get; set; }
    public int CHARGEN_MODE { get; set; }
    public int RAND_MOUNTAIN_ENCOUNTERS_SET { get; set; }
    public int RAND_FOREST_ENCOUNTERS_SET { get; set; }
    public int RAND_UNDERGROUND_ENCOUNTERS_SET { get; set; }
    public int RAND_PLAINS_ENCOUNTERS_SET { get; set; }
    public int RAND_HIGHWAY_ENCOUNTERS_SET { get; set; }
    public string WM_STORED_AREA { get; set; }
    public string WM_STORED_WP { get; set; }
    public int DEBUG_SKILL_CHECK_OVERRIDE { get; set; }
    public int WORLD_MAP_TRIPS_COUNT { get; set; }
    public GameObject PARTY_STORE_SLOT_1 { get; set; }
    public GameObject PARTY_STORE_SLOT_2 { get; set; }
    public GameObject PARTY_STORE_SLOT_3 { get; set; }
    public int CIR_FADE_FOLLOWER_1 { get; set; }
    public int CIR_FADE_FOLLOWER_2 { get; set; }
    public int CIR_FADE_FOLLOWER_3 { get; set; }
    public int CLI_ALISTAIR_AGREE_LEVEL { get; set; }
    public int AI_USE_GUI_TABLES_FOR_FOLLOWERS { get; set; }
    public int MODULE_WORLD_MAP_ENABLED { get; set; }
    public int TUTORIAL_ENABLED { get; set; }
    public int PARTY_PICKER_GUI_ALLOWED_TO_POP_UP { get; set; }
    public int CLIMAX_ARMY_CURRENT_AREA_BUFFER_SIZE { get; set; }
    public GameObject PARTY_LEADER_STORE { get; set; }
    public int STEALING_ORZ_COUNTER { get; set; }
    public string CUTSCENE_SET_PLOT { get; set; }
    public int CUTSCENE_SET_PLOT_FLAG { get; set; }
    public string CUTSCENE_TALK_SPEAKER { get; set; }
    public int AI_PARTY_CLEAR_TO_ATTACK { get; set; }
    public int NTB_ARCANE_HORROR_POSITION { get; set; }
    public int DISABLE_WORLD_MAP_ENCOUNTER { get; set; }
    public int STEALING_CIR_COUNTER { get; set; }
    public int DOG_WARLIKE_COUNTER { get; set; }
    public int PARTY_OVERRIDE_CONVERSATION_ACTIVE { get; set; }
    public int DEBUG_ENABLE_PARTY_ITEM_SCALING { get; set; }
    public int ABILITY_ALLY_NUMBER { get; set; }
    public int NTB_ARCANE_HORROR_NEXT_POSITION { get; set; }
    public int DEATH_HINT { get; set; }
    public int AREA_LOAD_HINT { get; set; }
    public GameObject WORLD_MAP_CURRENT_MAP { get; set; }
    public GameObject WORLD_MAP_CURRENT_LOCATION { get; set; }
    public int DISABLE_FOLLOWER_CONVERSATION { get; set; }
    public string WORLD_MAP_STORED_PRE_CAMP_AREA { get; set; }
    public int TUTORIAL_HAVE_SEEN_LEVEL_UP { get; set; }
    public int DISABLE_APPEARANCE_LEVEL_LIMITS { get; set; }

    //DHK
    public int GLOBAL_MAX_TIME_BEFORE_DECAY { get; set; }
    public int GAME_MODE { get; set; }
    public string CONVERSATION { get; set; }//The resource conversation Name to be played When the conversation mode kicks in
    public int CONVERSATION_IN_PROGRESS { get; set; }
    #endregion
    Engine engine { get; set; }
    xGameObjectBase oBase { get; set; }

    public static xGameObjectMOD instance = null;
    public GameObject oHero { get; set; }

    //Active party is extrapolated based on FOLLOWER_STATE_ACTIVE
    public List<GameObject> oPartyPool { get; set; }

    public List<xGameObjectPLO> oPlots { get; set; }

    string Script = "demo_module";//Starting script
    public string tArea { get; set; }//Transition to area
    public string tWaypoint { get; set; }//Transition to waypoint

    //Temp debug player reference template
    public string player = "demo000cr_player";

    int dCounter;//This is the deleting counter
    int counter;

    //GameObject oPlayer;
    //List<GameObject> oCharacters;

    // Use this for initialization
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        this.name = "GameModule";
        //DontDestroyOnLoad(gameObject);

        engine = xGameObjectMOD.instance.GetComponent<Engine>();
        oBase = gameObject.GetComponent<xGameObjectBase>();
        
        //Deleting previous temp files if any
        engine.DeleteTemp();

        //Set GameObject type module
        oBase.nObjectType = EngineConstants.OBJECT_TYPE_MODULE;

        //Damn strings also need to be initialized C# 5-
        //if (PARTY_OVERRIDE_CONVERSATION == null) PARTY_OVERRIDE_CONVERSATION = string.Empty;
        engine.SetLocalString(engine.GetModule(), "PARTY_OVERRIDE_CONVERSATION", string.Empty);

        if (oPartyPool == null) oPartyPool = new List<GameObject>();
        if (oPlots == null) oPlots = new List<xGameObjectPLO>();

        //Initialize the camera in the scene
        HandleCamera();

        //Initialize the module in start mode, as opposed to load mode
        InitializeModule("START");//Or LOAD

        //oCharacters = new List<GameObject>();

        /* @brief use the following function for a game GameObject area
        to be used for script running in the current level*/
        //HandleArea();
        //HandleCamera();
        //HandlePlayer();
        //HandleEnemy();
        //HandleFollower();
        //HandleConsole();
        //HandleLogging();
        //HandleScripts();
        //HandlePlayerPrefs();
        //HandleXML();
    }

    // Update is called once per frame
    void Update()
    {
        dCounter++;
        if (1000 - dCounter < 0)//Deleting Temp unnecessary files once in a while
        {
            dCounter = 0;
            engine.DeleteTemp();
        }

        //Check for important things, such as game state changes
        counter++;
        if (10 - counter < 0)
        {
            counter = 0;
            #region GAME MODE

            #region CONVERSATION
            if (GAME_MODE == EngineConstants.GM_CONVERSATION && CONVERSATION_IN_PROGRESS == EngineConstants.FALSE) 
            {
                if (CONVERSATION != null || CONVERSATION != string.Empty)
                {
                    //Time to start the conversation
                    CONVERSATION_IN_PROGRESS = EngineConstants.TRUE;
                    engine.Engine_Conversation();
                }
                else
                {
                    //WTF, I'm supposed to start the conversation that is empty?
                    throw new NotImplementedException();
                }
            }
            #endregion
            #region COMBAT
            else if (GAME_MODE == EngineConstants.GM_COMBAT)
            {
                //Time to start the conversation
                Console.WriteLine();
            }
            #endregion

            #endregion
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            /*GameObject _player = GameObject.Find("demo000cr_player");
            List<xProperty> _p = _player.GetComponent<xGameObjectUTC>().oProperties;
            float _h = _p.Find(x => x.nID == EngineConstants.PROPERTY_DEPLETABLE_HEALTH).fValueTypeBase;
            float _d = _p.Find(x => x.nID == EngineConstants.PROPERTY_ATTRIBUTE_DEXTERITY).fValueTypeBase;
            float _s = _p.Find(x => x.nID == EngineConstants.PROPERTY_ATTRIBUTE_STRENGTH).fValueTypeBase;
            gameObject.GetComponent< Engine>().DisplayFloatyMessage(_player,_player.name,0, 52224);*/

            /*GameObject _creature = GameObject.Find("demo100cr_barkeep");
            var c = _creature.GetComponent<xGameObjectUTC>().ConversationURI;
            engine.DisplayFloatyMessage(_creature, _creature.name, 0, 52224, 2);*/
        }
        //Check to see what was clicked on
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;

            if (Physics.Raycast(ray.origin, ray.direction, out rch, Mathf.Infinity))
            {
                var p = rch.transform.parent;
                if (p != null)
                {
                    GameObject pat = p.gameObject;
                    //TO DO world map transition
                    //if (pat != null && pat.tag == "WorldMap")

                    //Area transition
                    #region area transition
                    if (pat != null && pat.tag == "AreaTransition")
                    {
                        string sArea = pat.GetComponent<xGameObjectUTP>().PLC_AT_DEST_AREA_TAG;
                        string sWP = pat.GetComponent<xGameObjectUTP>().PLC_AT_DEST_TAG;
                        if (sArea != "" && sWP != "")
                        {
                            //engine.Warning(w.name + " was clicked");
                            GameObject _player = GameObject.Find("demo000cr_player");

                            //Set placeable action as area transition
                            engine.UpdateProperty(pat, "PLC_ACTION", EngineConstants.PLACEABLE_ACTION_AREA_TRANSITION.ToString());

                            xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_USE);
                            engine.SetEventCreatorRef(ref ev, _player);
                            engine.SignalEvent(pat, ev);
                        }
                    }
                    #endregion

                    //Conversation
                    #region conversation
                    if (pat != null && pat.tag == "Creature")
                    {
                        //If not in combat And has conversation
                        if (engine.GetLocalInt(engine.GetModule(), "GAME_MODE") != EngineConstants.GM_COMBAT &&
                            engine.HasConversation(pat) != EngineConstants.FALSE) 
                        {

                                //engine.UpdateProperty(gameObject, "CONVERSATION", sConversation);

                                //engine.WR_SetGameMode(EngineConstants.GM_CONVERSATION);
                                //For now doing debug only the player can initiate dialogues
                                GameObject _player = GameObject.Find("demo000cr_player");
                                engine.UT_Talk(pat, _player);                            
                        }
                    }
                    #endregion
                }
            }
        }
    }

    void HandleCamera()
    {
        // Find the 'main' camera object.
        GameObject oCamera = GameObject.FindWithTag("MainCamera");

        oCamera.transform.position = new Vector3(0, 10, 0);
        oCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        //DontDestroyOnLoad(oCamera);
    }

    void HandlePlayer()
    {
        GameObject oPlayer = GameObject.Find("oPlayer");
        if (oPlayer == null)
        {
            //oPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/objectPrefab"));
            oPlayer = engine.CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, "", Vector3.zero);
            oPlayer.transform.position = new Vector3(0, 0, 0);
            oPlayer.name = "oPlayer";
            oPlayer.tag = "Player";
        }
        //Debug.Log("Found player object: " + oPlayer);

        //create oCube primitive and assign it as child to player
        GameObject oCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        oCube.name = "oPlayerMesh";
        oCube.GetComponent<Renderer>().material.color = Color.blue;
        oCube.transform.parent = oPlayer.transform;

        //add rigid body
        Rigidbody oPlayerRigidBody = oPlayer.AddComponent<Rigidbody>();
        oPlayerRigidBody.drag = Mathf.Infinity;
        oPlayerRigidBody.angularDrag = Mathf.Infinity;
        oPlayerRigidBody.constraints = RigidbodyConstraints.FreezePositionY |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationZ;

        //Add player movement script
        oPlayer.AddComponent<xPlayerMovement>();
    }

    void HandleEnemy()
    {
        GameObject oEnemy = GameObject.Find("oEnemy");
        if (oEnemy == null)
        {
            //oEnemy = (GameObject)Instantiate(Resources.Load("Prefabs/objectPrefab"), new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            //oEnemy = (GameObject)Instantiate(Resources.Load("Prefabs/objectPrefab"));
            oEnemy = engine.CreateObject(EngineConstants.OBJECT_TYPE_CREATURE, "", Vector3.zero);
            oEnemy.name = "oEnemy";
            oEnemy.tag = "Creature";
        }
        //Debug.Log("Found Enemy object: " + oEnemy);

        //create oCube primitive and assign it as child to Enemy
        GameObject oCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        oCube.GetComponent<Renderer>().material.color = Color.red;
        oCube.transform.parent = oEnemy.transform;

        //add rigid body
        Rigidbody oEnemyRigidBody = oEnemy.AddComponent<Rigidbody>();
        oEnemyRigidBody.drag = Mathf.Infinity;
        oEnemyRigidBody.angularDrag = Mathf.Infinity;
        oEnemyRigidBody.constraints = RigidbodyConstraints.FreezePositionY |
                                       RigidbodyConstraints.FreezeRotationX |
                                       RigidbodyConstraints.FreezeRotationZ;

        //add sphere trigger collider
        SphereCollider oEnemyAggro = oEnemy.AddComponent<SphereCollider>();
        oEnemyAggro.isTrigger = true;
        oEnemyAggro.radius = 5;

        //reposition enemy
        oEnemyRigidBody.MovePosition(new Vector3(10, 0, 0));
        //Debug.Log("enemy tag is: " + oEnemy.tag);//Untagged

        //object type placeholder set static temporarily, the goal is to read an area XML with all the info
        oEnemy.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_CREATURE;
        oEnemy.GetComponent<xGameObjectBase>().nAppearanceType = EngineConstants.APP_TYPE_HURLOCK;
        //oEnemy.engine.SetTeamId(oEnemy, EngineConstants.PROVING_TEAM_OPPONENTS);
        //Add the default weapon as a simple placeholder
        oEnemy.AddComponent<creature_core>();
    }

    void HandleArea()
    {
        GameObject oArea;
        oArea = engine.CreateObject(EngineConstants.OBJECT_TYPE_AREA, "21646", Vector3.zero);
        /*oArea.GetComponent<xGameObject>().Initialize();
        oArea.tag = "Area";
        oArea.name = Application.loadedLevelName;
        oArea.engine.SetLocalInt(oArea, EngineConstants.AREA_ID, 130); //arl120ar_blacksmith*/
        //Debug.Log(Application.loadedLevelName);
    }

    void InitializeModule(string type)
    {
        switch (type)
        {
            case "START":
                {
                    tArea = "demo100ar_wilderness";//"demo200ar_tavern";//21646
                    tWaypoint = "demo100wp_start";

                    //Add demo module script on module object, And because it's custom, set the flag true
                    //Very important to set the custom flag, this is how redirected events are handled
                    //This has to be done manually only for the module once, other objects set this on the fly
                    oBase.bCustom = EngineConstants.TRUE;
                    gameObject.AddComponent(Type.GetType(Script));

                    //Signal event module start
                    xEvent ev = engine.Event(EngineConstants.EVENT_TYPE_MODULE_START);
                    engine.SetEventCreatorRef(ref ev, gameObject);
                    engine.SetEventObjectRef(ref ev, 0, gameObject);
                    engine.SignalEvent(gameObject, ev);

                    //Signal event game mode explore For now during debug, otherwise GM_pregame
                    //Followed by showstartmenu
                    engine.WR_SetGameMode(EngineConstants.GM_EXPLORE);

                    break;
                }
            case "LOAD":
                {
                    break;
                }
            default: break;
        }
    }
}
