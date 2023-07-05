using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Monster
{
    public enum MonsterState { IDLE, MOVE, ATTACK, RUSH }
    public class MiddleBoss : MonoBehaviour
    {
        // info
        float damage;
        float rushDis;
        float atkRange;
        float skillRange;
        float skillCool;

        bool isAttack;
        bool isSkill;

        Animator animator;
        GameObject targetObj;

        MonsterState curState;

        string[] animName = new string[] { "Idle", "Move", "Attack", "Rush" };

        private void Awake()
        {
            //animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // test
            Initialized(null);
        }

        private void Initialized(GameObject obj)
        {
            //animator.SetBool(animName[0], true);

            damage = 10f;
            rushDis = 6f;
            atkRange = 10f;
            skillRange = 20f;

            isAttack = false;
            isSkill = false;

            curState = MonsterState.IDLE;

            targetObj = obj;
        }

        void ChangeState(MonsterState state)
        {
            if (state == curState) return;

            switch(state)
            {
                case MonsterState.IDLE:
                    break;
                case MonsterState.MOVE:
                    break;
                case MonsterState.ATTACK:
                    break;
                case MonsterState.RUSH:
                    break;
                default:
                    Debug.Log("예외 발생");
                    break;
            }

            ChangeAnim(state);
            curState = state;
        }

        void Move()
        {
            
        }
        void Rush()
        {
            StartCoroutine(Test());
        }

        void ChangeAnim(MonsterState state)
        {
            string _animName = animName[(int)state];

            animator.SetBool(animName[(int)curState], false);
            animator.SetBool(_animName, true);
        }

        MonsterState StateHandler()
        {
            float _dis = Vector3.Distance(transform.position, targetObj.transform.position);

            if (_dis < skillRange && skillCool <= 0f && !isAttack)
            {
                return MonsterState.RUSH;
            }

            if (_dis < atkRange && !isSkill)
            {
                return MonsterState.ATTACK;
            }
            
            return MonsterState.MOVE;
        }

        private void Update()
        {
            //var _state = StateHandler();

            //ChangeState(_state);

            // Test Code
            if(Input.GetKeyDown(KeyCode.Q))
            {
                transform.position = new Vector3(0.5f, 0.5f, 0.5f);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                ori = transform.position;
                var _hitBox = Instantiate(Manager.GameManager.Instance.ResourceLoadObj("HitBox")).GetComponent<HitBox>();
                _hitBox.Initialized(transform, Color.red, 1f, rushDis + 1f, 1f, () => Rush());
            }
        }
        
        // test
        private Vector3 ori;
        IEnumerator Test()
        {
            var rig = GetComponent<Rigidbody>();
            rig.velocity = Vector3.forward * 10f;
            while (Vector3.Distance(ori, transform.position) < rushDis)
            {
                yield return null;
            }
            rig.velocity = Vector3.zero;
            yield break;
        }
    }
}