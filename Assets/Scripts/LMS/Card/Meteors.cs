using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class Meteors : MonoBehaviour
    {
        public void Initialized(GameObject obj, Vector3 pos)
        {
            transform.position = pos - new Vector3(0f, 0.5f, 0f);

            var _targetPos = pos + new Vector3(0f, 3f, 0f);

            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.Teleport(obj, _targetPos, 0.2f, null, () => TeleportEffect(_targetPos)));
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(2f, () => 
            Manager.GameManager.Instance.ExecuteCoroutine(SkillAction.Teleport(obj, pos, 0.2f, null, () => TeleportEffect(pos)))));
        }

        private void TeleportEffect(Vector3 pos)
        {
            var _newEffect = Utility.ObjectPool.Instance.GetObject<ParticleSystem>("Teleport");
            _newEffect.transform.position = pos;
        }
    }
}

