using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpowner : MonoBehaviour
{
    public GameObject MonsterPrefab;
    void Start()
    {
        StartCoroutine(SpawnMonst());
    }
    IEnumerator SpawnMonst()
    {
         yield return new WaitForSeconds(1);
        MonsterPrefab = Instantiate(MonsterPrefab, Vector3.zero, Quaternion.identity, transform);
        MonsterPrefab.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1);
        Monster M = MonsterPrefab.GetComponent<Monster>();
        M.WarpTo(transform.position);
    }
   
}
