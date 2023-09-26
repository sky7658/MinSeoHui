using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IDamageable
{
    LMS.UI.HpBarUI hpBar;
    float maxHp = 50;
    float damage = 5;
    private void Awake()
    {
        hpBar = transform.GetChild(0).transform.GetChild(0).GetComponent<LMS.UI.HpBarUI>();
    }
    public float GetDamage()
    {
        return damage;
    }

    public void TakeDamage(float damage, Vector3 reactVec)
    {
        hpBar.UpdateHpBar(damage);
        maxHp -= damage;
    }
}
