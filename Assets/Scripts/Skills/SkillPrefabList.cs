using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPrefabList : MonoBehaviour
{
    public static SkillPrefabList instance;

    public GameObject[] prefabList; 

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetPrefab(int ind)
    {
        return prefabList[ind];
    }
}
