using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Melee : MonoBehaviour, IMechanic
{
    public float melee_damage = 20;
    public float attack_speed = 1;
    public float range = 3f;
    public Animator animator;
    public float offrotation = 0;
    private float slashLiveTime = 0.3f;
    private float timePass=0;
    private int skillId;
    void Start()
    {
        animator.speed = attack_speed;
        slashLiveTime = slashLiveTime / attack_speed;

        transform.eulerAngles = new Vector3(
           0,
           transform.eulerAngles.y,
           0
       );
        transform.position = transform.position + (transform.forward * range);

        transform.eulerAngles = new Vector3(
           0,
           transform.eulerAngles.y + offrotation,
           0
       );

    }
    private void OnEnable()
    {
        timePass = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(timePass > slashLiveTime)
        {
            SkillPool.instance.ReturnToPool(skillId, gameObject);
        }
        timePass += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("hit:" + collision.gameObject.name);
    }

    public float GetDamage()
    {
        return melee_damage;
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
        animator.speed = attack_speed;
        slashLiveTime = slashLiveTime / attack_speed;

        transform.eulerAngles = new Vector3(
           0,
           transform.eulerAngles.y,
           0
       );
        transform.position = transform.position + (transform.forward * range);

        transform.eulerAngles = new Vector3(
           0,
           transform.eulerAngles.y + offrotation,
           0
       );
    }
}
