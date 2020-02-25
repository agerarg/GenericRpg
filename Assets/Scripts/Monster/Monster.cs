using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    public string monsterName;
    public float lifeLimit;
    public MonsterOnUI monsterOnUI;
    private NavMeshAgent m_Agent;
    private MonsterOnUI instanceOfMonsterOnUI;
    private float realLife;
    //private bool isOnMotion = false;
    private PlayerMove playerMove;
    private float CheckTimeing=0;
    private bool isBarActive = false;
    private GameObject CanvasUI;
    public void WarpTo(Vector3 pos)
    {
        m_Agent.Warp(pos + new Vector3(0, -14.03f,0));
    }

    void Start()
    {
        CanvasUI = GameObject.Find("Canvas");
        playerMove = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
        realLife = lifeLimit;
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void CreateTheBar()
    {
        instanceOfMonsterOnUI = Instantiate(monsterOnUI, Vector3.zero, Quaternion.identity, CanvasUI.transform);
        instanceOfMonsterOnUI.SetName(monsterName);
        instanceOfMonsterOnUI.SetTarget(transform);
        UpdateLifeOnUI();
    }

    void UpdateLifeOnUI()
    {
        instanceOfMonsterOnUI.SetLife(realLife/ lifeLimit);
    }

    void OnCollisionEnter(Collision collision)
    {
        IMechanic SkillMech = collision.gameObject.GetComponent<IMechanic>();
        float dmg = SkillMech.GetDamage();
        if (!isBarActive)
        {
            isBarActive = true;
            CreateTheBar();
        }

        DamagePopUI dmgPop = DamageNumbers.instance.Get();
        dmgPop.gameObject.SetActive(true);

        int rnd = Random.Range(1, 100);
        bool critical = false;
        if(rnd<50)
        {
            critical = true;
            dmg = dmg * 2;
        }
        dmgPop.Setup(dmg,transform, critical);

        realLife -= dmg;
        if (realLife<=0)
        {
            instanceOfMonsterOnUI.DestroyThis();
            Destroy(gameObject);
        }
        else
            UpdateLifeOnUI();
    }

    float DistanceToThePlayer()
    {
        float dist = Vector3.Distance(playerMove.GetPlayerPosition(), transform.position);
        return dist;
    }

    void Update()
    {
       CheckTimeing += Time.deltaTime;
        if (CheckTimeing>1f)
        {
            if (DistanceToThePlayer() < 30f)
            {
                if(!isBarActive)
                {
                    isBarActive = true;
                    CreateTheBar();
                }
                m_Agent.destination = playerMove.GetPlayerPosition();
            }
            else
            {
                if (isBarActive)
                {
                    isBarActive = false;
                    instanceOfMonsterOnUI.DestroyThis();
                }
            }
            CheckTimeing = 0;
        }
       
    }
}
