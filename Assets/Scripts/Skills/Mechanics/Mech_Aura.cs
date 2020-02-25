using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Aura : MonoBehaviour, IMechanic
{
    public float aura_damage = 10;
    public float range = 3f;
    private float slashLiveTime = 0.3f;
    private float timePass = 0;
    private float rangeGrown=0f;
    private int skillId;
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        rangeGrown = slashLiveTime/ range;
    }
    private void OnEnable()
    {
        timePass = 0;
    }
    // Update is called once per frame
    void Update()
    {
        rangeGrown += rangeGrown * Time.deltaTime;
        if (timePass > slashLiveTime)
        {
            SkillPool.instance.ReturnToPool(skillId, gameObject);
        }
        timePass += Time.deltaTime;
        transform.localScale += new Vector3(rangeGrown, rangeGrown, rangeGrown);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("hit:" + collision.gameObject.name);
    }

    public float GetDamage()
    {
        return aura_damage;
    }
    public void SetId(int id)
    {
        skillId = id;
    }
    public void Reactivate(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
        timePass = 0;
        transform.localScale = new Vector3(1, 1, 1);
        rangeGrown = slashLiveTime / range;
    }
}
