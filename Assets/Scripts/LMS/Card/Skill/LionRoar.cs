using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class LionRoar : MonoBehaviour
    {
        private float radius;
        private float damage;
        private void Awake()
        {
            radius = GetComponent<SphereCollider>().radius;
        }
        public void Initialized(Vector3 pos, float damage)
        {
            transform.position = pos;

            this.damage = damage; // 임시 설정
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Monster")
            {
                var _dir = other.transform.position - transform.position; // 튕겨낼 방향 설정
                var _dis = Vector3.Distance(transform.position + _dir.normalized * radius, other.transform.position); // 목표 위치까지 얼마나 더 가야하는지 계산
                var _mon = other.GetComponent<Monster>();

                _mon.TakeDamage((int)damage, Vector3.zero); // 데미지 주기

                Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.BounceOut(_mon, _dir.normalized, _dis));
            }

        }
    }
}


