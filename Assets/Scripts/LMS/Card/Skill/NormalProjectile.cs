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
        public override void Initialized(Vector3 arrow, Vector3 pos, float speed, string prefName, CardInfo info = null)
        {
            base.Initialized(arrow, pos, speed, prefName, info);
            coroutine = Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(1f, () => Release(this)));
        }
        protected override void OnTriggerEnter(Collider other) // ¼öÁ¤
        {
            if (other.tag == "Finish")
            {
                if (coroutine != null)
                {
                    Manager.GameManager.Instance.StopCoroutine(coroutine);
                    coroutine = null;
                }
                Release(this);
            }
        }
    }
}