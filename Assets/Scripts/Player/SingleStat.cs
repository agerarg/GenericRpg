using System;

[Serializable]
public class SingleStat
{
    public bool IsActive = false;
    public float StatBasic = 0;
    public float StatItem = 0;
    public float StatBuff = 0;
    public float StatPer = 0;
    public float GetValue()
    {
        float f = StatBasic;
        f += StatItem;
        if (StatPer > 0)
            f += ((f / 100) * StatPer);
        f += StatBuff;
        return f;
    }
}