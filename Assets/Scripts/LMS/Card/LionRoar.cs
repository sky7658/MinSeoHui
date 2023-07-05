using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class LionRoar : MonoBehaviour
    {
        private float radius;
        private void Awake()
        {
            radius = GetComponent<SphereCollider>().radius;
        }
        public void Initialized(Vector3 pos)
        {
            transform.position = pos;
        }
        private void OnTriggerEnter(Collider other)
        {
            //if(other.tag == "Enemy")
            //{
                var _dir = other.transform.position - transform.position; // 튕겨낼 방향 설정
                var _dis = Vector3.Distance(transform.position + _dir.normalized * radius, other.transform.position); // 목표 위치까지 얼마나 더 가야하는지 계산
                Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.BounceOut(other.gameObject, _dir.normalized, _dis));
            //}

        }
    }
}


