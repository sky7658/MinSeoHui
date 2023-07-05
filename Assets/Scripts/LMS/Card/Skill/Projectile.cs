using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public abstract class Projectile : SkillEffect
    {
        protected float damage;
        
        protected Rigidbody rigidBody;
        public override void Initialized(Vector3 arrow, Vector3 pos, float speed, CardInfo info, string prefName)
        {
            base.Initialized(arrow, pos, speed, info, prefName);
            transform.position = pos; // ������
            rigidBody.velocity = arrow * speed; // ���ư��� ����
            transform.rotation = Quaternion.LookRotation(arrow); // �ٶ󺸴� ����
        }
        protected override void Awake()
        {
            base.Awake();
            rigidBody = GetComponent<Rigidbody>();
        }

        protected abstract void OnTriggerEnter(Collider other);
        protected virtual void Release()
        {
            rigidBody.velocity = Vector3.zero;
            Manager.GameManager.Instance.ExecuteCoroutine(DisableObject());
        }
    }
}