using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class NormalProjectile : Projectile
    {
        private Coroutine coroutine;
        protected override void Awake()
        {
            base.Awake();
            col = GetComponent<SphereCollider>();
        }
        public override void Initialized(Vector3 arrow, Vector3 pos, float speed, string prefName, float damage, CardInfo info = null)
        {
            base.Initialized(arrow, pos, speed, prefName, damage, info);
            coroutine = Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(1f, () => Release(this)));
        }
        protected override void OnTriggerEnter(Collider other) // ¼öÁ¤
        {
            if (other.tag == "Monster")
            {
                if (coroutine != null)
                {
                    Manager.GameManager.Instance.StopCoroutine(coroutine);
                    coroutine = null;
                }
                Release(this);
                other.GetComponent<IDamageable>().TakeDamage((int)damage, Vector3.zero);
            }
        }
    }
}