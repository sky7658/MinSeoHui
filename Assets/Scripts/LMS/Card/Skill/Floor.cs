using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class Floor : SkillEffect
    {
        private Vector3 initialScale;

        public override void Initialized(Vector3 arrow, Vector3 pos, float executeTime, CardInfo info, string prefName)
        {
            base.Initialized(arrow, pos, executeTime, info, prefName);
            transform.position = pos;
            initialScale = transform.localScale;

            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.ScaleMotion(transform, initialScale, 1f));
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(2f, () => Release()));
        }

        void Release()
        {
            foreach(var particle in particleSystems) { Utility.ParticleUtil.SetParticleLoop(particle, false); }
            Manager.GameManager.Instance.ExecuteCoroutine(DisableObject());
        }
    }

}
