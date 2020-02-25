using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Dictionary<string, SingleStat> Stats = new Dictionary<string, SingleStat>();
    public float Life;
    public float Mana;
    void Start()
    {
        SingleStat _LifeLimit = new SingleStat
        {
            StatBasic = 100f
        };
        StatAdd("LifeLimit", _LifeLimit);
        SingleStat _ManaLimit = new SingleStat
        {
            StatBasic = 100f
        };
        StatAdd("ManaLimit", _ManaLimit);

        SingleStat _LifeRegeneration = new SingleStat
        {
            StatBasic = 1f
        };
        StatAdd("LifeRegeneration", _LifeRegeneration);

        SingleStat _ManaRegeneration = new SingleStat
        {
            StatBasic = 1f
        };
        StatAdd("ManaRegeneration", _ManaRegeneration);

        SingleStat _AttackSpeed = new SingleStat
        {
            StatBasic = 100f
        };
        StatAdd("AttackSpeed", _AttackSpeed);

        SingleStat _CastingSpeed = new SingleStat
        {
            StatBasic = 100f
        };
        StatAdd("CastingSpeed", _CastingSpeed);

        SingleStat _MovementSpeed = new SingleStat
        {
            StatBasic = 400f
        };
        StatAdd("MovementSpeed", _MovementSpeed);

        SingleStat _Attack = new SingleStat
        {
            StatBasic = 10f
        };
        StatAdd("Attack", _Attack);

        SingleStat _MagicAttack = new SingleStat
        {
            StatBasic = 5f
        };
        StatAdd("MagicAttack", _MagicAttack);

        SingleStat _Defense = new SingleStat
        {
            StatBasic = 5f
        };
        StatAdd("Defense", _Defense);

        SingleStat _MagicDefense = new SingleStat
        {
            StatBasic = 5f
        };
        StatAdd("MagicDefense", _MagicDefense);

        SingleStat _CriticalChance = new SingleStat
        {
            StatBasic = 1f
        };
        StatAdd("CriticalChance", _CriticalChance);
        SingleStat _CriticalPower = new SingleStat
        {
            StatBasic = 25f
        };
        StatAdd("CriticalPower", _CriticalPower);

        SingleStat _MagicalCriticalChance = new SingleStat
        {
            StatBasic = 1f
        };
        StatAdd("MagicalCriticalChance", _MagicalCriticalChance);

        SingleStat _MagicalCriticalPower = new SingleStat
        {
            StatBasic = 25f
        };
        StatAdd("MagicalCriticalPower", _MagicalCriticalPower);

        Life = StatGet("LifeLimit");
        Mana = StatGet("ManaLimit");
    }
    public void StatAdd(string key, SingleStat ss)
    {
        if (Stats.ContainsKey(key))
        {
            Stats[key] = ss;
        }
        else
        {
            Stats.Add(key, ss);
        }
    }
    public float StatGet(string key) //SingleStat
    {
        if (Stats.ContainsKey(key))
        {
            return Stats[key].GetValue();
        }
        else
        {
            return 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


