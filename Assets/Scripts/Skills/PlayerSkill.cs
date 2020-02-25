using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private string skillName;

    public enum ESkillType { projectile, melee, aura };
    public enum ESkillDamageType { physical, fire, cold, lightning };

    private ESkillType skillType;
    private ESkillDamageType skillDamageType;

    private ITriggerConditions condition=null;

  
    private float damage;
    private float speed;

    private float onTimeEvery = 0;

    //Setup
    private string sName;
    private int sTrigger;
    private string sCon;
    private int sSkillType;

    private PlayerMove PM;
    private RaycastHit m_HitInfo;

    private int idSkill;
    void Start()
    {
        PM = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
        /* skillName = "default";
         damage = 0;
         speed = 5f;
         skillType = ESkillType.melee;
         skillDamageType = ESkillDamageType.physical;
         //ConOnPressKey con = new ConOnPressKey("q");
        ConOnEveryTime con2 = new ConOnEveryTime(2f);
         condition = con2;*/
    }

    public void Setup(string name,int trigger,string con, int skilltype, int id)
    {
        idSkill = id;
        skillName = name;
        ITriggerConditions cond=null;
        switch (trigger)
        {
            case 0:
                cond = new ConOnPressKey(con.ToLower());
            break;
            case 1:
                cond = new ConOnEveryTime(3f);
             break;
        }
        condition = cond;
        //Debug.Log(skillName+" created!");

        sName = name;
        sTrigger = trigger;
        sCon = con;
        sSkillType = skilltype;
    }

    void Update()
    {
        condition.Trigger(this);
    }
    private void FireSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            Vector3 hitpoint = m_HitInfo.point;
            hitpoint = new Vector3(hitpoint.x, 2, hitpoint.z);

            Quaternion targetRotation = Quaternion.LookRotation(hitpoint - PM.GetPlayerPosition());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 500 * Time.deltaTime);

           
            GameObject skill = SkillPrefabList.instance.GetPrefab(sSkillType);
            // GameObject OP = Instantiate(skill, PM.GetPlayerPosition(), transform.rotation);
            GameObject OP = SkillPool.instance.Get(idSkill, skill, PM.GetPlayerPosition(), transform.rotation);
             IMechanic Mech = OP.GetComponent<IMechanic>();
            Mech.SetId(idSkill);
            Mech.Reactivate(PM.GetPlayerPosition(), transform.rotation);
            // Debug.Log("Used Skill " + skillName + "! SkillType:"+ sSkillType);

        }
    }
    //////////////////////////////////////////////
    //On Condition Triggers
    //////////////////////////////////////////////
    public void OnPressKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            FireSkill();
        }
    }
    public void EveryTimer(float time)
    {
        onTimeEvery += Time.deltaTime;
        if(onTimeEvery>time)
        {
            onTimeEvery = 0;
            FireSkill();
        }
    }
    //////////////////////////////////////////////
}
