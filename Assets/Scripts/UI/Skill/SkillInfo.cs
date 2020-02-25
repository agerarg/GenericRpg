using UnityEngine;
using TMPro;
public class SkillInfo : MonoBehaviour
{
    public TextMeshProUGUI skillName;
    private string sName;
    private int sTrigger;
    private string sCon;
    private int sSkillType;
    public void Setup(string Name,int Trigger,string Con, int SkillType)
    {
        sName = Name;
        sTrigger = Trigger;
        sCon = Con;
        sSkillType = SkillType;
        skillName.SetText(sName);
    }

    public void GoEditMode()
    {
        Debug.Log("edit"+ sName);
    }
}
