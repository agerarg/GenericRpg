using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillManager : MonoBehaviour
{
    public static UISkillManager instance;

    public PlayerSkill PlayerSkilPrefab;
    public SkillInfo UiSkillLinePref;
    public Transform SkillsLayaut;
    public Transform SkillsHolder;

    public GameObject uiSkills;
    public GameObject uiSkillList;
    public GameObject uiSkillNew;

    public TMP_Dropdown triggerDrop;
    public TMP_Dropdown typeDrop;
    public TMP_InputField conditionField;
    public TextMeshProUGUI inputName;
    public TextMeshProUGUI inputCondition;

    private PlayerMove PM;

    private int triggerDropSave=0;
    private int typeDropSave = 0;
    private string skillName;
    private string condition;
    private int SkillCount=0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiSkillList.SetActive(false);
        uiSkills.SetActive(false);
        uiSkillNew.SetActive(false);
        PM = (PlayerMove)FindObjectOfType(typeof(PlayerMove));
    }

    public void BackToSkillList()
    {
        uiSkillList.SetActive(true);
        uiSkillNew.SetActive(false);
    }
    public void OpenAddSkill()
    {
        if (SkillCount < 5)
        {
            uiSkillList.SetActive(false);
            uiSkillNew.SetActive(true);
        }
    }
    public void OpenSkills()
    {
        uiSkillList.SetActive(true);
        uiSkills.SetActive(true);
        PM.CanMove(false);
    }
    public void CloseSkills()
    {
        uiSkillList.SetActive(false);
        uiSkills.SetActive(false);
        PM.CanMove(true);
    }
    
    void turnConditionReadOnly(bool turn,string txt)
    {
        //inputCondition.SetText(txt);
        conditionField.text = txt;
        conditionField.readOnly = turn;
    }

    public void SkillTriggerChose()
    {
        triggerDropSave = triggerDrop.value;
        switch(triggerDropSave)
        {
            case 0:
                turnConditionReadOnly(false, "");
                break;
            case 1:
                turnConditionReadOnly(true,"3");
            break;
        }
    }

    public void SkillTypeChose()
    {
        typeDropSave = typeDrop.value;
    }

    public void CreateSkill()
    {

        skillName = inputName.text;
        condition = inputCondition.text;

        if (skillName.Length > 1 && condition.Length > 1)
        {
            PlayerSkill PS = Instantiate(PlayerSkilPrefab, new Vector3(0, 0, 0), Quaternion.identity, SkillsHolder);
            PS.Setup(skillName, triggerDropSave, condition, typeDropSave, SkillCount);

            SkillInfo info = Instantiate(UiSkillLinePref, new Vector3(0, 0, 0), Quaternion.identity, SkillsLayaut);
            info.Setup(skillName, triggerDropSave, condition, typeDropSave);
            BackToSkillList();
            SkillCount++;
        }
        else
        {
            Debug.Log("Error: Skill Name and Condition is needed");
        }
    }

}
