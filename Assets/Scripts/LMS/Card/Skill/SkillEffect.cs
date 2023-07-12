using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;

namespace LMS.Cards
{
    public class SkillEffect : MonoBehaviour
    {
        protected CardInfo info;

        protected List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        protected virtual void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                particleSystems.Add(transform.GetChild(i).GetComponent<ParticleSystem>());
                ParticleUtil.SetParticleLoop(particleSystems[i], true);
            }
        }

        public virtual void Initialized(Vector3 arrow, Vector3 pos, float speed, CardInfo info, string prefName)
        {
            this.info = info;
            ParticleUtil.InitParticleColor(particleSystems, prefName, this.info.property);
        }
        protected IEnumerator DisableObject()
        {
            int _particleCounts = 77; // 임의의 숫자 초기화
            while (_particleCounts > 0)
            {
                _particleCounts = 0;
                foreach (var p in particleSystems) { _particleCounts += p.particleCount; }
                yield return null;
            }

            // 게임 오브젝트 비활성화 (수정)
            Destroy(gameObject);
            yield break;
        }
    }

}
