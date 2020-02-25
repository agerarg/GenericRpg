using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    public static DamageNumbers instance;
    public DamagePopUI damagePrefab;
    public Transform mainCanvas;
    private Queue<DamagePopUI> damagesPool = new Queue<DamagePopUI>();
    private void Awake()
    {
        instance = this;
    }

    public DamagePopUI Get()
    {
        if (damagesPool.Count == 0)
        {
            DamagePopUI newDmg = Instantiate(damagePrefab, Vector3.zero, Quaternion.identity, mainCanvas);
            newDmg.gameObject.SetActive(false);
            damagesPool.Enqueue(newDmg);
        }
        return damagesPool.Dequeue();
    }
    public void ReturnToPool(DamagePopUI shot)
    {
        shot.gameObject.SetActive(false);
        damagesPool.Enqueue(shot);
    }
}
