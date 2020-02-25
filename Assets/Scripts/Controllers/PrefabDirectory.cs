using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDirectory : MonoBehaviour
{
    public GameObject[] prefabs;

    public static PrefabDirectory instance;
    void Awake()
    {
        instance = this;
    }
}
