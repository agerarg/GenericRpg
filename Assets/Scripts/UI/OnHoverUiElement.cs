using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverUiElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerMove PM;
    private bool activate=false;
    void Start()
    {
        PM = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PM.IsMoving())
        {
            activate = true;
            PM.CanMove(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(activate)
            PM.CanMove(true);
        activate = false;
    }
}
