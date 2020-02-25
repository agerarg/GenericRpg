using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterOnUI : MonoBehaviour
{
    public TextMeshProUGUI monsterName;
    public Slider life;
    private Transform monsterTarget;
    private bool isThisActive = false;
    void Start()
    {
        life.value = 1;
    }
    public void SetTarget(Transform trans)
    {
        monsterTarget = trans;
        isThisActive = true;
    }
    public void SetName(string newName)
    {
        monsterName.SetText(newName);
    }
    public void SetLife(float newlife)
    {
        life.value = newlife;
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isThisActive)
        {
            if (monsterTarget != null)
            {
                Vector3 newPos = Camera.main.WorldToScreenPoint(monsterTarget.position);
                if (newPos.z < 0)
                    transform.position = new Vector3(15000, 0, 0);
                else
                    transform.position = newPos;
            }
        }
    }
}
