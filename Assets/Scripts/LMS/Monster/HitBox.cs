using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace LMS.Monster
{
    public class HitBox : MonoBehaviour
    {
        private RawImage hitBox;
        private float colorA = 0.7f;
        private void Awake()
        {
            hitBox = transform.GetChild(0).GetComponent<RawImage>();
        }

        public void Initialized(Transform trs, Color32 color, float w, float h, float executeTime, Action skill)
        {
            var _dir = trs.forward + new Vector3(90f, 0, 0);

            hitBox.color = new Color(color.r, color.g, color.b, colorA);
            hitBox.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
            hitBox.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);

            transform.position = trs.position; // 오브젝트의 기준점으로 부터 바닥까지의 y 크기를 빼줘야함
            transform.rotation = Quaternion.Euler(_dir.x, _dir.y, _dir.z);

            Manager.GameManager.Instance.ExecuteCoroutine(DrawBox(executeTime, () => skill()));
        }

        private IEnumerator DrawBox(float executeTime, Action skill)
        {
            float _elapsed = 0f;

            while(_elapsed < executeTime)
            {
                _elapsed += Time.smoothDeltaTime;
                transform.localScale = new Vector3(1f, _elapsed / executeTime, 1f);
                yield return null;
            }
            skill();
            Destroy(gameObject); // 수정
            yield break;
        }
    }

}
