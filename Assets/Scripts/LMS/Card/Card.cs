using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.UI;

namespace LMS.Cards
{
    public class Card : MonoBehaviour
    {
        public CardInfo cardInfo;
        public RawImage cardImg { get; set; }
        public Image cardMask { get; set; }

        // 카드 스킬
        public bool delayEnabled { get; set; }
        public bool isHighlight { get; set; }
        public delegate IEnumerator ActionDelegate(GameObject obj, Vector3 direction, CardInfo info);
        private ActionDelegate skill { get; set; }



        private void Awake()
        {
            cardImg = GetComponent<RawImage>();
            cardMask = transform.GetChild(0).GetComponent<Image>();
        }

        public void Initialized(string imgName, CardProperty property = CardProperty.NONE)
        {
            delayEnabled = false;
            isHighlight = false;

            cardImg.transform.localScale = new Vector3(1f, 1f, 1f);
            cardMask.transform.localScale = new Vector3(1f, 1f, 1f);

            cardMask.gameObject.SetActive(false);

            SetCardImg(imgName);
            cardInfo = new CardInfo(1f, 2, Grade.EPIC, CardBase.skillTypes[imgName], property);
            SetCardSkill();
        }

        private void SetCardSkill()
        {
            switch (cardInfo.type)
            {
                case SkillType.LIONROAR:
                    skill = CardSkill.LinoRoar;
                    break;
                case SkillType.METEORS:
                    skill = CardSkill.Meteors;
                    break;
                case SkillType.SLASHES:
                    break;
                case SkillType.HEAL:
                    skill = CardSkill.HpHeal;
                    break;
            }
        }

        private void SetCardImg(string name)
        {
            cardImg.texture = Manager.GameManager.Instance.ResourceLoadImg(name);
            if(cardImg.texture == null)
            {
                Debug.Log("이미지가 없습니다.");
                return;
            }
        }

        public void HighlightTrigger()
        {
            Manager.GameManager.Instance.ExecuteCoroutine(CardAction.SelectAction(this, isHighlight));
            if (isHighlight) isHighlight = false;
            else isHighlight = true;
        }

        /// <summary>
        /// 카드의 스킬을 실행해주는 함수
        /// </summary>
        /// <param name="obj"> 스킬을 쓰는 대상</param>
        public void ExecuteSkill(GameObject obj, Vector3 direction)
        {
            delayEnabled = true;
            if(cardInfo.count - 1 != 0) // 카드를 사용했을 때 갯수가 남아있다면 실행
            {
                cardMask.gameObject.SetActive(true);
                Manager.GameManager.Instance.ExecuteCoroutine(CardAction.DelayAction(this));
            }
            Manager.GameManager.Instance.ExecuteCoroutine(skill(obj, direction, cardInfo));
        }

        /// <summary>
        /// 카드의 위치를 이동해주는 함수
        /// </summary>
        /// <param name="targetPos"> </param>
        /// <param name="targetRot"> </param>
        /// <param name="duration"> 실행 시간</param>
        public void MoveTo(Vector3 targetPos, Quaternion targetRot, float duration)
        {
            Manager.GameManager.Instance.ExecuteCoroutine(CardAction.MoveToAction(gameObject, targetPos, targetRot, duration));
        }
    }
}