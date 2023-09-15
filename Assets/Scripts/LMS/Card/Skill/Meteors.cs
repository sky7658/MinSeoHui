using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class Meteors : MonoBehaviour
    {
        public float colliderSize = 9f;
        private float damage;

        private float duration;
        private int takeCount;

        public void Initialized(GameObject obj, Vector3 pos, float damage)
        {
            this.damage = damage;
            duration = 3f;
            takeCount = 5;

            transform.position = pos - CardBase.characterSkillHeight;

            var _targetPos = pos + new Vector3(0f, 3f, 0f);

            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.Teleport(obj, _targetPos, 0.2f, false, null, () => TeleportEffect(_targetPos)));
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(3f, () =>
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.Teleport(obj, pos, 0.2f, true, null, () => TeleportEffect(pos)))));
            Manager.GameManager.Instance.ExecuteCoroutine(DealDamage());
        }

        private void TeleportEffect(Vector3 pos)
        {
            var _newEffect = Utility.ObjectPool.Instance.GetObject<ParticleSystem>("Teleport");
            Utility.UtilFunction.TurnOnOff(Utility.ObjectPool.Instance.objectInfos[3], _newEffect.gameObject, true);
            _newEffect.transform.position = pos;
        }

        private IEnumerator DealDamage()
        {
            int _count = 0;
            yield return new WaitForSeconds(0.5f);

            while (_count < takeCount)
            {
                var hits = Physics.SphereCastAll(transform.position, colliderSize, Vector3.up, 0f, LayerMask.GetMask("Water"));

                Debug.Log(hits.Length);

                foreach(var hit in hits)
                {
                    hit.transform.GetComponent<IDamageable>().TakeDamage((int)damage, Vector3.zero);
                }
                _count++;
                yield return new WaitForSeconds(duration / takeCount);
            }

            yield break;
        }
    }
}

