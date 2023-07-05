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
                var _dir = other.transform.position - transform.position; // ƨ�ܳ� ���� ����
                var _dis = Vector3.Distance(transform.position + _dir.normalized * radius, other.transform.position); // ��ǥ ��ġ���� �󸶳� �� �����ϴ��� ���
                Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.BounceOut(other.gameObject, _dir.normalized, _dis));
            //}

        }
    }
}


