using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IDamageable
{
    LMS.UI.HpBarUI hpBar;
    int maxHp = 50;
    int damage = 5;
    private void Awake()
    {
        hpBar = transform.GetChild(0).transform.GetChild(0).GetComponent<LMS.UI.HpBarUI>();
    }
    public int GetDamage()
    {
        return damage;
    }

    public void TakeDamage(int damage, Vector3 reactVec)
    {
        hpBar.UIActive();

        hpBar.UpdateHpBar(damage);
        maxHp -= damage;
    }
    
}
