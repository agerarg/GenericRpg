using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapFollow : MonoBehaviour
{
    public Transform player;
    public float fixer;
    private Transform mainCam; 
    void Start()
    {
        mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, 100f , player.position.z);

        Vector3 newRotation = new Vector3(90f, mainCam.eulerAngles.y + fixer, 90f);
        this.transform.eulerAngles = newRotation;

    }
}
