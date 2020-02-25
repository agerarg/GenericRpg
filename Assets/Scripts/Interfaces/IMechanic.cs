using UnityEngine;

public interface IMechanic
{
    float GetDamage();
    void SetId(int id);
    void Reactivate(Vector3 pos,Quaternion rot);
}
