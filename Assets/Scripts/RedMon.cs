using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedMon : Monster
{
    protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (base.Targerting()&&curHealth>0)
        {
            StartCoroutine("Attack");
        }
    }
    
    public override void Init(Action OnDieCallBack = null)
    {
        maxHealth = 100;
        curHealth = maxHealth;
        damage = 10;
        isChase = false;
        isAttack = false;
        meleeArea.enabled = false;
        base.Init(OnDieCallBack);
    }
    
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetTrigger("Attack 01");
        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;
        
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;
        
        isChase = true;
        isAttack = false;
    }
}
