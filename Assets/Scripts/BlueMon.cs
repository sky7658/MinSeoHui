using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueMon : Monster
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
        maxHealth = 70;
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
        var bullet = BulletPool.GetObject(this.transform, target);

        yield return new WaitForSeconds(1f);
        
        isChase = true;
        isAttack = false;
    }
}
