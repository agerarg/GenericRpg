using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
    public static SkillPool instance;
    private void Awake()
    {
        instance = this;
    }
    private Queue<GameObject>[] SkillShot = new Queue<GameObject>[5];

    private void Start()
    {
        for(int i=0;i<5;i++)
        {
            SkillShot[i] = new Queue<GameObject>();
        }
    }

    public GameObject Get(int id, GameObject skill,Vector3 pos, Quaternion rot)
    {
        if (SkillShot[id].Count == 0)
            {
                GameObject newskill = Instantiate(skill, pos, rot, transform);
                newskill.SetActive(false);
                SkillShot[id].Enqueue(newskill);
            }
        GameObject shot = SkillShot[id].Dequeue();
        shot.SetActive(true);
        return shot;
    }

   public void ReturnToPool(int id,GameObject shot)
    {
        shot.SetActive(false);
        SkillShot[id].Enqueue(shot);
    }

}
