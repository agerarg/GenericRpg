using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Proyectile : MonoBehaviour, IMechanic
{
    public float proyectile_damage = 5;
    public float proyectile_speed = 5;
    public float lifeTime = 5;
    private float lifeTimeStep = 0;
    private int skillId;
    void Start()
    {
        transform.eulerAngles = new Vector3(
            0,
            transform.eulerAngles.y,
            0
        );
       
    }

    private void OnEnable()
    {
        lifeTimeStep = 0;
    }

    private void Update()
    {
        lifeTimeStep += Time.deltaTime;
        //rb.MovePosition(transform.position + (transform.forward * proyectile_speed));
        transform.position = transform.position + (transform.forward * proyectile_speed);
        if(lifeTimeStep>lifeTime)
        {
            SkillPool.instance.ReturnToPool(skillId, gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        SkillPool.instance.ReturnToPool(skillId, gameObject);
    }
    public float GetDamage()
    {
        return proyectile_damage;
    }

    public void SetId(int id)
    {
        skillId = id;
    }
    public void Reactivate(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(
           0,
           transform.eulerAngles.y,
           0
       );
        lifeTimeStep = 0;
    }
}
