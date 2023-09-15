using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.Manager;

namespace LMS.UI
{
    public class ExpBarUI : MonoBehaviour
    {
        private Image frontExpBar;

        private float currentExp;
        private float maxExp;

        public float frontBarSpeed;

        private Coroutine frontCoroutine;
        private Coroutine expUICoroutine;

        private void Awake() // 처음 생성시에만
        {
            frontExpBar = transform.GetChild(0).GetComponent<Image>();
            Initialized();
        }

        public void Initialized()
        {
            currentExp = 0f;
            maxExp = 100f;

            frontBarSpeed = 12f;

            if (frontCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(frontCoroutine);
                frontCoroutine = null;
            }
            if (expUICoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(expUICoroutine);
                expUICoroutine = null;
            }
        }

        public void UpdateExpBar(float value)
        {
            if (frontCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(frontCoroutine);
                frontCoroutine = null;
            }

            frontCoroutine = GameManager.Instance.ExecuteCoroutine(ExpUpdate(frontExpBar, frontBarSpeed, value));
        }

        private IEnumerator ExpUpdate(Image expBar, float speed, float value)
        {
            if (value < expBar.fillAmount)
            {
                while (expBar.fillAmount - value > 0.0001f)
                {
                    expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, value, speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (value - expBar.fillAmount < 0.0001f)
                {
                    expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, value, speed * Time.deltaTime);
                    yield return null;
                }
            }

            expBar.fillAmount = value;
            yield break;
        }
    }
}