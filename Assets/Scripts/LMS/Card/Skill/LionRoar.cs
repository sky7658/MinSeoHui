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

            this.damage = damage; // �ӽ� ����
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Monster")
            {
                var _dir = other.transform.position - transform.position; // ƨ�ܳ� ���� ����
                var _dis = Vector3.Distance(transform.position + _dir.normalized * radius, other.transform.position); // ��ǥ ��ġ���� �󸶳� �� �����ϴ��� ���
                var _mon = other.GetComponent<Monster>();

                _mon.TakeDamage((int)damage, Vector3.up); // ������ �ֱ�

                Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.BounceOut(_mon, _dir.normalized, _dis));
            }

        }
    }
}


