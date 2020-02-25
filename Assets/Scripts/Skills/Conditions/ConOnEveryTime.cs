
public class ConOnEveryTime : ITriggerConditions
{
    private float time;
    public ConOnEveryTime(float t)
    {
        time = t;
    }
    public void Trigger(PlayerSkill ps)
    {
        ps.EveryTimer(time);
    }
}
