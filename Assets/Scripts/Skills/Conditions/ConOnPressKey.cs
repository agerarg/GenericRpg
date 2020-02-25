using System.Text.RegularExpressions;
using UnityEngine;

public class ConOnPressKey : ITriggerConditions
{
    KeyCode key;
    public ConOnPressKey(string k)
    {
       string tkey = k.ToUpper();
        tkey = tkey.Substring(0, 1);
        key = (KeyCode)System.Enum.Parse(typeof(KeyCode), tkey);
    }
    public void Trigger(PlayerSkill ps)
    {
        ps.OnPressKey(key);
    }


}
