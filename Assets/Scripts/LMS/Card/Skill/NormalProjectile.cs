using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class NormalProjectile : Projectile
    {
        public override void Initialized(Vector3 arrow, Vector3 pos, float speed, CardInfo info, string prefName)
        {
            base.Initialized(arrow, pos, speed, info, prefName);
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(1f, () => Release()));
        }
        protected override void OnTriggerEnter(Collider other) // ¼öÁ¤
        {
            if (other.tag == null)
            {
                Release();
            }
        }
    }
}