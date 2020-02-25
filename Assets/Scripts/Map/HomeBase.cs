using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    float checkPlayerFarAway = 0;
    private PlayerMove playerMove;
    void Start()
    {
        playerMove = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPlayerFarAway > 5f)
        {
            float dist = Vector3.Distance(playerMove.GetPlayerPosition(), transform.position);
            if (dist > 200f)
            {
                playerMove = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
                gameObject.SetActive(false);
            }
            checkPlayerFarAway = 0;
        }
        checkPlayerFarAway += Time.deltaTime;
    }
}
