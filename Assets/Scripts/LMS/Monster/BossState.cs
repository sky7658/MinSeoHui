using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Enemy;

// ���� �������̽� Ŭ����

namespace SHY.Enemy 
{
    public enum BossStateName
    {
        Idle,
        Move,
        Attack,
        Rush,
        Die
    };

    public interface BossState
    {
        public BossStateName stateName { get; }
        public void Enter(Boss boss);
        public void Action(Boss boss);
        public void Exit(Boss boss);
    }
    public class Idle : BossState
    {
        public BossStateName stateName { get; } = BossStateName.Idle;
        public void Enter(Boss boss)
        {
            boss.anim.SetBool("isMove", false);
            boss.nav.isStopped = true;
        }
        public void Action(Boss boss){
            boss.DecideState();
        }
        public void Exit(Boss boss){}
    }

    public class Move : BossState
    {
        public BossStateName stateName { get; } = BossStateName.Move;
        public void Enter(Boss boss)
        {
            boss.anim.SetBool("isMove", true);
            boss.nav.isStopped = false;
        }
        public void Action(Boss boss)
        {
            boss.nav.SetDestination(boss.target.position);
            boss.DecideState();
        }
        public void Exit(Boss boss)
        {
            boss.anim.SetBool("isMove", false);
            boss.nav.isStopped = true;
        }
    }

    public class Attack : BossState
    {
        public BossStateName stateName { get; } = BossStateName.Attack;
        public void Enter(Boss boss){
            boss.anim.SetBool("isMove", false);
            boss.anim.SetTrigger("doAttack");
            boss.nav.isStopped = true;
            boss.Attack();
        }
        public void Action(Boss boss)
        {
        }
        public void Exit(Boss boss){
            boss.nav.isStopped = false;
        }
    }

    public class Rush : BossState
    {
        public BossStateName stateName { get; } = BossStateName.Rush;
        public void Enter(Boss boss)
        {
            boss.anim.SetBool("isMove", false);
            boss.anim.SetTrigger("doRush");
            boss.nav.isStopped = true;
            boss.ExecuteSkill();
        }
        public void Action(Boss boss){}
        public void Exit(Boss boss){
            boss.nav.isStopped = false;
        }
    }

    public class Die : BossState
    {
        public BossStateName stateName { get; } = BossStateName.Die;
        public void Enter(Boss boss)
        {
            boss.anim.SetBool("isMove", false);
            boss.anim.SetTrigger("doDie");
            boss.nav.isStopped = true;
        }
        public void Action(Boss boss){}
        public void Exit(Boss boss){}
    }
}
