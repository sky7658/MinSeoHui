 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.Manager;

namespace LMS.UI
{
    public class HpBarUI : MonoBehaviour
    {
        private Image frontHpBar;
        private Image backHpBar;

        private float currentHp;
        private float maxHp;

        public float frontBarSpeed;
        public float backBarSpeed;

        private Coroutine frontCoroutine;
        private Coroutine backCoroutine;
        private Coroutine hpUICoroutine;

        public bool isWorld;
        private void Awake() // ó�� �����ÿ���
        {
            frontHpBar = transform.GetChild(1).GetComponent<Image>();
            backHpBar = transform.GetChild(0).GetComponent<Image>();
            Initialized();
        }

        public void Initialized(/*float hp, bool isWorld*/)
        {
            currentHp = 100f;
            maxHp = 100f;

            //currentHp = hp;
            //this.maxHp = hp;
            //this.isWorld = isWorld

            if (isWorld) gameObject.SetActive(false);

            frontBarSpeed = 12f;
            backBarSpeed = 5f;

            if (frontCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(frontCoroutine);
                frontCoroutine = null;
            }
            if (backCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(backCoroutine);
                backCoroutine = null;
            }
            if (hpUICoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(hpUICoroutine);
                hpUICoroutine = null;
            }
        }

        private void Update() // �ӽ� ������Ʈ ��� ���߿� ����
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                var randX = Random.Range(10f, 50f);
                UpdateHpBar(10f);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                var randX = Random.Range(-50f, -10f);
                UpdateHpBar(10f, false);
            }
        }

        /// <summary>
        /// HpBar�� ������Ʈ ���ݴϴ�.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="getDamage"></param>
        public void UpdateHpBar(float value, bool getDamage = true)
        {
            if (frontCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(frontCoroutine);
                frontCoroutine = null;
            }
            if (backCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(backCoroutine);
                backCoroutine = null;
            }

            if (getDamage) currentHp -= value;
            else currentHp += value;

            frontCoroutine = GameManager.Instance.ExecuteCoroutine(HpUpdate(frontHpBar, frontBarSpeed));
            backCoroutine = GameManager.Instance.ExecuteCoroutine(HpUpdate(backHpBar, backBarSpeed));
        }

        /// <summary>
        /// HpBar�� ���� �ð����ȸ� ����ǰ� ���ݴϴ�.
        /// </summary>
        public void UIActive()
        {
            if(hpUICoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(hpUICoroutine);
                hpUICoroutine = null;
            }

            gameObject.SetActive(true);
            hpUICoroutine = GameManager.Instance.ExecuteCoroutine(HpUIActive());
        }
        private IEnumerator HpUIActive()
        {
            // �׻� ī�޶� �������� �ٶ󺸱�
            float _elapsed = 0f;
            while (_elapsed < 2f)
            {
                var _cam = Camera.main.transform;
                transform.LookAt(transform.position + _cam.rotation * Vector3.forward, _cam.rotation * Vector3.up);

                _elapsed += Time.smoothDeltaTime;

                yield return null;
            }

            gameObject.SetActive(false);
            yield break;
        }

        private IEnumerator HpUpdate(Image hpBar, float speed)
        {
            float _value = currentHp / maxHp;
            if (_value < hpBar.fillAmount)
            {
                while (hpBar.fillAmount - _value > 0.0001f)
                {
                    hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, _value, speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (_value - hpBar.fillAmount > 0.0001f)
                {
                    hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, _value, speed * Time.deltaTime);
                    yield return null;
                }
            }

            hpBar.fillAmount = _value;
            yield break;
        }
    }
}
