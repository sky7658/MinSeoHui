using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LMS.Cards
{
    public class SkillAction
    {
        /// <summary>
        /// Ư�� �ð� ���� ������Ʈ Scale�� �����մϴ�.
        /// </summary>
        /// <param name="targetT"></param>
        /// <param name="scale"></param>
        /// <param name="executeTime"></param>
        /// <returns></returns>
        public static IEnumerator ScaleMotion(Transform targetT, Vector3 scale, float executeTime)
        {
            float _elapsed = 0f;
            while (_elapsed < executeTime)
            {
                _elapsed += Time.smoothDeltaTime;
                targetT.transform.localScale = new Vector3(_elapsed / executeTime * scale.x, _elapsed / executeTime * scale.y, _elapsed / executeTime * scale.z);
                yield return null;
            }

            targetT.transform.localScale = scale;

            yield break;
        }
        /// <summary>
        /// Ư�� �ð��� ���� ���� ���� �Լ��� �����մϴ�.
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="obj"></param>
        /// <param name="delegateFun"></param>
        /// <returns></returns>
        public static IEnumerator RetentionTime(float endTime, Action delegateFun = null)
        {
            float _elapsed = 0f;

            while (_elapsed < endTime)
            {
                _elapsed += Time.smoothDeltaTime;
                yield return null;
            }
            if (delegateFun != null) delegateFun();
            yield break;
        }
        public static IEnumerator BounceOut(GameObject obj, Vector3 direction, float distance)
        {
            var _oriPos = obj.transform.position;
            var _body = obj.GetComponent<Rigidbody>();
            if (_body == null)
            {
                Debug.Log("RigidBody�� �����ϴ�.");
                yield break;
            }
            while (Vector3.Distance(_oriPos, obj.transform.position) < distance)
            {
                _body.velocity = direction * 50f;
                yield return null;
            }
            _body.velocity = Vector3.zero; // ����
            yield break;
        }

        public static IEnumerator Teleport(GameObject obj, Vector3 pos, float executeTime, Action startEffect = null, Action EndEffect = null)
        {
            float _elapsed = 0f;
            obj.transform.localScale = Vector3.zero;

            if(startEffect != null) startEffect(); // Effect�� ���

            while(_elapsed < executeTime) 
            { 
                _elapsed += Time.smoothDeltaTime;
                yield return null;
            }
            obj.transform.localScale = Vector3.one;
            obj.transform.position = pos;

            if(EndEffect != null) EndEffect(); // Effect�� ���

            yield break;
        }
    }
}
