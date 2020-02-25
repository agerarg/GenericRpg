using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUI : MonoBehaviour
{
    public Color NormalHit;
    public Color CriticalHit;
    private TextMeshProUGUI dmgNumber;
    private Color dmgColor;
    private float moveSpeed = 50f;
    private float disapearTime = 1;
    private float disapearSpeed = 5;
    private bool enableThis = false;
    private Transform target;
    private Camera CameraMain;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private float increaseSacelAmount = 1f;
    private Vector3 offset;
    private int sortOrder;
    public void Setup(float number, Transform hitPos,bool criticalHit)
    {
        CameraMain = Camera.main;

        target = hitPos;
        dmgNumber = GetComponent<TextMeshProUGUI>();
        Vector3 newPos = CameraMain.WorldToScreenPoint(target.position);
        transform.position = newPos; 
        dmgNumber.SetText(Mathf.Round(number) + "");
        disapearTime = DISAPPEAR_TIMER_MAX;
        enableThis = true;

        if(criticalHit)
        {
            CriticalHit.a = 1;
            dmgColor = CriticalHit;
            dmgNumber.fontSize = 44;
        }
        else
        {
            NormalHit.a = 1;
            dmgColor = NormalHit;
            dmgNumber.fontSize = 36;
        }
        dmgColor.a = 1;
        offset = Vector3.zero;
        transform.localScale = Vector3.one;
        moveSpeed = 50f;
        sortOrder++;
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.sortingOrder = sortOrder;
    }
    // Update is called once per frame
    void Update()
    {
        if (enableThis)
        {
            offset += new Vector3(0, 1 * moveSpeed * Time.deltaTime, 0);
            moveSpeed -= moveSpeed * 8 * Time.deltaTime;
            if (target != null)
            {
                Vector3 newPos = CameraMain.WorldToScreenPoint(target.position);
                transform.position = newPos+ offset;
            }

            
            disapearTime -= Time.deltaTime;

           if(disapearTime > DISAPPEAR_TIMER_MAX * 0.5f)
            {
                transform.localScale += Vector3.one * increaseSacelAmount * Time.deltaTime;
            }
           else
            {
                transform.localScale -= Vector3.one * increaseSacelAmount * Time.deltaTime;
            }

            if (disapearTime < 0)
            {
                dmgColor.a -= disapearSpeed * Time.deltaTime;
                if (dmgColor.a <= 0)
                {
                    enableThis = false;
                    DamageNumbers.instance.ReturnToPool(this);
                }
            }
            else
            {
                dmgColor.a = 1;
            }
            dmgNumber.color = dmgColor;
        }
    }
}
