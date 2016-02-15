using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class xGameObjectUTC : xGameObjectBase
{
    public List<xThreat> oThreats { get; set; }
    public List<xProperty> oProperties { get; set; }
    public List<int> oAbilities { get; set; }//Talent, skill, spell
    public List<int> oAbilitiesActive { get; set; }//In use abilities
    public List<int> oAbilitiesQuick { get; set; }//Quick bar
    public List<AbilityCooldown> oAbilitiesCooldown { get; set; }
    public List<GameObject> oPerception { get; set; }
    public List<xTactic> oTactics { get; set; }
    public List<xEffect> oEffects { get; set; }//Applied effects, doesn't matter positive or negative
    public List<GameObject> oGear { get; set; }//This is the active gear, not the inventory
    public GameObject[] oWeaponSet = { null, null };//This is the weapon set, maximum 2

    public int bStatue { get; set; }
    public int bGhost { get; set; }

    #region variables initialized from UTC
    public string ResRefName { get; set; }
    public bool LocalCopy { get; set; }
    public string Variable2da { get; set; }
    public Dictionary<string, string> VariableList { get; set; }
    public List<GameObject> InventoryList { get; set; }
    public string xname { get; set; }
    public int NameStringID { get; set; }
    public bool NameRequiresReTranslation { get; set; }
    public string Description { get; set; }
    public int DescriptionStringID { get; set; }
    public bool DescriptionRequiresReTranslation { get; set; }
    public string Tag { get; set; }
    public int Race { get; set; }
    public int Gender { get; set; }
    public int Group { get; set; }
    public int Team { get; set; }
    public bool Selectable { get; set; }
    public bool PlotGiver { get; set; }
    public int Class { get; set; }
    public int PackageType { get; set; }
    public int Package { get; set; }
    public int PackageAI { get; set; }
    public int ConversationURI { get; set; }
    public int ScriptURI { get; set; }
    public int Gold { get; set; }
    public int PerceptionRange { get; set; }
    public string Comments { get; set; }
    public int CharacterURI { get; set; }
    public int Appearance { get; set; }
    public int Head { get; set; }
    public int Hair { get; set; }
    public int HairTint { get; set; }
    public int Eyes { get; set; }
    public int EyeTint { get; set; }
    public int Beard { get; set; }
    public int BodyTintMask { get; set; }
    public int BodyTint { get; set; }
    public int SkinTint { get; set; }
    public int TattooTint { get; set; }
    public string HeadMorph { get; set; }
    public int Tattoo { get; set; }
    public int Rank { get; set; }
    public int ArtFidelity { get; set; }
    public int TreasureCategory { get; set; }
    public int Combatant { get; set; }
    public bool Immortal { get; set; }
    public bool Plot { get; set; }
    public bool NoPermanentDeath { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public List<int[]> SkillList { get; set; }
    public List<int> TalentList { get; set; }
    public List<int> SpellList { get; set; }
    #endregion

    #region variables initialized from ARE
    public Vector3 position { get; set; }
    public Vector3 orientation { get; set; }
    //public string Tag { get; set; }//from UTC
    public List<Vector3> GeometryList { get; set; }//vertex
    public int ObjectType { get; set; }
    public Guid GUID { get; set; }
    public List<Guid> AssociatedInstanceGUIDList { get; set; }
    public int ObjectURI { get; set; }
    public string ObjectResRefName { get; set; }
    public string ObjectName { get; set; }
    public string ParentTag { get; set; }
    public bool Active { get; set; }
    public int Importance { get; set; }
    //public int Appearance { get; set; }//from UTC
    public int InitialAnimation { get; set; }
    public int ParentInitialAnimation { get; set; }
    public int PickLockLevel { get; set; }
    public int ParentPickLockLevel { get; set; }
    //public int Team { get; set; }//from UTC
    public int ParentTeam { get; set; }
    public bool Trackable { get; set; }
    public bool WeaponsDrawn { get; set; }
    public int RoomID1 { get; set; }
    public int RoomID2 { get; set; }
    //public int Rank { get; set; }//from UTC
    public int ParentRank { get; set; }
    //public int TreasureCategory { get; set; }//from UTC
    public int ParentTreasureCategory { get; set; }
    public float InteractionRadius { get; set; }
    public int TrapDetectionDifficulty { get; set; }
    public int ParentTrapDetectionDifficulty { get; set; }
    public int TrapDisarmDifficulty { get; set; }
    public int ParentTrapDisarmDifficulty { get; set; }
    public int Useable { get; set; }
    public int ParentUseable { get; set; }
    public string CloseToOpenSound { get; set; }
    public string OpenToCloseSound { get; set; }
    public string DestroyedSound { get; set; }
    public string UsedSound { get; set; }
    public string LockedSound { get; set; }
    public string HitSound { get; set; }
    //public string Variable2da { get; set; }//from UTC
    //public Dictionary<string, string> ParentVariableList { get; set; }//unused?
    //public Dictionary<string, string> VariableList { get; set; }//from UTC
    #endregion

    #region VariableList
    public int AI_DISABLE_PATH_BLOCKED_ACTION { get; set; }
    public int AI_CUSTOM_AI_VAR_INT { get; set; }
    public float AI_CUSTOM_AI_VAR_FLOAT { get; set; }
    public int AI_LAST_TACTIC { get; set; }
    public float CREATURE_SPAWN_HEALTH_MOD { get; set; }
    public GameObject AI_CUSTOM_AI_VAR_OBJECT { get; set; }
    public GameObject AI_REGISTERED_WP { get; set; }
    public int AI_MOVE_TIMER { get; set; }
    public string AI_CUSTOM_AI_VAR_STRING { get; set; }
    public int CREATURE_COUNTER_1 { get; set; }
    public int CREATURE_COUNTER_2 { get; set; }
    public int CREATURE_COUNTER_3 { get; set; }
    public int CREATURE_DO_ONCE_A { get; set; }
    public int CREATURE_DO_ONCE_B { get; set; }
    public int CREATURE_SPAWN_COND { get; set; }
    public int AI_THREAT_SWITCH_TIMER_MIN { get; set; }
    public int SOUND_SET_FLAGS_0 { get; set; }
    public int SOUND_SET_FLAGS_1 { get; set; }
    public int SOUND_SET_FLAGS_2 { get; set; }
    public int SPAWN_HOSTILE_LYING_ON_GROUND { get; set; }
    public int CREATURE_SPAWNED { get; set; }
    public int CREATURE_LYRIUM_USE { get; set; }
    public int CREATURE_SPAWN_DEAD { get; set; }
    public int CREATURE_RULES_FLAG0 { get; set; }
    public int SHOUTS_ACTIVE { get; set; }
    public string SHOUTS_CONVERSATION_OVERRIDE { get; set; }
    public float SHOUTS_DELAY { get; set; }
    public int AMBIENT_SYSTEM_STATE { get; set; }
    public int AMBIENT_MOVE_PATTERN { get; set; }
    public string AMBIENT_MOVE_PREFIX { get; set; }
    public int AMBIENT_ANIM_PATTERN { get; set; }
    public float AMBIENT_ANIM_FREQ { get; set; }
    public int AMBIENT_ANIM_PATTERN_OVERRIDE { get; set; }
    public float AMBIENT_ANIM_FREQ_OVERRIDE { get; set; }
    public int AI_CUSTOM_AI_ACTIVE { get; set; }
    public int AI_BALLISTA_SHOOTER_STATUS { get; set; }
    public int AI_FLAG_PREFERS_RANGED { get; set; }
    public GameObject AI_THREAT_TARGET { get; set; }
    public int AI_THREAT_HATED_RACE { get; set; }
    public int AI_THREAT_HATED_CLASS { get; set; }
    public int AI_THREAT_HATED_GENDER { get; set; }
    public int AI_THREAT_SWITCH_TIMER { get; set; }
    public GameObject AI_TARGET_OVERRIDE { get; set; }
    public int AI_TARGET_OVERRIDE_DUR_COUNT { get; set; }
    public int AI_THREAT_TARGET_SWITCH_COUNTER { get; set; }
    public GameObject AI_PLACEABLE_BEING_USED { get; set; }
    public int AMBIENT_ANIM_STATE { get; set; }
    public int AMBIENT_MOVE_STATE { get; set; }
    public int AMBIENT_MOVE_COUNT { get; set; }
    public float DEBUG_TRACKING_POS { get; set; }
    public int IS_SUMMONED_CREATURE { get; set; }
    public int STAT_DMG { get; set; }
    public int STAT_HIT { get; set; }
    public int STAT_MISS { get; set; }
    public int STAT_CRIT { get; set; }
    public int SURR_SURRENDER_ENABLED { get; set; }
    public string SURR_PLOT_NAME { get; set; }
    public int SURR_PLOT_FLAG { get; set; }
    public int AMBIENT_COMMAND { get; set; }
    public int FOLLOWER_SCALED { get; set; }
    public int AMBIENT_ANIM_OVERRIDE_COUNT { get; set; }
    public int AMBIENT_TICK_COUNT { get; set; }
    public int RUBBER_HOME_ENABLED { get; set; }
    public float RUBBER_HOME_LOCATION_X { get; set; }
    public float RUBBER_HOME_LOCATION_Y { get; set; }
    public float RUBBER_HOME_LOCATION_Z { get; set; }
    public float RUBBER_HOME_LOCATION_FACING { get; set; }
    public string SURR_CONVERSATION_OVERRIDE { get; set; }
    public int TS_OVERRIDE_CATEGORY { get; set; }
    public int TS_OVERRIDE_RANK { get; set; }
    public int TS_OVERRIDE_MONEY { get; set; }
    public float TS_OVERRIDE_ITEM { get; set; }
    public float TS_OVERRIDE_HIGH { get; set; }
    public int SURR_INIT_CONVERSATION { get; set; }
    public int FLAG_STOLEN_FROM { get; set; }
    public int BASE_GROUP { get; set; }
    public int AI_HELP_TEAM_STATUS { get; set; }
    public int AI_FLAG_STATIONARY { get; set; }
    public float DEBUG_TRACKING_POS_Y { get; set; }
    public float TS_OVERRIDE_EQUIPMENT { get; set; }
    public int TS_OVERRIDE_SCALING { get; set; }
    public int TS_OVERRIDE_STEALING { get; set; }
    public int COMBAT_LAST_WEAPON { get; set; }
    public int PHYSICS_DISABLED { get; set; }
    public int TS_TREASURE_GENERATED { get; set; }
    public int AI_LIGHT_ACTIVE { get; set; }
    public int GO_HOSTILE_ON_PERCEIVE_PC { get; set; }
    public int CREATURE_REWARD_FLAGS { get; set; }
    public int AI_WAIT_TIMER { get; set; }
    public int CLIMAX_ARMY_ID { get; set; }
    public float AI_THREAT_GENERATE_EXTRA_THREAT { get; set; }
    public int CREATURE_DAMAGED_THE_HERO { get; set; }
    public int LOOKAT_DISABLED { get; set; }
    public int MIN_LEVEL { get; set; }
    public float ROAM_DISTANCE { get; set; }
    public int CREATURE_HAS_TIMER_ATTACK { get; set; }
    public float TS_OVERRIDE_REACTIVE { get; set; }

    //DHK
    public int EFFECT_FLAG_DISABLE { get; set; }
    public float GORE { get; set; }
    public int FOLLOWER_STATE { get; set; }
    public int COMBAT_STATE { get; set; }
    public int ACTIVE_WEAPON_SET { get; set; }
    public int bControlled { get; set; }//If creature+ follower+ TRUE= TRUE
    public int bTactics { get; set; }//If TRUE, then AI kicks in on party members
    public int bStealthed { get; set; }
    #endregion

    // Use this for initialization
    void Awake()
    {
        ACTIVE_WEAPON_SET = 0;//0= main,1= alternate

        if (oThreats == null) oThreats = new List<xThreat>();
        if (oProperties == null) oProperties = new List<xProperty>();
        if (oAbilities == null) oAbilities = new List<int>();
        if (oAbilitiesActive == null) oAbilitiesActive = new List<int>();
        if (oAbilitiesCooldown == null) oAbilitiesCooldown = new List<AbilityCooldown>();
        if (oPerception == null) oPerception = new List<GameObject>();
        if (oTactics == null) oTactics = new List<xTactic>();
        if (oEffects == null) oEffects = new List<xEffect>();
        if (oGear == null) oGear = new List<GameObject>();
        InitiateGear();

        if (VariableList == null) VariableList = new Dictionary<string, string>();
        if (InventoryList == null) InventoryList = new List<GameObject>();
        if (SkillList == null) SkillList = new List<int[]>();
        if (TalentList == null) TalentList = new List<int>();
        if (SpellList == null) SpellList = new List<int>();

        if (oAbilitiesQuick == null) oAbilitiesQuick = new List<int>();
        InitiateAbilitiesQuick();
    }

    private void InitiateGear()
    {
        for (var i = 0; i < 15; i++)
        {
            GameObject _Object = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/objectPrefab"));
            _Object.GetComponent<xGameObjectBase>().nObjectType = EngineConstants.OBJECT_TYPE_INVALID;
            _Object.name = "GearPlaceholder";
            _Object.transform.parent = gameObject.transform;
            _Object.SetActive(false);
            oGear.Add(_Object);
        }
    }

    private void InitiateAbilitiesQuick()
    {
        for (var i = 0; i < EngineConstants.QUICKBAR_SLOTS_MAX; i++)
        {
            oAbilitiesQuick.Add(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If cooldown is done, remove from list
        foreach (AbilityCooldown _ability in oAbilitiesCooldown)
        {
            if (_ability.timeStamp <= Time.time)
            {
                oAbilitiesCooldown.Remove(_ability);
            }
        }
    }
}

public class AbilityCooldown
{
    public int nAbility { get; set; }
    public int nCooldown { get; set; }
    public float timeStamp { get; set; }
    public AbilityCooldown(int nAbility, int nCooldown)
    {
        this.nAbility = nAbility;
        this.nCooldown = nCooldown;
        timeStamp = Time.time + nCooldown;//Frames or seconds?
    }

}